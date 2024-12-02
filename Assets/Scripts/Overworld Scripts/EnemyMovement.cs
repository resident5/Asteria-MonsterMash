using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;

    public float moveSpeedX, moveSpeedZ;

    public Transform target;
    public EnemyData eData;

    public float detectionRadius;
    public bool facingRight;
    public LayerMask playerMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        eData = GetComponent<EnemyData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.OVERWORLD)
            Movement();
    }

    void Movement()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        PlayerMovement player = null;
        foreach (Collider item in colliders)
        {
            player = item.GetComponent<PlayerMovement>();
            if (player != null)
            {
                eData.ChangeState(EnemyData.EnemyState.CHASING);
                target = player.transform;
            }
        }

        if (player == null)
        {
            eData.ChangeState(EnemyData.EnemyState.IDLE);
        }

        if (eData.enemyState == EnemyData.EnemyState.CHASING)
        {
            Vector3 dir = target.position - transform.position;
            dir.Normalize();

            rb.velocity = dir * moveSpeedX;
            Flip(dir.x);
        }

    }

    void Flip(float xDirection)
    {
        if (xDirection > 0 && !facingRight || xDirection < 0 && facingRight)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
