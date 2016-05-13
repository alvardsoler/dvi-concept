using UnityEngine;
using System.Collections;

public class PowerUpPicker : MonoBehaviour
{
	public enum PowerUpType
	{
		Life,
		Ammo,
		Money}

	;

	/**
	 * Life, ammo, money...
	*/
	public PowerUpType target;
	/**
	 * Amount of money, ammo, life...	 
	 */
	public float amount;

	private AudioSource audioS;

	void Start ()
	{
		audioS = (GetComponent<AudioSource> ()) ? GetComponent<AudioSource> () : null;
	}

	void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "Player") {
			bool r = false;
			switch (target) {
			case PowerUpType.Ammo:
				r = collider.gameObject.GetComponent<PlayerController> ().incAmmo (amount);
				break;
			case PowerUpType.Life:
				r = collider.gameObject.GetComponent<PlayerController> ().incLife (amount);
				break;
			case PowerUpType.Money:
				Debug.Log ("mooooooney");
				r = true;
				break;
			}
				
			if (r && audioS && !audioS.isPlaying) {
				Debug.Log ("play");
				audioS.Play ();
			}
			if (r) {
				GameManager.getInstance ().playSoundOfPowerUp (audioS.clip, transform.position);
				Destroy (transform.parent.gameObject);

			}
		}
	}
}
