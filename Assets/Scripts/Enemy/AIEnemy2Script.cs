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
    private bool firstAttack;
    private bool dead;

    // Use this for initialization
    void Start()
    {
        firstAttack = true;
        lastHitTime = Time.deltaTime;
        this.dead = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        health = 50;
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
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist < 13 && firstAttack)
        {
            //play speech
            firstAttack = false;
            GameObject.Find("SpeechSoundEffect").GetComponent<AudioSource>().Play();
        }
        if (health <= 0)
        {
            // Trigger dead animation
            if(!this.dead)
                player.GetComponent<PlayerMovementScript>().increaseKilledEnemies();
            this.dead = true;
            Dead();
            return;
        }

        transform.LookAt(player);
        if (nav.remainingDistance <= GetComponent<enemy2Script>().maxDistance)
            Attack();
        else
            Run();
    }

    void takeHit()
    {
        print("hits");
        var playerScript = player.GetComponent<PlayerMovementScript>();
        var attackDamage = playerScript.getActiveDamage();
        var damage = playerScript.getRage() == true ? attackDamage*2 : attackDamage;
        playerScript.increaseRage();
        health -= (int)damage;
        nav.isStopped = true;
        Hit();
        // Decrement HP
        nav.isStopped = false;

    }

    void OnTriggerEnter(Collider other)
    {
        var timeDiff = time - lastHitTime;
        // Player attacked enemy, decrement HP and trigger the hit animation
        if (other.gameObject.tag == "PlayerWeapon" && health > 0 && timeDiff > 1.3f)
        {
            lastHitTime = time;
            takeHit();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
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