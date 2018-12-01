using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Command
{
    public const int NumberOfDirections = 5;
    public enum Direction
    {
        None = 0, Left = 1, Right = 2, Up = 3, Down = 4
    }


    Direction[] directions;

    public Command(Direction[] directions)
    {
        this.directions = directions;
    }

    public Direction GetDirection(int playerIdx)
    {
        return directions[playerIdx];
    }


    public override string ToString()
    {
        string s = "";
        foreach (var direction in directions)
        {
            s += direction + ",";
        }

        return s;
    }
}
