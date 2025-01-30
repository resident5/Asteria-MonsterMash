using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();

        Quest quest = GetQuestByID("CollectPotionsQuest");
        Debug.Log(quest.ToString());
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
            idTOQuestMap.Add(questInfo.id, new Quest(questInfo));
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
}
