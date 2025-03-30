using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyState
{
    public EnemyAttackingState(EnemyController e, EnemyStateMachine eState) : base(e, eState)
    {
    }

    public override void Enter()
    {
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }
    public override void PhysicsUpdate()
    {
    }
    public override void Exit()
    {
    }

    public override void ChangeState(EnemyState newEnemyState)
    {
    }

    public override void OnCollision(Collision other)
    {

    }
    public override void OnTrigger(Collision other)
    {

    }
}
