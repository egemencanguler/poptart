using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board2Dto3D : MonoBehaviour
{
    public static readonly Vector3 Offset = new Vector3(30,30,30);
    
    Dictionary<int,Transform> unitToObjectMap = new Dictionary<int, Transform>();
    Dictionary<int,Unit> objectToUnit = new Dictionary<int, Unit>();
    List<Unit> units = new List<Unit>();
    List<GameObject> objects = new List<GameObject>();
    
    
    public GameObject[] prefabs;
    GameObject container = null;

    GameObject GetPrefab(UnitType type)
    {
        return prefabs[(int) type];
    }

    IEnumerator Start()
    {
        yield return null;
        CreateObjects();
    }


    void CreateObjects()
    {
        if (container != null)
        {
            Destroy(container);
        }
        
        Board board = Board.Instance;
        foreach (var tile in board.grid.Values)
        {
            UnitType type = GetUnitType(tile);
            if (type != UnitType.Empty)
            {
                Transform t = Instantiate(GetPrefab(type)).transform;
                t.position = ConvertPosTo3D(tile.position);
                
                unitToObjectMap.Add(tile.Unit.GetInstanceID(),t);
                objectToUnit.Add(t.gameObject.GetInstanceID(),tile.Unit);
                units.Add(tile.Unit);
                objects.Add(t.gameObject);
            }
        }
    }

    Vector3 ConvertPosTo3D(Vector2 pos)
    {
        return Offset + new Vector3(pos.x,0,pos.y) / Board.Instance.cellsize;
    }


    void Update()
    {
        
        foreach (var unit in units)
        {
            if (unit != null)
            {
                Transform t = unitToObjectMap[unit.GetInstanceID()];
                t.transform.position = ConvertPosTo3D(unit.transform.position);
            }
        }

        for (int i = objects.Count - 1; i >= 0; i--)
        {
            var o = objects[i];
            if (objectToUnit[o.GetInstanceID()] == null)
            {
                Destroy(o);
                objects.RemoveAt(i);
            }
        }
        
    }

    UnitType GetUnitType(GridTile tile)
    {
        if (tile == null) return UnitType.Empty;
        Unit u = tile.Unit;
        if (u is PlayerCharacter)
        {
            return UnitType.Player;
        }

        if (u is BadDog)
        {
            return UnitType.BadDog;
        }

        if (u is Finish)
        {
            return UnitType.Finish;
        }

        if (u is Laser)
        {
            return UnitType.Laser;
        }

        if (u is DummyUnit)
        {
            return UnitType.Wall;
        }

        return UnitType.Empty;
    }
}

public enum UnitType
{
    Player = 0, BadDog = 1, Finish = 2, Laser = 3, Wall = 4, Empty = 5
    
}

