using System;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(Teleporter))]
public class TeleporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Teleporter teleporter = (Teleporter)target;

        var scenes = EditorBuildSettings.scenes;
        string[] sceneNames = new string[scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            sceneNames[i] = scenes[i].path.Trim();
        }

        string[] truncatedStringList = GetTruncatedSceneNames(sceneNames);

        int currentIndex = Array.IndexOf(truncatedStringList, teleporter.nextSceneName);
        currentIndex = EditorGUILayout.Popup("Next Scene", currentIndex, truncatedStringList);

        if (currentIndex > 1)
            teleporter.nextSceneName = truncatedStringList[currentIndex];

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

    private string[] GetTruncatedSceneNames(string[] sceneNames)
    {
        string[] truncatedSceneNames = new string[sceneNames.Length];

        for (int i = 0; i < sceneNames.Length; i++)
        {
            string stringName = sceneNames[i];
            stringName = stringName.Substring(stringName.LastIndexOf('/') + 1);
            stringName = stringName.Substring(0, stringName.IndexOf('.'));
            truncatedSceneNames[i] = stringName;
        }
        return truncatedSceneNames;
    }
}

