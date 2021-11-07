using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationTimeOut : StateMachineBehaviour
{
    [SerializeField] private int times = 1;
    private int count = 1;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(count <= times)
        {
            animator.SetBool("timeOut", false);
            count++;
        }
        else animator.SetBool("timeOut", true);
    }
}
