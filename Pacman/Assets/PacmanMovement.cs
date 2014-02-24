using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Rigidbody))]
public class PacmanMovement : MonoBehaviour {


	public float speed = 10.0f;
	public float maxVelocityChange = 10.0f;
	public float gravity = 10.0f;
	public float shoulderMultiplier = 0.45f;

	private Rigidbody myRigidbody;
	private Transform myTransform;
	private Vector3 desiredVelocity;
	private Quaternion rotation;

	private bool forward,left,right,down;

	// Use this for initialization

	void Awake () {

		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;

		myRigidbody = rigidbody;
		myTransform = transform;
	
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		desiredVelocity = myTransform.forward * speed;


		//framover
		Debug.DrawRay(myTransform.position - ( Vector3.right * shoulderMultiplier ), Vector3.forward, Color.blue);
		Debug.DrawRay(myTransform.position + ( Vector3.right * shoulderMultiplier ), Vector3.forward, Color.blue);
		


		//left
		Debug.DrawRay(myTransform.position - ( Vector3.forward * shoulderMultiplier ), Vector3.right, Color.blue);
		Debug.DrawRay(myTransform.position + ( Vector3.forward * shoulderMultiplier ), Vector3.right, Color.blue);



		//right
		Debug.DrawRay(myTransform.position - ( Vector3.forward * shoulderMultiplier ), Vector3.left, Color.blue);
		Debug.DrawRay(myTransform.position + ( Vector3.forward * shoulderMultiplier ), Vector3.left, Color.blue);
		


		//down
		Debug.DrawRay(myTransform.position - ( Vector3.right * shoulderMultiplier ), Vector3.back, Color.blue);
		Debug.DrawRay(myTransform.position + ( Vector3.right * shoulderMultiplier ), Vector3.back, Color.blue);
		



	}



	void FixedUpdate () {

		myRigidbody.velocity = desiredVelocity;


		if (Input.GetAxis ("Horizontal") > 0)
		{
			if (!Physics.Raycast(myTransform.position - ( Vector3.forward * shoulderMultiplier ), Vector3.right, 1) && 
			    !Physics.Raycast(myTransform.position + ( Vector3.forward * shoulderMultiplier ), Vector3.right, 1))
			{
				myTransform.forward = Vector3.right;
			}

		}

		if (Input.GetAxis ("Horizontal") < 0)
		{
			if (!Physics.Raycast(myTransform.position - ( Vector3.forward * shoulderMultiplier ), Vector3.left, 1) && 
			    !Physics.Raycast(myTransform.position + ( Vector3.forward * shoulderMultiplier ), Vector3.left, 1))
			{
				myTransform.forward = Vector3.left;
			}
				
		}

		if (Input.GetAxis ("Vertical") > 0)
		{
			if (!Physics.Raycast(myTransform.position - ( Vector3.right * shoulderMultiplier ), Vector3.forward, 1) && 
			    !Physics.Raycast(myTransform.position + ( Vector3.right * shoulderMultiplier ), Vector3.forward, 1))
			{
				myTransform.forward = Vector3.forward;
			}
				
		}

		if (Input.GetAxis ("Vertical") < 0)
		{
			if (!Physics.Raycast(myTransform.position - ( Vector3.right * shoulderMultiplier ), Vector3.back, 1) && 
			    !Physics.Raycast(myTransform.position + ( Vector3.right * shoulderMultiplier ), Vector3.back, 1))
			{
				myTransform.forward = Vector3.back;
			}
				
		}
	}
}
