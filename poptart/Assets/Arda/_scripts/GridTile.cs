using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour {

    Dictionary<Direction, GridTile> neighbours;

    private Unit unit;

    private bool empty;

    public bool Empty {
        get {
            return empty;
        }
    }

    public void Turn ( ) {
        unit.Turn ( );
    }

    public GridTile GetNeighbour (Direction direction) {
        if (neighbours.ContainsKey (direction))
            return neighbours[direction];

        return null;
    }
}