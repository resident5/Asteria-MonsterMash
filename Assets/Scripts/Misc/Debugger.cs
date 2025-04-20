using B83.LogicExpressionParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    public bool isDebugging;
    Item testItem;

    public GameManager debugGManager;

    public GameObject testEnemy;

    public Sprite sprite;

    private void Start()
    {
        testItem = Resources.Load<Item>("Scriptable Objects/Items/Potion");
        debugGManager = GameManager.Instance;
    }

    private void Update()
    {
        if (isDebugging)
        {
            //NumberExpression exp = GameManager.Instance.variableManager.parser.ParseNumber("npcTalk");

            if (Input.GetKeyDown(KeyCode.Equals))
            {
                PlayerController.Instance.playerData.GainExperience(20);
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                InventoryManager.Instance.AddItem(testItem);
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                PlayerController.Instance.playerData.TakeDamage(20);
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
                var playerPos = debugGManager.playerController.transform.position;

                Vector3 randomPointAroundPlayer = playerPos + Random.insideUnitSphere * 2;
                Vector3 randomFlatPoint = new Vector3(randomPointAroundPlayer.x, playerPos.y, randomPointAroundPlayer.z);

                GameObject mon = Instantiate(testEnemy, randomPointAroundPlayer, Quaternion.identity);
                mon.GetComponent<EnemyData>().SetupEmote(debugGManager.worldCanvas);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                debugGManager.playerController.playerData.GainExperience(debugGManager.playerController.playerData.requiredXP);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                debugGManager.playerController.playerData.battleMons[0].monsterStats.battleStats.LevelUp();
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

    [ContextMenu("Search For Sprite")]
    private void SearchForSprite()
    {
        List<GameObject> matchingObjects = new List<GameObject>();

        GameObject[] rootObjects = GetDontDestroyOnLoadObjects();
        foreach (GameObject rootObject in rootObjects)
        {
            SearchInChildren(rootObject.transform, matchingObjects);
        }

        Debug.Log($"Found {matchingObjects.Count} objects with the specified sprite.");
        foreach (GameObject obj in matchingObjects)
        {
            Debug.Log($"Object: {obj.name}, Scene: {obj.scene.name}");
        }
    }

    private void SearchInChildren(Transform parent, List<GameObject> matchingObjects)
    {
        foreach (Transform child in parent)
        {
            Image image = child.GetComponent<Image>();
            if (image != null && image.sprite == sprite)
            {
                matchingObjects.Add(child.gameObject);
            }

            if (child.childCount > 0)
            {
                SearchInChildren(child, matchingObjects);
            }
        }
    }

    private GameObject[] GetDontDestroyOnLoadObjects()
    {
        GameObject temp = null;
        try
        {
            temp = new GameObject();
            DontDestroyOnLoad(temp);
            Scene dontDestroyOnLoad = temp.scene;
            DestroyImmediate(temp);
            return dontDestroyOnLoad.GetRootGameObjects();
        }
        catch
        {
            if (temp != null)
            {
                DestroyImmediate(temp);
            }
            return new GameObject[0];
        }
    }
}
