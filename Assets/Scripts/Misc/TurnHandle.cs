using System;
using UnityEngine;
using UnityEngine.UI;

public class TurnHandle : MonoBehaviour
{
    public RectTransform rect;
    public RectTransform imageTransform;

    [SerializeField] private float value;
    public BattleUnit battleUnit;

    public Image icon;

    public const float MINVALUE = 0;
    public const float MAXVALUE = 100;

    private float ImageWidth => imageTransform.rect.width;

    private void Start()
    {
        rect = GetComponent<RectTransform>();

        //float halfWidth = imageTransform.rect.width / 2;
        //startPos = imageTransform.position + new Vector3(halfWidth, 0, 0);
        //endPos = imageTransform.position - new Vector3(halfWidth, 0, 0);

        //value = MAXVALUE;


        //Get the battle unit's AP which is between 0 and 100
        //Calculate between the newPosition the handle should be at based on the AP
        //IE if the the width if 500 and the ap is 50 then the position should be at 250

    }

    private void Update()
    {
        value = Mathf.Clamp(battleUnit.currentActionValue, MINVALUE, MAXVALUE);
        float inverseNormalizeValue = 1 - (value / MAXVALUE);

        float newPosX = CalculateXPosition(inverseNormalizeValue);
        Vector2 newPosition = new Vector3(newPosX, rect.anchoredPosition.y);
        rect.anchoredPosition = newPosition;
    }

    private float CalculateXPosition(float normalizedValue)
    {
        return ImageWidth * normalizedValue;
    }
}
