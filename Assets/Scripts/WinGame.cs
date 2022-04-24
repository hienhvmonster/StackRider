using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    private bool winCheck = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (winCheck) return;
        BallStack playerBallStack = collision.transform.GetComponentInParent<BallStack>();

        if (playerBallStack != null) Win();
        //else Debug.Log("my name: " + name + " & your: " + collision.transform.name);
    }

    private void Win()
    {
        this.PostEvent(EventID.OnWin,transform.position);
        winCheck = true;
        VibarationController.SoftVibrate();
    }
}
