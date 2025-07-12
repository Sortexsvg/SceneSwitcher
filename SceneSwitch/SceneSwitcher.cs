using System.Collections.Generic;
using System;
using UnityEditor.SceneManagement;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace DevHelper.SceneSwitcher
{
    public class SceneSwitcher : EditorWindow 
    {
        private string[] scenePaths;
        private string[] sceneNames;
        private int selectedSceneIndex = 0;

        [MenuItem("Tools/Scene Switcher")]
        public static void ShowWindow()
        {
            GetWindow<SceneSwitcher>("Scene Switcher");
        }

        private void OnEnable()
        {
            scenePaths = AssetDatabase.FindAssets("t:Scene",new string[] { "Assets" });
            if(scenePaths!=null)
            {
                sceneNames = new string[scenePaths.Length];
                for (int i = 0; i < scenePaths.Length; i++)
                {
                    scenePaths[i] = AssetDatabase.GUIDToAssetPath(scenePaths[i]);
                    sceneNames[i] = Path.GetFileNameWithoutExtension(scenePaths[i]);
                }
            }
        }

        private void OnGUI()
        {
            if (sceneNames.Length == 0)
            {
                EditorGUILayout.LabelField("No scenes in Project");
                return;
            }

            EditorGUILayout.LabelField("Select Scene:", EditorStyles.boldLabel);
            selectedSceneIndex = EditorGUILayout.Popup(selectedSceneIndex, sceneNames);

            if (GUILayout.Button("Switch Scene"))
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(scenePaths[selectedSceneIndex]);
                    Close();
                }
            }
        }
    }
}
