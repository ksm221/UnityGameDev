using UnityEngine;

public class AirborneState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Jyrto bossScript = animator.GetComponent<Jyrto>();
        if (bossScript != null)
        {
            bossScript.JumpDown();
        }
    }
}
    