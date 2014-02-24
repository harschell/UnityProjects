using UnityEngine;
using System.Collections;

public class Lantern : MonoBehaviour {


	public float lumen;
	public AudioClip equipSound;

	[HideInInspector]
	public bool on = true;
	[HideInInspector]
	public Light lantern;


	
	
	void Start(){
		lantern = GetComponentInChildren<Light>();

		StartCoroutine("Lumen");

	}
	
	void Update(){
		
		// recharge lantern by pressing R.
		if(Input.GetKeyDown(KeyCode.R) && lumen < 100f){
			lumen += 10f;
		}
		
		if(lumen > 100){
			lumen = 100;
		}

		if(lumen < 0){
			lumen = 0;
		}
		
		
		//Turn on lantern by pressing F.
		if(Input.GetButtonDown("Lantern")){
			on = !on;
			audio.PlayOneShot(equipSound);
			if(on){
				StartCoroutine("Lumen");
			}
		}
		
		
		//Checks if flashlight is on or off.
		if( on )
		{
			lantern.light.enabled = true;
			gameObject.renderer.enabled = true;
		}
		else if( !on )
		{
			lantern.light.enabled = false;
			lantern.light.range = 0f;
			gameObject.renderer.enabled = false;
		}
		
		
	}

	/*
	void OnGUI(){
		
		GUI.Label(new Rect(10,10,200,20), "Battery: " + battery.ToString("0.0") );
		
	}

	*/
	
	IEnumerator Lumen() {
		//lumen >= 0 && 
		while ( on ){
			lumen -= 0.1f;
			lantern.light.intensity = lumen/30;
			lantern.light.range = lumen / 5;
			yield return new WaitForSeconds(.1f);
		}
	}




	
}