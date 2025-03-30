using System;

public class BattleEvents
{
    public event Action<EnemyData> onEnemyDefeated;

    public void EnemyDefeated(EnemyData enemy)
    {
        if (onEnemyDefeated != null)
        {
            onEnemyDefeated(enemy);
        }
    }
}
