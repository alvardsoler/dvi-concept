using UnityEngine;
using System.Collections;

public class Shotgun : Weapon
{
    
	public override bool Fire ()
	{
		Debug.Log ("shotgun fire");
		if (Time.time - lastShot > shotInterval && (ammo > 0 || maxAmmo == 0)) {
			lastShot = Time.time;            
			GameObject aux1 = Instantiate (projectile, bulletHole.transform.position, projectile.transform.rotation) as GameObject;
			GameObject aux2 = Instantiate (projectile, bulletHole.transform.position, projectile.transform.rotation) as GameObject;
			GameObject aux3 = Instantiate (projectile, bulletHole.transform.position, projectile.transform.rotation) as GameObject;

            
			Vector3 angle = transform.forward;

			//angle.y += 30f;

            

			Vector3 force = angle * projectileSpeed;
            
			aux1.GetComponent<Projectil> ().setDamage (damage);
			aux1.GetComponent<Rigidbody> ().AddForce (force);


			force = Quaternion.Euler (0, -15, 0) * angle * projectileSpeed;
			aux2.GetComponent<Projectil> ().setDamage (damage);
			aux2.GetComponent<Rigidbody> ().AddForce (force);

			force = Quaternion.Euler (0, 15, 0) * angle * projectileSpeed;
			aux3.GetComponent<Projectil> ().setDamage (damage);
			aux3.GetComponent<Rigidbody> ().AddForce (force);

			if (maxAmmo != 0)
				ammo--;
			return true;
		} else
			return false;
	}
}
