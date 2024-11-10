using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;


public class TurnOrderSlider : MonoBehaviour
{
    public GameObject unitHandle;
    public RectTransform sliderArea;
    public List<TurnHandle> handles;

    private Vector3 startPos;
    private Vector3 endPos;
    private float speed = 2f;
    public float stepSize = 1.5f;
    public float lerpSpeed = 1f;

    private void Start()
    {
        float halfWidth = sliderArea.rect.width / 2;
        startPos = sliderArea.position + new Vector3(halfWidth, 0, 0);
        endPos = sliderArea.position - new Vector3(halfWidth, 0, 0);
    }

    public void Init(List<BattleUnit> battleUnits)
    {
        foreach (BattleUnit unit in battleUnits)
        {
            GameObject ob = Instantiate(unitHandle, sliderArea.GetChild(0).transform);
            var handle = ob.GetComponent<TurnHandle>();
            handle.battleUnit = unit;
            handle.imageTransform = sliderArea;

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

    private void Update()
    {

        //if (handles != null)
        //{
        //    for (int i = 0; i < handles.Count; i++)
        //    {
        //        //Calculate this to make it keep track of turn order
        //        //if (i > 0)
        //        //{
        //        //    targetX = Mathf.Lerp(startPos.x, endPos.x, (handles[i].battleUnit.currentActionValue / 50));
        //        //}
        //        //else
        //        //{
        //        //    targetX = Mathf.Lerp(startPos.x, endPos.x, handles[i].battleUnit.currentActionValue / 100);
        //        //}
        //        float targetX = Mathf.Clamp(startPos.x, endPos.x, handles[i].battleUnit.currentActionValue / 100);
        //        Vector3 handlePos = handles[i].rect.position;
        //        Vector3 targetPos = new Vector3(targetX * 2, handlePos.y, handlePos.z);

        //        handles[i].rect.position = Vector3.Lerp(handlePos, targetPos, lerpSpeed * Time.deltaTime);
        //        handles[i].UpdateHUD();

        //    }
        //    //foreach (var handle in handles)
        //    //{
        //    //    float targetX = Mathf.Lerp(endPos.x, startPos.x, handle.battleUnit.actionValue / 100);
        //    //    Vector3 handlePos = handle.rect.position;

        //    //    Vector3 targetPos = new Vector3(targetX, handlePos.y, handlePos.z);
        //    //    handle.rect.position = Vector3.Lerp(handlePos, targetPos, lerpSpeed * Time.deltaTime);

        //    //    handle.UpdateHUD();
        //    //}

        //}
    }

    //public void UpdateHUD(float duration)
    //{
    //    float time = 0;

    //    Debug.Log("Start Updating HUD");

    //    //while (time < duration)
    //    //{

    //    //}

    //    foreach (var handle in handles)
    //    {
    //        float targetX = Mathf.Lerp(endPos.x, startPos.x, handle.battleUnit.actionValue + stepSize / 100);
    //        Vector3 handlePos = handle.rect.position;

    //        Vector3 targetPos = new Vector3(targetX, handlePos.y, handlePos.z);
    //        handle.rect.position = Vector3.Lerp(handlePos, targetPos, lerpSpeed * Time.deltaTime);

    //        handle.UpdateHUD();

    //        time += Time.deltaTime;
    //    }
    //}
}

