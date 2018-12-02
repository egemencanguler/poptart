using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCamera : MonoBehaviour 
{

    void Start()
    {
        int boardWidth = Board.Instance.GetComponent<BoardLoader>().BoardWidth;
        int boardHeight = Board.Instance.GetComponent<BoardLoader>().BoardHeight;
        Debug.Log("Board: " + boardWidth + "," + boardHeight);
        float cellSize = Board.Instance.cellsize;

        float posX = boardWidth * cellSize / 2;
        float posY = boardHeight * cellSize / 2;
        Vector3 pos = transform.position;
        pos.x = posX + boardWidth * 0.25f * cellSize;
        pos.y = posY;
        transform.position = pos;
        
        GetComponent<Camera>().orthographicSize = boardWidth * 1.5f * 0.5f * cellSize;

    }
}
