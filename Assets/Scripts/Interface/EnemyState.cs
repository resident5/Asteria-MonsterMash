using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyController enemyData;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState(EnemyController e, EnemyStateMachine eState)
    {
        enemyData = e;
        enemyStateMachine = eState;
    }

    public virtual void Enter() { }
    public virtual void FrameUpdate()
    {
        Debug.Log("Current state = " + this.GetType().Name);
    }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
    public virtual void ChangeState(EnemyState newState) { }
    public virtual void OnCollision(Collision other) { }
    public virtual void OnTrigger(Collision other) { }
}
