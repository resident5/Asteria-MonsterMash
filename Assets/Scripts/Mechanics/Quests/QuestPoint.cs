using UnityEngine;
using System.Collections;

//Just following tutorial this stuff is going to be moved to NPCs and Interactible objects instead
//It will also be given during quests

[RequireComponent(typeof(CapsuleCollider))]
public class QuestPoint : Interactable
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
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
            //Debug.Log($"Quest with id {questId} updated to state {currentQuestState} ");
        }
    }

    public override void Interact()
    {
        SubmitPressed();
    }

    private void SubmitPressed()
    {
        if (!playerisNear)
            return;


        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
            EventManager.Instance.questEvents.StartQuest(questId);
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
            EventManager.Instance.questEvents.FinishQuest(questId);
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
