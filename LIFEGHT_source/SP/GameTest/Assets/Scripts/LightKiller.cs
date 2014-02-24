using UnityEngine;
using System.Collections;

public class LightKiller : MonoBehaviour {

	public GameObject toHide;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (light.intensity <= 0.0) {
			toHide.renderer.enabled=false;
		}
	}
}
