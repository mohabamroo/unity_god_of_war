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
    public float lastHitTime;
    private float time;

    // Use this for initialization
    void Start()
    {
        lastHitTime = Time.deltaTime;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        health = 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        //Falsify();
        //startPosition = transform.localPosition;
        // Enemy always following the player
        nav.SetDestination(player.position);
        // Stopping distance for the enemy to throw fireballs
        nav.stoppingDistance = GetComponent<enemy2Script>().maxDistance;

        if (health <= 0)
        {
            // Trigger dead animation
            Dead();
            return;
        }

        transform.LookAt(player);
        if (nav.remainingDistance<=GetComponent<enemy2Script>().maxDistance)
            Attack();
        else
            Run();
    }

    void takeHit()
    {
        health -= 10;
        nav.isStopped = true;
        Hit();
        // Decrement HP
        nav.isStopped = false;

    }

    void OnTriggerEnter(Collider other)
    {
        var timeDiff = time - lastHitTime;
        // Player attacked enemy, decrement HP and trigger the hit animation
        if (other.gameObject.tag == "Weapon" && health > 0 && timeDiff > 1.3f)
        {
            lastHitTime = time;
            takeHit();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
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
        // Destroy the object after a delay
        Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

}