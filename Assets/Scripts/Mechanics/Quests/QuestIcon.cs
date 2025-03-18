using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]

    [SerializeField] private GameObject requirementNotMetIcon;
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject requirementNotMetToFinishIcon;
    [SerializeField] private GameObject canFinishIcon;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        requirementNotMetIcon.SetActive(false);
        canStartIcon.SetActive(false);
        requirementNotMetToFinishIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        switch (newState)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                if (startPoint) { requirementNotMetIcon.SetActive(true); }
                break;
            case QuestState.CAN_START:
                if (startPoint) { canStartIcon.SetActive(true); }
                break;
            case QuestState.IN_PROGRESS:
                if (finishPoint) { requirementNotMetToFinishIcon.SetActive(true); }
                break;
            case QuestState.CAN_FINISH:
                if (finishPoint) { canFinishIcon.SetActive(true); }
                break;
            case QuestState.COMPLETED:
                break;
        }


    }

}
