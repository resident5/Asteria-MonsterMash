using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BattleUnit
{
    public List<BattleUnit> Allies => manager.battleInfo.ListOfAllies;
    public List<BattleUnit> Enemies => manager.battleInfo.ListOfEnemies;

    public List<UnitCreatorSO> Summons;

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

    public void Init(PlayerData pData)
    {
        UnitCreatorSO unitInstance = Instantiate(pData.playerCreator);
        data = unitInstance.data;
        statusEffects = new List<StatusEffectSO>();
        healthbar = GetComponentInChildren<HealthBar>();

        healthbar.unit = this;
        baseActionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / data.stats.Speed, 0, MAX_ACTION_VALUE));
        currentActionValue = baseActionValue;
    }

}
