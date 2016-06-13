using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	private static GameManager instance;


	public Transform player;

	public PlayerController playerController;
	public UIManager uiManager;

	public void Awake ()
	{
		if (instance == null)
			instance = this;


	}

	void Start ()
	{
		Config ();
	}

	private void Config ()
	{
		player = GameObject.Find ("soldado").gameObject.transform;
		//if (playerController == null)
		playerController = player.GetComponent<PlayerController> ();
		//else {
		//	player = playerController.transform;
		//}
		GameObject.Find ("RTS_Camera").GetComponent<RTS_Cam.RTS_Camera> ().SetTarget (player);
	}

	public static GameManager getInstance ()
	{
		
		return instance;
	}


	public void playSoundOfPowerUp (AudioClip clip, Vector3 pos)
	{
		AudioSource.PlayClipAtPoint (clip, pos);
	}

	// Update is called once per frame
	void Update ()
	{
		if (player == null) {
			Debug.Log ("no palyer");
			Config ();
		}
			
	}

	public PlayerController getPlayerController ()
	{
		return playerController;
	}

	public Transform getPlayer ()
	{
		return player;
	}
}
