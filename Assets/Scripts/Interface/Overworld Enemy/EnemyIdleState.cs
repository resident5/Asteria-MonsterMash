using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    EnemyData eData;
    EnemyMovement eMovement;

    public EnemyIdleState(EnemyData e, EnemyStateMachine eState) : base(e, eState)
    {
        eData = e;
        eMovement = e.movement;

    }

    public override void Enter()
    {
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        //if(eData.player != null && GameManager.Instance.state == GameManager.GameState.OVERWORLD)
        //{
        //    //Charge up awareness then when full (100%) have them chase
        //    ChangeState(eData.ChasingState);
        //}

        if(eData.awareness >= 100 && GameManager.Instance.state == GameManager.GameState.OVERWORLD)
        {
            ChangeState(eData.ChasingState);
        }
    }
    public override void PhysicsUpdate()
    {
    }
    public override void Exit()
    {
    }
    public override void ChangeState(EnemyState newEnemyState)
    {
        eData.StateMachine.ChangeState(newEnemyState);
    }

    public override void OnCollision(Collision other)
    {

    }
    public override void OnTrigger(Collision other)
    {
    }


}
