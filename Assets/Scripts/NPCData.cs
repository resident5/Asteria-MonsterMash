using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class NPCData : MonoBehaviour, Interactable
{
    public Sprite dialogueImage;
    public string dialogue;
    GameManager gManager;

    private void Start()
    {
        gManager = GameManager.Instance;
    }
    //TODO: Might want to make this cleaner to do I don't think you should go through 4 classes to set a sprite imo
    public void Interact()
    {
        gManager.state = GameManager.GameState.DIALOGUE;
        gManager.dialogueSystem.gameManager = gManager;
        gManager.dialogueSystem.dialogueUI.right.VNImage.sprite = dialogueImage;
        StartCoroutine(gManager.dialogueSystem.StartDialogue(dialogue));
    }
}
