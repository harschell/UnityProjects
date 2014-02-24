using UnityEngine;
using System.Collections;

[AddComponentMenu("Level1_activation_door")]
public class Level1_activation_door : Activation
{
	public void activate()
	{
		transform.FindChild("Graphics").gameObject.renderer.enabled = false;
		transform.FindChild("Graphics").gameObject.GetComponent<BoxCollider>().enabled = false;
	}

	public void deactivate()
	{
		transform.FindChild("Graphics").gameObject.renderer.enabled = true;
		transform.FindChild("Graphics").gameObject.GetComponent<BoxCollider>().enabled = true;
	}
}