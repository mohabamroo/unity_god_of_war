using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    Animator anim;
    float jumpDoubleTapTime;
    bool jumpDoubleTap = true;
    public int health;
    public float lastHitTime;
    private float time;
    // Use this for initialization
    void Start()
    {

        this.lastHitTime = Time.deltaTime;
        this.anim = GetComponent<Animator>();
        this.health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        this.time += Time.deltaTime;
        if (health < 0)
        {
            return;
        }
        this.updatePosition();
    }

    void updatePosition()
    {
        this.handleMovement();
        this.handleRotation();
        this.handleJumpLogic();
        this.handleAttackLogic();
    }

    void handleMovement()
    {
        var xInput = Input.GetAxis("Vertical");
        if (xInput == 0)
        {
            anim.SetBool("walking", false);
            anim.SetBool("back", false);
        }
        else
        {
            if (xInput > 0)
            {
                anim.SetBool("walking", true);
                anim.SetBool("back", false);
            }
            else
            {
                anim.SetBool("walking", false);
                anim.SetBool("back", true);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && xInput > 0)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
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

    public void takeHit(float amount)
    {
        // TODO: check blocking
        this.health -= 1;
        if (this.health <= 0)
        {
            this.anim.SetTrigger("die");
            // StartCoroutine("setDie");

        }
        else
        {
            this.anim.SetTrigger("hit");
            // StartCoroutine("setHit");

        }
    }

    IEnumerator setHit()
    {
        yield return new WaitForSeconds(0.5f);
        this.anim.SetTrigger("hit");

    }

    IEnumerator setDie()
    {
        yield return new WaitForSeconds(0.5f);
        this.anim.SetTrigger("die");

    }

    void OnTriggerEnter(Collider enemy)
    {
        // TODO: fix time
        var timeDiff = this.time - this.lastHitTime;
        if (enemy.CompareTag("Weapon") && this.health > 0 && timeDiff > 1.3)
        {
            print("player atacked");
            this.lastHitTime = this.time;
            this.takeHit(10);
        }
        else
        {
            print("player was not attacked");
        }
    }

    void increaseHits()
    {

    }

    void handleRotation()
    {
        var rotInput = Input.GetAxis("Horizontal");
        if (rotInput > 0)
        {
            this.anim.SetBool("right", true);
            this.anim.SetBool("left", false);
        }
        if (rotInput < 0)
        {
            this.anim.SetBool("left", true);
            this.anim.SetBool("right", false);
        }
        if (rotInput == 0)
        {
            this.anim.SetBool("left", false);
            this.anim.SetBool("right", false);
        }
        transform.Rotate(0, rotInput, 0);
    }
}
