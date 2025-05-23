using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Properties;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDController : Singleton<HUDController>
{
    public Animator anim;
    public CanvasGroup fadeCanvas;

    [Header("Menu Manager")]
    public Stack<Menu> menuStack = new Stack<Menu>();
    public Menu main;

    [Header("Summons")]
    public GameObject UIMonDataPrefab;
    public Transform teamHolder;

    public Button firstButton;

    public Transform contextMenuHolder;
    public GameObject contextMenuPrefab;

    [Header("Inventory")]
    public Transform inventoryHolder;

    public BattleUnit selectedUnit;
    public Item selectedItem;

    public UnitStats itemTargetStats;

    [Header("Misc")]
    public Popup popup;

    private void Start()
    {
        main.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.Instance.overworldEvents.onReceivedItem += DisplayPopupInfo;
    }

    private void OnDisable()
    {
        EventManager.Instance.overworldEvents.onReceivedItem -= DisplayPopupInfo;
    }

    public void ShowMenu(Menu newMenu)
    {
        var newMenuLevel = newMenu.level;
        if (menuStack.Count > 0)
        {
            var topMenu = menuStack.Peek();
            if (menuStack.Peek() == newMenu)
            {
                Debug.Log("Menu already exists" + newMenu.name);
                return;
            }

            //If the new menu's level is equal to the top menu's level
            //New menu ON
            //Top menu pop out and off
            if (newMenuLevel == topMenu.level)
            {
                menuStack.Pop().SetActive(false);
            }
        }

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

        menuStack.Push(newMenu);

    }

    public void HideMenu()
    {
        if (menuStack.Count <= 0)
            return;

        //If only the main menu is at the top
        if (menuStack.Count == 1)
        {
            menuStack.Pop().SetActive(false);
            menuStack.Clear();
            return;
        }
        if (menuStack.Count > 0)
        {
            menuStack.Pop().SetActive(false);

            if (menuStack.Count > 0)
            {
                menuStack.Peek().SetActive(true);
            }
        }

        var newMenu = menuStack.Peek();

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
    }

    public void ShowPauseMenu(PlayerData playData, bool isPaused)
    {
        anim.SetBool("pause", isPaused);
    }

    public void CloseMenu()
    {
        foreach (var menu in menuStack)
        {
            if (menu != null)
                menu.SetActive(false);
        }
        menuStack.Clear();
    }

    public void SetupMenu(PlayerData playerData)
    {
        /* When player clicks on a button it should show the corresponding context menu
         * Teams = Examine, Use Item, Back (Each of these are buttons that needs to be instantiated BASED on the context menu)
         * Inventory = Use Item on Team, Back
         * Context menu should know what button was clicked
         * Clicking on the context menu should send the data to HUD?
         * EXAMPLE:
         *  Click on Lyon's Button to show Teams context menu
         *  Click on Use Item to show the Inventory screen
         *  Clicking on an item in the inventory screen should use the item on Lyon
         *  This should work for any spirit you have in your teams menu
         */
        foreach (Transform child in teamHolder)
        {
            Destroy(child.gameObject);
        }

        //Add player info
        GameObject pObj = Instantiate(UIMonDataPrefab, teamHolder);
        TeamButtonConfig configStats = pObj.GetComponent<TeamButtonConfig>();

        configStats.InitStats(playerData.playerStats);

        //Add mon info
        foreach (var mons in playerData.battleMons)
        {
            //Setup mon stats in menu
            GameObject obj = Instantiate(UIMonDataPrefab, teamHolder);
            Button btn = obj.GetComponent<Button>();
            TeamButtonConfig monbuttonConfig = obj.GetComponent<TeamButtonConfig>();

            monbuttonConfig.InitStats(mons.monsterStats);
        }

    }

    //public void UpdateMenu(PlayerData playerData)
    //{
    //    //Clear team menu
    //    foreach (Transform child in teamHolder)
    //    {
    //        DestroyImmediate(child);
    //    }

    //    //Add player info
    //    GameObject pObj = Instantiate(UIMonDataPrefab, teamHolder);
    //    Button pBtn = pObj.GetComponent<Button>();
    //    UIMonStats pStats = pObj.GetComponent<UIMonStats>();

    //    pStats.InitStats(playerData.data);

    //    //Add mon info
    //    foreach (var mons in playerData.battleMons)
    //    {
    //        //Setup mon stats in menu
    //        GameObject obj = Instantiate(UIMonDataPrefab, teamHolder);
    //        Button btn = obj.GetComponent<Button>();
    //        UIMonStats monStats = obj.GetComponent<UIMonStats>();

    //        monStats.InitStats(mons.data);
    //    }
    //}

    public void SelectMon(BattleUnit unit)
    {
        selectedUnit = unit;
    }

    public void SelectItem(Item item)
    {
        selectedItem = item;
    }

    private void DisplayPopupInfo(string info, Sprite sprite)
    {
        if (sprite != null)
        {
            popup.ShowPopupImage(sprite, info);
        }
        else
        {
            popup.ShowPopup(sprite, info);
        }
    }

    public void FadeIn()
    {
        LeanTween.alphaCanvas(fadeCanvas, 1, 1.2f);
    }

    public void FadeOut()
    {
        LeanTween.alphaCanvas(fadeCanvas, 0, 1.2f);
    }
}
