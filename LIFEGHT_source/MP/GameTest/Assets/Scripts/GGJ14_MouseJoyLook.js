#pragma strict
#pragma implicit
#pragma downcast

public enum RotationAxes { MouseJoyXAndY = 0, MouseJoyX = 1, MouseJoyY = 2 }
public var axes : RotationAxes = RotationAxes.MouseJoyXAndY;
public var sensitivityX : float = 15;
public var sensitivityY : float = 15;
public var JsensitivityX : float = 3;
public var JsensitivityY : float = 3;
public var invertedY : boolean = false;
public var player_num : int = 1;


public var minimumX = -360;
public var maximumX = 360;

public var minimumY = -60;
public var maximumY = 60;

private var rotationY = 0;

// Use this for initialization
function Awake ()
{
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
			
//	sensitivityX = 15;
//	sensitivityY = 15;
//	JsensitivityX = 3;
//	JsensitivityY = 3;
//
//	minimumX = -360;
//	maximumX = 360;
//
//	minimumY = -60;
//	maximumY = 60;

	rotationY = 0;
}

// Update is called once per frame
function Update ()
{
	if (axes == RotationAxes.MouseJoyXAndY)
	{
		var rotationX=0;
		var dirY=1;
		
		if (invertedY)
		{
			dirY = -1;
		}
		
		
		if(Mathf.Abs(Input.GetAxis("JoyPadViewX"+player_num))>0.5f){
			rotationX = transform.localEulerAngles.y + Input.GetAxis("JoyPadViewX"+player_num) * JsensitivityX;


			
		}else{	
			rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X"+player_num) * sensitivityX;
		}
		
		if(Mathf.Abs(Input.GetAxis("JoyPadViewY"+player_num))>0.5f){
			rotationY += -dirY*Input.GetAxis("JoyPadViewY"+player_num)* JsensitivityY;
			Debug.Log ("Y:" + Input.GetAxis("JoyPadViewY"+player_num));
		}else{
			rotationY += dirY*Input.GetAxis("Mouse Y") * sensitivityY;
		}
		
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
	
	}else if (axes == RotationAxes.MouseJoyX)
	{
		if(Mathf.Abs(Input.GetAxis("JoyPadViewX"+player_num))>0.5f){
			transform.Rotate(0, Input.GetAxis("JoyPadViewX"+player_num) * JsensitivityX, 0);
		} else{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
	}
	else
	{
		if(Mathf.Abs(Input.GetAxis("JoyPadViewY"+player_num))>0.5f){
			rotationY -= Input.GetAxis("JoyPadViewY"+player_num)* JsensitivityY;
		}else{	
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		}
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
	}
}

@script AddComponentMenu ("Camera-Control/MouseJoy Look")
