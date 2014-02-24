using UnityEngine;
using System.Collections;

public class CollectingPaperWithSphereCast : MonoBehaviour 
{
	public int papers = 0; // counter of how many papers collected
	public int papersToWin = 8; // total number of papers in the scene
	
	public float distanceToPaper = 5.5f; // maximum distance that the raycast will detect
	
	public float sphereRadius = 1.0f; // the width of the sphere that is being SphereCast
	
	void Update() 
	{
		if ( Input.GetMouseButtonDown(0) || Input.GetKeyDown( KeyCode.E ) )
		{
			RaycastHit hit;
			
			Ray rayOrigin = Camera.main.ScreenPointToRay( new Vector3( Screen.width * 0.5f, Screen.height * 0.5f, 0 ) );
			
			if ( Physics.SphereCast( rayOrigin, sphereRadius, out hit, distanceToPaper ) )
			{
				//Debug.Log( "SphereCast Hit : " + hit.collider.gameObject.name );
				//Debug.DrawLine( Camera.main.transform.position, hit.point, Color.red, 1.5 );
				
				if ( hit.collider.gameObject.name == "Paper" )
				{
					//Debug.Log( "SPHERE hit Paper for sure" );
					
					papers += 1;
					
					Destroy( hit.collider.gameObject );
					
					if ( papers == papersToWin )
					{
						Debug.Log( "You have collected All Papers !" );
						
						// load Win Scene here !!!!
					}
				}
			}
		}
	}
}



/*

using UnityEngine;
using System.Collections;

public class CollectingPaperWithSphereCast : MonoBehaviour {

	public int papers = 0;	// counter of how many papers collected
	public int papersToWin = 8;	// total number of papers in the scene

	public float distanceToPaper = 5.5f;	// maximum distance that the raycast will detect

	public float sphereRadius = 1.0f;	// the width of the sphere that is being SphereCast



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown("Fire1"))
		{

			RaycastHit hit;
			
			Ray rayOrigin = Camera.main.ScreenPointToRay( new Vector3( Screen.width * 0.5f, Screen.height * 0.5f, 0 ) );
			
			//if ( Physics.SphereCast( rayOrigin, sphereRadius, out hit, distanceToPaper ) )
			{
			//	Debug.Log( "SphereCast Hit : " + hit.collider.gameObject.name );

			}
		}
	}
}*/
