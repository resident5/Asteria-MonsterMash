using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;

    public float moveSpeedX, moveSpeedZ;

    public EnemyData eData;

    public float detectionRadius;
    public bool facingRight;

    public PlayerMovement player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        eData = GetComponent<EnemyData>();
    }

    // Update is called once per frame
    void Update()
    {
        //DetectPlayer();
    }

    //void DetectPlayer()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
    //    PlayerMovement player = null;

    //    foreach (Collider item in colliders)
    //    {
    //        player = item.GetComponent<PlayerMovement>();
    //        if (player != null && GameManager.Instance.state == GameManager.GameState.OVERWORLD)
    //        {
    //            eData.StateMachine.ChangeState(eData.ChasingState);
    //        }
    //        else
    //        {
    //            eData.StateMachine.ChangeState(eData.IdleState);
    //        }
    //    }

    //    //if (eData.enemyState == EnemyData.EnemyState.CHASING)
    //    //{
    //    //    Vector3 dir = target.position - transform.position;
    //    //    dir.Normalize();

    //    //    rb.velocity = dir * moveSpeedX;
    //    //    Flip(dir.x);
    //    //}

    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
