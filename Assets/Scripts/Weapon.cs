using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public float projectileSpeed;
	public int maxAmmo; // maxAmmo = 0 implica balas infinitas
	public int ammo;
	public float damage;
	public GameObject projectile;
	public GameObject bulletHole;

	public float shotInterval;
	protected float lastShot;

	void Awake ()
	{
		lastShot = 0;
	}

	
	public virtual	void Fire ()
	{
		if (Time.time - lastShot > shotInterval && (ammo > 0 || maxAmmo == 0)) {
			lastShot = Time.time;
			GameObject aux = Instantiate (projectile, bulletHole.transform.position, projectile.transform.rotation) as GameObject;
			Vector3 force = transform.forward * projectileSpeed;
			aux.GetComponent<Projectil> ().setDamage (damage);
			aux.GetComponent<Rigidbody> ().AddForce (force);            
            if (maxAmmo != 0)
                ammo--;
		}
	}
		
}
