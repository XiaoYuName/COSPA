using ARPG;
using UnityEngine;

public class RuntimeAnimatorAttackSpeed : StateMachineBehaviour
{
    private Character character;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = GameManager.Instance.Player;
        if (character != null)
            animator.speed = character.GetState().AttackSpeed;

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character != null)
            animator.speed = 1;
    }
}
