using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class NPCData : MonoBehaviour, Interactable
{
    public Sprite dialogueIcon;
    public string dialogue;
    GameManager gManager;

    private void Start()
    {
        gManager = GameManager.Instance;
    }

    public void Interact()
    {
        gManager.state = GameManager.GameState.DIALOGUE;
        gManager.dialogueSystem.gameManager = gManager;
        StartCoroutine(gManager.dialogueSystem.StartDialogue(dialogue));
    }
}
