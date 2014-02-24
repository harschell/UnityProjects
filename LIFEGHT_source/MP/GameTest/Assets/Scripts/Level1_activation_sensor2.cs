using UnityEngine;
using System.Collections;

[AddComponentMenu("Level1_activation_sensor2")]
public class Level1_activation_sensor2 : Activation
{
	public GameObject  door;
	public void activate()
	{
		door.GetComponent<Level1_activation_door>().activate ();
	}
	
	public void deactivate()
	{
		door.GetComponent<Level1_activation_door>().deactivate ();
	}
}
