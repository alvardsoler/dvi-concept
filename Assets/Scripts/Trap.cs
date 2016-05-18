using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    private Collider col;

    public float damageDelay = 0.5f;
    public float damage;
    private float lastAttack = 0f;

	// Use this for initialization
	void Start () {
        this.col = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("enterrr");
        if (other.gameObject.tag.Equals("Player") && Time.time - lastAttack > damageDelay)
        {
            lastAttack = Time.time;
            other.gameObject.GetComponent<PlayerController>().Hit(this.damage);
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("enterrr");
        if (other.gameObject.tag.Equals("Player") && Time.time - lastAttack > damageDelay)
        {
            lastAttack = Time.time;
            other.gameObject.GetComponent<PlayerController>().Hit(this.damage);
        }
    }

    /*  void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.tag.Equals("Player") && Time.time - lastAttack > damageDelay)
      {
          lastAttack = Time.time;
          collision.gameObject.GetComponent<PlayerController>().Hit(this.damage);
      }
  }*/
}
