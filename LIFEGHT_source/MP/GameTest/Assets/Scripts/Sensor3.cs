//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.18051
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

[AddComponentMenu("Sensor3")]
public class Sensor3 : MonoBehaviour
{
	public GameObject[] player = new GameObject[]{null,null};
	public enum BaseColor
	{
		Red, Green, Blue
	};
	public BaseColor color;
	public GameObject toActivate;
	public Activation activation;

	private bool active = false;

	void Start ()
	{
		player = new GameObject[]{GameObject.Find("Player1"),GameObject.Find("Player2")};
		active = false;
	}

	void Update()
	{
		for (int i=0; i<player.Length; i++)
		{
			Light l = player[i].transform.FindChild("PlayerLight"+(i+1)).gameObject.light;
			float light_range = l.range;
			if (Vector3.Distance (player[i].transform.position, transform.position) < light_range/3)
			{
				switch (color)
				{
				case BaseColor.Red:
					if (l.color.r != 1)
					{
						Debug.Log (l.color);
						continue;
					}
					break;
				case BaseColor.Green:
					if (l.color.g != 1)
					{
						continue;
					}
					break;
				case BaseColor.Blue:
					if (l.color.b != 1)
					{
						continue;
					}
					break;
				default:
					break;
				}
//				Debug.Log("activating something?");
//				Debug.Log(active);
				if (!active)
				{
					GetComponent<Level1_activation_sensor3>().activate();
					active = true;
				}
				return;
			}
		}

		if (active)
		{
//			toActivate.GetComponent<Activation> ().deactivate ();
			GetComponent<Level1_activation_sensor3> ().deactivate ();
		}
		active = false;
	}
}