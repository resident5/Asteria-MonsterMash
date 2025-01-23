using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B83.LogicExpressionParser;
using Newtonsoft.Json.Linq;

public class DialogueSystem : MonoBehaviour
{
    #region Singleton
    private static DialogueSystem instance;
    public static DialogueSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueSystem>();
            }
            return instance;
        }
    }
    #endregion

    public LinkedList<JObject> dialogueQueue = new LinkedList<JObject>();
    public LinkedListNode<JObject> currentDialogueNode;
    public DialogueUI dialogueUI => GameManager.Instance.hudController.dialogueUI;
    public GameManager gameManager;

    Parser parser = new Parser();

    private IDictionary eventList;
    public bool inDialogue = false;

    private bool isNSFWon = true;
    public int diaglogueIndex;


    const string DIALOGUEPATH = "Dialogues";

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void StartConversation(string dialogue)
    {
        gameManager.ChangeState(GameManager.GameState.DIALOGUE);
        StartCoroutine(StartDialogue(dialogue));
    }

    IEnumerator StartDialogue(string dialogueEvents)
    {
        dialogueQueue.Clear();
        inDialogue = true;
        dialogueUI.ActivateDialogueHolder(true);
        //Debug.Log(isNSFWon);

        PopulateDialogueList(dialogueEvents);

        currentDialogueNode = dialogueQueue.First;

        while (inDialogue == true)
        {
            //parseEvents(currentDialogueNode.Value);
            JObject diagEvents = currentDialogueNode.Value;
            foreach (var pair in diagEvents)
            {
                string key = pair.Key;
                JToken value = pair.Value;
                //Debug.Log("ENTRY" + entry.Key);
                switch (key)
                {
                    case "lName":
                        dialogueUI.ChangeDialogueBox(false, true);
                        break;
                    case "rName":
                        dialogueUI.ChangeDialogueBox(false, false);
                        break;
                    case "Name":
                        dialogueUI.ChangeDialogueBox(true);
                        break;
                    case "dialogue":
                        dialogueUI.ChangeText((string)value);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && dialogueUI.SkipOrContinue());
                        break;
                    case "narration":
                        dialogueUI.ChangeDialogueBox(true);
                        dialogueUI.ChangeText((string)value);
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && dialogueUI.SkipOrContinue());
                        break;
                    case "image":
                        //Intended for showing a popup of a message or image
                        break;
                    case "choice":
                        dialogueUI.dialogueChoiceHolder.CreateChoices((JObject)value);
                        break;
                    case "end":
                        EndDialogue();
                        break;
                    case "displayOn":
                        string text = (string)value;
                        var spriteLocation = Resources.Load<Sprite>("Dialogues/Sprites/" + text);
                        if (spriteLocation == null)
                        {
                            //Should be image missing screen
                            spriteLocation = Resources.Load<Sprite>("Dialogues/Sprites/Fade");
                        }
                        StartCoroutine(dialogueUI.DisplayImage(spriteLocation));
                        break;
                    case "displayOff":
                        dialogueUI.DisplayOff();
                        break;
                    case "condition": 
                        //Check Condition first then player event or skip event
                        LogicExpression exp = parser.Parse((string)value);

                        exp["nsfwOn"].Set(() => isNSFWon);

                        Debug.Log("IS NSFW ON? " + exp.GetResult() + " And the booolean? " + isNSFWon);
                        break;
                    case "variable":

                    case "jump":
                        //Jump to this event next
                        break;
                }
            }


            if (currentDialogueNode.Next != null)
            {
                currentDialogueNode = currentDialogueNode.Next;
            }
            else
            {
                inDialogue = false;
                gameManager.state = GameManager.GameState.OVERWORLD;
                dialogueUI.ActivateDialogueHolder(false);
                //End Events
            }

            yield return null;
        }
    }

    //public IEnumerator dialogueSpeech(string chosenEvent)
    //{
    //    var dialogueText = Resources.Load<TextAsset>(chosenEvent);
    //    eventList = MiniJSON.jsonDecode(dialogueText.text) as IDictionary;

    //    var introDiag = eventList["diag"] as IList;
    //    dialogueQueue.Clear();

    //    foreach (var x in introDiag)
    //    {
    //        var dict = x as IDictionary;
    //        dialogueQueue.Enqueue(dict);
    //    }

    //    ConditionalCheck(dialogueQueue.Dequeue());

    //    //Find a way to skip having to click for nonparseable events but having them trigger either way

    //    while (true)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            if (dialogueQueue.Count != 0)
    //            {
    //                ConditionalCheck(dialogueQueue.Dequeue());
    //            }
    //            else
    //            {
    //                state = BattleState.BOSSTURN;
    //                inDialogue = false;
    //                dialogueUI.SetActive(false);
    //                //BossTurn();

    //                yield break;
    //            }
    //        }

    //        yield return null;
    //    }
    //}

    //string ChooseEvent()
    //{
    //    //Intro cutscene at the start
    //    if (orcVariables.introDialogue == false)
    //    {
    //        orcVariables.introDialogue = true;
    //        return "Dialogues/Shy Orc Boss Dialogue/Intro1";
    //    }

    //    //AGGRESSIVE ROUTER
    //    if (orcVariables.pervertedRoute == 0 && orcVariables.pacifistRoute == 0)
    //    {
    //        if (orcVariables.bossDirectAttacked && orcVariables.aggressiveRoute == 0)
    //        {
    //            orcVariables.aggressiveRoute = 1;
    //            return "Dialogues/Shy Orc Boss Dialogue/Aggressive1";
    //        }

    //        //if (orcVariables.pervertedRoute == 1)
    //        //{
    //        //    orcVariables.pervertedRoute = 2;
    //        //    return "Dialogues/Shy Orc Boss Dialogue/Aggressive2";
    //        //}
    //    }

    //    //PERVERTED ROUTER
    //    if (orcVariables.pacifistRoute == 0 && orcVariables.aggressiveRoute == 0)
    //    {
    //        //If the player destroyed the boss's pants first
    //        if (orcVariables.destroyedLowerArmor && orcVariables.pervertedRoute == 0)
    //        {
    //            orcVariables.pervertedRoute = 1;
    //            return "Dialogues/Shy Orc Boss Dialogue/Pervert1";
    //        }

    //        //if (orcVariables.pervertedRoute == 1)
    //        //{
    //        //    orcVariables.pervertedRoute = 2;
    //        //    return "Dialogues/Shy Orc Boss Dialogue/Pervert2";
    //        //}

    //        //if (orcVariables.pervertedRoute == 2)
    //        //{
    //        //    orcVariables.pervertedRoute = 3;
    //        //    return "Dialogues/Shy Orc Boss Dialogue/Pervert3";
    //        //}
    //    }

    //    //PACIFIST ROUTER
    //    if (orcVariables.aggressiveRoute == 0 && orcVariables.pervertedRoute == 0)
    //    {
    //        //if (orcVariables.destroyedLowerArmor)
    //        //{
    //        //    orcVariables.pacifistRoute = 1;
    //        //    return "Dialogues/Shy Orc Boss Dialogue/Pacifist1";
    //        //}

    //    }


    //    //ENDINGS
    //    //If the players armor is destroyed but not the boss's armor
    //    if (orcVariables.destroyedAllPlayerArmor && !orcVariables.allArmorDestroyed)
    //    {
    //        Debug.Log("Naked Loss Scene");
    //        return "Dialogues/Shy Orc Boss Dialogue/NakedLoss1";
    //    }

    //    //If the boss's armor set is destroyed but not he players armor
    //    if (!orcVariables.destroyedAllPlayerArmor && orcVariables.allArmorDestroyed)
    //    {
    //        Debug.Log("Naked Win Scene");
    //        return "Dialogues/Shy Orc Boss Dialogue/NakedWin1";
    //    }

    //    //If both armors sets are destroyed
    //    if (orcVariables.allArmorDestroyed && orcVariables.destroyedAllPlayerArmor)
    //    {
    //        Debug.Log("Mutual Naked Scene");
    //        throw new NotImplementedException();
    //        return "Dialogues/Shy Orc Boss Dialogue/MutualNakedWin1";
    //    }


    //    //Skip to the boss turn
    //    return null;
    //}

    //void ConditionalCheck(IDictionary diagEvent)
    //{
    //    foreach (DictionaryEntry entry in diagEvent)
    //    {
    //        var key = entry.Key as string;

    //        if (key == "condition")
    //        {
    //            var conditional = diagEvent["condition"] as string;
    //            LogicExpression exp = parser.Parse(conditional);

    //            VariableAdjustment(exp);

    //            Debug.Log("IS NSFW ON? " + exp.GetResult() + " And the booolean? " + isNSFWon);

    //            if (exp.GetResult() == true)
    //            {
    //                ParseEvents(diagEvent, key);
    //            }
    //            else
    //            {
    //                skipDialogue = true;
    //                dialogueQueue.Dequeue();


    //            }
    //        }

    //        if (!skipDialogue)
    //            ParseEvents(diagEvent, key);
    //    }
    //}

    //void ParseEvents(IDictionary diagEvent, string key)
    //{
    //    var param = diagEvent[key] as string;

    //    switch (key)
    //    {
    //        case "dialogue":
    //            leftAlignedText.text = param;
    //            narrationDialogueBox.SetActive(false);
    //            leftDialogueBox.SetActive(true);
    //            break;
    //        case "narration":
    //            narrationAlignedText.text = param;
    //            narrationDialogueBox.SetActive(true);
    //            leftDialogueBox.SetActive(false);
    //            break;
    //        case "image":
    //            leftImage.sprite = Resources.Load<Sprite>(param);
    //            break;
    //        case "name":
    //            //Display actor name
    //            break;
    //        case "displayOn":
    //            var displaySprite = Resources.Load<Sprite>(param);
    //            if (displaySprite == null)
    //            {
    //                displaySprite = Resources.Load<Sprite>("Sprites/Shy Orc Endings/MissingImage");
    //            }
    //            StartCoroutine(DisplayImage(displaySprite));
    //            break;
    //        case "displayOff":
    //            DisplayOff();
    //            break;
    //        case "ending":
    //            ReturnToMainMenu();
    //            break;
    //        case "conditional":
    //            leftAlignedText.text = param;
    //            break;
    //        case "turn":
    //            switch (param)
    //            {
    //                case "boss":
    //                    //Change to boss state
    //                    return;
    //                case "player":
    //                    //Change to player state
    //                    return;
    //                case "dialogue":
    //                    //Change to dialgoue state
    //                    return;
    //                case "ending":
    //                    state = BattleState.ENDING;
    //                    return;
    //            }
    //            break;
    //        default:
    //            Debug.Log("Skip this one");
    //            dialogueQueue.Dequeue();
    //            return;
    //    }

    //}

    //void VariableAdjustment(LogicExpression xp)
    //{
    //    xp["hasNSFWOn"].Set(() => isNSFWon);
    //}


    //void parseEvents(JObject diagEvent)
    //{
    //    foreach (var pair in diagEvent)
    //    {
    //        string key = pair.Key;
    //        JToken value = pair.Value;
    //        //Debug.Log("ENTRY" + entry.Key);
    //        switch (key)
    //        {
    //            case "dialogue":
    //                // Debug.Log("Dialogue");
    //                dialogueUI.ChangeDialogueBox(false);
    //                dialogueUI.ChangeText((string)value);
    //                break;
    //            case "narration":
    //                //Debug.Log("NARRATION");
    //                dialogueUI.ChangeDialogueBox(true);
    //                dialogueUI.ChangeText((string)value);
    //                break;
    //            case "image":
    //                //Display player image
    //                break;
    //            case "name":
    //                //Display actor name
    //                break;
    //            case "choice":
    //                dialogueUI.dialogueChoiceHolder.CreateChoices((JObject)value);
    //                break;
    //            case "end":
    //                EndDialogue();
    //                break;
    //            case "condition":
    //                //string cond = diagEvent["condition"] as string;
    //                //LogicExpression exp = parser.Parse(cond);

    //                //exp["hasNSFWOn"].Set(() => isNSFWon

    //                //Debug.Log("IS NSFW ON? " + exp.GetResult() + " And the booolean? " + isNSFWon);
    //                //Check Condition first then player event or skip event
    //                break;
    //        }
    //    }
    //}

    void PopulateDialogueList(string eventName)
    {
        var jsonFile = Resources.Load<TextAsset>(DIALOGUEPATH + "/" + eventName);
        if (jsonFile == null)
        {
            Debug.LogError($"The file {eventName} cannot be found");
            return;
        }
        JObject obj = JObject.Parse(jsonFile.text);
        JArray objA = (JArray)obj["diag"];

        foreach (JObject x in objA)
        {
            var dict = x as JObject;

            dialogueQueue.AddLast(dict);
        }

    }

    void EndDialogue()
    {
        inDialogue = false;
    }
}

public class Dialog
{
    public string dialogue;
    public string image;
    public string name;

}

