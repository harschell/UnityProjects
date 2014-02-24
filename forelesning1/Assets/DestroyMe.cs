using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

	void OnBecameInvisible() {
 		Destroy(gameObject);
		Debug.Log ("Destroyed Alien!");
		}

}
