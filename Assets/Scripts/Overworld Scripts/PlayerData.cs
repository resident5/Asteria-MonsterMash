using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<UnitCreatorSO> battleMons;
    public GameManager gameManager => GameManager.Instance;

    private void Start()
    {
        battleMons = new List<UnitCreatorSO>();
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
    //Keep track of the player's movements and health
}
