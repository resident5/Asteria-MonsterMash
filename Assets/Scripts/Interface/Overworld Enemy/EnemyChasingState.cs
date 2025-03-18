using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyChasingState : EnemyState
{
    EnemyData eData;
    EnemyMovement eMovement;

    public EnemyChasingState(EnemyData e, EnemyStateMachine eState) : base(e, eState)
    {
        eData = e;
        eMovement = eData.movement;
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
        eData.movement.rb.velocity = Vector3.zero;
    }

    public override void ChangeState(EnemyState newEnemyState)
    {
        eData.StateMachine.ChangeState(newEnemyState);
    }

    public override void OnCollision(Collision other)
    {
        GameObject obj = other.gameObject;

        if (obj == eData.player.gameObject)
        {
            PlayerData pData = obj.GetComponent<PlayerData>();
            GameManager.Instance.InitiateBattle(pData, eData);
            ChangeState(eData.IdleState);
        }
    }
    public override void OnTrigger(Collision other)
    {

    }

    public void Move()
    {
        if (eData.player != null)
        {
            Vector3 dir = eData.player.transform.position - eMovement.transform.position;
            dir.Normalize();

            eMovement.rb.velocity = dir * eMovement.moveSpeedX;
            Flip(dir.x);
        }
    }

    void Flip(float xDirection)
    {
        if (xDirection > 0 && !eMovement.facingRight || xDirection < 0 && eMovement.facingRight)
        {
            eMovement.facingRight = !eMovement.facingRight;
            Vector3 rot = Vector3.zero;
            if (!eMovement.facingRight)
            {
                rot.y = 180;
            }

            eMovement.transform.localEulerAngles = rot;
        }
    }

}
