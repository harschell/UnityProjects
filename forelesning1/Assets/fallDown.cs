using UnityEngine;
using System.Collections;

public class fallDown : MonoBehaviour {
	
	public int currentScore = 0;
	
	void OnCollisionEnter(Collision i){
		if(i.transform.name == "Tank") return;
		
		//Adds Points.
		if(i.rigidbody.isKinematic = true){
			currentScore += 10;
			Debug.Log ("10 points!");
		}
		
		i.transform.parent=null;
		i.rigidbody.isKinematic = false;
	}
	
	void OnBecameInvisible() {
 		Destroy(gameObject);
		Debug.Log ("Destroyed Bullet!");
	}
	
}
