using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyData : UnitData
{
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChasingState ChasingState { get; set; }
    public EnemyAttackingState AttackingState { get; set; }

    [SerializeField]
    private float stateDelay = 1.5f;
    float delay = 0;

    public enum EnemyState
    {
        IDLE,
        CHASING,
        ATTACKING
    }

    public EnemyState enemyState;
    public BattleUnit[] enemyUnit;

    [Header("Unit Info")]
    public UnitCreatorSO unitInfo;
    public EnemyMovement movement;
    public PlayerMovement player;
    public float awareness;
    public float awarenessRate = 0.5f;

    public int availableExp = 50;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();

        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        ChasingState = new EnemyChasingState(this, StateMachine);
        AttackingState = new EnemyAttackingState(this, StateMachine);

    }

    void Start()
    {
        StateMachine.Initialize(IdleState);
        //enemyState = EnemyState.IDLE;
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //spriteRenderer.sprite = unitInfo.data.spriteImage;
    }

    private void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.OVERWORLD)
            StateMachine.CurrentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.state == GameManager.GameState.OVERWORLD)
            StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        StateMachine.CurrentState.OnCollision(collision);
    }

    public void EndBattle()
    {
        Destroy(emote.gameObject);
        Destroy(gameObject);
    }

    public void ChangeState(EnemyState newState)
    {
        delay += Time.deltaTime;

        if (delay >= stateDelay)
        {
            delay = 0;
            enemyState = newState;
        }
    }

    public override void TakeDamage(int damage)
    {

    }

    public override void GainFlatExperience(int exp)
    {
        throw new NotImplementedException();
    }

    public override void GainExperience(int exp)
    {
        throw new NotImplementedException();
    }

    public override void LevelUp()
    {
        throw new NotImplementedException();
    }
}
