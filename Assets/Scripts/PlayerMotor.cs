using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Action<object> _actionWin;
    private Action<object> _actionLose;
    private Action<object> _actionRun;

    private bool isStop = true;

    [SerializeField] private float maxDistanceRoad = 1.5f;
    private Rigidbody rigidb;
    [SerializeField] private Vector3 moveForward = new Vector3(0, 0, 1);
    private Vector3 moveSide = Vector3.zero;

    [SerializeField] private float sideMaxSpeed = 40f;

    private void Start()
    {
        rigidb = GetComponent<Rigidbody>();

        _actionWin = param => StopPlayer();
        this.RegisterListener(EventID.OnWin, _actionWin);

        _actionLose = param => StopPlayer();
        this.RegisterListener(EventID.OnLose, _actionLose);

        _actionRun = param => StartPlayer();
        this.RegisterListener(EventID.OnRun, _actionRun);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.OnWin, _actionWin);
        this.RemoveListener(EventID.OnLose, _actionLose);
        this.RemoveListener(EventID.OnRun, _actionRun);
    }

    private void FixedUpdate()
    {
        if (isStop) return;
        //rigidb.MovePosition((transform.position + moveForward + moveSide) * Time.fixedDeltaTime);
        //rigidb.AddForce((moveForward + moveSide) * Time.fixedDeltaTime, ForceMode.VelocityChange);
        transform.Translate((moveForward + moveSide) * Time.fixedDeltaTime);

        SetInRoad();

        moveSide.x = 0;
    }

    private void SetInRoad()
    {
        if (transform.position.x > maxDistanceRoad)
        {
            transform.position = new Vector3(maxDistanceRoad, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -maxDistanceRoad)
        {
            transform.position = new Vector3(-maxDistanceRoad, transform.position.y, transform.position.z);
        }
    }

    public void SetMoveSide(Vector3 moveSet)
    {
        if ((transform.position.x >= maxDistanceRoad && moveSet.x > 0) || (transform.position.x <= -maxDistanceRoad && moveSet.x < 0)) return;

        if (Mathf.Abs(moveSet.x) > sideMaxSpeed) moveSide.x = moveSet.normalized.x * sideMaxSpeed;
        else moveSide.x = moveSet.x;
    }

    private void StopPlayer()
    {
        isStop = true;
    }

    public void StartPlayer()
    {
        isStop = false;
        GetComponent<AnimatorController>().Run();
    }
}
