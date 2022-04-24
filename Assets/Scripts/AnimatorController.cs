using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void Idle()
    {
        animator.SetInteger("StickManStatus", 0);
    }

    public void Run()
    {
        animator.SetInteger("StickManStatus", 1);
    }

    public void Lose()
    {
        animator.SetInteger("StickManStatus", 0);
    }

    public void Cheer()
    {
        animator.SetInteger("StickManStatus", 3);
    }

    public void Dance()
    {
        animator.SetInteger("StickManStatus", (int)Random.Range(4, 6));
    }
}
