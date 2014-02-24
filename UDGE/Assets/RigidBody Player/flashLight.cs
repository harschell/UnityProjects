using UnityEngine;
using System.Collections;

public class flashLight : MonoBehaviour {

	private Light flashlight;
	private bool on = false;
	private int flickr = 0;
	public float battery;

	public float minFlickerSpeed = 1f;
	public float maxFlickerSpeed = 2.0f;

 
	void Start(){
  		flashlight = GetComponentInChildren<Light>();
	}
 
	void Update(){
   
		// recharge battery by pressing R.
		if(Input.GetKeyDown(KeyCode.R) && battery < 100f){
			battery += 10f;
		}

		if(battery > 100){
			battery = 100;
		}


		//Turn on flashlight by pressing F.
		if(Input.GetKeyDown(KeyCode.F) && battery != 0){
        	on = !on;
			if(on){
				StartCoroutine("Battery");
			}
		}


		//Checks if flashlight is on or off.
		if(on){
   		 	flashlight.light.enabled = true;
			StartCoroutine("FlickerFlashlight");
		}else if(!on)
    		flashlight.light.enabled = false;
		
		// If battery is empty, turn of flashlight.
		if(battery == 0){
			on = false;
		}



	}
	
	IEnumerator Battery() {
		while (battery > 0 && on){
			battery -= 0.1f;
			flashlight.light.intensity = battery/25;
			yield return new WaitForSeconds(.1f);
			Debug.Log(battery);
		}
	}

	//flickers flashlight.
	IEnumerator FlickerFlashlight() {

		flickr++;

		if(battery < 30 && on && flickr > (Random.Range(100f, 500f)) ){
			flashlight.light.enabled = true;
			yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
			flashlight.light.enabled = false;
			yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
			flickr = 0;
		}
	}
}