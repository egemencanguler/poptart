using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

    private Vector2 position;

    private Unit unit;

    public Unit Unit {
        get {
            return unit;
        }

        set {
            this.unit = value;
        }
    }

    private bool empty;

    public bool Empty {
        get {
            return empty;
        }
    }

    public bool isMovable ( ) {
        return true;
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
}