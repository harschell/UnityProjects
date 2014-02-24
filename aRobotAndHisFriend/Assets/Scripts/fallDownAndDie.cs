using UnityEngine;
using System.Collections;

public class fallDownAndDie : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {

		//check if you character fell off the platform
    	if(transform.position.y < -100){
			Vector3 pos = transform.position;
			pos.x = 2.610991f;
			pos.y = 19.25463f;
			pos.z = -8.449657f;
       		transform.position = pos;
    	}
	}
}


