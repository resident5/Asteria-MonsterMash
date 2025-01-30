using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugLevelChecker : MonoBehaviour
{
    [Range(1, 500)]
    public int maxLevels = 100;

    public float value1, value2, value3;

    public GameObject barPrefab;

    public Stack<GameObject> allBarsStack;

    public RectTransform panelRect;
    //public List<GameObject> allBars;

    public void Start()
    {
        allBarsStack = new Stack<GameObject>();
        CreateBarGraph();
    }

    private void Update()
    {
        if (allBarsStack.Count + 1 != maxLevels)
        {
            UpdateBarGraph();
        }
    }

    void CreateBarGraph()
    {
        for (int i = 1; i < maxLevels; i++)
        {
            AddBar(i);
        }
    }

    void IncreaseBarSize(DebugBar bar, int level)
    {
        RectTransform rect = bar.GetComponent<RectTransform>();
        int projectedExp = ExpCalculation(level);

        //float scaledValue = Mathf.Lerp(1, 450, Mathf.InverseLerp(1, 15000000, projectedLevel));

        Debug.Log("Projected level = " + projectedExp);
        float scaledValueDelta = (panelRect.sizeDelta.y * projectedExp) / ExpCalculation(maxLevels);

        Debug.Log("Scaled Value = " + scaledValueDelta);
        rect.sizeDelta = new Vector2(panelRect.sizeDelta.x / maxLevels - 10f, scaledValueDelta);
    }

    void UpdateBarGraph()
    {
        if (maxLevels < allBarsStack.Count + 1)
        {
            int difference = allBarsStack.Count - maxLevels;

            for (int i = 0; i < difference; i++)
            {
                RemoveBar();
            }
        }

        if (maxLevels > allBarsStack.Count + 1)
        {
            int difference = maxLevels - allBarsStack.Count;
            for (int i = allBarsStack.Count + 1; i < allBarsStack.Count + difference; i++)
            {
                AddBar(i);
            }
        }
    }

    void AddBar(int level)
    {
        GameObject obj = Instantiate(barPrefab, transform);
        DebugBar bar = obj.GetComponent<DebugBar>();

        int calculatedExp = ExpCalculation(level);
        bar.SetCalcText("" + calculatedExp);
        bar.SetLevelText("" + level);
        IncreaseBarSize(bar, level);
        allBarsStack.Push(obj);

    }

    void RemoveBar()
    {
        GameObject lastBar = allBarsStack.Pop();
        Destroy(lastBar);
    }

    int ExpCalculation(int testLevel)
    {
        int solverXp = 0;
        int level = testLevel;

        for (int i = 0; i < level; i++)
        {
            solverXp += (int)Mathf.Floor(level + value1 * Mathf.Pow(value2, level / value3));
        }

        return solverXp / 4;

    }
}

