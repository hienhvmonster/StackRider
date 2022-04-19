using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] private Transform followPoint;
    [SerializeField] private Vector3 basicDistance = new Vector3(5, 1.5f, -8);

    public float smoothSpeed = 0.125f;



    private void FixedUpdate()
    {
        //Vector3 moveVector = followPoint.position + basicDistance - transform.position;
        //moveVector.x = 0;
        //transform.position += moveVector;

        Vector3 desiredPos = followPoint.position + basicDistance;
        desiredPos.x = transform.position.x;
        Vector3 newPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

        transform.position = newPos;
    }
}
