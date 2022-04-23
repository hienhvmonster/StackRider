using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        BallStack playerBallStack = collision.transform.GetComponentInParent<BallStack>();

        if (playerBallStack != null) Win();
        //else Debug.Log("my name: " + name + " & your: " + collision.transform.name);
    }

    private void Win()
    {
        Debug.Log("win");
        this.PostEvent(EventID.Win,transform.position);
    }
}
