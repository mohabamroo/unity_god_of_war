using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    Animator anim;
    float jumpDoubleTapTime;
    bool jumpDoubleTap = true;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        this.updateMovement();
    }

    void updateMovement()
    {
        var xInput = Input.GetAxis("Vertical");
        if (xInput != 0)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
        if (Input.GetKey(KeyCode.LeftShift) && xInput > 0)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }

        this.handleJumpLogic();
        this.handleAttackLogic();

        var rotInput = Input.GetAxis("Horizontal");
        if (rotInput > 0)
        {
            anim.SetBool("right", true);
            anim.SetBool("left", false);
        }
        if (rotInput < 0)
        {
            anim.SetBool("left", true);
            anim.SetBool("right", false);
        }
        if (rotInput == 0)
        {
            anim.SetBool("left", false);
            anim.SetBool("right", false);
        }
        transform.Rotate(0, rotInput, 0);
    }

    void handleJumpLogic()
    {
        if (Input.GetButtonDown("Jump") && jumpDoubleTap)
        {
            if (Time.time - jumpDoubleTapTime < 0.5f)
            {
                Debug.Log("Double-tapped");
                this.anim.SetTrigger("double_jump");
                this.anim.ResetTrigger("jump");
                jumpDoubleTapTime = 0f;
            }
            else
            {
                Debug.Log("Single-tapped after space");
                this.anim.SetTrigger("jump");

            }
            jumpDoubleTap = false;
        }
        else
        {

        }
        if (Input.GetButtonDown("Jump") && !jumpDoubleTap)
        {
            Debug.Log("Signle-tapped");
            jumpDoubleTap = true;
            jumpDoubleTapTime = Time.time;
        }
    }

    void handleAttackLogic()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("attack");

        }

    }
}
