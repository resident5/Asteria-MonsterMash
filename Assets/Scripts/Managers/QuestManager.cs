using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool loadQuestState = true;

    private Dictionary<string, Quest> questMap;

    private int currentPlayerLevel;

    private void Awake()
    {
        questMap = CreateQuestMap();

        Quest quest = GetQuestByID("CollectPotionsQuest");
        //Debug.Log(quest.ToString());
    }

    private void OnEnable()
    {
        EventManager.Instance.questEvents.onStartQuest += StartQuest;
        EventManager.Instance.questEvents.onAdvanceQuest += AdvanceQuest;
        EventManager.Instance.questEvents.onFinishQuest += FinishQuest;

        EventManager.Instance.questEvents.onQuestStepStateChanged += QuestStepStateChange;

        EventManager.Instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
    }

    private void OnDisable()
    {
        EventManager.Instance.questEvents.onStartQuest -= StartQuest;
        EventManager.Instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        EventManager.Instance.questEvents.onFinishQuest -= FinishQuest;

        EventManager.Instance.questEvents.onQuestStepStateChanged -= QuestStepStateChange;

        EventManager.Instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
    }

    public void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            Debug.Log($"All Quests {quest.info.name}"); 
        }
        //State the initial state of all quests on the map
        foreach (Quest quest in questMap.Values)
        {
            //Initialize all loaded quest steps
            if(quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            EventManager.Instance.questEvents.QuestStateChange(quest);
        }
    }

    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private void ChangeQuestState(string id, QuestState questState)
    {
        Quest quest = GetQuestByID(id);
        quest.state = questState;
        EventManager.Instance.questEvents.QuestStateChange(quest);
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestByID(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private void PlayerLevelChange(int level)
    {
        currentPlayerLevel = level;
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        if(currentPlayerLevel < quest.info.levelRequirement)
        {
            meetsRequirements = false;
        }

        foreach (QuestInfoSO prerequisite in quest.info.questPrerequisities)
        {
            if(GetQuestByID(prerequisite.id).state != QuestState.COMPLETED)
            {
                meetsRequirements = false;
                break;
            }
        }

        return meetsRequirements;
    }

    public void StartQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }

    public void AdvanceQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    public void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);

        ClaimRewards(quest);

        ChangeQuestState(quest.info.id, QuestState.COMPLETED);
    }

    private void ClaimRewards(Quest quest)
    {
        //TODO: Add events to handle gold rewards and item rewards

        EventManager.Instance.playerEvents.PlayerExperienceGained(quest.info.experienceReward);
        EventManager.Instance.monsterEvents.MonsterExperienceGained(quest.info.experienceReward);

        //EventManager.Instance.playerEvents.PlayerGoldGained(quest.info.goldRewards);
        //EventManager.Instance.playerEvents.ItemGained(quest.info.itemRewards);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idTOQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if(idTOQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found" + questInfo.id);
            }
            idTOQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idTOQuestMap;
    }

    private Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];
        if(quest == null)
        {
            Debug.LogError("ID not found" + id);
        }

        return quest;
    }

    private void OnApplicationQuit()
    {
        foreach (Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }
    }

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();
            string serializeData = JsonUtility.ToJson(questData);
            PlayerPrefs.SetString(quest.info.id, serializeData);
        }
        catch(System.Exception e)
        {
            Debug.LogError($"Failed to save quest with id {quest.info.id}: {e}");
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;

        try
        {
            if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load quest with id {questInfo.id}: {e}");
        }
        return quest;
    }
}
