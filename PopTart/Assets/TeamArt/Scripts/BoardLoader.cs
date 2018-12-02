using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardLoader : MonoBehaviour {

    public int BoardWidth { private set; get; }
    public int BoardHeight { private set; get; }
    
    public TextAsset boardText;
    public PrefabList prefabList;

    void Awake ( ) 
    {
        GenerateBoard (boardText.text);
    }

    void GenerateBoard (string text) {
        string[ ] lines = text.Split ('\n');
        BoardWidth = lines.Length;
        for (int l = 0; l < lines.Length; l++)
        {
            string[ ] tokens = lines[l].Split (' ');
            BoardHeight = tokens.Length;
            for (int i = 0; i < tokens.Length; i++) {
                string t = tokens[i];

                string[ ] args = tokens[i].Split (',');

                int id = int.Parse (args[0]);

                Unit unit = Generate (id, new Vector2 (i, lines.Length - l - 1));

                if (unit != null)
                    unit.args = args;
            }
        }

        Board.Instance.Init ( );
    }

    Unit Generate (int id, Vector2 pos) {
        GameObject newObject = null;

        if (id != 0) {
            Debug.Log (id);
            newObject = Instantiate (prefabList.GetPrefab (id));
            newObject.transform.position = Board.Instance.boardToWorld (pos);
        }

        Unit unit = null;

        if (newObject != null)
            unit = newObject.GetComponent<Unit> ( );

        Board.Instance.CreateTile (pos, unit);

        return unit;
    }
}