using UnityEngine;
using System.Collections;

//Just following tutorial this stuff is going to be moved to NPCs and Interactible objects instead
//It will also be given during quests

[RequireComponent(typeof(CapsuleCollider))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    private bool playerisNear = false;
    private string questId;

    private QuestState currentQuestState;
    private QuestIcon questIcon;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        EventManager.Instance.questEvents.onQuestStateChanged += QuestStateChange;
        //EventManager.Instance.questEvents.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        EventManager.Instance.questEvents.onQuestStateChanged -= QuestStateChange;
        //EventManager.Instance.questEvents.onSubmitPressed -= SubmitPressed;

    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
            //Debug.Log($"Quest with id {questId} updated to state {currentQuestState} ");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            SubmitPressed();
        }
    }

    private void SubmitPressed()
    {
        if (!playerisNear)
            return;

        EventManager.Instance.questEvents.StartQuest(questId);
        //EventManager.Instance.questEvents.AdvanceQuest(questId);
        //EventManager.Instance.questEvents.FinishQuest(questId);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerisNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerisNear = false;
        }
    }
}
