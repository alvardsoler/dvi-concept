using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinishGame : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {
			SceneManager.LoadScene ("Finish");
		}
	}
}
