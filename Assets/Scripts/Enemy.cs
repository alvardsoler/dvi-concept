using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	protected Transform player;
	protected float playerDistance;

	protected float lifePoints;
	protected  float damage;
	protected float speed;
	protected float rotationDumping;
	protected float chaseStartRan;

	protected float lastAttack;
	protected float attackInterval = 2f;

	void Start ()
	{
		
	}
	// Update is called once per frame
	void Update ()
	{
		if (lifePoints > 0) {
          

			playerDistance = Vector3.Distance (player.position, transform.position);

			if (playerDistance < 15f) {
				lookAtPlayer ();
			} else {
				transform.rotation = Quaternion.identity;
			}

			if (playerDistance < 12f && playerDistance > 1f) {
				chase ();
			} else if (playerDistance <= 1f) {
				attack ();
			}
		}
	}

	public	virtual void attack ()
	{
		if (Time.time - lastAttack > attackInterval) {
			lastAttack = Time.time;
			Debug.Log ("attacking");

		}
	}

	public	virtual	void chase ()
	{
		
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}

	public	virtual	void lookAtPlayer ()
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

	public virtual  void die ()
	{
		
		Destroy (gameObject, 3f);
		Debug.Log ("i must die but i dont want to");
	}


}
