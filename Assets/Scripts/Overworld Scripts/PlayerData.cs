using Codice.CM.WorkspaceServer.DataStore.Merge;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    public List<UnitCreatorSO> battleMons;
    public GameManager gameManager => GameManager.Instance;

    public UnitCreatorSO playerCreator;

    public Interactable focusTarget;
    public float detectionRadius;

    [Header("Stats")]
    public Data data;

    private void Start()
    {
        InitializeStats();
    }

    public void InitializeStats()
    {
        data.id = playerCreator.data.id;
        data.level = playerCreator.data.level;
        data.unitName = playerCreator.data.unitName;
        data.description = playerCreator.data.description;
        data.stats = playerCreator.data.stats;
        data.isCapturable = playerCreator.data.isCapturable;
        data.isEnemy = playerCreator.data.isEnemy;
        data.spriteImage = playerCreator.data.spriteImage;
        data.animator = playerCreator.data.animator;
        data.battleMoves = playerCreator.data.battleMoves;
    }

    private void Update()
    {
        CheckNPC();
    }

    public void CheckNPC()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        bool foundtarget = false;

        foreach (Collider collider in colliders)
        {
            Interactable nData = collider.GetComponent<Interactable>();
            if (nData != null)
            {
                if (focusTarget != nData)
                {
                    focusTarget = nData;
                    foundtarget = true;
                }
                break;
            }
        }

        if(!foundtarget && focusTarget != null)
        {
            focusTarget = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject colObj = collision.gameObject;

        if (colObj.tag == "Enemy")
        {
            EnemyData eData = colObj.GetComponent<EnemyData>();

            if (eData.enemyState == EnemyData.EnemyState.CHASING)
            {
                gameManager.InitiateBattle(this, collision.gameObject);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        data.stats.Health -= damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    //Keep track of the player's movements and health
}
