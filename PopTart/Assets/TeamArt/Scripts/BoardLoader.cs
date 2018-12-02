using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardLoader : MonoBehaviour {

    public int BoardWidth { private set; get; }
    public int BoardHeight { private set; get; }

    public TextAsset boardText;
    public PrefabList prefabList;

    public CommandPanel panel;

    void Awake ( ) {
        GenerateBoard (boardText.text);
    }

    public void GenerateBoard (string text, bool setupPanel = true) {
        PlayerCharacter.userCount = 0;

        Board.Instance.Clear ( );

        string[ ] lines = text.Split ('\n');
        BoardWidth = lines.Length;

        if (setupPanel) {

            string[ ] mapConf = lines[0].Split (' ');

            int dogCount = int.Parse (mapConf[0]);
            int maxTurn = int.Parse (mapConf[1]);
            int up = int.Parse (mapConf[2]);
            int right = int.Parse (mapConf[3]);
            int down = int.Parse (mapConf[4]);
            int left = int.Parse (mapConf[5]);

            panel.Setup (dogCount, maxTurn, left, right, up, down);

        }

        for (int l = 1; l < lines.Length; l++) {
            string[ ] tokens = lines[l].Split (' ');
            BoardHeight = tokens.Length;
            for (int i = 0; i < tokens.Length; i++) {
                string t = tokens[i];

                string[ ] args = tokens[i].Split (',');

                int id = int.Parse (args[0]);

                Unit unit = Generate (id, new Vector2 (i, lines.Length - l - 2));

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