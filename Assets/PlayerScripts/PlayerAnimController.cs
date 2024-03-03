using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimController : PlayerAnimation
{
    private static readonly int IsStanding = Animator.StringToHash("IsStanding");
    private static readonly int IsShootingSword = Animator.StringToHash("IsShootingSword");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
    private static readonly int IsWin = Animator.StringToHash("IsWin");

    public void Standing()
    {
        animator.SetBool(IsStanding, true);
    }
    
    public void Move()
    {
        animator.SetBool(IsStanding, false);
        animator.SetBool(IsMoving, true);
    }

    public void ExitMove()
    {
        animator.SetBool(IsMoving, false);
    }

    public void ShootSword()
    {
        animator.SetTrigger(IsShootingSword);
    }

    public void Die()
    {
        animator.SetTrigger(IsDie);
    }

    public void Victory()
    {
        animator.SetTrigger(IsWin);
    }
}
