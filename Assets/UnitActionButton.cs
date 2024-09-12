using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionButton : MonoBehaviour
{
    private Button btn;
    private BattleManager battleManager;
    public UnitAction unitAction;

    public enum Category
    {
        FIGHT,
        SUMMON
    }


    private void Awake()
    {
        battleManager = BattleManager.Instance;
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        btn.onClick.AddListener(delegate { battleManager.AttackButton(unitAction); });
    }

}
