using UnityEngine;
using System.Collections;

public class OrthoSmoothFollow : MonoBehaviour
{
	public Transform target;
	public float smoothTime = 0.3f;

	private Vector3 velocity = Vector3.zero;

	public float diffX = -350f;
	public float diffZ = -350f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector3 goalPos = target.position;
		goalPos.y = transform.position.y;
		transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
	}
}
