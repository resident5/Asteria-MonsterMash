using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyChasingState : EnemyState
{
    EnemyController eController;

    public EnemyChasingState(EnemyController e, EnemyStateMachine eState) : base(e, eState)
    {
        eController = e;
    }

    public override void Enter()
    {
    }

    public override void FrameUpdate()
    {
        Move();
    }

    public override void PhysicsUpdate()
    {
    }
    public override void Exit()
    {
        eController.rb.velocity = Vector3.zero;
    }

    public override void ChangeState(EnemyState newEnemyState)
    {
        eController.StateMachine.ChangeState(newEnemyState);
    }

    public override void OnCollision(Collision other)
    {
        GameObject obj = other.gameObject;

        if (obj.tag == "Player")
        {
            PlayerController pController = obj.GetComponent<PlayerController>();
            GameManager.Instance.InitiateBattle(pController, eController);
            ChangeState(eController.IdleState);
        }
    }
    public override void OnTrigger(Collision other)
    {

    }

    public void Move()
    {
        if (eController.player != null)
        {
            Vector3 dir = eController.player.transform.position - eController.transform.position;
            dir.Normalize();

            eController.rb.velocity = dir * eController.moveSpeedX;
            Flip(dir.x);
        }
    }

    void Flip(float xDirection)
    {
        if (xDirection > 0 && !eController.facingRight || xDirection < 0 && eController.facingRight)
        {
            eController.facingRight = !eController.facingRight;
            Vector3 rot = Vector3.zero;
            if (!eController.facingRight)
            {
                rot.y = 180;
            }

            eController.transform.localEulerAngles = rot;
        }
    }

}
