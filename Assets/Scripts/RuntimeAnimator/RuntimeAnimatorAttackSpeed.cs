using ARPG;
using UnityEngine;

public class RuntimeAnimatorAttackSpeed : StateMachineBehaviour
{
    private Character character;
    public StateMode mode;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = GameManager.Instance.Player;
        if (character == null) return;
        if (mode == StateMode.攻击速度)
        {
            
            animator.speed = character.GetState().AttackSpeed+BUFFManager.Instance.GetTyepValue(character,BuffType.增益,mode);
        }
        else
        {
            animator.speed = character.GetState().ReleaseSpeed+BUFFManager.Instance.GetTyepValue(character,BuffType.增益,mode);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (character != null)
            animator.speed = 1;
    }
}
