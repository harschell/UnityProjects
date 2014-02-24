using UnityEngine;
using System.Collections;
[RequireComponent(typeof (Rigidbody))]
public class SlenderNPC : MonoBehaviour {

	public Transform target; // the Player
	public float moveSpeed = 6.0f;
	public float turnSpeed = 2.0f;
	public float rayDistance = 5.0f;
	public float minimumRange = 4.0f;
	public float maximumRange = 45.0f;
	public bool isNpcChasing = true;
	public NPC myState;
	public float freeRoamTimerMax = 5.0f;
	public float freeRoamTimerMaxRange = 3.5f;
	public bool isSlender = true;
	public bool isVisible = false;
	public float offScreenDot = 0.8f;

	private float freeRoamTimer = 1000.0f;
	private float freeRoamTimerMaxAdjusted = 5.0f;
	private Vector3 calcDir;
	private Transform myTransform; // the NPC
	private Rigidbody myRigidbody;
	private Vector3 desiredVelocity;
	private bool isGrounded = false;
	private float minimumRangeSqr;
	private float maximumRangeSqr;


	public enum NPC{
		Idle,
		FreeRoam,
		Chasing,
		RunningAway
	}




	
	void Start(){
		minimumRangeSqr = minimumRange * minimumRange;
		maximumRangeSqr = maximumRange * maximumRange;
		
		myTransform = transform;
		myRigidbody = rigidbody;
		
		myRigidbody.freezeRotation = true;
		
		myState = NPC.Chasing;

		if(isSlender){
			InvokeRepeating("TeleportEnemy",1.0f,5.0f);
		}
	}




	void TeleportEnemy(){

		CheckIfVisible();

		if (!isVisible)
		{
			float sqrDist = ( target.position - myTransform.position ).sqrMagnitude;

			if ( sqrDist > maximumRangeSqr + 25.0f )
			{

				float teleportDistance = maximumRange + 5.0f;

				Vector3 terrainPosCheck = target.position + ( myTransform.right * teleportDistance );
				terrainPosCheck.y = 5000.0f;

				RaycastHit hit;

				if (Physics.Raycast( terrainPosCheck, -Vector3.up,out hit,Mathf.Infinity ) )
				{
					if ( hit.collider.gameObject.name == "Terrain" ) 
					{
						myTransform.position = hit.point + new Vector3(0f, 0.25f, 0f);
					}
				}

			}
		}
	}



	
	void Update(){

		if(isSlender){

			SlenderDecisions();

		}else{

			MakeSomeDecisions();

		}

		switch( myState )
		{
			case NPC.Idle :

			//THis is causing slenderCarusell Bug.
			myTransform.LookAt(target);

				desiredVelocity = new Vector3( 0, myRigidbody.velocity.y, 0 );
				break;
				
			case NPC.FreeRoam :
				freeRoamTimer += Time.deltaTime;
				
				if(freeRoamTimer > freeRoamTimerMaxAdjusted)
				{
				freeRoamTimer = 0.0f;
				freeRoamTimerMaxAdjusted = freeRoamTimerMax + Random.Range(-freeRoamTimerMaxRange,freeRoamTimerMaxRange);
				calcDir = Random.onUnitSphere;
				calcDir.y = 0.0f;
				}

				Moving(calcDir);
				break;
				
			case NPC.Chasing :
				Moving( (target.position - myTransform.position).normalized );
				break;
				
			case NPC.RunningAway :
				Moving( (myTransform.position - target.position).normalized );
				break;
		}   
	}

	void CheckIfVisible()
	{
		Vector3 fwd = target.forward;
		Vector3 other = (myTransform.position - target.position).normalized;
		float dotProduct = Vector3.Dot(fwd,other);

		isVisible = false;

		if(dotProduct > offScreenDot)
		{
			isVisible = true;
		}
	}


	void SlenderDecisions()
	{

		CheckIfVisible();

		float sqrDist = ( target.position - myTransform.position ).sqrMagnitude;

		if(isVisible)
		{
			//Check the range
			if(sqrDist > maximumRangeSqr)
			{
				myState = NPC.Chasing;
			}
			else
			{
				RaycastHit hit;

				if(Physics.Linecast (myTransform.position,target.position,out hit) ){

					if( hit.collider.gameObject.name == target.name )
					{




						myState = NPC.Idle;

						//Decrease Health/sanity of the player.
					}
					else
					{
						myState = NPC.Chasing;
					}
				}
			}

		//Is Not visible
		}
		else
		{

			if(sqrDist > minimumRangeSqr)
			{
				myState = NPC.Chasing;
			}
			else
			{
				myState = NPC.Idle;
			}
		}

	}



	void MakeSomeDecisions() {

		float sqrDist = ( target.position - myTransform.position ).sqrMagnitude;

		if ( sqrDist > maximumRangeSqr ){
			if ( isNpcChasing ){
				myState = NPC.Chasing;
			}
			else{
				myState = NPC.FreeRoam;
			}
		}
		else if ( sqrDist < minimumRangeSqr ){
			if ( isNpcChasing ){
				myState = NPC.Idle;
			}
			else{
				myState = NPC.RunningAway;
			}
		}
		else{
			if ( isNpcChasing ){
				myState = NPC.Chasing;
			}
			else{
				myState = NPC.RunningAway;
			}
		}

	}




	void Moving( Vector3 lookDirection ){
	
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
		
		// movement 
		desiredVelocity = myTransform.forward * moveSpeed;
	}





	void FixedUpdate() {
		if ( isGrounded ){
			myRigidbody.velocity = desiredVelocity;
		}
	}
	
	void OnCollisionEnter( Collision collision ){
		if ( collision.collider.gameObject.name == "Floor" || collision.collider.gameObject.name == "Terrain" ){
			isGrounded = true;
		}
	}
	
	void OnCollisionStay( Collision collision ){
		if ( collision.collider.gameObject.name == "Floor" || collision.collider.gameObject.name == "Terrain" ){
			isGrounded = true;
		}
	}
	
	void OnCollisionExit( Collision collision )
	{
		if ( collision.collider.gameObject.name == "Floor" || collision.collider.gameObject.name == "Terrain" )
		{
			isGrounded = false;
		}
	}
}
