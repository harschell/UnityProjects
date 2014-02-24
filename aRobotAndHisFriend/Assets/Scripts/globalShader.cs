using UnityEngine;
using System.Collections;

public class globalShader : MonoBehaviour {
	
	public Shader toon;
	
	// Use this for initialization
	void Start () {
		if(GameObject.FindWithTag("Player")){
			Camera.main.SetReplacementShader(toon,null);
	}
	
}
}
