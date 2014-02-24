#pragma strict

var target: GameObject;  // Assign this to your player
var maxDist = 20.0; // distance before triggering follow
var minDist = 10.0; // distance before untriggering follow
var speed = 6.0;  // Should be set to the same speed as your character
private var acc = 0.0; // Amount of movement on the camera. this starts at 0 and builds until it reaches speed. 

function LateUpdate () // Had to be late update becuase it was studdering otherwise.  
	{
	var myDist = 0; // This variable is a (changing) threshhold to determine whether or not our camera should be moving or not. 
	// when the camera is not moving (acc==0), it is asigned to maxDist. 
	// Once the camera starts moving, we set myDist to minDist. This means the camera needs to get even CLOSER to the player before it stops
	// moving than it took to trigger it in the first place. this behavior does 2 things: 
	// When the camera is stopped, it lets the character walk around a bit before the cam starts to move. 
	// When the camera is moving, it prevents studdering as the character walks in-and-out of threshold. 
	
	if (acc == 0) // If the camera	 is not moving
		{
		myDist = maxDist; // Set our threshhold to Max
		}
	else // else the camera IS moving
		{
		myDist = minDist; // set threshold to min
		}
	transform.LookAt(target.transform);  // Aim at the character 
	
	var idist = Vector3.Distance(transform.position, target.transform.position); // Get distance between the cam and the player
	
	if (idist > myDist) // If distance beyond our current threshold 
		{
		acc = Mathf.Min(acc + 2, speed); // accelerate by a fixed amount (+2) until we're at max speed (speed)
		}
	else  // Else the distance is inside our threshold 
		{
		acc = Mathf.Max(acc - 2, 0);  // deacclerate by (-2) until we're at stopped (0).
		}

if (acc > 0) // If we're moving
		{
	    //transform.Translate(0, 0,  acc	 * Time.deltaTime); // Old way without collision; 
		
		//--VV-- This turns the local angle into a world coord that can be passed to the Move command 
		var iangle = transform.TransformDirection(Vector3(0,0, acc * Time.deltaTime));  
		//--AA--
		var controller : CharacterController = GetComponent(CharacterController); // Get controller 
		var flags = controller.Move(iangle);// Move with collision but without gravity. 
		
		}
	}