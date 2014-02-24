using UnityEngine;
using System.Collections;

[AddComponentMenu("Level1_activation_sensor3")]
public class Level1_activation_sensor3 : Activation
{
	public GameObject sensorToActivate;
	public GameObject light_cylinder_long;
	public GameObject light_cylinder;
	public Light light;
	public Light light_long;
	public GameObject  door;

	void Start()
	{
		deactivate ();
	}

	void Update()
	{
		if (!door.transform.FindChild ("Graphics").gameObject.renderer.enabled
		    &&
		    light_cylinder.renderer.enabled)
		{
			light_cylinder_long.renderer.enabled = true;
			light_long.enabled = true;
			light_cylinder.renderer.enabled = false;
			light.enabled = false;
		}
		if (door.transform.FindChild ("Graphics").gameObject.renderer.enabled
		    &&
		    light_cylinder_long.renderer.enabled)
		{
			light_cylinder_long.renderer.enabled = false;
			light_long.enabled = false;
			light_cylinder.renderer.enabled = true;
			light.enabled = true;
		}
	}

	public void activate()
	{
		if (door.transform.FindChild("Graphics").gameObject.renderer.enabled)
		{
			light_cylinder.renderer.enabled = true;
			light.enabled = true;
		} else
		{
			light_cylinder_long.renderer.enabled = true;
			light_long.enabled = true;
		}
	}
	
	public void deactivate()
	{
		light_cylinder.renderer.enabled = false;
		light.enabled = false;
		light_cylinder_long.renderer.enabled = false;
		light_long.enabled = false;
	}
}
