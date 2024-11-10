using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject mainMenu;
    public Animator anim;

    public GameObject UIMonDataPrefab;
    public Transform teamHolder;

    public Button firstButton;

    public GameObject[] firstPanels;

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
        if (paused)
        {
            foreach (var mon in playerData.battleMons)
            {
                GameObject obj = Instantiate(UIMonDataPrefab, teamHolder);
                UIMonStats monStats = obj.GetComponent<UIMonStats>();
                monStats.SetStatsBars(mon);
            }
        }
        else
        {
            foreach(Transform team in teamHolder)
            {
                Destroy(team);
            }
        }
    }

    public void HideAllPanels()
    {
        foreach(var panel in firstPanels)
        {
            panel.SetActive(false);
        }
    }
}
