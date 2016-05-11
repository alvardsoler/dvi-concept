using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public Transform player;
    public float playerDistance;

    public float lifePoints;
    public float damage;
    public float speed;
    public float rotationDumping;
    public float chaseStartRan;

    private bool animated;
    private Animator animator;

    private float lastAttack;
    private float attackInterval = 2f;
    void Start()
    {
        animated = (GetComponentInChildren<Animator>()) ? true : false;
        if (animated) animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (lifePoints > 0)
        {
          

            playerDistance = Vector3.Distance(player.position, transform.position);

            if (playerDistance < 15f)
            {
                lookAtPlayer();
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }

            if (playerDistance < 12f && playerDistance > 1f)
            {
                chase();
            }
            else if (playerDistance <= 1f)
            {
                if (animated) animator.SetFloat("Speed", 0f);
                attack();
            }
            else
            {
                if (animated) animator.SetFloat("Speed", 0f);
            }
        }
    }
    void attack()
    {
        if (Time.time - lastAttack > attackInterval)
        {
            lastAttack = Time.time;
            Debug.Log("attacking");
            if (animated)
            {
                animator.SetTrigger("Attack");                
            }
        }
    }
    void chase()
    {
        if (animated) animator.SetFloat("Speed", 1f);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void lookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDumping);
    }

    public void hit(float dmg)
    {
        Debug.Log("enemy hitted, lifepoints: " + lifePoints);
        lifePoints = lifePoints - dmg;
        if (lifePoints <= 0)
        {
            die();
        }
    }

    private void die()
    {
        if (animated) animator.SetTrigger("Death");
        Destroy(gameObject, 3f);
        Debug.Log("i must die but i dont want to");
    }


}
