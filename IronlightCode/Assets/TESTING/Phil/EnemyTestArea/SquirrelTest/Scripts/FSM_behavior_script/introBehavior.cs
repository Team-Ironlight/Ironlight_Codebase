// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/23/2020
// ----------------------------------------------------------------------------

using UnityEngine;

public class introBehavior : StateMachineBehaviour {

    private int rand;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        rand = Random.Range(0, 2);

        if (rand == 0)
        {
            animator.SetTrigger("idle");
        }
        else {
            animator.SetTrigger("jump");
        }
	}
    
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
    }

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}









































































































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh