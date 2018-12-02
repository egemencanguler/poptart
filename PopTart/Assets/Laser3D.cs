using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser3D : MonoBehaviour 
{
    public LineRenderer laserLineRenderer;
    public ParticleSystem particleSystem;
    public Transform barrelEndTransform;


    void Awake ( ) 
    {
        laserLineRenderer.positionCount = 2;
        
        SetEnd(transform.position + transform.forward * 10);
    }

    public void SetEnd (Vector2 end) 
    {

        float length = Vector2.Distance (end, transform.position);
        float barrelDistance = Vector3.Distance (barrelEndTransform.position, transform.position);

        var shape = particleSystem.shape;
        shape.scale = new Vector3 (0.1f, length - barrelDistance, 1);
        Vector2 toEnd = end - (Vector2) transform.position;
        particleSystem.transform.localPosition = toEnd / 2 + toEnd.normalized * barrelDistance / 2;

        Quaternion rot = Quaternion.Euler (0, 0, Vector2.SignedAngle (Vector2.up, toEnd.normalized));
        particleSystem.transform.rotation = rot;

        laserLineRenderer.SetPosition (0, barrelEndTransform.transform.position);
        laserLineRenderer.SetPosition (1, end);
    }


    public void UpdateState(Laser laser2D)
    {
        SetEnd(Board2Dto3D.ConvertPosTo3D(laser2D.laserLineRenderer.GetPosition(1)));
    }
}
