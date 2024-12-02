using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;

    public Rigidbody rb;
    public Animator animator;

    public float moveSpeedX, moveSpeedZ;

    public int steps;

    public bool facingRight = true;

    public Transform graphix;
    public Vector3 startPosition;
    public Vector3 previousPosition;

    public bool canMove = true;

    public LayerMask walkable;
    GameManager GameManager => GameManager.Instance;
    public InputManager InputManager => InputManager.Instance;


    [Header("Debug Values")]
    public float radius;
    public Vector3 debugTargetPosition;
    public float debugOffSet;

    [SerializeField]
    private Collider[] colliders;

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        previousPosition = startPosition;
    }

    void Update()
    {
        if (!GameManager.isPaused && GameManager.state == GameManager.GameState.OVERWORLD)
            PlayerInput();
    }

    void PlayerInput()
    {
        Vector3 moveDir = new Vector3(InputManager.MoveInput.x, 0, InputManager.MoveInput.y);
        //float x = Input.GetAxisRaw("Horizontal");
        //float z = Input.GetAxisRaw("Vertical");

        Vector3 targetDir = Vector3.zero;
        targetDir.x = moveDir.x * moveSpeedX * Time.deltaTime;
        targetDir.z = moveDir.z * moveSpeedZ * Time.deltaTime;
        animator.SetFloat("moveX", moveDir.x);
        animator.SetFloat("moveZ", moveDir.z);

        Vector3 targetPosition = transform.position + targetDir;
        MoveToPosition(targetPosition);

        //rb.velocity = new Vector3(targetDir.x * moveSpeedX, rb.velocity.y, targetDir.z * moveSpeedZ);

        //Step();

        Flip(moveDir.x);

    }

    private void MoveToPosition(Vector3 targetPos)
    {
        Vector3 targetFloorPos = new Vector3(targetPos.x, targetPos.y + debugOffSet, targetPos.z);
        debugTargetPosition = targetFloorPos;
        colliders = Physics.OverlapSphere(targetFloorPos, radius, walkable);
        bool isValid = false;

        foreach (var item in colliders)
        {
            TWalkable walkable = item.GetComponent<TWalkable>();
            if (walkable != null)
            {
                isValid = true;
            }
        }

        if (isValid)
        {
            transform.position = targetPos;
        }
    }

    void Flip(float moveDir)
    {
        if (moveDir > 0 && !facingRight || moveDir < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 rot = Vector3.zero;

            if (!facingRight)
            {
                rot.y = 180;
            }
            transform.localEulerAngles = rot;
        }
    }

    public int Step()
    {
        if (Vector3.Distance(previousPosition, transform.position) > 1)
        {
            previousPosition = transform.position;
            steps += 1;
        }

        return steps;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(debugTargetPosition, radius);
    }
}
