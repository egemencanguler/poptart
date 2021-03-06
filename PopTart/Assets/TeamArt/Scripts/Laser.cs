﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : Unit {

    [SerializeField]
    private GameObject PrefabLaserPart;

    public LineRenderer laserLineRenderer;
    public ParticleSystem particleSystem;
    public Transform spriteTransform;
    public Transform barrelEndTransform;

    private ArrayList parts = new ArrayList ( );

    private int process = 0;

    void Awake ( ) {
        laserLineRenderer.positionCount = 2;
    }

    public void SetEnd (Vector2 end) {
        foreach (LaserPart part in parts) {
            part.tile.Unit = null;
            Destroy (part.gameObject);
        }

        parts.Clear ( );

        float length = Vector2.Distance (end, transform.position);
        float barrelDistance = Vector3.Distance (barrelEndTransform.position, transform.position);

        var shape = particleSystem.shape;
        shape.scale = new Vector3 (0.1f, length - barrelDistance, 1);
        Vector2 toEnd = end - (Vector2) transform.position;
        particleSystem.transform.localPosition = toEnd / 2 + toEnd.normalized * barrelDistance / 2;

        Quaternion rot = Quaternion.Euler (0, 0, Vector2.SignedAngle (Vector2.up, toEnd.normalized));
        spriteTransform.transform.rotation = rot;
        particleSystem.transform.rotation = rot;

        laserLineRenderer.SetPosition (0, barrelEndTransform.transform.position);
        laserLineRenderer.SetPosition (1, end);
    }

    public override void Turn ( ) {
        if (int.Parse (args[3]) != 0) {

            ++process;

            if (process % int.Parse (args[3]) == int.Parse (args[4])) {
                SetEnd (Board.Instance.boardToWorld (tile.position));

                if ((process / int.Parse (args[3])) % 2 != 0)
                    process = process + int.Parse (args[3]) - 1;

            } else {
                Init (args);
            }
        }
    }

    public override void Init (string[ ] args) {
        int dir = int.Parse (args[1]);
        int length = int.Parse (args[2]);

        direction = (Direction) dir;

        if (int.Parse (args[3]) != 0) {
            if (process % int.Parse (args[3]) == int.Parse (args[4])) {
                SetEnd (Board.Instance.boardToWorld (tile.position));
                return;
            }
        }

        GridTile temp;
        LaserPart laserPart;

        switch (direction) {
        case Direction.up:
            SetEnd (Board.Instance.boardToWorld (new Vector2 (tile.position.x, tile.position.y + length)));

            for (int i = 1; i <= length; ++i) {
                Vector2 pos = new Vector2 (tile.position.x, tile.position.y + i);

                laserPart = Instantiate (PrefabLaserPart, Board.Instance.boardToWorld (pos), Quaternion.identity).GetComponent<LaserPart> ( );

                temp = Board.Instance.GetTile (pos);
                if (temp != null) {

                    if (temp.Unit != null && temp.Unit is PlayerCharacter)
                        ((PlayerCharacter) temp.Unit).Die ( );

                    temp.Unit = laserPart;
                    laserPart.tile = temp;
                    laserPart.laser = this;

                    parts.Add (laserPart);
                } else {
                    Destroy (temp);
                }
            }

            break;
        case Direction.right:
            SetEnd (Board.Instance.boardToWorld (new Vector2 (tile.position.x + length, tile.position.y)));

            for (int i = 1; i <= length; ++i) {
                Vector2 pos = new Vector2 (tile.position.x + i, tile.position.y);

                laserPart = Instantiate (PrefabLaserPart, Board.Instance.boardToWorld (pos), Quaternion.identity).GetComponent<LaserPart> ( );

                temp = Board.Instance.GetTile (pos);
                if (temp != null) {

                    if (temp.Unit != null && temp.Unit is PlayerCharacter)
                        ((PlayerCharacter) temp.Unit).Die ( );

                    temp.Unit = laserPart;
                    laserPart.tile = temp;
                    laserPart.laser = this;

                    parts.Add (laserPart);
                } else {
                    Destroy (temp);
                }
            }

            break;
        case Direction.down:
            SetEnd (Board.Instance.boardToWorld (new Vector2 (tile.position.x, tile.position.y - length)));

            for (int i = 1; i <= length; ++i) {
                Vector2 pos = new Vector2 (tile.position.x, tile.position.y - i);

                laserPart = Instantiate (PrefabLaserPart, Board.Instance.boardToWorld (pos), Quaternion.identity).GetComponent<LaserPart> ( );

                temp = Board.Instance.GetTile (pos);

                if (temp != null) {

                    if (temp.Unit != null && temp.Unit is PlayerCharacter)
                        ((PlayerCharacter) temp.Unit).Die ( );

                    temp.Unit = laserPart;
                    laserPart.tile = temp;
                    laserPart.laser = this;

                    parts.Add (laserPart);

                } else {
                    Destroy (temp);
                }
            }

            break;
        case Direction.left:
            SetEnd (Board.Instance.boardToWorld (new Vector2 (tile.position.x - length, tile.position.y)));

            for (int i = 1; i <= length; ++i) {
                Vector2 pos = new Vector2 (tile.position.x - i, tile.position.y);

                laserPart = Instantiate (PrefabLaserPart, Board.Instance.boardToWorld (pos), Quaternion.identity).GetComponent<LaserPart> ( );

                temp = Board.Instance.GetTile (pos);
                if (temp != null) {

                    if (temp.Unit != null && temp.Unit is PlayerCharacter)
                        ((PlayerCharacter) temp.Unit).Die ( );

                    temp = Board.Instance.GetTile (pos);
                    temp.Unit = laserPart;
                    laserPart.tile = temp;
                    laserPart.laser = this;

                    parts.Add (laserPart);
                } else {
                    Destroy (temp);
                }
            }

            break;
        }
    }
}