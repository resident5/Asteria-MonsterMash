using System;
using System.Collections;
using UnityEngine;

public class PlayerEvents
{
    public event Action<int> onPlayerLevelChange;

    public void PlayerLevelChanged(int level)
    {
        if (onPlayerLevelChange != null)
        {
            onPlayerLevelChange(level);
        }
    }

    public event Action onMovementDisabled;

    public void PlayerMovementDisabled()
    {
        if (onMovementDisabled != null)
        {
            onMovementDisabled();
        }
    }

    public event Action<int> onPlayerExperienceGained;

    public void PlayerExperienceGained(int experience)
    {
        if (onPlayerExperienceGained != null)
        {
            onPlayerExperienceGained(experience);
        }
    }

    public event Action<int> onPlayerExperienceChanged;

    public void PlayerExperienceChanged(int experience)
    {
        if (onPlayerExperienceChanged != null)
        {
            onPlayerExperienceChanged(experience);
        }
    }

    public event Action onPlayerCaptureTarget;

    public void PlayerCapturedTarget()
    {
        if (onPlayerCaptureTarget != null)
        {
            onPlayerCaptureTarget();
        }
    }

}