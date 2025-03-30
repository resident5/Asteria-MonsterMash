using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BattleUnit
{
    public List<BattleUnit> Allies => manager.battleInfo.ListOfAllies;
    public List<BattleUnit> Enemies => manager.battleInfo.ListOfEnemies;

    public List<MonsterData> Summons;

    public override void OnTurnStart()
    {
        base.OnTurnStart();
        BattleManager.Instance.battleHUD.SetHUD(this);
        BattleManager.Instance.battleHUD.EnableHUD();
    }

    public override void OnTurnUpdate()
    {
        base.OnTurnUpdate();
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        BattleManager.Instance.battleHUD.ClearHUD();
        BattleManager.Instance.battleHUD.DisableHUD();
    }

    public void Init(PlayerStats pStats)
    {
        //UnitCreatorSO unitInstance = Instantiate(pData.playerCreator);
        //data = unitInstance.data;
        myDataStats = pStats;
        statusEffects = new List<StatusEffectSO>();
        healthbar = GetComponentInChildren<HealthBar>();
        animator.runtimeAnimatorController = myDataStats.animatorController;

        healthbar.unit = this;
        baseActionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / myDataStats.battleStats.Speed, 0, MAX_ACTION_VALUE));
        currentActionValue = baseActionValue;
    }

}
