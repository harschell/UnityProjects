using UnityEngine;
using System.Collections;

[AddComponentMenu("Prefab Instantiator")]
public class PrefabInstantiator : MonoBehaviour
{
	public int howMany=0;
	public Vector3 displacement;
	public GameObject prefab;

	void Start()
	{
		Vector3 disp = new Vector3(0,0,0);
		for (int i=0; i<howMany; i++)
		{
			disp.x += displacement.x;
			disp.y += displacement.y;
			disp.z += displacement.z;
			Instantiate(
				prefab,
				new Vector3(
					transform.position.x + disp.x,
					transform.position.y + disp.y,
					transform.position.z + disp.z),
				Quaternion.identity);
		}
	}
}