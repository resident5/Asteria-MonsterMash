using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;

public class EncounterEvent : MonoBehaviour
{
    private int playerSteps;
    private PlayerController player;

    [SerializeField] private float timer = 0;
    [SerializeField] private float maxTime = 7f;
    [SerializeField] private Canvas worldCanvas;

    public GameObject[] encounterList;
    [SerializeField] private Transform unitHolder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.GetComponent<PlayerController>();
            playerSteps = player.steps;
        }
    }

    public void CalculateEncounter()
    {
        //player.Step();
    }

    private void OnTriggerStay(Collider other)
    {
        //Check if the player is in the event area and not in battle
        if (other.gameObject.tag == "Player" && GameManager.Instance.state != GameManager.GameState.BATTLE)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                timer = 0;
                SpawnMonster();
            }

            //currentEncounter += encounterRate * Time.deltaTime;
            //playerSteps = Mathf.RoundToInt(playerSteps - player.steps);

            //bool encounterEnemy = Random.Range(1, 101) <= playerSteps ? true : false;

            //if(encounterEnemy)
            //{
            //    Debug.Log("ENCOUNTER");
            //}
        }
    }

    public void SpawnMonster()
    {
        int rand = Random.Range(0, encounterList.Length);
        GameObject mon = Instantiate(encounterList[rand], player.transform.position, Quaternion.identity, unitHolder);
        EnemyController eController = mon.GetComponent<EnemyController>();
        eController.enemyData.SetupEmote(GameManager.Instance.worldCanvas);
    }
}
