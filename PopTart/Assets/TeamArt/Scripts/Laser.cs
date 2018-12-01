using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : Unit {

    public LineRenderer laserLineRenderer;
    public ParticleSystem particleSystem;
    public Transform spriteTransform;
    public Transform barrelEndTransform;

    void Awake ( ) {
        laserLineRenderer.positionCount = 2;
    }

    public void SetEnd (Vector2 end) {

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

    public override void Turn ( ) { }

    public override void Init (string[ ] args) {
        int dir = int.Parse (args[1]);
        int length = int.Parse (args[2]);

        direction = (Direction) dir;

        switch (direction) {
        case Direction.up:
            SetEnd (Board.Instance.boardToWorld (new Vector2 (tile.position.x, tile.position.y + length)));
            break;
        case Direction.right:
            SetEnd (Board.Instance.boardToWorld (new Vector2 (tile.position.x + length, tile.position.y)));
            break;
        case Direction.down:
            SetEnd (Board.Instance.boardToWorld (new Vector2 (tile.position.x, tile.position.y - length)));
            break;
        case Direction.left:
            SetEnd (Board.Instance.boardToWorld (new Vector2 (tile.position.x - length, tile.position.y)));
            break;
        }
    }
}