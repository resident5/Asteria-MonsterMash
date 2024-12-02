using System.Collections.Generic;
using UnityEngine;

//TODO: Make a base Data script that both playerData and enemyData can inherit from
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
        data = playerCreator.data;
        //data.id = playerCreator.data.id;
        //data.level = playerCreator.data.level;
        //data.unitName = playerCreator.data.unitName;
        //data.description = playerCreator.data.description;
        //data.stats = playerCreator.data.stats;
        //data.cannotBeCaptured = playerCreator.data.cannotBeCaptured;
        //data.spriteImage = playerCreator.data.spriteImage;
        //data.animator = playerCreator.data.animator;
        //data.battleMoves = playerCreator.data.battleMoves;
    }

    private void Update()
    {
        CheckNPC();
    }

    public void CheckNPC()
    {
        bool foundtarget = false;

        if (focusTarget == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

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
                }
            }

        }
        else
        {
            if (focusTarget != null)
            {
                Debug.Log("Current Target = " + focusTarget);
            }
        }

        if (!foundtarget && focusTarget != null)
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
                gameManager.InitiateBattle(this, eData);
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
