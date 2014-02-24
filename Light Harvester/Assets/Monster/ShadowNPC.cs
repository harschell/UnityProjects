using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Rigidbody))]
public class ShadowNPC : MonoBehaviour {

	public Transform target;
	public float moveSpeed = 13.0f;
	public float turnSpeed = 2.0f;
	public float rayDistance = 5.0f;
	
	public AudioClip[] footsteps;
	public float nextFoot;

	private Lantern lanternScript;
	private Vector3 desiredVelocity;

	private Transform myTransform; // the NPC
	private Rigidbody myRigidbody;
	private bool isGrounded = true;

	private float startSpeed;
	private float startNextFoot;
	private float runningSpeed;
	private float runningNextFoot;
	private Transform lightTransform;

	public NPC myState;
	public enum NPC{
		Chasing,
		RunningAway
	}


	// Use this for initialization
	void Awake () {

		myTransform = transform;
		myRigidbody = rigidbody;
		myRigidbody.freezeRotation = true;

		startSpeed = moveSpeed;
		startNextFoot = nextFoot;

		runningSpeed = startSpeed + 2.0f;
		runningNextFoot = nextFoot - 0.25f;

		myState = NPC.Chasing;

		GameObject targetObject = GameObject.Find( "Player" );
		
		if ( targetObject )
		{
			target = targetObject.transform;
			
			lanternScript = target.GetComponentInChildren< Lantern >();
		}
		else
		{
			Debug.Log( "no object named player was found" );
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		desiredVelocity = myTransform.forward * moveSpeed;

		switch( myState )
		{
		case NPC.Chasing :
		//	myTransform.LookAt(target);
			Movement( (target.position - myTransform.position).normalized );
			break;
			
		case NPC.RunningAway :
			//myTransform.LookAt((myTransform.position - target.position).normalized);
			Movement( (myTransform.position - target.position).normalized );
			//Moving( (myTransform.position - target.position).normalized );
			break;
		}
	}





	void Movement ( Vector3 lookDirection )
	{

		// rotation	
		RaycastHit hit;
		
		float shoulderMultiplier = 0.75f;
		
		
		Vector3 leftRayPos = myTransform.position - ( myTransform.right * shoulderMultiplier );
		Vector3 rightRayPos = myTransform.position + ( myTransform.right * shoulderMultiplier );
		
		if ( Physics.Raycast( leftRayPos, myTransform.forward, out hit, rayDistance ) ){
			if ( hit.collider.gameObject.name != "Terrain" )
			{
				Debug.DrawLine( leftRayPos, hit.point, Color.red );
				
				lookDirection += hit.normal * 20.0f;
			}
		}
		else if ( Physics.Raycast( rightRayPos, myTransform.forward, out hit, rayDistance ) ){
			if ( hit.collider.gameObject.name != "Terrain" )
			{
				Debug.DrawLine( rightRayPos, hit.point, Color.red );
				
				lookDirection += hit.normal * 20.0f;
			}
		}
		else{
			Debug.DrawRay( leftRayPos, myTransform.forward * rayDistance, Color.yellow );
			Debug.DrawRay( rightRayPos, myTransform.forward * rayDistance, Color.yellow );
		}
		
		if ( myRigidbody.velocity.sqrMagnitude < 1.75f )
		{
			lookDirection += myTransform.right * 20.0f;
		}



		Quaternion lookRot = Quaternion.LookRotation( lookDirection );

		myTransform.rotation = Quaternion.Slerp( myTransform.rotation, lookRot, turnSpeed * Time.deltaTime );

		float sqrDist = ( target.position - myTransform.position ).sqrMagnitude;

		if(sqrDist < (lanternScript.lantern.light.range * lanternScript.lantern.light.range))
		{
			Debug.Log((lanternScript.lantern.light.range * lanternScript.lantern.light.range));
			myState = NPC.RunningAway;
		}
		else
		{
			myState = NPC.Chasing;;
		}
	}



	void FixedUpdate() {
		if ( isGrounded ){
			myRigidbody.velocity = desiredVelocity;
		}
	}

	//Thanks to Cristian Dario Montenegro Bahamon @ http://www.youtube.com/watch?v=_cJR6QQ9T94
	IEnumerator Start () {
		
		while(true)
		{
			if(rigidbody.velocity.magnitude > 0.3F)
			{
				audio.PlayOneShot(footsteps[Random.Range(0,6)]);
				yield return new WaitForSeconds(nextFoot);
			}
			else
			{
				yield return 0;
			}
			
		}
		
	}


}
