using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationControls : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("d");
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool spacePressed = Input.GetKeyDown(KeyCode.Space);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isJumping = animator.GetBool(isRunningHash);

        // WALKING

        if (forwardPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (!forwardPressed && isWalking)
        { 
            animator.SetBool(isWalkingHash, false);
        }

        // WALKING (BACKWARDS)

        //if (backwardPressed && !isWalking)
        //{
        //    print("backwardPressed");
        //    animator.SetBool(isWalkingHash, true);
        //}
        //if (!backwardPressed && isWalking)
        //{
            
        //    animator.SetBool(isWalkingHash, false);
        //}
        
        // RUNNING

        if (!isRunning && (shiftPressed && isWalking))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!shiftPressed && isWalking) || (shiftPressed && !isWalking))
        {
            animator.SetBool(isRunningHash, false);
        }

        // JUMPING

        if (spacePressed)
        {
            /*animator.SetBool(isRunningHash, false);*/
            /*animator.SetBool(isWalkingHash, false);*/
            animator.SetTrigger(isJumpingHash);
        }

        //if (!isJumping && spacePressed)
        //{
        //    animator.SetBool(isJumpingHash, true);
        //}
        //if (isJumping && !spacePressed)
        //{
        //    animator.SetBool(isJumpingHash, false);
        //}

        debug();

    }

    private void debug()
    {
        if (animator.GetBool(isWalkingHash))
        {
            print("Walking");
        }
        if (animator.GetBool(isRunningHash))
        {
            print("Running");
        }
    }
}
