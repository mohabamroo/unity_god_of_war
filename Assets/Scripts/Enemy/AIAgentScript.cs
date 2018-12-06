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
    float time;

    // Use this for initialization
    void Start()
    {
        // animator.SetBool("run", false);
        // playerHealth = player.GetComponent <PlayerHealth> ();
        // enemyHealth = GetComponent <EnemyHealth> ();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy always following the player
        nav.SetDestination(player.position);

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

    void OnCollisionEnter(Collision col)
    {
        print("collision");
        // Player is within enemy attack zone, trigger attack animation
        if (col.gameObject.tag == "Player")
        {
            print("detected player");
            animator.SetBool("attack", true);
            animator.SetBool("run", false);
            animator.SetBool("hit", false);
            animator.SetBool("dead", false);
        }

        // Player attacked enemy, decrement HP and trigger the hit animation
        if (col.gameObject.tag == "Weapon")
        {
            animator.SetBool("attack", false);
            animator.SetBool("run", false);
            animator.SetBool("hit", true);
            animator.SetBool("dead", false);
            // Decrement HP
            health -= 10;
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