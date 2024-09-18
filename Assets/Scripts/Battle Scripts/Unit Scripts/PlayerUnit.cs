using System.Collections;
using UnityEngine;


public class PlayerUnit : BattleUnit
{
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        BattleManager.Instance.HUDcontroller.SetHUD(this);
    }

    public override void OnTurnUpdate()
    {
        base.OnTurnUpdate();
    }

    public override void OnTurnEnd()
    {
        BattleManager.Instance.HUDcontroller.DisableHUD();
        base.OnTurnEnd();
    }

}
