using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;

    public float moveSpeedX, moveSpeedZ;
    public float radius;

    public bool facingRight = true;

    public Transform graphix;

    public UnitCreatorScriptableObject playerCreator;

    public GameManager gameManager => GameManager.Instance;

    public LayerMask walkable;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.OVERWORLD)
            PlayerInput();
    }

    void PlayerInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 targetDir = Vector3.zero;
        targetDir.x = x;
        targetDir.z = z;

        rb.velocity = new Vector3(targetDir.x * moveSpeedX, rb.velocity.y, targetDir.z * moveSpeedZ);


        animator.SetFloat("moveX", targetDir.x);
        animator.SetFloat("moveZ", targetDir.z);

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameManager.InitiateBattle(collision.gameObject);
        }
    }

    private void CheckFloor()
    {

    }
}
