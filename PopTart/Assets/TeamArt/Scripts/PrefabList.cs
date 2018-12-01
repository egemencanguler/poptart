using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PrefabList",menuName = "PopTart/PrefabList")]
public class PrefabList : ScriptableObject
{
    [SerializeField] GameObject[] prefabs;

    public enum Type
    {
        OurDog = 1,
        Wall = 2
    }
    
    
    public GameObject GetPrefab(int prefabIdx)
    {
        return prefabs[(int) prefabIdx];
    }
    
    public GameObject GetPrefab(Type type)
    {
        return prefabs[(int) type];
    }

}
