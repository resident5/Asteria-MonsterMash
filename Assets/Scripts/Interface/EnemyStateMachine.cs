using System.Collections;
using UnityEngine;


public class EnemyStateMachine
{
    public EnemyState CurrentState { get; set; }
    public EnemyState PreviousState { get; set; }

    public void Initialize(EnemyState StartingState)
    {
        CurrentState = StartingState;
        CurrentState.Enter();
    }

    public void ChangeState(EnemyState newEnemyState)
    {
        if(newEnemyState != CurrentState)
        {
            PreviousState = CurrentState;
        }

        CurrentState.Exit();
        CurrentState = newEnemyState;
        CurrentState.Enter();
    }
}
