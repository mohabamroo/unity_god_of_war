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
            // Trigger dead animation
            animator.SetBool("dead", true);
            // Delay then destroy the object
            if (time > 1)
            {
                Destroy(gameObject);
            }
        }
    }

    void followAndAttackPlayer()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist < 2)
        {
            this.attackPlayer();
            this.nav.isStopped = true;
        }
        else
        {
            this.nav.isStopped = false;
        }
    }

    void attackPlayer()
    {
        transform.LookAt(player.transform);
        animator.SetBool("attack", true);
        animator.SetBool("run", false);
        animator.SetBool("hit", false);
        animator.SetBool("dead", false);
    }

    void takeHit()
    {
        print("enemy hit");
        health -= 10;
        this.lastHitTime = this.time;
        this.nav.isStopped = true;
        // this.player.GetComponent increaseHits();
        animator.SetBool("attack", false);
        animator.SetBool("run", false);
        animator.SetBool("hit", true);
        animator.SetBool("dead", false);
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
        print("Time: ");
        print(timeDiff);
        if (col.tag == "Weapon" && timeDiff > 1)
        {
            this.takeHit();
            // Increase rage for kratos
            GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovementScript>().increaseRage();

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

        if (col.gameObject.tag == "Weapon")
        {
            animator.SetBool("attack", false);
            animator.SetBool("run", true);
            animator.SetBool("hit", false);
            animator.SetBool("dead", false);
        }
    }
}