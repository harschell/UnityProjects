using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float moveSpeed = 0;
	private Vector3 moveDirection = Vector3.zero;

	public float xDistance = 250f;


	// Update is called once per frame
	void Update () {

		CharacterController controller = GetComponent<CharacterController>();
		moveDirection = new Vector3(1,0,0);
		moveDirection *= moveSpeed;
	
		
		controller.Move(moveDirection * Time.deltaTime);

		if( (transform.position.x - xDistance) > xDistance){
			Application.LoadLevel(Application.loadedLevel + 1);
		}

	}
}
