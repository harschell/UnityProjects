using UnityEngine;
using System.Collections;

public class Squirm : MonoBehaviour {
	
	private Vector3 moveDirection = Vector3.zero;
	private Transform mesh;

	void Awake () {
		mesh = transform.Find("BONE_Body_0");
	}

	// Use this for initialization
	void Start () {
		animation["sala_Squirm"].speed = 4;
		setFace(3);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Jump")){
			animation["sala_Squirm"].wrapMode = WrapMode.Once;
			animation.Play("sala_Squirm"); 
			animation.CrossFade("sala_Swim");
		}
	}

	void setFace(int newFace){
		//Sala's facial expressions are stored in an Array of Textures called "faceTex".
		//The material for Sala's face is stored on "BONE_Body_0", which was defined in the Awake function.
		//mesh.renderer.materials[0].mainTexture = faceTex[newFace];
	}
}
