using B83.LogicExpressionParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Debugger : MonoBehaviour
{
    public bool isDebugging;
    Item testItem;

    public GameManager debugGManager;

    public GameObject testEnemy;

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

            if (Input.GetKeyDown(KeyCode.Equals))
            {
                PlayerData.Instance.GainExperience(20);
            }

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

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                var playerPos = debugGManager.playerData.transform.position;

                Vector3 randomPointAroundPlayer = playerPos + Random.insideUnitSphere * 2;
                Vector3 randomFlatPoint = new Vector3(randomPointAroundPlayer.x, playerPos.y, randomPointAroundPlayer.z);

                GameObject mon = Instantiate(testEnemy, randomPointAroundPlayer, Quaternion.identity);
                mon.GetComponent<EnemyData>().SetupEmote(debugGManager.playerData.worldCanvas);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                debugGManager.playerData.GainExperience(debugGManager.playerData.requiredXP);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                debugGManager.playerData.battleMons[0].data.stats.LevelUp();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                int i = debugGManager.hudController.menuStack.Count - 1;
                string list = "";

                foreach (Menu go in debugGManager.hudController.menuStack)
                {
                    list += i-- + " " + go.gameObject.name + "\n";
                }
                Debug.Log(list);
            }
        }
    }
}
