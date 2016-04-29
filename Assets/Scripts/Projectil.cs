using UnityEngine;
using System.Collections;

public class Projectil : MonoBehaviour
{

	private float damage;

	public void setDamage (float damage)
	{
		this.damage = damage;
	}

	void Awake ()
	{
		Destroy (gameObject, 2f);
	}

	void OnCollisionEnter (Collision collision)
	{		
		if (collision.gameObject.tag.Equals ("Enemy")) {
			Enemy e = collision.gameObject.GetComponent<Enemy> ();
			e.hit (damage);
		}
	}
}
