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


	// Update is called once per frame
	void Update ()
	{
		playerDistance = Vector3.Distance (player.position, transform.position);

		if (playerDistance < 15f) {
			lookAtPlayer ();
		} else {
			transform.rotation = Quaternion.identity;
		}

		if (playerDistance < 12f) {
			chase ();
		}

	}

	void chase ()
	{
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}

	void lookAtPlayer ()
	{
		Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationDumping);
	}

	public void hit (float dmg)
	{		
		lifePoints = lifePoints - dmg;
		if (lifePoints <= 0) {
			die ();
		}
	}

	private void die ()
	{
		
	}


}
