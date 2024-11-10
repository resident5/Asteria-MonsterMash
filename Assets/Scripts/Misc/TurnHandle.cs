using System;
using UnityEngine;
using UnityEngine.UI;

public class TurnHandle : MonoBehaviour
{
    public RectTransform rect;
    public float value;
    public BattleUnit battleUnit;

    public Image icon;

    public const float MINVALUE = 0;
    public const float MAXVALUE = 100;

    public RectTransform imageTransform;

    private Vector3 startPos;
    private Vector3 endPos;

    public void Start()
    {
        rect = GetComponent<RectTransform>();

        float halfWidth = imageTransform.rect.width / 2;
        startPos = imageTransform.position + new Vector3(halfWidth, 0, 0);
        endPos = imageTransform.position - new Vector3(halfWidth, 0, 0);

        value = MAXVALUE;
    }

    public void UpdateHUD()
    {
        value = battleUnit.currentActionValue;
        value = Math.Clamp(value, MINVALUE, MAXVALUE);
    }

    public void Update()
    {
        UpdateHUD();
        //Convert the current action value into a percentage
        float normalizedPos = value / 100;
        
        //
        if(normalizedPos < 0)
        {
            normalizedPos = 0;
        }
        transform.position = Vector3.Lerp(startPos, endPos, normalizedPos);
    }
}
