using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	
	public float moveSpeed = 0;

	private Vector3 moveDirection = Vector3.zero;
	
	//private float lowest = 0.66f;
	//private float highest;

	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController>();
		
		moveDirection = new Vector3(Input.GetAxis("Horizontal") * 1.1f,Input.GetAxis("Vertical"),0);
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= moveSpeed;

		if(transform.localPosition.x < -10f){
			moveDirection.x += moveSpeed;
			if(Input.GetAxis("Horizontal") < 0){
				moveDirection.x += moveSpeed * 1.2f;
			}
		}
		
		if (Input.GetButton("Jump")){
			moveDirection.x += moveSpeed * 1.4f;
		}
		
		controller.Move(moveDirection * Time.deltaTime);
		
		
	}
	

	
}
