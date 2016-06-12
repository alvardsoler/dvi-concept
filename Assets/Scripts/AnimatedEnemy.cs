using UnityEngine;
using System.Collections;

public class AnimatedEnemy : MonoBehaviour
{

	private Transform player;
	public float playerDistance;

	public float lifePoints;
	public float damage;
	public float speed;
	public float rotationDumping;
	

	public float lookAtDistance;
	// 15f
	public float chaseDistance;
	// 12f
	public float attackDistance;
	//4f
    
	private Animator animator;

	private float lastAttack;
	private float attackInterval = 2.9f;

	private int attackHash = Animator.StringToHash ("Attack");
	private int deathHash = Animator.StringToHash ("Death");

	private Spawner spawner;

    public GameObject lifePowerUp;
    public GameObject ammoPowerUp;

    void Start ()
	{
		player = GameManager.getInstance ().getPlayer ();
		animator = GetComponentInChildren<Animator> ();
		if (transform.parent && transform.parent.GetComponent<Spawner> ())
			spawner = transform.parent.GetComponent<Spawner> ();
	}
	// Update is called once per frame
	void Update ()
	{
		if (lifePoints > 0) {
			
			playerDistance = Vector3.Distance (player.position, transform.position);

			if (playerDistance < lookAtDistance) {
				lookAtPlayer ();
			} /*else {
				transform.rotation = Quaternion.identity;
			}*/

			if (playerDistance < chaseDistance && playerDistance > attackDistance) {
				chase ();
			} else if (playerDistance <= attackDistance) {
				animator.SetFloat ("Speed", 0f);
				attack ();
			} else {
				animator.SetFloat ("Speed", 0f);
			}
		}
	}

	public  void attack ()
	{
		if (Time.time - lastAttack > attackInterval) {
			lastAttack = Time.time;
			Debug.Log ("attacking");
			// attack when sword descend?
			player.GetComponent<PlayerController> ().Hit (this.damage);            
			animator.SetTrigger (attackHash);            
		}
	}

	public  void chase ()
	{
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);

		animator.SetFloat ("Speed", 1f);
		if (stateInfo.IsName ("Run"))
			transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}

	public  void lookAtPlayer ()
	{
		Quaternion rotation = Quaternion.LookRotation (player.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationDumping);
	}

	public void hit (float dmg)
	{
		if (lifePoints > 0) {
			lifePoints = lifePoints - dmg;
			Debug.Log ("enemy hitted, lifepoints: " + lifePoints);
			if (lifePoints <= 0) {
				die ();
			}
		}
	}

	void DeathFinished ()
	{		
		Debug.Log ("dead");
		if (spawner != null)
			spawner.criatureDestroyed (this.gameObject);
        dropPowerUp();
        Destroy (gameObject);
	}

	public  void die ()
	{
		animator.SetTrigger (deathHash);			
	}

    private void dropPowerUp()
    {
        float lp = player.GetComponent<PlayerController>().lifePoints;
        int ndx = Random.Range(0, 100);

        Debug.Log("Enemy killed. Life Points = " + lp + ", ndx = " + ndx);

        if (lp > 75)
        {
            if (ndx < 60) { } //Nada
            else if (ndx < 80)
            {
                // Botiquín
                createPowerUp(PowerUpPicker.PowerUpType.Life, 5);
            }
            else
            {
                //Munición
                createPowerUp(PowerUpPicker.PowerUpType.Ammo, 5);
            }
        }
        else if (lp > 50)
        {
            if (ndx < 40) { } //Nada
            else if (ndx < 70)
            {
                // Botiquín
                createPowerUp(PowerUpPicker.PowerUpType.Life, 10);
            }
            else
            {
                //Munición
                createPowerUp(PowerUpPicker.PowerUpType.Ammo, 5);
            }
        }
        else if (lp > 25)
        {
            if (ndx < 20) { } //Nada
            else if (ndx < 65)
            {
                // Botiquín
                createPowerUp(PowerUpPicker.PowerUpType.Life, 15);
            }
            else
            {
                //Munición
                createPowerUp(PowerUpPicker.PowerUpType.Ammo, 5);
            }
        }
        else
        {
            if (ndx < 10) { } //Nada
            else if (ndx < 80)
            {
                // Botiquín
                createPowerUp(PowerUpPicker.PowerUpType.Life, 25);
            }
            else
            {
                //Munición
                createPowerUp(PowerUpPicker.PowerUpType.Ammo, 5);
            }
        }
    }

    private void createPowerUp(PowerUpPicker.PowerUpType type, float amount)
    {
        // Spawn a PowerUp
        GameObject go = null;

        if (type == PowerUpPicker.PowerUpType.Ammo)
        {
            go = Instantiate(ammoPowerUp) as GameObject;
        }
        if (type == PowerUpPicker.PowerUpType.Life)
        {
            go = Instantiate(lifePowerUp) as GameObject;
        }
        
        PowerUpPicker pup = go.GetComponent<PowerUpPicker>();
        go.GetComponentInChildren<PowerUpPicker>().setAmount(amount);

        // Set it to the position of the killed enemy
        Vector3 coordinates = new Vector3(transform.position.x, -3.5f , transform.position.z);
        go.transform.position = coordinates;

    }

}
