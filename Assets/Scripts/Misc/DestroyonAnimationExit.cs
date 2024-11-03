using UnityEngine;

public class DestroyOnAnimationExit : StateMachineBehaviour
{
    // handles the destruction code when the animation exits
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Destroy the GameObject when the animation state exits
        Destroy(animator.gameObject);
    }
}
