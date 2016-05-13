using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Text life;
	public Text ammo;
	public Text weapon;

	// Update is called once per frame
	void OnGUI ()
	{
		life.text = "Life: " + GameManager.getInstance ().getPlayerController ().lifePoints;
	
		if (GameManager.getInstance ().getPlayerController ().getWeapon ().maxAmmo == 0)
			ammo.text = "Ammo: infinite";
		else
			ammo.text = "Ammo: " + GameManager.getInstance ().getPlayerController ().getWeapon ().ammo + "/" + GameManager.getInstance ().getPlayerController ().getWeapon ().maxAmmo;

		weapon.text = GameManager.getInstance ().getPlayerController ().getWeapon ().name;
	}
}
