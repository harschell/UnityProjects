using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class PlayerLogic : MonoBehaviour {

	public AudioSource soundCuore = null;

	//public attributes
	public float stamina;
	public float maxStamina;
	public float intensity;
	public float toEnable;
	public float toSleep=0.0f;
	public float timeToChangeSkyBox=0.0f;
	public bool enStartColor;
	public Color startColor;
	public Color disableColor;
	//
	public enum States{
		DISABLE,
		GO_ENABLE,
		ANIM_ENABLE,
		ENABLE
	};
	public States anims=States.ENABLE;
	private float atime=0.0f;
	//lights
	public Color[] lights;

	//private attrs
	private GameObject playerLight;
	private int selectIndex;
	private Material[] skys;
	private Material[] materials;
	private Material mmesh;
	private Color lastcolor;

	//utility methos
	public void addStamina(float sdt){
		stamina+=sdt;
		stamina = Mathf.Min (maxStamina, stamina);
	}
	public int getCurrentLight(){
		return selectIndex;
	}
	public Color getCurrentColorLight(){
		return lights[selectIndex];
	}
	public Color getColorLight(int id){
		return lights[id%lights.Length];
	}

	void setColor(Color c){
		mmesh.SetColor ("_Color", c);
	}
	Color getColor(){
		return mmesh.GetColor ("_Color");
	}
	// Use this for initialization
	void Start () {
		if (soundCuore != null) {
			soundCuore.PlayDelayed (2.0f);
		}
		playerLight=transform.FindChild("PlayerLight").gameObject;
		playerLight.light.color=lights[0];
		mmesh=transform.FindChild("Mesh").gameObject.renderer.material;
		//load skybox
		bool flag = File.Exists (Application.dataPath + "/Standard Assets/BlueSky/BlueSky.mat");
		Debug.Log ("Flag: " + flag);
		Debug.Log ("Path: " + Application.dataPath + "/Standard Assets/BlueSky/BlueSky.mat");
		materials = new Material[5];
		materials[0] = Resources.Load("sky/SkyBox", typeof(Material)) as Material;
		materials[1] = Resources.Load("RedSky/RedSky", typeof(Material)) as Material;
		materials[2] = Resources.Load("OrangeSky/OrangeSky", typeof(Material)) as Material;
		materials[3] = Resources.Load("BlueSky/BlueSky", typeof(Material)) as Material;
		materials[4] = Resources.Load("GreenSky/GreenSky", typeof(Material)) as Material;
		Debug.Log (materials[0]);
		Debug.Log (materials[1]);
		Debug.Log (materials[2]);
		Debug.Log (materials[3]);
		Debug.Log (materials[4]);
	}

	public void enablePlayer(){
		 if (anims == States.DISABLE)
						anims = States.GO_ENABLE;
	}
	// Update is called once per frame
	void Update () {
		
		Color color;
		color.a=1.0f;

		switch (anims) {
		case States.DISABLE:
			playerLight.light.intensity=0.0f;
			atime=0.0f;
			setColor(disableColor);
			break;
		case States.GO_ENABLE:
			if(enStartColor) setColor(startColor);
			if(atime >= toSleep){
				lastcolor=getColor();
				atime=0.0f;
				anims=States.ANIM_ENABLE;
			} 
			atime+=Time.deltaTime;
			break;
		case States.ANIM_ENABLE:
			if(atime>toEnable) {
				anims=States.ENABLE;
				break;
			}
			atime+=Time.deltaTime;
			playerLight.light.intensity = (atime / toEnable) * intensity;
			color.r=(1.0f-lastcolor.r)*playerLight.light.intensity+lastcolor.r;
			color.g=(1.0f-lastcolor.g)*playerLight.light.intensity+lastcolor.g;
			color.b=(1.0f-lastcolor.b)*playerLight.light.intensity+lastcolor.b;
			;
			setColor(color);
			break;
		case States.ENABLE:
			for (int i=0; i < lights.Length-1; i++) {
				if (Input.GetKeyDown (KeyCode.Alpha1 + i)) {
					selectIndex = i+1;
					RenderSettings.skybox=materials[i+1];
					playerLight.light.color = lights [selectIndex];
					setColor(lights [selectIndex]);
				}
			}
			playerLight.light.intensity = (stamina / maxStamina) * intensity;
			float factor=1.0f-(stamina / maxStamina);
			if(soundCuore != null){
				soundCuore.audio.volume=factor;
			}
			stamina -= Time.deltaTime;
			atime=0.0f;
		break;
		}

	}
}
