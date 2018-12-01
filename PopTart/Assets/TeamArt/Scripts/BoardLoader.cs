using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dalak;

public class BoardLoader : MonoBehaviour
{

    public TextAsset boardText;
    public PrefabList prefabList;
    
    

    void Awake()
    {
        GenerateBoard(boardText.text);
    }

    void GenerateBoard(string text)
    {
        
    }

    void Generate(int id, Vector2 pos)
    {
        if (id == 0)
        {
            Debug.Log("Empty");
            return;
        }

        GameObject prefab = prefabList.GetPrefab(id);
        Instantiate(prefab).transform.position = pos;
        
    }
}
