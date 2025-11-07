using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DusmanYapayZeka : MonoBehaviour
{
    public float VerecegiHasar;

    public NavMeshAgent agent;

    private Animator animator;

    public bool vefatmi = false;

    public float health = 100;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;

    bool walkPointSet;
    public float walkPointUzaklik;

    public float timeBetweenAttack;

    bool alreadyAttacked;
     

    public Transform Namlu;
    public GameObject MermiPrefab;

    public float sightRange, attackRange;

    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        player = GameObject.Find("Oyuncu").transform;

        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (vefatmi) return;
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        animator.SetBool("HareketHalinde",true);

        if (distanceToWalkPoint.magnitude < 1f) 
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointUzaklik,walkPointUzaklik);
        float randomX = Random.Range(-walkPointUzaklik, walkPointUzaklik);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }

    private void Chasing()
    {
        animator.SetBool("HareketHalinde", true);
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        animator.SetBool("HareketHalinde", false);
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            GameObject mermi = Instantiate(MermiPrefab);

            mermi.transform.parent = GameObject.Find("Mermiler").transform;

            mermi.transform.position = Namlu.position;
            mermi.GetComponentInChildren<TrailRenderer>().Clear();
            mermi.transform.GetChild(0).GetComponent<TrailRenderer>().enabled = true;
            mermi.transform.GetComponent<MeshRenderer>().enabled = true;
            mermi.transform.GetComponent<MermiKod>().MermiHasari = VerecegiHasar; // degisir

            mermi.GetComponent<MermiKod>().TargetPosition = player.position;

            alreadyAttacked = true;
            Invoke("ResetAttack",timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float Damage)
    {
        health -= Damage;
        Debug.LogWarning(Damage);

        if (health <= 0) 
        { 
            Invoke("DestroyEnemy", 5f);
            animator.SetLayerWeight(1,0);
            animator.SetBool("Oldumu", true);
            vefatmi = true;
            agent.destination = transform.position;
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
