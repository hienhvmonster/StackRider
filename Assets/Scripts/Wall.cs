using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private bool isAbleToStop = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isAbleToStop) return;
        BallStack playerBallStack = collision.transform.GetComponentInParent<BallStack>();

        if (playerBallStack != null) StopBall(playerBallStack);
    }

    private void StopBall(BallStack playerBallStack)
    {
        isAbleToStop = false;
        VibarationController.HardVibrate();
        playerBallStack.RemoveBall();
    }
}
