using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;

public class EncounterEvent : MonoBehaviour
{
    int playerSteps;
    public PlayerMovement player;

    public float timer = 0;
    public float maxTime = 7f;
    public GameObject[] encounterList;
    public Canvas worldCanvas;

    public Transform unitHolder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.GetComponent<PlayerMovement>();
            playerSteps = player.steps;
        }
    }

    public void CalculateEncounter()
    {
        player.Step();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
        mon.GetComponent<EnemyData>().SetupEmote(worldCanvas);
    }
}
