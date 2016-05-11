using UnityEngine;
using System.Collections;

public class AnimatedEnemy : MonoBehaviour
{
    public Transform player;
    public float playerDistance;

    public float lifePoints;
    public float damage;
    public float speed;
    public float rotationDumping;
    public float chaseStartRan;

    
    private Animator animator;

    private float lastAttack;
    private float attackInterval = 2f;
    void Start()
    {
        
        animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (lifePoints > 0)
        {
            
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

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
                animator.SetFloat("Speed", 0f);
                attack();
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }
    void attack()
    {
        if (Time.time - lastAttack > attackInterval)
        {
            lastAttack = Time.time;
            Debug.Log("attacking");
            
            
                animator.SetTrigger("Attack");
            
        }
    }
    void chase()
    {
        animator.SetFloat("Speed", 1f);
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
        animator.SetTrigger("Death");
        Destroy(gameObject, 3f);
        Debug.Log("i must die but i dont want to");
    }


}
