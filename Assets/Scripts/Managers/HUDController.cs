using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : Singleton<HUDController>
{
    public GameObject mainMenu;
    public Animator anim;
    public CanvasGroup fadeCanvas;

    [Header("Summons")]
    public GameObject UIMonDataPrefab;
    public Transform teamHolder;

    public Button firstButton;
    public GameObject[] firstPanels;

    public Transform contextMenuHolder;
    public GameObject contextMenuPrefab;

    [Header("Dialogue")]
    public DialogueUI dialogueUI;

    [Header("Inventory")]
    public Transform inventoryHolder;
    public List<InventorySlot> inventorySlots;

    public GameObject teamSelectScreen;

    public BattleUnit selectedUnit;
    public Item selectedItem;

    [Header("Misc")]
    public Popup popup;


    private void Start()
    {
        mainMenu.SetActive(false);
    }

    public void ShowPauseMenu(PlayerData playData, bool isPaused)
    {
        anim.SetBool("pause", isPaused);
        firstButton.Select();
        SetupMenu(playData, isPaused);
        mainMenu.SetActive(isPaused);
    }

    public void SetupMenu(PlayerData playerData, bool paused)
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

        if (paused)
        {
            GameObject pObj = Instantiate(UIMonDataPrefab, teamHolder);
            Button pBtn = pObj.GetComponent<Button>();
            UIMonStats pStats = pObj.GetComponent<UIMonStats>();
            //pBtn.onClick.AddListener(() => pBtn.GetComponent<ContextMenuButton>().ContextMenuSelector());
            //Setup player stats in menu
            pStats.SetupPlayerStats(playerData);

            foreach (var mon in playerData.battleMons)
            {
                //Setup mon stats in menu
                GameObject obj = Instantiate(UIMonDataPrefab, teamHolder);
                Button btn = obj.GetComponent<Button>();
                UIMonStats monStats = obj.GetComponent<UIMonStats>();

                monStats.SetStatsBars(mon);
            }
        }
        else
        {
            foreach (Transform teamObj in teamHolder)
            {
                Destroy(teamObj.gameObject);
            }
        }
    }

    public void HideAllPanels()
    {
        foreach (var panel in firstPanels)
        {
            panel.SetActive(false);
        }
    }

    public void SelectMon(BattleUnit unit)
    {
        selectedUnit = unit;
    }

    public void SelectItem(Item item)
    {
        selectedItem = item;
    }

    public void DisplayPopupInfo(Sprite sprite, string info)
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
        Debug.Log("Fade Out");
        LeanTween.alphaCanvas(fadeCanvas, 0, 1.2f);
    }
}
