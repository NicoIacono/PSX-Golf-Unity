using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
    public CameraController cam;
    public Transform target;

    public bool usePosition = false;
    public float yPosOffset = 0;

    public bool useRotation = false;

    void Update()
    {
        if (usePosition)
        {
            transform.position = new Vector3(target.position.x, target.position.y+yPosOffset, target.position.z);
        }

        if (useRotation)
        {
            transform.rotation = Quaternion.Euler(0, cam.xRot+90 , 0);
        }
    }
}
