using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy2Script : MonoBehaviour
{

    public Animator animator;
    public GameObject skeleton;
    NavMeshAgent nav;
    Transform player;

    public int health;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        health = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Falsify();
        //startPosition = transform.localPosition;
        // Enemy always following the player
        nav.SetDestination(player.position);
        // Stopping distance for the enemy to throw fireballs
        nav.stoppingDistance = GetComponent<enemy2Script>().maxDistance;

        Debug.Log(animator.GetBool("dead"));

        if (health <= 0 && !animator.GetBool("dead"))
        {
            // Trigger dead animation;
            Dead();
            return;
        }

        transform.LookAt(player);
        if (nav.remainingDistance<=GetComponent<enemy2Script>().maxDistance)
            Attack();
        else
            Run();
    }

    void OnCollisionStay(Collision col)
    { 
        // Player attacked enemy, decrement HP and trigger the hit animation
        if (col.gameObject.tag == "Weapon")
        {
            // Decrement HP
            health -= 10;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Weapon")
            Falsify();
    }

    void Falsify()
    {
        animator.SetBool("attack", false);
        animator.SetBool("run", false);
        animator.SetBool("hit", false);
        animator.SetBool("dead", false);
    }

    void Attack()
    {
        Falsify();
        animator.SetBool("attack", true);
    }

    void Run()
    {
        Falsify();
        animator.SetBool("run", true);
    }

    void Hit()
    {
        Falsify();
        animator.SetBool("hit", true);
    }

    void Dead()
    {
        Falsify();
        animator.SetBool("dead", true);
        // Destroy the object after 2.0f s delay
        Destroy(gameObject, 2.0f);
    }

}