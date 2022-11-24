using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationControls : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        if (forwardPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (!forwardPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (!isRunning && (shiftPressed && isWalking))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!shiftPressed && isWalking) || (shiftPressed && !isWalking))
        {
            animator.SetBool(isRunningHash, false);
        }

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
