using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStack : MonoBehaviour
{
    private Stack<Transform> balls = new Stack<Transform>();
    [SerializeField] Transform lowestPoint;
    private StickManToBall stickManToBall;


    private void Start()
    {
        lowestPoint.localPosition = Vector3.zero;
        stickManToBall = GetComponent<StickManToBall>();
    }

    public void AddBall(Transform ballToAdd)
    {
        Debug.Log("add ball: " + lowestPoint.position.y + " & " + ballToAdd.position.y);
        if (lowestPoint.position.y - ballToAdd.position.y < 0.9) transform.position += Vector3.up;
        lowestPoint.localPosition += Vector3.down;
        ballToAdd.SetParent(transform);
        ballToAdd.localPosition = lowestPoint.localPosition;
        balls.Push(ballToAdd);
        stickManToBall.AddRigidbody();
    }

    public void RemoveBall()
    {
        if (balls.Count == 1)
        {
            //lose
            Debug.Log("lose");

            return;
        }

        Transform ballToRemove = balls.Pop();
        ballToRemove.SetParent(null);
        lowestPoint.localPosition += Vector3.up;
    }

    public int BallCount()
    {
        return balls.Count;
    }
}
