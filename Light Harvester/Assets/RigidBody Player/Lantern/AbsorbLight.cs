using UnityEngine;
using System.Collections;

public class AbsorbLight : MonoBehaviour {
	
	public Transform target;
	public AudioClip absorbSound;

	private Lantern lanternScript;
	private float range;
	private Light lightSource;
	private Transform lightTransform;
	private float maxRangeSqr;
	//private bool lightSourceDepleted = false;

	private Transform targetLantern;
//	private Color colorOrginal =  new Color(0.92f, 0.81f, 0.6f, 1f);
	private Color colorBlue = new Color(0.46f, 0.64f, 0.89f, 1f);

	

	// Use this for initialization
	void Start () 
	{
		lightTransform = transform;
		
		lightSource = GetComponent<Light>();
		
		range = light.range;
		
		maxRangeSqr = (range * range)/ 1.25f;
		
		
		GameObject targetObject = GameObject.Find( "Player" );
		
		if ( targetObject )
		{
			target = targetObject.transform;

			lanternScript = target.GetComponentInChildren< Lantern >();
		}
		else
		{
			Debug.Log( "no object named player was found" );
		}


		GameObject targetObject2 = GameObject.Find( "Magic_Lantern" );
		
		if ( targetObject )
		{
			targetLantern = targetObject2.transform ;
			
			lanternScript = target.GetComponentInChildren< Lantern >();
		}
		else
		{
			Debug.Log( "no object named player was found" );
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		CheckDistance();
	}
	
	
	void CheckDistance()
	{	
		float sqrDist = ( target.position - lightTransform.position ).sqrMagnitude;
		
		// sjekker avstanden og om lyset allerede er slukket.
		if ( sqrDist < maxRangeSqr && lightSource.light.intensity > 0 ){
			
			
			RaycastHit hit;
			
			if(Physics.Linecast (lightTransform.position,target.position,out hit) ){
				
				Debug.DrawLine(lightTransform.position,target.position,Color.green);
				
				if( hit.collider.gameObject.name == target.name )
				{
				
					if (Input.GetButtonDown( "Absorb" ) && lightSource.light.intensity > 0 && lanternScript.on)
					{
						StartCoroutine( "Absorb" );
						audio.PlayOneShot(absorbSound);
						light.color = Color.Lerp(light.color, colorBlue, 10f);
						StartCoroutine( "AbsorbLaser" );
					}
				}	
			}
		}	
	}



	IEnumerator Absorb() 
	{
		while ( lightSource.light.intensity > 0 && lanternScript.on )
		{
			lightSource.light.intensity -= 0.1f;
			lanternScript.lumen += 1.1f;
			lightSource.light.range -= 0.1f;
			yield return new WaitForSeconds(.12f);
		}
	}

	IEnumerator AbsorbLaser()
	{
		while ( lightSource.light.intensity > 0 && lanternScript.on )
		{
			transform.position = Vector3.MoveTowards(transform.position, targetLantern.position + new Vector3(0f,0.135984f), 0.1f);
		
			yield return new WaitForSeconds(.01f);


		}
	}
}
