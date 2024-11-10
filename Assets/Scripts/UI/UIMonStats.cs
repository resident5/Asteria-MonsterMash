using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMonStats : MonoBehaviour
{
    //public BattleUnit battleMon;

    public Slider healthSlider;
    public Slider lustSlider;

    public Image monImage;
    public TMP_Text monNameText;

    public Gradient gradient;

    public Image fillImage;

    public void SetStatsBars(UnitCreatorSO unit)
    {
        monNameText.text = unit.unitName;

        healthSlider.maxValue = unit.stats.MaxHealth;
        healthSlider.value = unit.stats.Health;

        lustSlider.maxValue = 100;
        lustSlider.value = unit.stats.Lust;

        fillImage.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        fillImage.color = gradient.Evaluate(healthSlider.normalizedValue);
    }

    public void SetLust(int lust)
    {
        lustSlider.value = lust;
        fillImage.color = gradient.Evaluate(lustSlider.normalizedValue);
    }
}
