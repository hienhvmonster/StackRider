using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Action<object> _actionLose;
    private Action<object> _actionRun;

    private bool isStop = true;
    private bool isAttached = false;

    private Transform sphere;

    // Start is called before the first frame update
    void Start()
    {

        sphere = transform.GetChild(0);

        _actionLose = param => StopBall();
        this.RegisterListener(EventID.OnLose, _actionLose);

        _actionRun = param => StartBall();
        this.RegisterListener(EventID.OnRun, _actionRun);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.OnLose, _actionLose);
        this.RemoveListener(EventID.OnRun, _actionRun);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isAttached) return;
        

        BallStack playerBallStack = collision.transform.GetComponentInParent<BallStack>();

        if (playerBallStack != null) AddBall(playerBallStack);
    }

    private void AddBall(BallStack playerBallStack)
    {
        isAttached = true;
        playerBallStack.AddBall(transform);
        if (playerBallStack.BallCount() > 1)
        {
            VibarationController.HardVibrate();
            GameManager.instance.AddCoin(1);
        }
        else
        {
            GameManager.instance.AddCoin(0);
        }
    }

    private void FixedUpdate()
    {
        if (isStop) return;
        if (!isAttached) return;

        transform.eulerAngles = Vector3.zero;
        sphere.Rotate(new Vector3(90, 0, 0) * Time.fixedDeltaTime);
    }

    private void StartBall()
    {
        isStop = false;
    }

    public void StopBall()
    {
        isStop = true;
    }
}
