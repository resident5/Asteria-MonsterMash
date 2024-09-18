using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<BattleUnit> battleMons;
    public GameManager gameManager => GameManager.Instance;

    private void Start()
    {
        battleMons = new List<BattleUnit>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameManager.InitiateBattle(this, collision.gameObject);
        }
    }
    //Keep track of the player's movements and health
}
