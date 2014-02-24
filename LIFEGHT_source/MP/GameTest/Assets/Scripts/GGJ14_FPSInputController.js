private var motor : CharacterMotor;

private var direction : int;
private var grill : float;
public var player_num : int = 1;

// Use this for initialization
function Awake () {
	motor = GetComponent(CharacterMotor);
	direction = 0;
	grill = 0;
}

// Update is called once per frame
function Update () {
	// Get the input vector from keyboard or analog stick
	var directionVector = new Vector3(Input.GetAxis("Horizontal"+player_num), 0, Input.GetAxis("Vertical"+player_num));
	
	if (directionVector != Vector3.zero) {
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength = directionVector.magnitude;
		directionVector = directionVector / directionLength;
		
		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1, directionLength);
		
		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;
		
		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
	}
	
	// Apply the direction to the CharacterMotor
	motor.inputMoveDirection = transform.rotation * directionVector;
	
	// calc jump with GRILLETTI
	
	var new_grill = Input.GetAxis("JoyJump"+player_num);
	new_grill = Mathf.Clamp(new_grill,0,1);
	if (grill != 0 && grill > new_grill && direction !=-1)
	{
		direction = -1;
		motor.inputJump = true;
	} else
	{
		motor.inputJump = false;
	}
	if (new_grill==grill)
	{
		direction = 0;
	}
	if (grill < new_grill)
	{
		direction = 1;
	}
	grill = new_grill;
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
@script AddComponentMenu ("Character/FPS Input Controller")
