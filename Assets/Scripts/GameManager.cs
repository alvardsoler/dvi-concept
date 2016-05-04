using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	private static GameManager instance;


	public PlayerController player;
	public UIManager uiManager;

	public void Start ()
	{
		if (instance == null)
			instance = this;
		
	}

	public static GameManager getInstance ()
	{
		
		return instance;
	}



	// Update is called once per frame
	void Update ()
	{
	
	}
}
