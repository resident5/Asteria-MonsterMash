using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUDController : MonoBehaviour
{
    public GameObject actionButtonPrefab;

    public GameObject firstMenu;
    public GameObject secondMenu;
    public Transform actionHolder;

    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text lustText;

    public void SetHUD(BattleUnit playerUnit)
    {
        //healthText.text = playerUnit.currentHealth.ToString();
        //manaText.text = playerUnit.currentMana.ToString();
        //lustText.text = playerUnit.currentLust.ToString();

        foreach (var action in playerUnit.myBattleMoves)
        {
            GameObject obj = Instantiate(actionButtonPrefab, actionHolder);
            UnitActionButton uAction = obj.GetComponent<UnitActionButton>();
            uAction.unitAction = action;
            obj.GetComponentInChildren<TMP_Text>().text = uAction.unitAction.name;
            //GameObject actionButton = Instantiate(action, actionHolder);

        }
        Init();
    }

    public void DisableHUD()
    {
        foreach (Transform item in actionHolder)
        {
            Destroy(item.gameObject);
        }
    }

    public void Init()
    {
        firstMenu.SetActive(true);
        secondMenu.SetActive(false);
    }
}
