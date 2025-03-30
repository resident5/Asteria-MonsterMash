using System;
using System.Collections;
using UnityEngine;

public class PlayerEvents
{
    //Called whenever the player's level changes
    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChanged(int level)
    {
        if (onPlayerLevelChange != null)
        {
            onPlayerLevelChange(level);
        }
    }

    //Called whenever the player's movement is disabled
    public event Action onMovementDisabled;
    public void PlayerMovementDisabled()
    {
        if (onMovementDisabled != null)
        {
            onMovementDisabled();
        }
    }

    //Called whenever the player gains experience
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