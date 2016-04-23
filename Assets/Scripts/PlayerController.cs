using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{


    public float walkSpeed = 1.0f;

    public float turnSmoothing = 3.0f;

    public float rotateSpeed;

    private float speed;
    private Gun gun;
    private Vector3 lastDirection;
    private float h;
    private float v;
    private Transform cameraTransform;
    private Rigidbody rigidbody;
    private bool isMoving;
    private Vector3 movement;

    // jump
    public float jumpCooldown = 1.0f;
    public float jumpHeight = 5f;
    private float timeToNextJump = 0;
    void Awake()
    {
        cameraTransform = Camera.main.transform;
        //    rigidbody = this.GetComponent<Rigidbody>();
        speed = walkSpeed;
        gun = gameObject.GetComponentInChildren<Gun>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (speed != walkSpeed) speed = walkSpeed;
        Rotate();
        Move();
        JumpManagement();
        

        if (Input.GetButton("fire"))
        {
            Fire();
        }

    }
    private void JumpManagement()
    {
        Debug.Log("jumping");
        if (rigidbody.velocity.y < 10)
        {
            if (timeToNextJump > 0)
                timeToNextJump -= Time.deltaTime;
        }
        if (Input.GetButton("jump"))
        {
            if (timeToNextJump <= 0)
            {
                rigidbody.velocity = new Vector3(0f, jumpHeight, 0);
                timeToNextJump = jumpCooldown;
            }
        }
        
    }
    private void Fire()
    {
        gun.Fire();
    }
    private void Rotate()
    {
        // Rotate left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -1 * rotateSpeed, 0);
        }
        // Rotate right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1 * rotateSpeed, 0);
        }
    }
    private void Move()
    {
        // Move forward
        rigidbody.velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += transform.forward * speed;
            rigidbody.velocity = transform.forward * speed;
        }
        // Move backward
        if (Input.GetKey(KeyCode.S))
        {
            //transform.position -= transform.forward * speed;
            rigidbody.velocity = -transform.forward * speed;
        }
    }

    public bool IsFlying()
    {
        return false;
    }
    public bool IsAiming()
    {
        return false;
    }
    public bool isSprinting()
    {
        return false;
    }
}
