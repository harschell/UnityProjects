using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Restart : MonoBehaviour
{
	void Update()
	{
		if (Input.GetButton ("Restart"))
						Application.LoadLevel (0);
	}
}