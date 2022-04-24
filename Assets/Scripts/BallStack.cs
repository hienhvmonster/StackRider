using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStack : MonoBehaviour
{
    private Stack<Transform> balls = new Stack<Transform>();
    [SerializeField] Transform lowestPoint;
    [SerializeField] Transform ballTrashCan;

    private bool isWin = false;
    private int coinWin = 5;

    [SerializeField] private float ballDestroyDuration = 0.8f;
    private float prevBallDestroyTime = 0f;


    private void Start()
    {
        lowestPoint.localPosition = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (isWin)
        {
            if (balls.Count > 0)
            {
                if (Time.time - prevBallDestroyTime >= ballDestroyDuration)
                {
                    VibarationController.HardVibrate();
                    Transform ballToRemove = RemoveBall();
                    ballToRemove.gameObject.SetActive(false);
                    GameManager.instance.AddCoin(coinWin);
                    coinWin += 5;
                    prevBallDestroyTime = Time.time;
                }
            }
            else
            {
                this.PostEvent(EventID.OnDoneGame);
                isWin = false;
            }
        }
    }

    public void AddBall(Transform ballToAdd)
    {
        //Debug.Log("add ball: " + lowestPoint.position.y + " & " + ballToAdd.position.y);
        if (lowestPoint.position.y - ballToAdd.position.y < 0.9) transform.position += Vector3.up;
        lowestPoint.localPosition += Vector3.down;
        ballToAdd.SetParent(transform);
        ballToAdd.localPosition = new Vector3(lowestPoint.localPosition.x, lowestPoint.localPosition.y, lowestPoint.localPosition.z);
        balls.Push(ballToAdd);
    }

    public Transform RemoveBall()
    {
        if (balls.Count == 1 && !isWin)
        {
            //lose
            this.PostEvent(EventID.OnLose);
            GetComponent<AnimatorController>().Lose();
            return null;
        }

        Transform ballToRemove = balls.Pop();
        ballToRemove.SetParent(ballTrashCan);
        lowestPoint.localPosition += Vector3.up;
        ballToRemove.GetComponent<Ball>().StopBall();

        return ballToRemove;
    }

    public int BallCount()
    {
        return balls.Count;
    }

    public void WinGame()
    {
        isWin = true;
    }
}
