using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    public void Walk(bool walk)
    {
        anim.SetBool(AnimationTag.WalkParam, walk);
    }
    public void Run(bool run)
    {
        anim.SetBool(AnimationTag.RunParam, run);
    }
    public void Attack()
    {
        anim.SetTrigger(AnimationTag.AttackTrigger);
    }
    public void Dead()
    {
        anim.SetTrigger(AnimationTag.DeadTrigger);
    }
}
