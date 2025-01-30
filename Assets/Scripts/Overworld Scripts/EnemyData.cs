using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyData : MonoBehaviour
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

    public Emote emote;

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
        DetectPlayer();
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

    public void SetupEmote(Canvas canvas)
    {
        emote.transform.SetParent(canvas.transform);
    }

    public void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, movement.detectionRadius);

        foreach (Collider item in colliders)
        {
            //if(item.TryGetComponent<PlayerMovement>(out PlayerMovement playerMove))
            //{
            //    playerMove = player;
            //}
            if (item.gameObject.tag == "Player")
            {
                player = item.GetComponent<PlayerMovement>();
                emote.Activate();
            }
        }

        CheckAwareness();

        if (awareness <= 0)
        {
            emote.DeActivate();
        }
    }

    public void CheckAwareness()
    {
        if (player != null)
        {
            awareness += 1 * awarenessRate * Time.deltaTime;
        }
        else
        {
            awareness -= 1 * awarenessRate * Time.deltaTime;
        }

        awareness = Mathf.Clamp(awareness, 0, 100);
        emote.warningFill.fillAmount = awareness / 100f;

    }

    public void EndBattle()
    {
        Destroy(emote.gameObject);
        Destroy(gameObject);
    }
    //IEnumerator AnimateAwareness()
    //{

    //    yield return null;

    //}

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
