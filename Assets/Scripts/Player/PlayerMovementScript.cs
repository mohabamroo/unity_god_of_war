using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    Animator anim;
    AnimatorClipInfo[] m_CurrentClipInfo;
    float lastAttackTime;
    bool jumpDoubleTap = true;
    public int health;
    public int rage;
    public float lastHitTime;
    private float time;
    public bool blocking;

    public Collider weaponCollider;
    // Use this for initialization
    void Start()
    {

        this.lastHitTime = Time.deltaTime;
        this.anim = GetComponent<Animator>();
        this.health = 100;
        this.rage = 0;
        DisableWeaponCollider();
        blocking = false;
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
        // this.handleRotation();
        this.handleJumpLogic();
        this.handleAttackLogic();
        handleBlockLogic();
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
        if (Input.GetButtonDown("Jump"))
        {
            m_CurrentClipInfo = this.anim.GetCurrentAnimatorClipInfo(0);
            var clip_name =
            m_CurrentClipInfo[0].clip.name;
            if (clip_name == "running_jump")
            {
                this.anim.SetTrigger("double_jump");
            }
            else
            {
                this.anim.SetTrigger("jump");
            }
        }
    }

    void handleAttackLogic()
    {
        if (Time.time - lastAttackTime < 0.2f) {
            return;
        } else {
            lastAttackTime = Time.time; 
        }
        if (Input.GetMouseButton(0))
        {
            // left
            anim.SetTrigger("attack");
        }

        if (Input.GetMouseButton(1))
        {
            // right
            anim.SetTrigger("heavy_attack");
        }
    }

    void handleBlockLogic()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            anim.SetTrigger("block");
            blocking = true;
        }
        else
        {
            anim.ResetTrigger("block");
            blocking = false;
        }
    }

    public void takeHit(int amount)
    {
        m_CurrentClipInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        var clip_name =
        m_CurrentClipInfo[0].clip.name;
        if (clip_name == "attack_com" || clip_name == "heavy_attack")
        {
            return;
        }
        // TODO: check blocking
        if (!blocking)
        {
            this.health -= amount;
            if (this.health <= 0)
            {
                this.anim.SetTrigger("die");
                // StartCoroutine("setDie");
            }
            else
            {
                blocking = false;
                this.anim.SetTrigger("hit");
                // StartCoroutine("setHit");
            }
        }
    }

    public void takeObstacleHit()
    {
        this.health -= 10;
            if (this.health <= 0)
            {
                this.anim.SetTrigger("die");
                // StartCoroutine("setDie");
            }
            else
            {
                blocking = false;
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
            //print("player was not attacked");
        }
    }

    void increaseHits()
    {

    }

    public void increaseRage()
    {
        if (this.rage < 30)
            this.rage += 10;

    }

    public void increaseHealthWithChest()
    {
        this.health = 100;
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

    public void EnableWeaponCollider()
    {
        weaponCollider.enabled = true;
        print(weaponCollider.enabled);
    }

    public void DisableWeaponCollider()
    {
        weaponCollider.enabled = false;
        print(weaponCollider.enabled);
    }
}
