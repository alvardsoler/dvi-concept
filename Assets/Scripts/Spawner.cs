using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Básicamente se crean amount cantidad de criature cada spawnTime.
 * 
*/
public class Spawner : MonoBehaviour
{

	//List<GameObject> criatures;

	public GameObject criature;
	public int amount;
	public float spawnTime;
	public float spawnRadious;

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn ()
	{
		if (Vector3.Distance (GameManager.getInstance ().getPlayer ().position, transform.position) < 30f) {
			for (int i = 0; i < amount; i++) {
				GameObject o = Instantiate (criature, (Random.insideUnitSphere * spawnRadious + transform.position), Random.rotation) as GameObject;
				o.transform.parent = this.transform;
			}
		}
	}
}
