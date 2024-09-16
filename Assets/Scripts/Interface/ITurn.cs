using System.Collections;
using UnityEngine;

public interface ITurn
{
    public void OnTurnStart();
    public void OnTurnEnd();
    public void OnTurnUpdate();
}
