using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDPlayerController : MonoBehaviour 
{
	Animator myanim;
	public float moveSpeed;
	public float jumpForce = 90.0f;
	public float HalfjumpForce = 45.0f;
	private Rigidbody myRB;
	public LayerMask groundLayer;
	private Collider myCol;
	bool isPushingJump;
	public bool grounded = false;
	Collider[] groundCollisions;
	public float groundCheckRadius = 0.2f;
	public Transform groundCheck;
	public bool doubleJump = false;

	// Use this for initialization
	void Start () 
	{
		myRB = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Run Bitch
		myRB.linearVelocity = new Vector2 (moveSpeed, myRB.linearVelocity.y);
		
		//For Jumping
		if ((grounded || !doubleJump) && Input.GetAxis ("Jump") > 0) 
		{
			if (!isPushingJump)
			{


				//If Not Grounded and Double Jump Is True
				if (!doubleJump && !grounded)
				{
					doubleJump = true;
					// do half jump here
					myRB.linearVelocity = new Vector2 (myRB.linearVelocity.x, myRB.linearVelocity.y + HalfjumpForce);
				} else {
					// do full jump here (not at the beginning)
					myRB.linearVelocity = new Vector2 (myRB.linearVelocity.x, jumpForce);
				}

				grounded = false;
				isPushingJump = true;
			}
		}
		else isPushingJump = false;
	} 

	void FixedUpdate()
	{
		//Grounded Check
		groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);

		if (groundCollisions.Length > 0) grounded = true;
		else grounded = false;

		//for Double Jump Is False
		if (grounded) 
		{
			doubleJump = false;
		}

	}
}
