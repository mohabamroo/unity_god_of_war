using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgentScript : MonoBehaviour
{

    public Animator animator;
    public NavMeshAgent nav;
    public Transform player;

    public int health;
    private float time;
    public float lastHitTime;
    private float lastAttackTime;
    private bool firstAttack;
    public Collider weaponCollider;
    public float deadTime;
    // Use this for initialization
    void Start()
    {
        // animator.SetBool("run", false);
        // playerHealth = player.GetComponent <PlayerHealth> ();
        // enemyHealth = GetComponent <EnemyHealth> ();
        this.lastHitTime = Time.deltaTime;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        health = 100;
        firstAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.time += Time.deltaTime;
        // Enemy always following the player
        nav.SetDestination(player.position);
        this.followAndAttackPlayer();


        if (health <= 0)
        {
            time += Time.deltaTime;
            deadTime += Time.deltaTime;
            // Trigger dead animation
            animator.SetBool("dead", true);
            // Delay then destroy the object
            if (deadTime > 2.5)
            {
                var clip_name =
                 this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                var playerScript = player.GetComponent<PlayerMovementScript>();
                playerScript.increaseXP(50);
                if (clip_name != "dead")
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    void restAfterAttack()
    {
        weaponCollider.enabled = false;
        animator.SetBool("attack", false);
        this.lastAttackTime = 0;
    }
    void followAndAttackPlayer()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist < 13 && firstAttack)
        {
            //play speech
            firstAttack = false;
            GameObject.Find("SpeechSoundEffect").GetComponent<AudioSource>().Play();
        }
        if (dist < 2)
        {
            if (!this.animator.GetBool("idle"))
            {
                animator.SetBool("idle", true);
            }
            this.nav.isStopped = true;
            if (this.lastAttackTime > 2)
            {
                this.attackPlayer();
                Invoke("restAfterAttack", 2.5f);
            }
            else
            {
                this.lastAttackTime += Time.deltaTime;
                animator.SetBool("run", false);
            }
        }
        else
        {
            this.nav.isStopped = false;
            animator.SetBool("run", true);
            animator.SetBool("attack", false);
            animator.SetBool("hit", false);
            animator.SetBool("dead", false);
        }
    }

    void attackPlayer()
    {
        weaponCollider.enabled = true;
        transform.LookAt(player.transform);
        animator.SetBool("attack", true);
        animator.SetBool("run", false);
        animator.SetBool("hit", false);
        animator.SetBool("dead", false);
    }

    void takeHit()
    {
        var playerScript = player.GetComponent<PlayerMovementScript>();
        var damage = playerScript.getRage() == true ? 20 : 10;
        health -= damage;
        if (health < 0 && this.deadTime == 0)
        {
            this.deadTime = this.time;
            playerScript.increaseXP(50);
        }
        this.lastHitTime = this.time;
        this.nav.isStopped = true;
        // Increase rage for kratos
        playerScript.increaseRage();
        animator.SetBool("attack", false);
        animator.SetBool("run", false);
        animator.SetBool("dead", false);
        animator.SetBool("hit", true);
        // Decrement HP
        this.nav.isStopped = false;

    }

    void OnTriggerEnter(Collider col)
    {
        print("collision with enemy");
        // Player is within enemy attack zone, trigger attack animation
        if (col.tag == "Player")
        {
            // print("detected player");
            // this.attackPlayer();
        }

        // Player attacked enemy, decrement HP and trigger the hit animation
        var timeDiff = this.time - this.lastHitTime;
        //print("Time: ");
        //(timeDiff);
        if (col.tag == "PlayerWeapon" && timeDiff > 1)
        {
            this.takeHit();
        }
    }

    void OnCollisionExit(Collision col)
    {
        // Enemy is not within attack zone, enable running animation
        if (col.gameObject.tag == "Player")
        {
            animator.SetBool("run", true);
            animator.SetBool("attack", false);
            animator.SetBool("hit", false);
            animator.SetBool("dead", false);
        }

        if (col.gameObject.tag == "PlayerWeapon")
        {
            animator.SetBool("run", true);
            animator.SetBool("attack", false);
            animator.SetBool("hit", false);
            animator.SetBool("dead", false);
        }
    }
}