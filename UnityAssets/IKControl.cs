using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public Transform leftLegTarget = null;
    public Transform rightLegTarget = null;
    public Transform leftHandTarget = null;
    public Transform rightHandTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    void OnAnimatorIK()
    {
        if (animator)
        {
            if (ikActive)
            {
                if (leftLegTarget != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftLegTarget.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftLegTarget.rotation);
                }
                if (rightLegTarget != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, rightLegTarget.position);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, rightLegTarget.rotation);
                }
                if (leftHandTarget != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
                }
                if (rightHandTarget != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
                }
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
