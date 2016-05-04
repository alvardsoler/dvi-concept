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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.hit(damage);
            Destroy(this);
        }
    }
	
}
