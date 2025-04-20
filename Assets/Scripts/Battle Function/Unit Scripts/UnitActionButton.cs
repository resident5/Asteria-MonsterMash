using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionButton : MonoBehaviour
{
    private Button btn;
    private BattleManager battleManager;
    public UnitActionSO unitAction;

    private void Awake()
    {
        battleManager = BattleManager.Instance;
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        if (GameManager.Instance.debugger.isDebugging)
            Debug.Log("Activate Listener");

        btn.onClick.AddListener(delegate { 
            battleManager.OnPlayerTurn(unitAction); });
    }

}
