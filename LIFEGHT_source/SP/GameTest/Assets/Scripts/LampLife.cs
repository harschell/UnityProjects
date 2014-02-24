using UnityEngine;
using System.Collections;

public class LampLife : MonoBehaviour {
	
	public AudioSource soundLampBreak;
	public AudioSource soundPaura;
	public AudioSource soundStrillo;
	public AudioSource soundBattito;
	public AudioSource soundSospiro;

	//attributes setting from editor
	public float timeToLive;	
	public GameObject player=null; 
	public float timeToOut;
	public float pointFactor = 2.0f;
	public float freq=1.0f;

	//local attributes 
	private float ctime=0.0f;
	private float ctFOut=0.0f;
	private float oldints;
	private GameObject objLight=null; 
	private GameObject psystem=null; 
	private Color oldColor;
	//states
	public enum States{
		IS_LIVE,
		IS_TODEATH,
		IS_DEATH
	};
	//state
	public States state = States.IS_LIVE;


	// Use this for initialization
	void Start () {
		soundPaura.PlayDelayed (3.0f);
		soundStrillo.PlayDelayed (6.0f);
		soundBattito.PlayDelayed (7.0f);
		soundSospiro.PlayDelayed (8.0f);
		//get light
		objLight = transform.FindChild ("GLight").gameObject;
		psystem = transform.FindChild ("PSystem").gameObject;
		//get player
		if (player == null) {
			player=GameObject.Find("Player");		
		}
		//get intensity
		oldints = objLight.light.intensity;
		oldColor = getColor ();
	}
	//set color
	void setColor(Color c){
		renderer.material.SetColor ("_Color", c);
	}
	//get color
	Color getColor(){
		return renderer.material.GetColor ("_Color");
	}
	// Update is called once per frame
	void Update () {
		switch (state) {
		case States.IS_LIVE:
			if (Vector3.Distance (player.transform.position, transform.position) < objLight.light.range) {
				    ctime += Time.deltaTime;
					player.GetComponent<PlayerLogic>().addStamina(Time.deltaTime*pointFactor);
					if (ctime > timeToLive){
					state = States.IS_TODEATH;
					psystem.particleSystem.enableEmission=true;
					psystem.particleSystem.emissionRate=10;
					}
				}
				break;
		case States.IS_TODEATH:
				ctFOut += Time.deltaTime;
				float factor=Mathf.Cos ((ctFOut/timeToOut)*freq);
			    objLight.light.intensity = 1.0f-factor*oldints;
				Color color;
				color.r=oldColor.r-factor*oldColor.r;
				color.g=oldColor.g-factor*oldColor.g;
				color.b=oldColor.b-factor*oldColor.b;
				color.a=oldColor.a-factor*oldColor.a;
				setColor(color);
				player.GetComponent<PlayerLogic>().addStamina(Time.deltaTime*pointFactor);
				if(ctFOut>timeToOut){
					objLight.light.intensity=0.0f;
					color.r=0.0f;
					color.g=0.0f;
					color.b=0.0f;
					color.a=0.0f;
					setColor(color);
					ctFOut=0.0f;
					state=States.IS_DEATH;
					psystem.particleSystem.enableEmission=false;
					psystem.particleSystem.emissionRate=0;
					soundLampBreak.Play();
			    }
				break;
		case States.IS_DEATH:
				objLight.light.enabled = false;
				player.GetComponent<PlayerLogic>().enablePlayer();
			break;
		default :
				break;
		}
	}
}
