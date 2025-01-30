using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugBar : MonoBehaviour
{
    public TMP_Text calculationText;

    public TMP_Text levelText;


    public void SetCalcText(string text)
    {
        calculationText.text = text;
    }

    public void SetLevelText(string text)
    {
        levelText.text = text;
    }
}
