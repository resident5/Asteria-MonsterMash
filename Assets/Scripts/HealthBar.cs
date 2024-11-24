using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public BattleUnit unit;
    public Image fill;

    public void Update()
    {
        if (unit != null)
        {
            float amount = (float)unit.myStats.Health / unit.myStats.MaxHealth;
            fill.fillAmount = amount;
            //Debug.Log("My unit health = " + amount);
        }
    }
}
