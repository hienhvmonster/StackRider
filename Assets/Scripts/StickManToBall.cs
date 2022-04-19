using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManToBall : MonoBehaviour
{
    private Rigidbody rigidb = null;

    private void Start()
    {
        rigidb = GetComponent<Rigidbody>();
    }

    public void AddRigidbody()
    {
        if (rigidb != null) return;
        rigidb = gameObject.AddComponent<Rigidbody>();
        rigidb.useGravity = true;
    }

    private void FixedUpdate()
    {
        transform.eulerAngles = Vector3.zero;
    }
}
