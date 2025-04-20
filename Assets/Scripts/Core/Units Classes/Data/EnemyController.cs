using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChasingState ChasingState { get; set; }
    public EnemyAttackingState AttackingState { get; set; }

    public EnemyState enemyState;

    [SerializeField]
    private float stateDelay = 1.5f;
    float delay = 0;

    public Rigidbody rb;
    public Animator animator;

    public float moveSpeedX, moveSpeedZ;

    public EnemyData enemyData;

    public float detectionRadius;
    public bool facingRight;

    public PlayerController player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        enemyData.Init();

        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        ChasingState = new EnemyChasingState(this, StateMachine);
        AttackingState = new EnemyAttackingState(this, StateMachine);

        StateMachine.Initialize(IdleState);
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
    public enum EnemyState
    {
        IDLE,
        CHASING,
        ATTACKING
    }

    public void EndBattle()
    {
        Destroy(enemyData.emote.gameObject);
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

    private void OnCollisionEnter(Collision collision)
    {
        StateMachine.CurrentState.OnCollision(collision);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
