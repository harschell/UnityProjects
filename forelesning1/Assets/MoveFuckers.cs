using UnityEngine;
using System.Collections;

public class MoveFuckers : MonoBehaviour {
	
	private float zStart,yStart;
	private bool goBack = false;
	
	public float zDistance,yDistance,zMovePerFrame,yMovePerFrame;
	
	// Use this for initialization
	void Start () {
		zStart = transform.position.z;
		yStart = transform.position.y;
		Debug.Log("START!");
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3	pos	= transform.position;
		
		if(goBack){
			pos.z -= zMovePerFrame;
		}else if(!goBack){
			pos.z += zMovePerFrame;
		}
		transform.position = pos;
		
		
		//Viss den er kommet til veiens ende, gå et hakk ned.
		if(pos.z >= zStart + zDistance){
			pos.y -= yMovePerFrame;
			transform.position = pos;
			goBack = true;
		}
		
		//Skifter kjørerettning.
		if(pos.z < zStart){
			goBack = false;	
			pos.y -= yMovePerFrame;
			transform.position = pos;
		}
		
		//Viss den er kommet helt ned, restart.
		if(pos.y <= yDistance && pos.z < zStart){
			pos.z = zStart;
			pos.y = yStart;
			transform.position = pos;
			Debug.Log(pos);
		}
		
		
	}
}
