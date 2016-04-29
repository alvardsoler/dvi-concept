using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Text life;
	public Text ammo;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		life.text = "Life: " + GameManager.getInstance ().player.lifePoints;
		ammo.text = "Ammo: " + GameManager.getInstance ().player.getWeapon ().ammo + "/" + GameManager.getInstance ().player.getWeapon ().maxAmmo;
	}
}
