using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvents : MonoBehaviour
{
    public event Action<int> onMonsterLevelChange;
    public void MonsterLevelChanged(int level)
    {
        if (onMonsterLevelChange != null)
        {
            onMonsterLevelChange(level);
        }
    }

    public event Action<int> onMonsterExperienceGained;
    public void MonsterExperienceGained(int experience)
    {
        if (onMonsterExperienceGained != null)
        {
            onMonsterExperienceGained(experience);
        }
    }

    public event Action<int> onMonsterExperienceChanged;
    public void MonsterExperienceChanged(int experience)
    {
        if (onMonsterExperienceChanged != null)
        {
            onMonsterExperienceChanged(experience);
        }
    }
}
