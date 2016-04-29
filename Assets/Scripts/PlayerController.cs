using UnityEngine;
using System.Collections;

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

	private Weapon weapon;
    
	private float h;
	private float v;
	private float h_r;
	private float v_r;
	private Rigidbody rigid;

	// stats
	public float lifePoints = 100;


	void Awake ()
	{		
		speed = walkSpeed;
		weapon = gameObject.GetComponentInChildren<Weapon> ();
		rigid = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		h = Input.GetAxis ("Horizontal"); // left stick (movement)
		v = Input.GetAxis ("Vertical"); // left stick (movement)
		h_r = Input.GetAxis ("Horizontal_right"); // right stick (rotate)
		v_r = Input.GetAxis ("Vertical_right"); // right stick (rotate)

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
		return weapon;
	}

	private void Fire ()
	{
		weapon.Fire ();
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
