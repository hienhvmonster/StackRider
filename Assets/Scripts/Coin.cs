using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        BallStack playerBallStack = collision.transform.GetComponentInParent<BallStack>();

        if (playerBallStack != null) AddCoin();
        //else Debug.Log("my name: " + name + " & your: " + collision.transform.name);
    }

    private void AddCoin()
    {
        GameManager.instance.AddCoin(1);
        Destroy(gameObject);
    }
}
