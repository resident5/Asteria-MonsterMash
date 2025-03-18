using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class NPCData : Interactable
{
    public Sprite dialogueImage;
    public string dialogue;
    DialogueSystem dialogueSystem => DialogueSystem.Instance;

    private Transform worldCanvas;

    public Emote emote;

    private void Awake()
    {
        worldCanvas = GameObject.Find("World Canvas").transform;
    }

    private void Start()
    {
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
