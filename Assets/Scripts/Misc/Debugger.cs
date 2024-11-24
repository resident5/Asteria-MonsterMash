using B83.LogicExpressionParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public bool isDebugging;
    Item testItem;

    public GameManager debugGManager;

    private void Start()
    {
        testItem = Resources.Load<Item>("Scriptable Objects/Items/Potion");
        debugGManager = GameManager.Instance;
    }

    private void Update()
    {
        if (isDebugging)
        {
            NumberExpression exp = GameManager.Instance.variableManager.parser.ParseNumber("npcTalk");

            if (Input.GetKeyDown(KeyCode.F1))
            {
                InventoryManager.Instance.AddItem(testItem);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                exp["npcTalk"].Set(exp.GetNumber() + 1);
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                PlayerData.Instance.TakeDamage(20);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                debugGManager.cameraManager.SwapToOverWorldCam();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                debugGManager.cameraManager.SwapToBattleCam();
            }
        }
    }
}
