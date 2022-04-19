using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isAttached = false;
    private SphereCollider sphereCollider;
    private Rigidbody rigidb;

    private Transform sphere;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
        rigidb = GetComponent<Rigidbody>();
        rigidb.useGravity = false;

        sphere = transform.GetChild(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isAttached) return;
        

        BallStack playerBallStack = collision.transform.GetComponentInParent<BallStack>();

        if (playerBallStack != null) AddBall(playerBallStack);
        //else Debug.Log("my name: " + name + " & your: " + collision.transform.name);
    }

    private void AddBall(BallStack playerBallStack)
    {
        isAttached = true;
        Destroy(rigidb);
        playerBallStack.AddBall(transform);
    }

    private void FixedUpdate()
    {
        if (!isAttached) return;

        transform.eulerAngles = Vector3.zero;
        sphere.Rotate(new Vector3(90, 0, 0) * Time.fixedDeltaTime);
    }

}
