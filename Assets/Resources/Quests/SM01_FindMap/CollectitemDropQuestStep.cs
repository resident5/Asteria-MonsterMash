using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectitemDropQuestStep : QuestStep
{
    //Quest step for collecting items that drop from enemies

    //Having this quest should spawn a monster that drops the item in the next scene

    private int numOfItemsCollected = 0;

    private int numOfItemsRequired = 1;

    [SerializeField]
    private Item itemReward;

    [SerializeField]
    private string sceneName;

    private bool isQuestArea = false;


    private void OnEnable()
    {
        EventManager.Instance.battleEvents.onEnemyDefeated += ReceiveItem;
        EventManager.Instance.overworldEvents.onEnterNewScene += CheckIfQuestArea;
    }

    private void OnDisable()
    {
        EventManager.Instance.battleEvents.onEnemyDefeated -= ReceiveItem;
        EventManager.Instance.overworldEvents.onEnterNewScene -= CheckIfQuestArea;
    }

    private void CheckIfQuestArea()
    {
        Debug.Log("Check scene");
        Debug.Log($"Active scene name = {SceneManager.GetActiveScene().name} and sceneName = {sceneName}");
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            isQuestArea = true;
        }
        else
        {
            isQuestArea = false;
        }
    }

    private void ReceiveItem(EnemyData defeatedEnemy)
    {
        //Debug after defeating the enemy, inject the item into the player's inventory

        //Maybe RNG this
        //Maybe trigger dialogue right after
        if (isQuestArea)
        {
            if (numOfItemsCollected < numOfItemsRequired)
            {
                numOfItemsCollected++;
                InventoryManager.Instance.AddItem(itemReward, 1);
                UpdateState();
            }
        }

        if (numOfItemsCollected >= numOfItemsRequired)
            FinishQuestStep();
    }

    private void UpdateState()
    {
        string state = numOfItemsCollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.numOfItemsCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
