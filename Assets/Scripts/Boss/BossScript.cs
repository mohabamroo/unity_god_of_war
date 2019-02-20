using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{

    public Animator animator;
    public GameObject Hips;
    NavMeshAgent nav;
    Transform player;

    public int health;
    float attack_time;
    float angry_time;
    float time;

    public int weakPoint1Hits;
    public int weakPoint2Hits;
    public int weakPoint3Hits;
    bool[] allowedAttacks;
    bool allAttacksBlocked;
    float lastHitTime;
    public int maxHealth;
    float deadTime;

    public GameObject gameplayUI;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(player);
        nav = GetComponent<NavMeshAgent>();
        nav.ResetPath();
        this.health = this.maxHealth;
        nav.SetDestination(player.position);
        attack_time = 5.0f;
        angry_time = 11.0f;

        weakPoint1Hits = 0;
        weakPoint2Hits = 0;
        weakPoint3Hits = 0;
        allowedAttacks = new bool[3] { true, true, true };
        allAttacksBlocked = false;

    }

    void Update()
    {
        this.time += Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0)
        {
            this.deadTime += Time.deltaTime;
            this.nav.isStopped = true;
            // Delay then destroy the object
            if (this.deadTime > 3f)
            {
                gameplayUI.GetComponent<CanvasScript>().OpenCredits();
                Destroy(gameObject);
            }
            return;
        }

        if (!(allowedAttacks[0] || allowedAttacks[1] || allowedAttacks[2]))
        {
            allAttacksBlocked = true;
        }

        nav.SetDestination(player.position);
        attack_time -= Time.deltaTime;
        angry_time -= Time.deltaTime;
        float dist = Vector3.Distance(player.position, transform.position);
        if (attack_time < 0 && !allAttacksBlocked && dist<3)
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

        if (nav.remainingDistance > 3)
        {
            this.nav.isStopped = false;
            animator.SetBool("walk", true);
            animator.SetBool("die", false);
            animator.SetBool("hit", false);
            animator.SetBool("angry", false);
            animator.SetBool("light_attack", false);
            animator.SetBool("heavy_attack", false);
        } else  {
            this.nav.isStopped = true;
            animator.SetBool("walk", false);
        }

        if (angry_time < 0)
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

    public void incrementHits(int weakPoint)
    {
        if (weakPoint == 1)
        {
            weakPoint1Hits += 1;
        }
        else if (weakPoint == 2)
        {
            weakPoint2Hits += 1;
        }
        else if (weakPoint == 3)
        {
            weakPoint3Hits += 1;
        }

        if (weakPoint1Hits >= 3 || weakPoint2Hits >= 3 || weakPoint3Hits >= 3)
        {
            this.takeHeavyHit();
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

        int random = Random.Range(0, 3);
        print("random: " + random.ToString());
        var loopCounter = 0;
        while (allowedAttacks[random] == false && loopCounter < 5)
        {
            loopCounter++;
            random = Random.Range(0, 3);
        }

        return random;
    }

    void OnTriggerEnter(Collider col)
    {
        var timeDiff = this.time - this.lastHitTime;
        var clip_name = this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (col.tag == "PlayerWeapon" && timeDiff > 1.5f && clip_name != "heavy_hit")
        {
            this.takeNormalHit();
        }
    }

    void takeNormalHit()
    {
        // normal hit
        if (health < 0)
        {
            return;
        }
        print("normal attack");
        this.lastHitTime = this.time;
        this.animator.SetTrigger("hit");
        this.health = this.health - (int)(this.maxHealth * 0.05);
        this.checkDeath();
    }

    void takeHeavyHit()
    {
        if (health < 0)
        {
            return;
        }
        print("heavy_attack");
        this.lastHitTime = this.time;
        this.health = this.health - (int)(this.maxHealth * 0.2);
        this.animator.SetTrigger("heavy_hit");
        this.checkDeath();
    }

    void checkDeath()
    {
        if (this.health <= 0)
        {
            this.deadTime = 0;
            this.nav.isStopped = true;
            this.animator.SetTrigger("die");
            this.animator.SetBool("dead", true);
            this.animator.SetBool("walk", false);
            this.animator.SetBool("angry", false);
            this.animator.SetBool("left_kick", false);
            this.animator.SetBool("right_kick", false);
            this.animator.SetBool("punch", false);
        }
    }

}