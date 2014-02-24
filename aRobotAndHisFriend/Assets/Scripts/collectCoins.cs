using UnityEngine;
using System.Collections;

public class collectCoins : MonoBehaviour {
	
	public int currentCoins = 0;
	public int currentContracts = 0;
	public AudioClip coinSound;
	public AudioClip contractSound;
	
	void OnControllerColliderHit(ControllerColliderHit hit){
 		if (hit.transform.tag == "coin"){
			Destroy(hit.gameObject);
			currentCoins++;
			Debug.Log ("currentCoins: " + currentCoins);
			audio.PlayOneShot(coinSound);
		} else if (hit.transform.tag == "contract"){
			Destroy(hit.gameObject);
			currentContracts++;
			Debug.Log ("currentContract: " + currentContracts);
			audio.PlayOneShot(contractSound);
		}
	}
	
	
	
	
}
