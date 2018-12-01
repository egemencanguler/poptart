using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardLoader : MonoBehaviour {

    public TextAsset boardText;
    public PrefabList prefabList;

    void Awake ( ) {
        GenerateBoard (boardText.text);
    }

    void GenerateBoard (string text) {
        string[ ] lines = text.Split ('\n');
        for (int l = 0; l < lines.Length; l++) {
            string[ ] tokens = lines[l].Split (' ');
            for (int i = 0; i < tokens.Length; i++) {
                string t = tokens[i];
                int id = int.Parse (tokens[i]);
                Generate (int.Parse (tokens[i]), new Vector2 (i, lines.Length - l - 1));
            }
        }
    }

    void Generate (int id, Vector2 pos) {
        GameObject newObject = null;

        if (id != 0) {
            newObject = Instantiate (prefabList.GetPrefab (id));
            newObject.transform.position = Board.Instance.boardToWorld (pos);
        }

        if (newObject != null)
            Board.Instance.CreateTile (pos, newObject.GetComponent<Unit> ( ));
        else
            Board.Instance.CreateTile (pos);
    }
}