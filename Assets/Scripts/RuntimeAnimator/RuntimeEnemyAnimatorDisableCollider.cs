using System.Collections;
using System.Collections.Generic;
using ARPG;
using UnityEngine;

public class RuntimeEnemyAnimatorDisableCollider : StateMachineBehaviour
{
    private Enemy enemy;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        enemy = animator.transform.parent.GetComponent<Enemy>();
        if(enemy != null)
            enemy.DamageCollider2D.enabled = false;
    }
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemy != null)
            enemy.DamageCollider2D.enabled = true;
    }
}
