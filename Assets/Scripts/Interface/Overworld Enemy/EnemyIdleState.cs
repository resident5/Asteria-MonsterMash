using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyIdleState : EnemyState
{
    private EnemyController eController;
    private bool FoundPlayer => DetectedPlayer();
    private float Awareness { get => eController.enemyData.awareness; set => eController.enemyData.awareness = value; }

    public EnemyIdleState(EnemyController e, EnemyStateMachine eState) : base(e, eState)
    {
        eController = e;
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
            ChangeState(eController.ChasingState);
        }
    }
    public override void PhysicsUpdate()
    {
    }
    public override void Exit()
    {
        Awareness = 0;
        eController.enemyData.emote.DeActivate();
    }
    public override void ChangeState(EnemyState newEnemyState)
    {
        eController.StateMachine.ChangeState(newEnemyState);
    }

    public override void OnCollision(Collision other)
    {

    }
    public override void OnTrigger(Collision other)
    {
    }

    private bool DetectedPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(eController.transform.position, eController.detectionRadius);

        foreach (Collider item in colliders)
        {
            if (item.gameObject.tag == "Player")
            {
                eController.enemyData.emote.Activate();

                eController.player = item.GetComponent<PlayerController>();

                return true;
            }
        }

        return false;
    }

    private void UpdateAwareness(bool isIncreasing)
    {
        if (isIncreasing)
        {
            Awareness += 1 * eController.enemyData.awarenessRate * Time.deltaTime;
        }
        else
        {
            Awareness -= 1 * eController.enemyData.awarenessRate * Time.deltaTime;
        }

        eController.enemyData.awareness = Mathf.Clamp(eController.enemyData.awareness, 0, 100);
        eController.enemyData.emote.warningFill.fillAmount = eController.enemyData.awareness / 100f;

    }

}
