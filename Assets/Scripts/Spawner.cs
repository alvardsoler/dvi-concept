using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Básicamente se crean amount cantidad de criature cada spawnTime.
 * 
*/
public class Spawner : MonoBehaviour
{

	List<GameObject> criatures;

	public GameObject criature;
	public int amount;
	public float spawnTime;
	public float spawnRadious;

	// Use this for initialization
	void Start ()
	{
        criatures = new List<GameObject>(amount);
        Spawn();
		/*InvokeRepeating ("Spawn", spawnTime, spawnTime);*/
	}
    public void criatureDestroyed(GameObject goDstr)
    {
        criatures.Remove(goDstr);
        if (criatures.Count == 0)
            Invoke("Spawn", spawnTime);
    }

    void Spawn ()
	{
        // check player is near spawner
		if (Vector3.Distance (GameManager.getInstance ().getPlayer ().position, transform.position) < 30f) {
			for (int i = 0; i < amount; i++) {
                // GameObject o = Instantiate (criature, (Random.insideUnitSphere * spawnRadious + transform.position), Random.rotation) as GameObject;
                Vector2 posAux2d = Random.insideUnitCircle;
                Vector3 posAux3d = new Vector3(posAux2d.x,this.transform.position.y, posAux2d.y);
                posAux3d = posAux3d * spawnRadious + transform.position;
                GameObject o = Instantiate(criature, posAux3d, Random.rotation) as GameObject;
                o.transform.parent = this.transform;
                criatures.Add(o);

            }
		}
	}
}
