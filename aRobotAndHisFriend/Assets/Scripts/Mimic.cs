using UnityEngine;
using System.Collections;

public class Mimic : MonoBehaviour {

	private Vector3[] position = new Vector3[30];
	private Quaternion[] rotation = new Quaternion[30];
	private bool[] jump = new bool[30];
	private GameObject targetToMimic = GameObject.FindWithTag("Player");
	
	
	private int counter;
	
	public float waitTime = 0.2f;
	public bool follow;
	
	
	// Update is called once per frame
	void Update () {
		StartCoroutine(sampleMimic());
		
		
		while(follow){
			
			
		}
	}
	
	
	// Denne sampler posisjon, rotasjon og jump vært 0.2 sek.
	IEnumerator sampleMimic() {
		while(follow){
			position[counter] = targetToMimic.transform.position;
			rotation[counter] = targetToMimic.transform.rotation;
			// Dette blir kansje et problem viss den kun sampler vært 0.2 sek.
			if (Input.GetButton("Jump")){
				jump[counter] = true;	
			}	
			counter++;
			yield return new WaitForSeconds(waitTime);
		}
    }
	
}
