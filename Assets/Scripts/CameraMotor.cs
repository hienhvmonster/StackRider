using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] private Transform followPoint;
    private Vector3 basicDistance;

    public float smoothSpeed = 0.125f;

    private bool isRotateMode = false;
    private Vector3 rotateAroundPoint;

    private void Start()
    {
        basicDistance = transform.position - followPoint.position;
        this.RegisterListener(EventID.Win, param => WinGame((Vector3)param));
    }

    private void FixedUpdate()
    {
        //Vector3 moveVector = followPoint.position + basicDistance - transform.position;
        //moveVector.x = 0;
        //transform.position += moveVector;

        if (!isRotateMode)
        {
            Vector3 desiredPos = followPoint.position + basicDistance;
            desiredPos.x = transform.position.x;
            Vector3 newPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

            transform.position = newPos;
        }
        else
        {
            transform.RotateAround(rotateAroundPoint, Vector3.up, 45f * Time.fixedDeltaTime);
        }
    }

    private void WinGame(Vector3 winCenter)
    {
        rotateAroundPoint = winCenter;
        isRotateMode = true;
    }
}
