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
        if (GameManager.getInstance().player.getWeapon().maxAmmo == 0)
            ammo.text = "Ammo: infinite";
        else
		    ammo.text = "Ammo: " + GameManager.getInstance ().player.getWeapon ().ammo + "/" + GameManager.getInstance ().player.getWeapon ().maxAmmo;
	}
}
