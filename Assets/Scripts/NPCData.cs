using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class NPCData : Interactable
{
    public Sprite dialogueImage;
    public string dialogue;
    DialogueSystem dialogueSystem;

    public Transform worldCanvas;

    public Emote emote;

    private void Start()
    {
        dialogueSystem = DialogueSystem.Instance;
        SetupEmote();
    }

    public void SetupEmote()
    {
        //worldCanvas = GameObject.Find("World Canvas");
        emote.transform.SetParent(worldCanvas.transform); 
    }

    public override void Interact()
    {
        //gManager.state = GameManager.GameState.DIALOGUE;
        //gManager.dialogueSystem.gameManager = gManager;
        //gManager.dialogueSystem.dialogueUI.right.VNImage.sprite = dialogueImage;
        dialogueSystem.StartConversation(dialogue);
    }
}
