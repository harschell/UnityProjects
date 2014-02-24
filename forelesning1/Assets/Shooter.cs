using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public Rigidbody bullet;
	public float power = 1500f;
	public float moveSpeed = 50f;
	
	
	// Update is called once per frame
	void Update () {
		float z = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed * -1;
		
		transform.Translate(0,0,z);
		
		if(Input.GetButtonUp("Fire1")){
			Rigidbody instance = Instantiate(bullet, transform.position,transform.rotation) as Rigidbody;
			Vector3 up = transform.up;//.TransformDirection(Vector3.up);
			instance.AddForce(up * power);
		}
		
		
	}
}
