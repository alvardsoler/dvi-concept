using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

	// controls
	public bool playingWithPad = true;

	// movement
	public float walkSpeed = 1.0f;
	public float maxSpeed = 3f;
	public float acceleration = 2f;

	private float speed;
	private bool isMoving;
	private Vector3 movement;

	// rotatin
	public float rotateSpeed;

	private Animator animator;

	// weapons
	/*public List<Weapon> weapons;*/
	public Weapon[] weapons;
	private int weaponSelected;
	private Weapon weapon;
    
	private float h;
	private float v;
	private float h_r;
	private float v_r;
	private Rigidbody rigid;

	// stats
	public float lifePoints = 100;
	public float maxLifePoints = 100;



	private bool alreadyDead = false;

	void Start ()
	{
		//weapons = new List<Weapon> (1);
		speed = walkSpeed;
		animator = GetComponent<Animator> ();

		weapons = gameObject.GetComponentsInChildren<Weapon> ();

		/*gameObject.GetComponentsInChildren*/
		for (int i = 1; i < weapons.Length; i++) {			
			Debug.Log ("get");
			weapons [i].gameObject.SetActive (false);
		}
		
		//weapons.Add(gameObject.GetComponentInChildren<Weapon>());
		weaponSelected = 0;
		//weapon = ;
		rigid = GetComponent<Rigidbody> ();
	}

	void DeathFinished ()
	{
		Debug.Log ("Player dead");
	}

	void Die ()
	{
		if (!alreadyDead) {
			alreadyDead = true;

			animator.SetTrigger ("Death");
		}
	}

	void FixedUpdate ()
	{
		if (lifePoints <= 0)
			Die ();
		h = Input.GetAxis ("Horizontal"); // left stick (movement)
		v = Input.GetAxis ("Vertical"); // left stick (movement)
		h_r = Input.GetAxis ("Horizontal_right"); // right stick (rotate)
		v_r = Input.GetAxis ("Vertical_right"); // right stick (rotate)

		if (Input.GetButtonDown ("next_weapon"))
			nextWeapon ();
		else if (Input.GetButtonDown ("previous_weapon")) {
			previousWeapon ();
		}
		Rotate ();

		Move ();

		if (playingWithPad) {
			if (Input.GetAxis ("fire") == -1) {				
				Fire ();
			}
		} else {
			if (Input.GetButton ("fire"))
				Fire ();
		}
	}

	public Weapon getWeapon ()
	{
		return weapons [weaponSelected];
	}

	public void nextWeapon ()
	{
		Debug.Log ("next weapon");
		if (weapons.Length > 1) {
			/*weapons [weaponSelected].gameObject.GetComponent<MeshRenderer> ().enabled = false;*/
			weapons [weaponSelected].gameObject.SetActive (false);
			weaponSelected++;

			// get first weapon
			if (weaponSelected >= weapons.Length)
				weaponSelected = 0;

			/*weapons [weaponSelected].gameObject.GetComponent<MeshRenderer> ().enabled = true;*/
			weapons [weaponSelected].gameObject.SetActive (true);
		}
	}

	public void previousWeapon ()
	{
		Debug.Log ("prev weapon");
		if (weapons.Length > 1) {

			weapons [weaponSelected].gameObject.SetActive (false);
			weaponSelected--;

			// get first weapon
			if (weaponSelected < 0)
				weaponSelected = weapons.Length - 1;

			/*weapons [weaponSelected].gameObject.GetComponent<MeshRenderer> ().enabled = true;*/
			weapons [weaponSelected].gameObject.SetActive (true);
		}
	}

	public void Hit (float dmg)
	{
		lifePoints -= dmg;
	}

	private void Fire ()
	{
		if (weapons [weaponSelected].Fire ()) {
			animator.SetTrigger ("Shot");
		}

		//weapon.Fire ();
	}


	private void Rotate ()
	{
		if (playingWithPad) {
			// solo hay que rotar en el eje y
			if (h_r != 0 || v_r != 0) {
				float rotateAngle = Mathf.Atan2 (h_r, -v_r) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler (0, rotateAngle, 0);	
			}						
		} else {
			transform.Rotate (0, h * rotateSpeed, 0);	
		}
	}

	private void Move ()
	{
		// Move forward
		if (playingWithPad) {
			movement.Set (h, 0, v);
			movement = movement.normalized * speed * Time.deltaTime;

			rigid.MovePosition (transform.position + movement);
		} else {
			rigid.velocity = transform.forward * speed * v;	
		}			
		animator.SetFloat ("Speed", Mathf.Abs (v));
	}

	public bool incLife (float amount)
	{
		if (lifePoints < maxLifePoints) {
			lifePoints += amount;
			if (lifePoints > maxLifePoints)
				lifePoints = maxLifePoints;
			return true;
		} else
			return false;
	}

	public bool incAmmo (float amount)
	{
		return weapons [weaponSelected].incAmmo ((int)amount);
	}

	public bool IsFlying ()
	{
		return false;
	}

	public bool IsAiming ()
	{
		return false;
	}

	public bool isSprinting ()
	{
		return false;
	}
}
