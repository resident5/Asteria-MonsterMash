using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public PlayerEvents playerEvents;
    public MiscEvents miscEvents;
    public QuestEvents questEvents;
    public MonsterEvents monsterEvents;
    public BattleEvents battleEvents;
    public OverworldEvents overworldEvents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        playerEvents = new PlayerEvents();
        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        monsterEvents = new MonsterEvents();
        battleEvents = new BattleEvents();
        overworldEvents = new OverworldEvents();
    }
}
