using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private PlayerMotor playerMotor;

    // Start is called before the first frame update
    void Start()
    {
        mouseSwipeUse = true;
        detectSwipeOnlyAfterRelease = false;
        playerMotor = GetComponent<PlayerMotor>();
    }

    protected override void SendSwipe(SwipeDirection direction)
    {
        if (!mouseSwipeUse) return;
        playerMotor.SetMoveSide(upPosition - downPosition);
    }
}
