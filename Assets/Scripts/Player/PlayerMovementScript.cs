using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovementScript : MonoBehaviour
{
    Animator anim;
    AnimatorClipInfo[] m_CurrentClipInfo;
    float lastAttackTime;
    public int currentLevelXP;
    public int totalXP = 0;
    bool jumpDoubleTap = true;
    public int health;
    public int rage;
    public float lastHitTime;
    private float time;
    public bool blocking;
    public int rageLimit;
    private float deadTime;
    float rageTime;
    public bool rageActivated = false;

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
        if (health < 1)
        {
            this.deadTime += Time.deltaTime;
            print("dead time for player");
            print(deadTime);
            if (this.deadTime > 4.5f)
            {
                this.loadGameOver();
            }
        }

        this.checkRageMoodTime();
        this.updatePosition();
    }

    void checkRageMoodTime()
    {
        if (this.rageActivated && ((this.rageTime - this.time) > 5))
        {
            this.rageActivated = false;
        }
    }

    void updatePosition()
    {
        this.handleMovement();
        this.handleRotation();
        this.handleJumpLogic();
        this.handleAttackLogic();
        this.handleBlockLogic();
        this.handleRageLogic();
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
        if (Time.time - lastAttackTime < 0.2f)
        {
            return;
        }
        else
        {

            lastAttackTime = Time.time;
            if (Input.GetMouseButton(0))
            {
                // left
                // this.lookAtEnemy();
                anim.SetTrigger("attack");
            }

            if (Input.GetMouseButton(1))
            {
                // right
                this.lookAtEnemy();
                anim.SetTrigger("heavy_attack");
            }
        }
    }

    void lookAtEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            transform.LookAt(enemies[0].transform);
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
            GameObject.FindGameObjectWithTag("HealthBar").GetComponent<healthBarController>().getHit();
            this.health -= amount;
            if (this.health <= 0)
            {
                this.anim.SetTrigger("die");
                deadTime = 0f;
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
            if (enemy.CompareTag("WeakPoint"))
            {
                print("player should be hit by weak point");
            }

        }
    }

    void increaseHits()
    {

    }

    public void increaseRage()
    {
        this.rage += 10;
        if (this.rage > this.rageLimit)
        {
            this.rage = this.rageLimit;
        }

    }

    public void increaseHealthWithChest()
    {
        this.health = 100;
    }

    void handleRotation()
    {
        var rotAngle = 5;
        if (Input.GetKey(KeyCode.A))
        {
            // rotate left
            transform.Rotate(0, -rotAngle, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            // rotate right
            transform.Rotate(0, rotAngle, 0);
        }
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

    public void increaseXP(int points)
    {
        this.currentLevelXP += points;
        this.totalXP += points;
        if (this.currentLevelXP >= 500)
        {
            this.currentLevelXP = 0;
            GameObject.FindGameObjectWithTag("StateHolder").GetComponent<StateScript>().loadNextLevel();
        }
    }

    void handleRageLogic()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (this.rage < this.rageLimit)
            {
                print("Not enough rage");
                print(this.rage);
            }
            else
            {
                this.activateRage();
                print("activating rage");
            }
        }
    }
    void activateRage()
    {
        // TODO: attach rage sound to animation
        this.rageTime = this.time;
        this.anim.SetTrigger("rage");
        this.rageActivated = true;
        this.rage = 0;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, -1 * Time.deltaTime);
            }
        }
    }

    void loadGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public bool getRage()
    {
        return this.rageActivated;
    }
}
