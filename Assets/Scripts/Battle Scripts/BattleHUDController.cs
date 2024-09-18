using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Primary List is for Fight, Summon, Item, Flee options
//Options List is the box that populates the options after clicking Fight, Summon and Item
//Secondary List is for 
public class BattleHUDController : MonoBehaviour
{
    public GameObject actionButtonPrefab;

    public GameObject primaryList;

    public Transform fightHolder;
    public Transform otherHolder;

    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text lustText;

    public PlayerUnit playerUnit;

    //public Button fightBtn;
    //public Button otherBtn;
    //public Button itemBtn;
    //public Button fleeBtn;

    public void SetHUD(PlayerUnit currentPlayerTurn)
    {
        foreach (var action in currentPlayerTurn.myBattleMoves)
        {
            GameObject obj = null;
            switch (action.menuType)
            {
                case UnitAction.MenuType.FIGHT:
                    obj = Instantiate(actionButtonPrefab, fightHolder);
                    break;
                case UnitAction.MenuType.OTHER:
                    obj = Instantiate(actionButtonPrefab, otherHolder);
                    break;
            }
            UnitActionButton uAction = obj.GetComponent<UnitActionButton>();
            uAction.unitAction = action;
            obj.GetComponentInChildren<TMP_Text>().text = uAction.unitAction.name;
        }

        Init();
    }

    public void DisableHUD()
    {
        foreach (Transform item in fightHolder)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in otherHolder)
        {
            Destroy(item.gameObject);
        }
    }

    public void Init()
    {
        primaryList.SetActive(true);
        fightHolder.parent.gameObject.SetActive(false);
        otherHolder.parent.gameObject.SetActive(false);
    }
}
