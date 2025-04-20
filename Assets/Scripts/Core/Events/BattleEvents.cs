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

    public event Action<EnemyData> onPlayerSubmitted;

    public void PlayerSubmitted(EnemyData enemy)
    {
        if (onPlayerSubmitted != null)
        {
            onPlayerSubmitted(enemy);
        }
    }

    public event Action<EnemyData> onPlayerDefeated;

    public void PlayerDefeated(EnemyData enemy)
    {
        if (onPlayerDefeated != null)
        {
            onPlayerDefeated(enemy);
        }
    }
}
