using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;
using StarterAssets;
using System;

public class enemyScript : MonoBehaviour
{
    
    public Transform player;
    private NavMeshAgent agent;
    Animator animator;


    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;
    // Start is called before the first frame update

    private TPSController TPSController;

    public float currentHealth = 5;

    private bool isDead= false;
    private bool isHit = false;
    private bool isKnockedDown = false;
    private bool isAttack = false;

    public float attackRange;
    void Start()
    {
        TPSController = GetComponent<TPSController>();
        //BulletCollider;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || isKnockedDown)
        {
            agent.isStopped = true;
            return;
        }

        timer = (timer - Time.deltaTime);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the enemy is close to the player to initiate an attack
        if (distanceToPlayer < attackRange)
        {
           
            Attack();
            
        }
        else
        {
            animator.SetBool("Attack", false);
            ResumeWalking();
        }

        if (timer < 0.0f)
        {
            float distance = (player.transform.position - agent.destination).sqrMagnitude;
            if(distance > maxDistance*maxDistance)
            {
                agent.destination = player.position;

            }
            timer = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            KnockdeDown();
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("TakeDamage");
            isHit = true;
            agent.isStopped = true;
            Invoke("ResumeWalking", 1.5f); // Adjust the delay as needed
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        isDead=true;
    }

    private void ResumeWalking()
    {
        if (!isDead)
        {
            // Resume the NavMeshAgent and reset the hit flag
            agent.isStopped = false;
            isHit = false;
            isKnockedDown = false;
            isAttack = false;
            animator.SetBool("Grapple", false);
        }
    }

    private void GetUp()
    {
        if (!isDead) { 
            animator.SetTrigger("GetUp");
            agent.isStopped = true;
            Invoke("ResumeWalking", 2.0f); // Adjust the delay as needed
        }
    }


    private void KnockdeDown()
    {
        if (!isDead)
        {
            animator.SetTrigger("KnockedDown");
            agent.isStopped = true;
            Invoke("GetUp", 4.0f); // Adjust the delay as needed
            isKnockedDown = true;
        }
    }

    private void Attack()
    {
        if (!isDead)
        {
           
            agent.isStopped = true;
            animator.SetBool("Attack", true);
            //agent.destination = player.position;
            isAttack = true;
            //Invoke("ResumeWalking", 1.5f); // Adjust the delay as needed
        }
    }

    private void Grapple()
    {
        if (!isDead)
        {
            animator.SetBool("Grapple",true);
            agent.isStopped = true;
            isAttack = true;
            Invoke("ResumeWalking", 4.0f); // Adjust the delay as needed
        }
    }


}
