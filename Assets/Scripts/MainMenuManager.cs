using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public MainMenuPanel[] mainPanels = new MainMenuPanel[10];

    MainMenuPanel activePanel;
    MainMenuPanel previousPanel;

    public void DisplayPanel()
    {
        activePanel.gameObject.SetActive(true);
    }

    void TurnOffAllOthersPanels()
    {
        foreach (var panel in mainPanels)
        {
            panel.gameObject.SetActive(false);
        }
    }

    public void OnStoryPress()
    {

    }

    public void OnSandboxPress()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
