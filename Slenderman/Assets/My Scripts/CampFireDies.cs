using UnityEngine;
using System.Collections;

public class CampFireDies : MonoBehaviour {

	public float burnTime = 10.0f;
	public Transform target; // the Player

	// Update is called once per frame
	void Update () 
	{
		float sqrDist = ( target.position - transform.position ).sqrMagnitude;

		if(sqrDist > 1000f && burnTime > -0.1f)
		{
			burnTime -= Time.deltaTime;

			if(burnTime <= 0.0f){
				GameObject targetObject = GameObject.Find( "Flame" );
				Destroy(targetObject);
			}
		}
	}

}
