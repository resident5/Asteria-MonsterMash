using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    public UIDocument document;
    private Button button;

    private List<Button> menuButtons = new List<Button>();

    public AudioSource audioSource;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        audioSource = GetComponent<AudioSource>();
        button = document.rootVisualElement.Q("SandboxButton") as Button;
        //button.RegisterCallback<KeyDownEvent>(OnSandboxClick);
        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        AddButtonEvents(OnAllButtonClick);
    }

    private void OnDisable()
    {
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(evt => OnAllButtonClick());
            menuButtons[i].UnregisterCallback<KeyDownEvent>(evt => OnAllButtonClick());
        }
    }

    public void AddButtonEvents(Action callback)
    {
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(evt => callback());
            menuButtons[i].RegisterCallback<KeyDownEvent>(evt => callback());
        }
    }

    public void OnAllButtonClick()
    {
        Debug.Log("PRESSED BUTTON!");
        audioSource.Play();
    }
}
