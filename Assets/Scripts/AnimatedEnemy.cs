using UnityEngine;
using System.Collections;

public class AnimatedEnemy : MonoBehaviour
{

	private Transform player;
	public float playerDistance;

	public float lifePoints;
	public float damage;
	public float speed;
	public float rotationDumping;
	public float chaseStartRan;

    
	private Animator animator;

	private float lastAttack;
	private float attackInterval = 2f;

	private int attackHash = Animator.StringToHash ("Attack");
	private int deathHash = Animator.StringToHash ("Death");

	void Start ()
	{
		player = GameManager.getInstance ().getPlayer ();
		animator = GetComponentInChildren<Animator> ();
	}
	// Update is called once per frame
	void Update ()
	{
		if (lifePoints > 0) {
			Debug.Log ("animated update");
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);

			playerDistance = Vector3.Distance (player.position, transform.position);

			if (playerDistance < 15f) {
				lookAtPlayer ();
			} else {
				transform.rotation = Quaternion.identity;
			}

			if (playerDistance < 12f && playerDistance > 1.2f) {
				chase ();
			} else if (playerDistance <= 1.2f) {
				animator.SetFloat ("Speed", 0f);
				attack ();
			} else {
				animator.SetFloat ("Speed", 0f);
			}
		}
	}

	public  void attack ()
	{
		if (Time.time - lastAttack > attackInterval) {
			lastAttack = Time.time;
			Debug.Log ("attacking");
			player.GetComponent<PlayerController> ().Hit (this.damage);
            
			animator.SetTrigger (attackHash);
            
		}
	}

	public  void chase ()
	{
		animator.SetFloat ("Speed", 1f);
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}

	public  void lookAtPlayer ()
	{
		Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationDumping);
	}

	public void hit (float dmg)
	{
		Debug.Log ("enemy hitted, lifepoints: " + lifePoints);
		lifePoints = lifePoints - dmg;
		if (lifePoints <= 0) {
			die ();
		}
	}

	public  void die ()
	{
		animator.SetTrigger (deathHash);
		Destroy (gameObject, 2.5f);
		Debug.Log ("i must die but i dont want to");
	}


}
