using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinMotor : MonoBehaviour
{
    private bool isWin;
    private Vector3 winPos;
    [SerializeField] private float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        this.RegisterListener(EventID.Win, param => Win((Vector3)param));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isWin) return;
        
        transform.Translate((winPos - transform.position) * Time.fixedDeltaTime * speed);
        if (Vector3.Magnitude((winPos - transform.position)) < 0.01)
        {
            transform.position = winPos;
            isWin = false;
            GetComponent<BallStack>().WinGame();
        }
    }

    private void Win(Vector3 winMiddle)
    {
        winPos = winMiddle;
        winPos.y = transform.position.y;
        isWin = true;
    }
}
