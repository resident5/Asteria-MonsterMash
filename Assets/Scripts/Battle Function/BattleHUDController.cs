
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Primary List is for Fight, Summon, Item, Flee options
//Options List is the box that populates the options after clicking Fight, Summon and Item
//Secondary List is for 
public class BattleHUDController : Singleton<BattleHUDController>
{
    public GameObject actionButtonPrefab;
    public GameObject monButtonPrefab;

    public PlayerUnit playerUnit;
    public TurnOrderSlider turnOrderSlider;

    public GameObject primaryList;

    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text lustText;

    [Header("Menu Manager")]
    public Stack<Menu> battleMenuStack = new Stack<Menu>();

    [Header("Button Holders")]
    [Space]
    public Transform fightHolder;
    public Transform otherHolder;
    public Transform summonHolder;

    public List<GameObject> fightButtonList;
    public List<GameObject> otherButtonList;
    public List<GameObject> summonButtonList;

    public Button primaryFirstButton;

    //When clicking the primary button have the button look for the first button of the next set of buttons
    public Button secondaryFirstButton;
    public Button tertiaryFirstButton;

    public GameObject[] panelsList;
    public Button previousButtonPressed;

    public void ShowMenu(Menu newMenu)
    {
        if (battleMenuStack.Count > 0)
            battleMenuStack.Peek().SetActive(false);


        newMenu.SetActive(true);
        

        if (newMenu.firstButton != null)
        {
            newMenu.firstButton.Select();
        }
        else
        {
            if (newMenu.holder.transform.childCount > 0)
            {
                newMenu.firstButton = newMenu.holder.transform.GetChild(0).GetComponent<Button>();
                newMenu.firstButton.Select();
            }
        }

        battleMenuStack.Push(newMenu);
    }

    public void HideMenu()
    {
        if (battleMenuStack.Count > 0)
        {
            var topMenu = battleMenuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if (battleMenuStack.Count > 0)
            {
                battleMenuStack.Peek().gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Setup the HUD for the player's actions
    /// </summary>
    /// <param name="currentPlayerTurn"></param>
    public void SetHUD(PlayerUnit currentPlayerTurn)
    {
        primaryFirstButton.Select();
        //Populate battle moves section
        foreach (var action in currentPlayerTurn.myDataStats.battleMoves)
        {
            SetupMoves(action);
        }

        //Populate summons list
        foreach (MonsterData summon in currentPlayerTurn.Summons)
        {
            SetupSummons(summon);
        }
        Init();
    }

    private void SetupMoves(UnitActionSO unitActions)
    {
        GameObject obj = null;
        switch (unitActions.menuType)
        {
            case UnitActionSO.MenuType.FIGHT:
                obj = Instantiate(actionButtonPrefab, fightHolder);
                var fightMenuHolder = fightHolder.GetComponent<Menu>();
                if (fightMenuHolder != null && fightMenuHolder.firstButton == null)
                {
                    fightMenuHolder.firstButton = obj.GetComponent<Button>();
                }
                fightButtonList.Add(obj);
                break;
            case UnitActionSO.MenuType.OTHER:
                obj = Instantiate(actionButtonPrefab, otherHolder);
                var otherMenuHolder = otherHolder.GetComponent<Menu>();
                if (otherMenuHolder != null && otherMenuHolder.firstButton == null)
                {
                    otherMenuHolder.firstButton = obj.GetComponent<Button>();
                }
                otherButtonList.Add(obj);
                break;
        }
        UnitActionButton uAction = obj.GetComponent<UnitActionButton>();
        uAction.unitAction = unitActions;
        obj.GetComponentInChildren<TMP_Text>().text = uAction.unitAction.name;

    }
    
    private void SetupSummons(MonsterData summon)
    {
        GameObject obj = Instantiate(monButtonPrefab, summonHolder);
        MonSummon mSummon = obj.GetComponent<MonSummon>();
        mSummon.monData = summon;
        obj.GetComponentInChildren<TMP_Text>().text = mSummon.monData.monsterStats.unitName;
        summonButtonList.Add(obj);
    }

    public void UpdateHUD()
    {
        //Destroy the entire HUD and recreate it
        //Only use for when you need to add new skills or summons mid fight
    }

    /// <summary>
    /// Clear out the HUD after the player has went to prep for the next player.
    /// Change this so it also destroys the summon skill as well
    /// </summary>
    public void ClearHUD()
    {
        foreach (var objBtn in otherButtonList)
        {
            Destroy(objBtn.gameObject);
        }

        foreach (var objBtn in fightButtonList)
        {
            Destroy(objBtn.gameObject);
        }

        foreach (var objBtn in summonButtonList)
        {
            Destroy(objBtn.gameObject);
        }

        otherButtonList.Clear();
        fightButtonList.Clear();
        summonButtonList.Clear();
    }

    public void EnableHUD()
    {
        foreach (Transform item in fightHolder)
        {
            item.parent.gameObject.SetActive(true);
        }

        foreach (Transform item in otherHolder)
        {
            item.parent.gameObject.SetActive(true);
        }
    }

    public void DisableHUD()
    {
        foreach (Transform item in fightHolder)
        {
            item.parent.gameObject.SetActive(false);
        }

        foreach (Transform item in otherHolder)
        {
            item.parent.gameObject.SetActive(false);
        }
    }

    //public void NextPanel(GameObject panel)
    //{
    //    //Turn on this panel
    //    //Turn on this panel's parent
    //    //Turn off everything in this panel's parent's children EXCEPT this

    //    Transform parent = panel.transform.parent;

    //    parent.gameObject.SetActive(true);
    //    panel.SetActive(true);

    //    foreach (Transform item in parent)
    //    {
    //        if (item.gameObject != panel)
    //        {
    //            item.gameObject.SetActive(false);
    //        }
    //    }

    //    foreach (Transform item in parent.parent)
    //    {
    //        if (item.gameObject != parent.gameObject)
    //        {
    //            item.gameObject.SetActive(false);
    //        }
    //    }
    //}

    //private void SetNavigation(List<GameObject> moveList)
    //{
    //    for (int i = 0; i < moveList.Count; i++)
    //    {
    //        Button btn = moveList[i].GetComponent<Button>();
    //        Navigation nav = btn.navigation;
    //        nav.mode = Navigation.Mode.Explicit;

    //        //If i is at the last element, set the select down to be the first element... else set select down to the next element
    //        nav.selectOnDown = i == moveList.Count - 1 ? moveList[0].GetComponent<Button>() : moveList[i + 1].GetComponent<Button>();

    //        //If i is at the first element, set the select up to be the last element... else set the select up to the previous element.
    //        nav.selectOnUp = i == 0 ? moveList[moveList.Count - 1].GetComponent<Button>() : moveList[i - 1].GetComponent<Button>();

    //        btn.navigation = nav;
    //    }


    //    //Button firstButton = moveList[0].GetComponent<Button>();
    //    //Button lastButton = moveList[moveList.Count - 1].GetComponent<Button>(); ;

    //    //Navigation firstNav = firstButton.navigation;
    //    //firstNav.mode = Navigation.Mode.Explicit;
    //    //firstNav.selectOnUp = lastButton;
    //    //firstNav.selectOnLeft = lastButton;

    //    //firstButton.navigation = firstNav;

    //    //Navigation lastNav = lastButton.navigation;
    //    //lastNav.mode = Navigation.Mode.Explicit;
    //    //lastNav.selectOnRight = firstButton;
    //    //lastNav.selectOnDown = firstButton;

    //    //lastButton.navigation = lastNav;

    //}

    private void Init()
    {
        primaryList.SetActive(true);
        fightHolder.parent.gameObject.SetActive(false);
        otherHolder.parent.gameObject.SetActive(false);
    }
}
