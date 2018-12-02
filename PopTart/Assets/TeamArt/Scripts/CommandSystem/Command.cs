using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Command {
    public const int NumberOfDirections = 5;
    public enum Direction {
        None = 0, Up = 1, Right = 2, Down = 3, Left = 4
    }

    Direction[ ] directions;
    public readonly int commandIdx;

    public Command (int commandIdx, Direction[ ] directions) {
        this.directions = directions;
        this.commandIdx = commandIdx;
    }

    public Direction GetDirection (int playerIdx) {
        return directions[playerIdx];
    }

    public override string ToString ( ) {
        string s = "";
        foreach (var direction in directions) {
            s += direction + ",";
        }

        return s;
    }
}