using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonSummon : MonoBehaviour
{
    public UnitCreatorSO monUnit;
    public Slider healthSlider;
    public Slider lustSlider;

    public Image monImage;
    public TMP_Text monNameText;

    public Gradient gradient;

    public Image fillImage;
    public Button btn;

    public void OnEnable()
    {
        btn.onClick.AddListener(() =>
        {
            bool success = BattleManager.Instance.SummonNewUnit(monUnit, true);

            if (!success)
            {
                Debug.Log("Cannot summon");
                //Send a message that the player has no room to summon. 
            }

            BattleHUDController.Instance.summonHolder.gameObject.SetActive(false);

        });
    }

    public void OnDisable()
    {
        
    }

    public void SetStatsBars(UnitCreatorSO unit)
    {
        monNameText.text = unit.data.unitName;

        healthSlider.maxValue = unit.data.stats.MaxHealth;
        healthSlider.value = unit.data.stats.Health;

        lustSlider.maxValue = 100;
        lustSlider.value = unit.data.stats.Lust;

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
