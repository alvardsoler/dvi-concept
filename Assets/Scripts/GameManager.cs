using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	private static GameManager instance;


	public Transform player;

	private PlayerController playerController;
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
		playerController = player.GetComponent<PlayerController> ();
	}

	public static GameManager getInstance ()
	{
		
		return instance;
	}



	// Update is called once per frame
	void Update ()
	{
	
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
