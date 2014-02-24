using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Rigidbody))]
public class Blinky : MonoBehaviour {

	public float speed = 10.0f;
	public float maxVelocityChange = 10.0f;
	public float gravity = 10.0f;
	public float shoulderMultiplier = 0.45f;

	public Transform target;
	
	private Rigidbody myRigidbody;
	private Transform myTransform;
	private Vector3 desiredVelocity;
	private Quaternion rotation;
	
	public NPC myState;
	public enum NPC{
		Chase,
		Scatter,
		Frightned
	}

	
	// Use this for initialization
	
	void Awake () {
		
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
		
		myRigidbody = rigidbody;
		myTransform = transform;
		
	}
	
	void Start () {

		GameObject targetObject = GameObject.Find( "PacMan" );
		if ( targetObject )
		{
			target = targetObject.transform;

		}
		else
		{
			Debug.Log( "no object named player was found" );
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		desiredVelocity = myTransform.forward * speed;


		if (myTransform.position.z != target.position.z)
		{
			//ned?
			if (myTransform.position.z > target.position.z && isBackOpen () && myTransform.position.z != target.position.z) {
				myTransform.forward = Vector3.back;
			}

			//opp?
			if (myTransform.position.z < target.position.z && isForwardOpen () && myTransform.position.z != target.position.z) {
				myTransform.forward = Vector3.forward;
			}
		}


		if (myTransform.position.x != target.position.x) 
		{
			//left?
			if (myTransform.position.x > target.position.x && isLeftOpen ()) {
				myTransform.forward = Vector3.left;
			}

			//right?
			if (myTransform.position.x < target.position.x && isRightOpen ()) {
				myTransform.forward = Vector3.right;
			}
		}


		//isRightOpen ();
		//isLeftOpen ();
		//isForwardOpen ();

		/*if (myRigidbody.velocity.sqrMagnitude < 1.75f)
		{
			if (isRightOpen ()) {
				myTransform.forward = Vector3.right;
				Debug.Log ("Right is Open");
			}
			else
			{
				if (isLeftOpen ()) {
					myTransform.forward = Vector3.left;					
					Debug.Log ("Left is Open");
				}
			}


		}

		/*if (isForwardOpen ()) 
		{
			myTransform.forward = Vector3.forward;
			Debug.Log ("Forward is Open");
		}*/


		/*
		//framover
		Debug.DrawRay(myTransform.position - ( Vector3.right * shoulderMultiplier ), Vector3.forward, Color.red);
		Debug.DrawRay(myTransform.position + ( Vector3.right * shoulderMultiplier ), Vector3.forward, Color.red);
		
		//left
		Debug.DrawRay(myTransform.position - ( Vector3.forward * shoulderMultiplier ), Vector3.right, Color.red);
		Debug.DrawRay(myTransform.position + ( Vector3.forward * shoulderMultiplier ), Vector3.right, Color.red);

		//right
		Debug.DrawRay(myTransform.position - ( Vector3.forward * shoulderMultiplier ), Vector3.left, Color.red);
		Debug.DrawRay(myTransform.position + ( Vector3.forward * shoulderMultiplier ), Vector3.left, Color.red);

		//down
		Debug.DrawRay(myTransform.position - ( Vector3.right * shoulderMultiplier ), Vector3.back, Color.red);
		Debug.DrawRay(myTransform.position + ( Vector3.right * shoulderMultiplier ), Vector3.back, Color.red);
		*/
	}
	
	
	
	void FixedUpdate () {
		
		myRigidbody.velocity = desiredVelocity;

	}





	bool isRightOpen()
	{
		if (!Physics.Raycast(myTransform.position - ( Vector3.forward * shoulderMultiplier ), Vector3.right, 1) && 
		    !Physics.Raycast(myTransform.position + ( Vector3.forward * shoulderMultiplier ), Vector3.right, 1))
		{
			//myTransform.forward = Vector3.right;
			return true;
		}
		else
		{
			return false;
		}
	}

	bool isLeftOpen()
	{
		if (!Physics.Raycast(myTransform.position - ( Vector3.forward * shoulderMultiplier ), Vector3.left, 1) && 
		    !Physics.Raycast(myTransform.position + ( Vector3.forward * shoulderMultiplier ), Vector3.left, 1))
		{
			//myTransform.forward = Vector3.left;
			return true;
		}
		else
		{
			return false;
		}
	}

	bool isForwardOpen()
	{
		if (!Physics.Raycast(myTransform.position - ( Vector3.right * shoulderMultiplier ), Vector3.forward, 1) && 
		    !Physics.Raycast(myTransform.position + ( Vector3.right * shoulderMultiplier ), Vector3.forward, 1))
		{
			//myTransform.forward = Vector3.forward;
			return true;
		}
		else
		{
			return false;
		}
	}

	bool isBackOpen()
	{
		if (!Physics.Raycast(myTransform.position - ( Vector3.right * shoulderMultiplier ), Vector3.back, 1) && 
		    !Physics.Raycast(myTransform.position + ( Vector3.right * shoulderMultiplier ), Vector3.back, 1))
		{
			//myTransform.forward = Vector3.back;
			return true;
		}
		else
		{
			return false;
		}
	}
}

