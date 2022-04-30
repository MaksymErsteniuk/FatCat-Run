using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterFirstRotate : StateMachineBehaviour
{
    [SerializeField] private string nameBool;
    [SerializeField] private GameObject player;
    [SerializeField] private string nameBoolAfterFirstRotate;
    private Transform _transformPlayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(nameBoolAfterFirstRotate, false);     
        _transformPlayer = player.GetComponent<Transform>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(nameBool, false);
        Quaternion rotatePlayer = _transformPlayer.rotation;
        rotatePlayer.y = 90;
        _transformPlayer.rotation = new Quaternion(_transformPlayer.rotation.x, 90, _transformPlayer.rotation.z, _transformPlayer.rotation.w);
    }


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
