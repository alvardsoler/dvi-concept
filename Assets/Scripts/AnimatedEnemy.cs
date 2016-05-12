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
	private float attackInterval = 2.9f;

	private int attackHash = Animator.StringToHash ("Attack");
	private int deathHash = Animator.StringToHash ("Death");

    private Spawner spawner;

	void Start ()
	{
		player = GameManager.getInstance ().getPlayer ();
		animator = GetComponentInChildren<Animator> ();
        if (transform.parent && transform.parent.GetComponent<Spawner>())
            spawner = transform.parent.GetComponent<Spawner>();
	}
	// Update is called once per frame
	void Update ()
	{
		if (lifePoints > 0) {
			
			playerDistance = Vector3.Distance (player.position, transform.position);

			if (playerDistance < 15f) {
				lookAtPlayer ();
			} /*else {
				transform.rotation = Quaternion.identity;
			}*/

			if (playerDistance < 12f && playerDistance > 4f) {
				chase ();
			} else if (playerDistance <= 4f) {
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
            // attack when sword descend?
			player.GetComponent<PlayerController> ().Hit (this.damage);            
			animator.SetTrigger (attackHash);            
		}
	}

	public  void chase ()
	{
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);

		animator.SetFloat ("Speed", 1f);
		if (stateInfo.IsName ("Run"))
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
        // after animator
        Destroy (gameObject, 2f);
        if (spawner != null) spawner.criatureDestroyed(this.gameObject);
		Debug.Log ("i must die but i dont want to");
	}


}
