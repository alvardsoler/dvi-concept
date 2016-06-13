using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Teleporter : MonoBehaviour
{

	public Vector3 destination;
	public string sceneDestination;

	private float t;

	void Start ()
	{
		t = Time.time;
	}

	void OnTriggerEnter (Collider other)
	{
		if (Time.time - t > 2)
		if (other.gameObject.tag.Equals ("Player")) {
			DontDestroyOnLoad (other.gameObject);
			SceneManager.LoadScene (sceneDestination);
			GameManager.getInstance ().getPlayerController ().setPosition (destination);
		}
	}
}
