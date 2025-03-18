using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class UIMonStats : MonoBehaviour
{
    //public BattleUnit battleMon;

    public Image healthFill;
    public Image manaFill;
    public Image lustFill;

    public Image monImage;
    public TMP_Text monNameText;

    public Gradient gradient;

    public Data unitData;

    public void InitStats(Data data)
    {
        unitData = data;
    }

    private void Update()
    {
        if (unitData != null)
        {
            monNameText.text = unitData.unitName;
            BattleStats stats = unitData.stats;
            stats.InitStats();

            Debug.Log($"Health Fill {healthFill.fillAmount}");

            healthFill.fillAmount = stats.Health / stats.MaxHealth;
            manaFill.fillAmount = stats.Mana / 100;
            lustFill.fillAmount = stats.Lust / 100;

            healthFill.color = gradient.Evaluate(1f);
        }

    }


    //public void SetStatsBars(UnitCreatorSO unit)
    //{
    //    monNameText.text = unit.data.unitName;
    //    BattleStats stats = unit.data.stats;

    //    healthFill.fillAmount = Mathf.Clamp01(stats.Health / stats.MaxHealth);
    //    lustFill.fillAmount = Mathf.Clamp01(stats.Lust / 100);

    //    fillImage.color = gradient.Evaluate(1f);
    //}

    //public void SetupPlayerStats(PlayerData pUnit)
    //{
    //    monNameText.text = pUnit.data.unitName;

    //    BattleStats stats = pUnit.data.stats;

    //    healthFill.fillAmount = Mathf.Clamp01(stats.Health / stats.MaxHealth);
    //    lustFill.fillAmount = Mathf.Clamp01(stats.Lust / 100);

    //    fillImage.color = gradient.Evaluate(1f);
    //}

    //public void SetHealth(int health)
    //{
    //    healthFill.fillAmount = health;
    //    healthSlider.value = health;
    //    fillImage.color = gradient.Evaluate(healthSlider.normalizedValue);
    //}

    //public void SetLust(int lust)
    //{
    //    lustSlider.value = lust;
    //    fillImage.color = gradient.Evaluate(lustSlider.normalizedValue);
    //}
}
