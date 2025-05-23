using System;
using UnityEngine;

[Serializable]
public class EnemyData : UnitData
{
    public BattleUnit[] enemyUnit;

    [Header("Unit Info")]
    public UnitCreatorSO unitInfo;

    public float awareness;
    public float awarenessRate = 0.5f;

    public int availableExp = 50;

    [Header("Rewards")]
    public Item itemReward;

    [Header("Dialogue")]
    public string dialogueScript;
    
    [Header("Stats")]
    public EnemyStats enemyStats;


    public void Init()
    {
        enemyStats = new EnemyStats(unitInfo);
    }

    public override void TakeDamage(int damage)
    {

    }

    public void SetItemReward(Item reward = null)
    {
        itemReward = reward;
    }

    public override void GainFlatExperience(int exp)
    {
        throw new NotImplementedException();
    }

    public override void GainExperience(int exp)
    {
        throw new NotImplementedException();
    }

    public override void LevelUp()
    {
        throw new NotImplementedException();
    }
}
