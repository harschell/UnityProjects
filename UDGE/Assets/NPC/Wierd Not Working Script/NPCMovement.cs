using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Rigidbody))]
public class NPCMovement : MonoBehaviour {

	private Transform myTransform; // us as the NPC
	private Rigidbody myRigidbody;
	private Vector3 desiredVelocity;
	private bool isGrounded = false;
	private float minimumRangeSqr;
	private float maximumRangeSqr;

	public Transform target; // The Player
	public float moveSpeed = 6.0f;
	public float turnSpeed = 2.0f;
	public float rayDistance = 5.0f;
	public float minimumRange = 4.0f;
	public float maximumRange = 45.0f;
	public bool isNpcChasing = true;



	public enum NPC{
		Idle,
		FreeRoam,
		Chasing,
		RunningAway
	}
	
	public NPC myState;






	// Use this for initialization
	void Start () {
		minimumRangeSqr = minimumRange * minimumRange;
		maximumRangeSqr = maximumRange * maximumRange;
		
		myTransform = transform;
		myRigidbody = rigidbody;
		
		myRigidbody.freezeRotation = true;
		
		myState = NPC.Chasing;
	}

	void Update (){

		float sqrDist = (target.position - myTransform.position).sqrMagnitude;

		if(sqrDist > maximumRangeSqr){
			if(isNpcChasing){
				myState = NPC.Chasing;
			}else{
				myState = NPC.FreeRoam;
			}

		}else if (sqrDist < minimumRangeSqr){
			if(isNpcChasing){
				myState = NPC.Idle;
			}else{
				myState = NPC.RunningAway;
			}

		}else{
			if(isNpcChasing){
				myState = NPC.Chasing;
			}
			myState = NPC.RunningAway;
		}


		switch( myState ){
			case NPC.Idle :
			desiredVelocity = new Vector3(0,myRigidbody.velocity.y,0);
			break;

		case NPC.FreeRoam :
			desiredVelocity = new Vector3(0,myRigidbody.velocity.y,0);
			break;

		case NPC.Chasing :
			Moving( (target.position - myTransform.position).normalized);
			break;

		case NPC.RunningAway :
			Moving( (myTransform.position - target.position).normalized);
			break;
		}

	}

	// Update is called once per frame
	void Moving (Vector3 lookDirection) {

		// rotation
		RaycastHit hit;

		float shoulderMultiplier = 0.75f;

		Vector3 leftRayPos = myTransform.position - (myTransform.right * shoulderMultiplier);
		Vector3 rightRayPos = myTransform.position + (myTransform.right * shoulderMultiplier);

		if(Physics.Raycast(leftRayPos,myTransform.forward,out hit,rayDistance) ){
			if(hit.collider.gameObject.name != "Terrain"){
				Debug.DrawLine (leftRayPos,hit.point,Color.red);

				lookDirection += hit.normal * 20.0f;
			}
		}else if(Physics.Raycast(rightRayPos,myTransform.forward,out hit,rayDistance) ){
			if(hit.collider.gameObject.name != "Terrain"){
				Debug.DrawLine (rightRayPos,hit.point,Color.red);
				
				lookDirection += hit.normal * 20.0f;
			}
		}
		else{
			Debug.DrawRay (leftRayPos,myTransform.forward * rayDistance, Color.yellow);
			Debug.DrawRay (rightRayPos,myTransform.forward * rayDistance, Color.yellow);
		}


		Quaternion lookRot = Quaternion.LookRotation(lookDirection);
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,lookRot,turnSpeed * Time.deltaTime);

		desiredVelocity = myTransform.forward * moveSpeed;
		//desiredVelocity.y = myRigidbody.velocity.y;
	}

	void FixedUpdate(){

		if(isGrounded){
			myRigidbody.velocity = desiredVelocity;
		}

	}






	//isGrounded

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.gameObject.name == "Floor" || collision.collider.gameObject.name == "Terrain"){
			isGrounded = true;
		}
	}

	void OnCollisionStay(Collision collision) {
		if (collision.collider.gameObject.name == "Floor" || collision.collider.gameObject.name == "Terrain"){
			isGrounded = true;
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.collider.gameObject.name == "Floor" || collision.collider.gameObject.name == "Terrain"){
			isGrounded = false;
		}
	}

}
