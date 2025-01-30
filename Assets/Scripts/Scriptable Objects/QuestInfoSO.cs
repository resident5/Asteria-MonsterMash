using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Quest Info SO", menuName = "Terre Tools/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] 
    public string id { get; private set; }

    [Header("General")]
    
    public string displayName;

    [Header("Requirements")]

    public int levelRequirement;
    public QuestInfoSO[] questPrerequisities;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;
    
    [Header("Rewards")]

    public int goldRewards;
    public int experienceReward;


    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        EditorUtility.SetDirty(this);
#endif
    }
}
