using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	private Transform transformPosition;



	// Use this for initialization
	void Start () {
		transformPosition = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{

		gameObject.transform.Rotate(0, 0, 2);
	}
}
