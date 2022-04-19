using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected bool mouseSwipeUse = false;

    protected Vector2 upPosition;
    protected Vector2 downPosition;


    [SerializeField]
    protected bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    protected float minDistanceForSwipe = 20f;

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            downPosition = mousePos;
            upPosition = mousePos;
        }

        if (!detectSwipeOnlyAfterRelease && Input.GetMouseButton(0))//every swipe
        {
            upPosition = mousePos;
            DetectSwipe();
        }

        if (Input.GetMouseButtonUp(0))
        {
            upPosition = mousePos;
            DetectSwipe();
        }
    }

    protected void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = upPosition.y - downPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = upPosition.x - downPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            downPosition = upPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(upPosition.y - downPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(upPosition.x - downPosition.x);
    }

    protected virtual void SendSwipe(SwipeDirection direction)
    {
        if (!mouseSwipeUse) return;
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = downPosition,
            EndPosition = upPosition
        };

        //Post on swipe event
    }

}


public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}
