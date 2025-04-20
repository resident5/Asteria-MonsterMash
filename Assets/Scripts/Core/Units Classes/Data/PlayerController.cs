using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    private GameManager GameManager => GameManager.Instance;
    private InputManager InputManager => InputManager.Instance;

    public PlayerData playerData;

    [Header("Player Movement")]
    public Rigidbody rb;
    public Animator animator;

    public float moveSpeedX, moveSpeedZ;

    public int steps;
    public bool facingRight = true;

    public bool canMove = true;

    public Vector3 startPosition;
    public Vector3 previousPosition;

    public Transform graphix;

    public LayerMask walkable;

    [Header("Interactable Values")]
    public Interactable focus;
    public float detectionRadius;

    [Header("Debug Values")]
    public float radius;
    public Vector3 debugTargetPosition;
    public float debugOffSet;

    [SerializeField]
    private Collider[] colliders;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        playerData.InitializeStats();
        startPosition = transform.position;
        previousPosition = startPosition;
        playerData.SetupEmote(GameManager.Instance.worldCanvas);

    }

    private void Update()
    {
        if (!GameManager.isPaused && GameManager.state == GameManager.GameState.OVERWORLD && canMove)
            PlayerMove();

        //CheckInteractables();

    }

    private void OnEnable()
    {
        EventManager.Instance.playerEvents.onPlayerExperienceGained += playerData.GainExperience;
    }

    private void OnDisable()
    {
        EventManager.Instance.playerEvents.onPlayerExperienceGained -= playerData.GainExperience;
    }

    private void PlayerMove()
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

    private void Flip(float moveDir)
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

    //public int Step()
    //{
    //    if (Vector3.Distance(previousPosition, transform.position) > 1)
    //    {
    //        previousPosition = transform.position;
    //        steps += 1;
    //    }

    //    return steps;
    //}

    //public void CheckInteractables()
    //{
    //    bool foundtarget = false;

    //    Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
    //    foreach (Collider collider in colliders)
    //    {
    //        Interactable interactable = collider.GetComponent<Interactable>();
    //        if (interactable != null)
    //        {
    //            SetFocus(interactable);
    //            foundtarget = true;
    //            break;
    //        }
    //    }


    //    if (!foundtarget)
    //    {
    //        focus = null;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Interactable interactable = other.GetComponent<Interactable>();
    //    if (interactable != null)
    //    {
    //        SetFocus(interactable);
    //    }
    //}

    public void SetFocus(Interactable newFocus)
    {
        if (focus != newFocus)
        {
            if (focus != null)
            {
                focus.DeFocused();
            }
            focus = newFocus;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(debugTargetPosition, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

    }
}
