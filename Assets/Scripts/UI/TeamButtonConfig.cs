using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Use events to handle updating the UI whenever leaving battle, healing, or dealing damage
/// </summary>
public class TeamButtonConfig : MonoBehaviour
{
    //public BattleUnit battleMon;

    public Image healthFill;
    public Image manaFill;
    public Image lustFill;

    public Image monImage;
    public TMP_Text monNameText;

    public Gradient gradient;

    public UnitStats unitStats;

    public void InitStats(UnitStats unitStats)
    {
        this.unitStats = unitStats;
    }

    private void Update()
    {
        if (unitStats != null)
        {
            monNameText.text = unitStats.unitName;
            BattleStats stats = unitStats.battleStats;

            healthFill.fillAmount = stats.Health / stats.MaxHealth;
            manaFill.fillAmount = stats.Mana / 100;
            lustFill.fillAmount = stats.Lust / 100;

            healthFill.color = gradient.Evaluate(1f);
        }

    }


    public void UpdateStatsBar()
    {
        monNameText.text = unitStats.unitName;
        BattleStats stats = unitStats.battleStats;

        healthFill.fillAmount = Mathf.Clamp01(stats.Health / stats.MaxHealth);
        manaFill.fillAmount = stats.Mana / 100;
        lustFill.fillAmount = stats.Lust / 100;

        healthFill.color = gradient.Evaluate(1f);
    }

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
