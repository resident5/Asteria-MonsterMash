using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffects
{
    public void OnTurnStart();
    public void OnHit();
    public void OnTurnUpdate();
    public void OnTurnEnd();
    
}

