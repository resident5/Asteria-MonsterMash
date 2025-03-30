using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Naninovel;

public class NPCData : Interactable
{
    //public Sprite dialogueImage;
    //public string dialogue;
    DialogueSystem dialogueSystem => DialogueSystem.Instance;
    DialogueManager dialogueManager => DialogueManager.Instance;

    private Transform worldCanvas;

    public Emote emote;

    public string dialogueScript;

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
        dialogueManager.StartConversation(dialogueScript);

        //dialogueSystem.StartConversation(dialogue);
    }
}
