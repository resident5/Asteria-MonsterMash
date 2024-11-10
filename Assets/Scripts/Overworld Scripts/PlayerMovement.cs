using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerData playerData;
    
    public Rigidbody rb;
    public Animator animator;

    public float moveSpeedX, moveSpeedZ;
    public float radius;

    public int steps;

    public bool facingRight = true;

    public Transform graphix;
    public Vector3 startPosition;
    public Vector3 previousPosition;

    public UnitCreatorSO playerCreator;
    public LayerMask walkable;
    GameManager GameManager => GameManager.Instance;
    public InputManager InputManager => InputManager.Instance;

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
        Vector3 moveInputValue = new Vector3(InputManager.MoveInput.x, 0, InputManager.MoveInput.y);
        //float x = Input.GetAxisRaw("Horizontal");
        //float z = Input.GetAxisRaw("Vertical");

        Vector3 targetDir = Vector3.zero;
        targetDir.x = moveInputValue.x;
        targetDir.z = moveInputValue.z;

        rb.velocity = new Vector3(targetDir.x * moveSpeedX, rb.velocity.y, targetDir.z * moveSpeedZ);
        animator.SetFloat("moveX", targetDir.x);
        animator.SetFloat("moveZ", targetDir.z);
        
        Step();

        Flip(targetDir.x);
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
}
