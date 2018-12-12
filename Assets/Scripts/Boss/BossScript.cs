using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour {

    public Animator animator;
    public GameObject Hips;
    NavMeshAgent nav;
    Transform player;

    public int health;
    float dying_time;
    float attack_time;
    float angry_time;

    int weakPoint1Hits;
    int weakPoint2Hits;
    int weakPoint3Hits;
    bool[] allowedAttacks;
    bool allAttacksBlocked;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player);
        nav = GetComponent<NavMeshAgent>();
        nav.ResetPath();
        health = 100;
        nav.SetDestination(player.position);
        attack_time = 5.0f;
        angry_time = 11.0f;

        weakPoint1Hits = 0;
        weakPoint2Hits = 0;
        weakPoint3Hits = 0;
        allowedAttacks = new bool[3]{true, true, true};
        allAttacksBlocked = false;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!(allowedAttacks[0] || allowedAttacks[1] || allowedAttacks[2]))
        {
            allAttacksBlocked = true;
        }

        nav.SetDestination(player.position);
        attack_time -= Time.deltaTime;
        angry_time -= Time.deltaTime;

        if (health <= 0)
        {
            dying_time += Time.deltaTime;
            // Trigger dead animation
            animator.SetBool("dead", true);
            // Delay then destroy the object
            if (dying_time > 1.0f)
            {
                Destroy(gameObject);
            }
        }


        if(attack_time < 0 && !allAttacksBlocked)
        {
            int random = pickAttack();

            if (random == 1)
            {
                animator.SetBool("right_kick", true);
                animator.SetBool("left_kick", false);
                animator.SetBool("punch", false);
            }

            if (random == 2)
            {
                animator.SetBool("right_kick", false);
                animator.SetBool("left_kick", true);
                animator.SetBool("punch", false);
            }

            if (random == 3)
            {
                animator.SetBool("right_kick", false);
                animator.SetBool("left_kick", false);
                animator.SetBool("punch", true);
            }

            animator.SetBool("walk", false);
            animator.SetBool("hit", false);
            animator.SetBool("die", false);
            animator.SetBool("angry", false);
            attack_time = 5.0f;
        }

        if (nav.remainingDistance > 5)
        {
            animator.SetBool("walk", true);
            animator.SetBool("die", false);
            animator.SetBool("hit", false);
            animator.SetBool("angry", false);
            animator.SetBool("light_attack", false);
            animator.SetBool("heavy_attack", false);
        }

        if(angry_time < 0)
        {
            animator.SetBool("walk", false);
            animator.SetBool("die", false);
            animator.SetBool("hit", false);
            animator.SetBool("angry", true);
            animator.SetBool("light_attack", false);
            animator.SetBool("heavy_attack", false);
            angry_time = 11.0f;
        }
    }

    public void incrementHits(int weakPoint){
        if(weakPoint == 1)
        {
            weakPoint1Hits += 1;
        }
        else if(weakPoint == 2)
        {
            weakPoint2Hits += 1;
        }
        else if(weakPoint == 3)
        {
            weakPoint3Hits += 1;
        }
    }


    private int pickAttack()
    {
        if (weakPoint1Hits >= 3)
        {
            allowedAttacks[0] = false;
        }
        if (weakPoint1Hits >= 3)
        {
            allowedAttacks[1] = false;
        }
        if (weakPoint1Hits >= 3)
        {
            allowedAttacks[2] = false;
        }

        int random = Random.Range(1, 4);
        while (allowedAttacks[random] == false)
        {
            random = Random.Range(0, 3);
        }

        return random;
    }
}