using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BattleUnit
{
    public List<BattleUnit> Allies => manager.battleInfo.ListOfEnemies;
    public List<BattleUnit> Enemies => manager.battleInfo.ListOfAllies;
    public SpriteRenderer sprite;

    public override void OnTurnStart()
    {
        base.OnTurnStart();
    }

    public override void OnTurnUpdate()
    {
        base.OnTurnUpdate();
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
    }

    private void Update()
    {
        if(manager.currentUnitTurn != null && manager.currentUnitTurn == this)
        {
            EnemyAttack();
        }
    }

    public void EnemyAttack()
    {
        //yield return new WaitForSeconds(battleRate);

        UnitActionSO chosenMove = GetRandomAction();
        BattleUnit[] targetList = GetEnemyTargets(chosenMove);

        foreach (var target in targetList)
        {
            target.ApplyAction(this, chosenMove);
        }

        OnTurnEnd();

        if (GameManager.Instance.debugger.isDebugging)
            Debug.Log("Enemy Turn has ended");

        manager.currentUnitTurn = null;

    }

    public BattleUnit[] GetEnemyTargets(UnitActionSO selectedAction)
    {
        int randTarget = 0;
        List<BattleUnit> targets = new List<BattleUnit>();

        switch (selectedAction.targetTypes)
        {
            case UnitActionSO.TargetTypes.RANDOMALLY:
                randTarget = Random.Range(0, Allies.Count);
                targets.Add(Allies[randTarget]);
                break;
            case UnitActionSO.TargetTypes.RANDOMOPPONENT:
                randTarget = Random.Range(0, Enemies.Count);
                targets.Add(Enemies[randTarget]);
                break;
            case UnitActionSO.TargetTypes.ALLALLIES:
                targets.AddRange(Allies);
                //Allow enemy to select multiple allies
                break;
            case UnitActionSO.TargetTypes.ALLENEMIES:
                targets.AddRange(Enemies);
                //Allow multiple enemies but not all
                break;
            case UnitActionSO.TargetTypes.ALL:
                targets.AddRange(Allies);
                targets.AddRange(Enemies);
                break;
        }

        return targets.ToArray();

    }

    private UnitActionSO GetRandomAction()
    {
        List<UnitActionSO> enemyActionsList = myDataStats.battleMoves;
        int rand = Random.Range(0, enemyActionsList.Count);

        return enemyActionsList[rand];
    }
}
