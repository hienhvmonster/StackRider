using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private BoxCollider boxCollider;
    private bool isFlyUp = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        if (isFlyUp)
        {
            transform.Translate(Vector3.up * Time.fixedDeltaTime);
        }

        transform.Rotate(new Vector3(0, 90, 0) * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        BallStack playerBallStack = collision.transform.GetComponentInParent<BallStack>();

        if (playerBallStack != null)
        {
            AddCoin();
        }
        //else Debug.Log("my name: " + name + " & your: " + collision.transform.name);
    }

    private void AddCoin()
    {
        VibarationController.SoftVibrate();
        boxCollider.isTrigger = true;
        GameManager.instance.AddCoin(1);
        isFlyUp = true;
        Invoke("CoinDisappear", 1f);
    }

    private void CoinDisappear()
    {
        Destroy(gameObject);
    }
}
