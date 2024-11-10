using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public float stateDelay = 2.5f;
    float delay = 0;

    public enum EnemyState
    {
        IDLE,
        CHASING,
        ATTACKING
    }

    public EnemyState enemyState;
    public BattleUnit[] enemyUnit;

    void Start()
    {
        enemyState = EnemyState.IDLE;
    }

    public void ChangeState(EnemyState newState)
    {
        //Turn into a interface
        //Make a delay for changing states
        //When changing to chasing/aggro state make a exclamation mark animation above the spirits
        delay += Time.deltaTime;

        if (delay >= stateDelay)
        {
            delay = 0;
            enemyState = newState;
        }
    }
}
