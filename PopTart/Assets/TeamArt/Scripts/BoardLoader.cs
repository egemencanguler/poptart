using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        float cellsize = 20;
        string[] lines = text.Split('\n');
        for (int l = 0; l < lines.Length; l++)
        {
            string[] tokens = lines[l].Split(' ');
            for (int i = 0; i < tokens.Length; i++)
            {
                string t = tokens[i];
                int id = int.Parse(tokens[i]);
                float x = cellsize * i + cellsize / 2;
                float y = cellsize * (lines.Length - l - 1) + cellsize / 2;
                if (int.Parse(tokens[i]) == 0)
                {
                    Debug.Log("Bosluk" + x + "," + y);
                }

                if (int.Parse(tokens[i]) == 1)
                {
                    Debug.Log("Köpek" + x + "," + y);
                }
                if (int.Parse(tokens[i]) == 2)
                {
                    Debug.Log("Duvar" + x + "," + y);
                }
            }

        }
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
