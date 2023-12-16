using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;
using StarterAssets;
using System;
using UnityEngine.Rendering;
using static UnityEngine.EventSystems.EventTrigger;

public class enemyScript : MonoBehaviour
{

    public Transform player;
    private NavMeshAgent agent;
    Animator animator;
    public GameObject axeThrow;



    public bool tryGrapple = false;

    bool inGrapple=false;


    public float maxTime = 0.0f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;
    float throwDistance;
    float attackTimer = 0.0f;
    float attackOrgrappleCoolDown = 2.0f;
    public float rotationSpeed = 5.0f;
    public float agentRange = 5.0f;
    // Start is called before the first frame update

    public float throwForce = 6f;

    private TPSController TPSController;

    public float currentHealth = 5;

    private bool isDead = false;
    private bool isHit = false;
    private bool isKnockedDown = false;
    public float attackRange;

    public bool isArmed;

    public EnemyDamageDealer dealer;

    public bool isPlayerInRange;

    public RoomTrigger associatedRoom;

    void Start()
    {
        TPSController = GetComponent<TPSController>();
        //BulletCollider;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        dealer = GetComponent<EnemyDamageDealer>();
        agent.isStopped = true;
        throwDistance = 5.0f;

        // Register this enemy with the room
        if (associatedRoom != null)
        {
            associatedRoom.RegisterEnemy(this);
        }
        else
        {
            Debug.LogWarning("No RoomTrigger found in the scene.");
        }
        SetAnimatorLayer();
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


        if (isPlayerInRange )
        {
            if (tryGrapple)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.isStopped = true;
        }


        // Check if the enemy is close to the player to initiate an attack
        if (distanceToPlayer < attackRange)
        {
            int randomValue = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 10f));
            if (isArmed)
            {
                if (attackTimer > attackOrgrappleCoolDown)
                {
                    Attack();
                    attackTimer = 0;
                    attackOrgrappleCoolDown = 2.0f;
                    inGrapple = false;
                }
            }
            else
            {
                if (randomValue <= 7)
                {
                    if (attackTimer > attackOrgrappleCoolDown)
                    {
                        Attack();
                        attackTimer = 0;
                        attackOrgrappleCoolDown = 2.0f;
                        inGrapple = false;
                    }
                }
                else
                {
                    if (attackTimer > attackOrgrappleCoolDown)
                    {
                        Grapple();
                        attackTimer = 0;
                        attackOrgrappleCoolDown = 8.0f;
                        inGrapple = true;
                    }

                }
            }

        }
        else if (distanceToPlayer < throwDistance && attackTimer > attackOrgrappleCoolDown)
        {
            int randomValue = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
            attackTimer = 1;
            if (randomValue == 1)
            {   
                if (isArmed )
                {
                    GameObject objectToThrow = Instantiate(axeThrow, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);


                    //axeThrow.SetActive(false);

                    Destroy(axeThrow);


                    ThrowObject(objectToThrow, throwForce);

                    attackTimer = 0;
                }
            }

        }


        attackTimer += Time.deltaTime;

        if (timer < 0.0f)
        {
             agent.destination = player.position;
             Quaternion lookRotation = Quaternion.LookRotation(player.position - agent.transform.position);
             agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
             timer = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);


        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isArmed)
            {
                GameObject objectToThrow = Instantiate(axeThrow, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);


                //axeThrow.SetActive(false);

                Destroy(axeThrow);


                ThrowObject(objectToThrow, throwForce);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            KnockdeDown();
        }

        


    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) { }
        else
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



    }

    public void Die()
    {
        animator.SetTrigger("Die");
        isDead = true;
        Destroy(gameObject, 5f);



    }

    private void ResumeWalking()
    {
        if (!isDead)
        {
            // Resume the NavMeshAgent and reset the hit flag
            agent.isStopped = false;
            isHit = false;
            isKnockedDown = false;
            animator.SetBool("Grapple", false);
            animator.SetBool("Attack", false);
            tryGrapple = false;
            Debug.Log("Try grapple is false");
        }
    }

    private void GetUp()
    {
        if (!isDead)
        {
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
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (!isDead && distanceToPlayer < attackRange)
        {
            Quaternion lookRotation = Quaternion.LookRotation(player.position - agent.transform.position);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            agent.isStopped = true;
            animator.SetBool("Attack", true);

            Invoke("ResumeWalking", 1.0f); // Adjust the delay as needed

        }
    }



    private void Grapple()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (!isDead && distanceToPlayer < attackRange)
        {
            Quaternion lookRotation = Quaternion.LookRotation(player.position - agent.transform.position);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            agent.isStopped = true;
            animator.SetBool("Grapple", true);
            Invoke("ResumeWalking", 10.0f); // Adjust the delay as needed
            //Invoke("GetComponentInChildren<EnemyDamageDealer>().EndDealDamage()", 4.0f)
            tryGrapple= true;
            Debug.Log("Try grapple is true");




        }
    }

    private void SetAnimatorLayer()
    {
        float baseLayerWeight = isArmed ? 0f : 1f;
        float armedLayerWeight = isArmed ? 1f : 0f;

        animator.SetLayerWeight(0, baseLayerWeight);
        animator.SetLayerWeight(1, armedLayerWeight);

        // Ensure that the current state in the other layer is set to 0 to avoid blending issues
        animator.Play(animator.GetCurrentAnimatorStateInfo(isArmed ? 0 : 1).fullPathHash, isArmed ? 0 : 1, 0f);
    }

    public void ThrowObject(GameObject objectToThrow, float throwForce)
    {
        if (!isDead && objectToThrow != null)
        {
            // Get the Rigidbody component from the object
            Rigidbody objectRigidbody = objectToThrow.GetComponent<Rigidbody>();

            if (objectRigidbody != null)
            {
                // Calculate the direction towards the player
                //float throwHeight = 1f;
                Vector3 throwDirection = new Vector3(0, 1, 0);

                //player.transform.position - objectToThrow.transform.position
                Vector3 throwDirectionfinal = (player.transform.position - objectToThrow.transform.position + throwDirection).normalized;


                // Apply force to throw the object
                objectRigidbody.AddForce(throwDirectionfinal * throwForce, ForceMode.Impulse);

                float rotationSpeed = 10f;
                objectRigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * rotationSpeed, ForceMode.Impulse);

                

                objectToThrow.GetComponent<EnemyDamageDealer>().throw1 = true;

                isArmed = false;

                SetAnimatorLayer();
            }
            else
            {
                Debug.LogWarning("The object does not have a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogWarning("The enemy is dead or the object to throw is null.");
        }
    }

    public void StartDealDamage()
    {
        EnemyDamageDealer[] damageDealers = GetComponentsInChildren<EnemyDamageDealer>();
        if (isArmed)
        {
            

           

            foreach (EnemyDamageDealer dealer in damageDealers)
            {
                Debug.Log("GameObject Name: " + dealer.gameObject.name);
                if (dealer.gameObject.name == "axe")
                {
                    dealer.StartDealDamage();
                   
                }
                
            }
        }
        else
        {
            if(inGrapple)
            {
                foreach (EnemyDamageDealer dealer in damageDealers)
                {
                   
                    if (dealer.gameObject.name == "mixamorig5:RightHand")
                    {
                        dealer.StartDealDamage();
                        Debug.Log("Grapple " + dealer.gameObject.name);
                    }

                }
            }
            else
            {
                foreach (EnemyDamageDealer dealer in damageDealers)
                {
                    
                    if (dealer.gameObject.name == "EnemyDamageDealer")
                    {
                        dealer.StartDealDamage();
                        Debug.Log("Punch " + dealer.gameObject.name);

                    }

                }
            }
        }
    }
    public void EndDealDamage()
    {
        EnemyDamageDealer[] damageDealers = GetComponentsInChildren<EnemyDamageDealer>();
        if (isArmed)
        {

            foreach (EnemyDamageDealer dealer in damageDealers)
            {
                if (dealer.gameObject.name == "axe")
                {
                    dealer.EndDealDamage();

                }
                return;
            }
        }
        else
        {
            if (inGrapple)
            {
                foreach (EnemyDamageDealer dealer in damageDealers)
                {
                   
                    if (dealer.gameObject.name == "mixamorig5:RightHand")
                    {
                        dealer.EndDealDamage();
                        
                    }

                }
            }
            else
            {
                foreach (EnemyDamageDealer dealer in damageDealers)
                {
                   
                    if (dealer.gameObject.name == "EnemyDamageDealer")
                    {
                        dealer.EndDealDamage();

                    }

                }
            }
        }
    }


}
