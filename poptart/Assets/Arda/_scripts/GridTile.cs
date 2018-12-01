using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

    public Vector2 position;

    private Unit unit;

    public Unit Unit {
        get {
            return unit;
        }

        set {
            this.unit = value;
        }
    }

    public bool Empty {
        get {
            return unit == null;
        }
    }

    public bool isMovable ( ) {
        return Empty;
    }

    public void Turn ( ) {
        unit.Turn ( );
    }

    public Vector2 GetNeighbourPosition (Direction direction) {
        switch (direction) {
        case Direction.up:
            return new Vector2 (position.x, position.y + 1);
        case Direction.right:
            return new Vector2 (position.x + 1, position.y);
        case Direction.down:
            return new Vector2 (position.x, position.y - 1);
        case Direction.left:
            return new Vector2 (position.x - 1, position.y);
        }

        return new Vector2 (-1, -1);
    }

    public void SetPosition (Vector2 position) {
        this.position = position;
        this.transform.position = Board.Instance.boardToWorld (position);
    }
}