using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinMotor : MonoBehaviour
{
    private AnimatorController anim;
    private Action<object> _actionWin;

    private bool isWin;
    private Vector3 winPos;
    [SerializeField] private float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        _actionWin = param => Win((Vector3)param);
        this.RegisterListener(EventID.OnWin, _actionWin);
        anim = GetComponent<AnimatorController>();
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.OnWin, _actionWin);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isWin) return;
        
        transform.Translate((winPos - transform.position) * Time.fixedDeltaTime * speed);
        if (Vector3.Magnitude((winPos - transform.position)) < 0.05)
        {
            transform.position = winPos;
            isWin = false;
            GetComponent<BallStack>().WinGame();
            anim.Dance();
        }
    }

    private void Win(Vector3 winMiddle)
    {
        winPos = winMiddle;
        winPos.y = transform.position.y;
        isWin = true;
        anim.Cheer();
    }
}
