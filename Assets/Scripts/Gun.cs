using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public float projectileSpeed;
    public GameObject projectile;
    public GameObject bulletHole;

    public float shotInterval;
    private float lastShot;
    void Awake()
    {
        lastShot = 0;
    }
    public void Fire()
    {
        if (Time.time - lastShot > shotInterval)
        {
            lastShot = Time.time;
            GameObject aux = Instantiate(projectile, bulletHole.transform.position, projectile.transform.rotation) as GameObject;
            aux.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        }
    }

}
