using UnityEngine;
using System.Collections;

public class EatsPlayer : MonoBehaviour {

	
	//OnControllerColliderHit(ControllerColliderHit hit)
		
	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.transform.tag == "shark"){
			Destroy(gameObject);
			Application.LoadLevel(Application.loadedLevel);
		}
	}

}

