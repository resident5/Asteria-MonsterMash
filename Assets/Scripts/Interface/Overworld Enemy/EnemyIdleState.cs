using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyIdleState : EnemyState
{
    private EnemyData eData;
    private EnemyMovement eMovement;
    private bool FoundPlayer => DetectedPlayer();
    private float Awareness { get => eData.awareness; set => eData.awareness = value; }

    public EnemyIdleState(EnemyData e, EnemyStateMachine eState) : base(e, eState)
    {
        eData = e;
        eMovement = e.movement;
    }

    public override void Enter()
    {
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        UpdateAwareness(FoundPlayer);
        
        if (Awareness >= 100)
        {
            ChangeState(eData.ChasingState);
        }
    }
    public override void PhysicsUpdate()
    {
    }
    public override void Exit()
    {
        Awareness = 0;
        eData.emote.DeActivate();
    }
    public override void ChangeState(EnemyState newEnemyState)
    {
        eData.StateMachine.ChangeState(newEnemyState);
    }

    public override void OnCollision(Collision other)
    {

    }
    public override void OnTrigger(Collision other)
    {
    }

    private bool DetectedPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(eData.transform.position, eMovement.detectionRadius);

        foreach (Collider item in colliders)
        {
            if (item.gameObject.tag == "Player")
            {
                eData.emote.Activate();

                eData.player = item.GetComponent<PlayerMovement>();

                return true;
            }
        }

        return false;
    }

    private void UpdateAwareness(bool isIncreasing)
    {
        if (isIncreasing)
        {
            Awareness += 1 * eData.awarenessRate * Time.deltaTime;
        }
        else
        {
            Awareness -= 1 * eData.awarenessRate * Time.deltaTime;
        }

        eData.awareness = Mathf.Clamp(eData.awareness, 0, 100);
        eData.emote.warningFill.fillAmount = eData.awareness / 100f;

    }

}
