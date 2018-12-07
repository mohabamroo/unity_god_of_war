using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_script : MonoBehaviour {

    public Animator animator;
    public GameObject Hips;
    NavMeshAgent nav;
    Transform player;

    public int health;
    float dying_time;
    float attack_time;
    float angry_time;

    // Use this for initialization
    void Start () {
      
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player);
        nav = GetComponent<NavMeshAgent>();
        nav.ResetPath();
        health = 100;
        nav.SetDestination(player.position);
        attack_time = -0.1f;
        angry_time = 10.0f;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

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

        if(nav.remainingDistance < 5 && attack_time < 0)
        {
            int random = Random.Range(0, 3);
            if (random <= 1)
            {
                animator.SetBool("light_attack", true);
                animator.SetBool("heavy_attack", false);
                attack_time = 0.5f;

            }
            else
            {
                animator.SetBool("light_attack", false);
                animator.SetBool("heavy_attack", true);
                attack_time = 0.5f;
            }
            animator.SetBool("walk", false);
            animator.SetBool("hit", false);
            animator.SetBool("die", false);
            animator.SetBool("angry", false);
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
            angry_time = 10.0f;
        }
    }


}