using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromRotateToMoving : StateMachineBehaviour
{
    public GameObject Cat;
    [SerializeField] private string ToMovingBool;
    [SerializeField] private string FirstRotate;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(Cat.gameObject.transform.rotation.eulerAngles.y);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Cat.transform.rotation.eulerAngles.y == 180)
        {
            animator.SetBool(ToMovingBool, true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(FirstRotate, false);
        animator.SetBool(ToMovingBool, false);
        Quaternion rotatePlayer = Cat.transform.rotation;
    }
}
