using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(ObjectGenerator))]
public class ObjectGeneratorEditor : Editor
{
    private ObjectGenerator _objectGenerator;
    private bool _isSpawning = false;
    
    private void OnEnable()
    {
        _objectGenerator = target as ObjectGenerator;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Spawn new Object"))
        {
            if (EditorUtility.DisplayDialog("Do you want to spawn a new object?",
                "If you press OK you are going to spawn a new cube", "Ok", "I don't want it"))
            {
                _isSpawning = true;   
                MarkSceneAsDirty();
            }
        }
        
        if (GUILayout.Button("Clear spawned objects"))
        {
            _objectGenerator.ClearSpawnedObjects();
            MarkSceneAsDirty();
            
        }
        
    }

    private void OnSceneGUI()
    {
        if (_isSpawning)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            var center = ray.GetPoint(0);
            
            SceneView.RepaintAll();

            Handles.color = Color.green;
            Handles.DrawWireDisc(center, Vector3.forward,0.5f);
            HandleUtility.AddDefaultControl(0);

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                GameObject newGO = PrefabUtility.InstantiatePrefab(Resources.Load("MyPrefab")) as GameObject;
                _objectGenerator.SpawnNewObject(newGO,center);
                MarkSceneAsDirty();
            }
        }
    }

    
    private void MarkSceneAsDirty()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(activeScene);
    }
   
}