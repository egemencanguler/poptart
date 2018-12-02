using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCamera : MonoBehaviour
{
    Vector3 cam2DPos;
    float cam2DSize;

    Vector3 cam3DPos = new Vector3(37, 38, 24);
    Vector3 camRot = new Vector3(49, -27,0);

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
        
        cam2DSize = boardWidth * 1.5f * 0.5f * cellSize;
        cam2DPos = pos;
        


    }

    bool camera2D = true;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            camera2D = !camera2D;
            if (camera2D)
            {
                transform.position = cam2DPos;
                transform.rotation = Quaternion.Euler(Vector3.zero);
                GetComponent<Camera>().orthographicSize = cam2DSize;
                GetComponent<Camera>().orthographic = true;
            }
            else
            {
                transform.position = cam3DPos;
                GetComponent<Camera>().orthographic = false;
                transform.rotation = Quaternion.Euler(camRot);

            }
            
            
        }
    }
}
