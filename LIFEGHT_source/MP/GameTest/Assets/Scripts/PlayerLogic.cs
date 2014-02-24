using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerLogic : MonoBehaviour {

	//public attributes
	public float[] stamina = new float[]{10,10};
	public float[] maxStamina= new float[]{10,10};
	public float[] intensity= new float[]{1,1};
	//public string[] keyChangeLight="e";
	public int player_num=1;

	//lights
	public Color[] lights = new Color[3];

	//private attrs
	private string[] keys = {"Red", "Green", "Blue"};
	private Light[] playerLight= new Light[2]{null, null};
	private int[] selectIndex = new int[2]{0,1};

	//utility methos
	public float addStamina(float s)
  	{
		if (stamina [player_num - 1] + s > maxStamina [player_num - 1])
		{
			s = maxStamina[player_num-1] - stamina[player_num-1];
		}
		stamina[player_num-1]+=s;
		return s;
	}
	public int getCurrentLight(){
		return selectIndex[player_num-1];
	}
	public Color getCurrentColorLight(){
		return lights[selectIndex[player_num-1]];
	}
	public Color getColorLight(int id){
		return lights[id%lights.Length];
	}
	// Use this for initialization
	void Start ()
	{
		lights [0] = Color.red;
		lights [1] = Color.green;
		lights [2] = Color.blue;
		for (int i=0; i!=3; i++)
			lights [i].a = 1f;
		playerLight=new Light[]{
			GameObject.Find("PlayerLight1").gameObject.light,
			GameObject.Find("PlayerLight2").gameObject.light
		};
		playerLight[0].color=lights[0];
		playerLight[1].color=lights[1];
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log (lights[player_num-1].Length);
		for (int i=0; i!=lights.Length; ++i)
		{
//			Debug.Log(player_num+" "+keys[i]+""+player_num+" "+Input.GetAxis (keys[i]+""+player_num));
			if (Input.GetAxis (keys[i]+""+player_num)==1)
			{
				selectIndex[player_num-1] = i;
				playerLight[player_num-1].color = lights [i];
				GameObject p;
				p = GameObject.Find("Player"+player_num).gameObject;
				p.transform.FindChild("Graphics").gameObject.GetComponent<Renderer>().material.color = lights[i];
			}
		}
//		Debug.Log (player_num+" "+stamina[player_num-1]);
		playerLight[player_num-1].intensity =
			(stamina[player_num-1]/
			 maxStamina[player_num-1])*
				intensity[player_num-1];
		stamina[player_num-1] -= Time.deltaTime;
		if (stamina[player_num-1]<0)
			stamina[player_num-1]=0;
	}
}
