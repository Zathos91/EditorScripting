using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public List<GameObject> _spawnedObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnedObjects = new List<GameObject>();
    }
    
    
#if UNITY_EDITOR
    public void SpawnNewObject(GameObject newGO, Vector3 center)
    {
        _spawnedObjects.Add(newGO);
        newGO.name = _spawnedObjects.Count.ToString();
        newGO.transform.position = center;
        PrefabUtility.ApplyPrefabInstance(newGO, InteractionMode.UserAction);
    }

    public void ClearSpawnedObjects()
    {
        foreach (var spawnedObject in _spawnedObjects)
        {
            DestroyImmediate(spawnedObject);
        }

        _spawnedObjects = new List<GameObject>();
    }
#endif
        
}
