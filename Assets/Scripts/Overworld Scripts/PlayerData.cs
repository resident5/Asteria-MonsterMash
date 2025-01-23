using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;

//TODO: Make a base Data script that both playerData and enemyData can inherit from
public class PlayerData : Singleton<PlayerData>
{
    public List<UnitCreatorSO> battleMons;
    public GameManager gameManager => GameManager.Instance;

    public UnitCreatorSO playerCreator;

    public Interactable focus;
    public float detectionRadius;

    [Header("Stats")]
    public Data data;

    [Header("Emote")]
    public Emote emote;
    public Canvas worldCanvas;

    private void Start()
    {
        InitializeStats();
        SetupEmote(worldCanvas);
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
        CheckInteractables();
    }

    public void CheckInteractables()
    {
        bool foundtarget = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);
                foundtarget = true;
                break;
            }
        }


        if (!foundtarget)
        {
            focus = null;
        }
    }

    public void SetFocus(Interactable newFocus)
    {
        if (focus != newFocus)
        {
            if (focus != null)
            {
                focus.DeFocused();
            }
            focus = newFocus;
        }

    }

    public void SetupEmote(Canvas canvas)
    {
        emote.transform.SetParent(canvas.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject colObj = collision.gameObject;

        //if (colObj.tag == "Enemy")
        //{
        //    EnemyData eData = colObj.GetComponent<EnemyData>();

        //    if (eData.enemyState == EnemyData.EnemyState.CHASING)
        //    {
        //        gameManager.InitiateBattle(this, eData);
        //    }
        //}
        //Change enemy to idle
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
