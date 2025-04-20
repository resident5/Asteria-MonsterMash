using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushController : MonoBehaviour
{
    public GameObject[] spawnAbleEnemies;
    void Start()
    {
        foreach (Transform child in transform)
        {
            EncounterEvent e = child.GetComponent<EncounterEvent>();
            e.encounterList = spawnAbleEnemies;
        }
    }

}
