using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSelector : MonoBehaviour
{
    private BattleManager battleManager;
    public UnitAction selectedBattleOption;

    private void Start()
    {
        battleManager = BattleManager.Instance;
    }

    private void Update()
    {
        ExecuteOption();
    }

    public void SelectOption()
    {
        //battleManager.currentBattleOption = selectedBattleOption;
        battleManager.eventSystem.SetSelectedGameObject(battleManager.unitList.enemyList[0].gameObject);
    }

    void ExecuteOption()
    {
        switch (selectedBattleOption.targetTypes)
        {
            case UnitAction.TargetTypes.ALLIES:
                break;
            case UnitAction.TargetTypes.ENEMIES:
                break;
            case UnitAction.TargetTypes.ENVIRONMENT:
                break;
            case UnitAction.TargetTypes.MULTIPLEALLIES:
                break;
            case UnitAction.TargetTypes.MULTIPLEENEMIES:
                break;
            case UnitAction.TargetTypes.ALL:
                break;
            default:
                break;
        }
    }
}
