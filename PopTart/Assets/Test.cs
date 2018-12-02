using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour 
{

    const float MovementDistance = 1;

    bool move = false;

    Vector2 movementDir;
    
    Vector2 startPos;


    void Move(Vector2 dir)
    {
        Debug.Log("Move");
        Vector2 pos = transform.position;
        Vector2 newPos = pos + Time.deltaTime * dir * 5;

        float distance = Vector2.Distance(startPos, newPos);
        if (distance >= MovementDistance)
        {
            move = false;
            transform.position = startPos + dir * MovementDistance;
        }
        else{
            transform.position = newPos;
        }
 
    }




    // Update is called once per frame
    void Update () 
    {
        if(!move)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                move = true;
                startPos = transform.position;
                movementDir = Vector2.up;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                move = true;
                startPos = transform.position;
                movementDir = Vector2.down;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                move = true;
                startPos = transform.position;
                movementDir = Vector2.left;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                move = true;
                startPos = transform.position;
                movementDir = Vector2.right;
            }
        }else
        {
            Move(movementDir);
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPos, 0.5f);
    }

}
