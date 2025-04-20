using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnOrderSlider : MonoBehaviour
{
    public GameObject unitHandle;
    public RectTransform sliderArea;
    public List<TurnHandle> handles;

    private float speed = 2f;
    public float stepSize = 1.5f;
    public float lerpSpeed = 1f;

    private void Start()
    {
        float halfWidth = sliderArea.rect.width / 2;
    }

    public void Init(List<BattleUnit> battleUnits)
    {
        foreach (BattleUnit unit in battleUnits)
        {
            GameObject ob = Instantiate(unitHandle, sliderArea.GetChild(0).transform);
            var handle = ob.GetComponent<TurnHandle>();
            handle.battleUnit = unit;
            handle.imageTransform = sliderArea.GetChild(0).GetComponent<RectTransform>();

            if (unit.GetComponent<EnemyUnit>())
                handle.icon.color = Color.red;

            if (unit.GetComponent<PlayerUnit>())
                handle.icon.color = Color.green;

            handles.Add(handle);
        }
    }

    public void DeInit()
    {
        foreach (var handle in handles)
        {
            Destroy(handle.gameObject);
        }

        handles.Clear();
    }

    public void AddNewUnit(BattleUnit battleUnit)
    {
        GameObject ob = Instantiate(unitHandle, sliderArea.GetChild(0).transform);
        var handle = ob.GetComponent<TurnHandle>();
        handle.battleUnit = battleUnit;
        handle.imageTransform = sliderArea.GetChild(0).GetComponent<RectTransform>();

        if(battleUnit is EnemyUnit enemyUnit)
            handle.icon.color = Color.red;
        else if (battleUnit is PlayerUnit playerUnit)
            handle.icon.color = Color.green;


        //else if (battleUnit is SummonUnit summonUnit)
        //    handle.icon.color = Color.blue;

        handles.Add(handle);

    }
}

