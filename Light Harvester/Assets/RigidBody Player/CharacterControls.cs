﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControls : MonoBehaviour {
	
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;

	private bool grounded = false;
	private float startSpeed;
	private float startNextFoot;
	private float runningSpeed;
	private float runningNextFoot;

	public AudioClip[] footsteps;
	public float nextFoot;
	
	
	
	void Awake () {

		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;

		startSpeed = speed;
		startNextFoot = nextFoot;

		runningSpeed = startSpeed + startSpeed;
		runningNextFoot = nextFoot - 0.25f;
	}

	void Update() {

		if (Input.GetButton("Run"))
		{
			speed = runningSpeed;
			nextFoot = runningNextFoot;

		}
		else
		{
			speed = startSpeed;
			nextFoot = startNextFoot;
		}
	}

	/*
		if (Input.GetButtonDown("left shift") && running)
		{
			running = false;
			speed -= 2f;
			nextFoot += 0.3f;
		}
		else if (Input.GetButtonDown("left shift") && !running)
		{
			running = true;
			speed += 2f;
			nextFoot -= 0.3f;
		}
	}
	*/
	
	void FixedUpdate () {


		if (grounded) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			// Jump
			if (canJump && Input.GetButton("Jump")) {
				rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}

		}
		
		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
	}
	
	void OnCollisionStay ( Collision collision ) {
		if ( collision.collider.gameObject.name == "Floor" || collision.collider.gameObject.name == "Terrain" ){
			grounded = true;    
		}
	}
	
	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}



	//Thanks to Cristian Dario Montenegro Bahamon @ http://www.youtube.com/watch?v=_cJR6QQ9T94
	IEnumerator Start () {

		
		while(true)
		{
			if(grounded && rigidbody.velocity.magnitude > 0.3F)
			{
				audio.PlayOneShot(footsteps[Random.Range(0,7)]);
				yield return new WaitForSeconds(nextFoot);
			}
			else
			{
				yield return 0;
			}
			   
		}
			   
	}

}
