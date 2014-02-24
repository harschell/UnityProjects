using UnityEngine;
using System.Collections;

public class LampLife : MonoBehaviour {

	//attributes setting from editor
	public float timeToLive;	
	public GameObject[] player = new GameObject[]{null,null};
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
	void Start ()
	{
		//get light
		objLight = transform.FindChild ("GLight").gameObject;
		psystem = transform.FindChild ("PSystem").gameObject;

		//get player
		player = new GameObject[]{
			GameObject.Find("Player1"),
			GameObject.Find("Player2")
		};
		player[0].transform.FindChild ("PlayerLight1").gameObject.light.enabled = false;
		player[1].transform.FindChild ("PlayerLight2").gameObject.light.enabled = false;
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
//			if (Vector3.Distance (player.transform.position, transform.position) < objLight.light.range) {
//					player.GetComponent<PlayerLogic>().addStamina(Time.deltaTime*pointFactor);
			ctime += Time.deltaTime;
			if (ctime > timeToLive)
			{
				ctFOut = 0;
				state = States.IS_TODEATH;
				psystem.particleSystem.enableEmission=true;
				psystem.particleSystem.emissionRate=10;
			}
//				}
				break;
		case States.IS_TODEATH:
			ctFOut += Time.deltaTime;
			float perc = ctFOut/timeToOut;
			float vc = (perc*freq*6.28f);
			vc *= vc;
			float i = perc*(1+Mathf.Cos(vc)*Mathf.Abs(Mathf.Cos(vc)))/2;
			objLight.light.intensity = i;
			Color color;
			color.r=i;//oldColor.r-perc*oldColor.r;
			color.g=i;//oldColor.g-perc*oldColor.g;
			color.b=i;//oldColor.b-perc*oldColor.b;
			color.a=i;//oldColor.a-perc*oldColor.a;
			setColor(color);
//				player.GetComponent<PlayerLogic>().addStamina(Time.deltaTime*pointFactor);
			if(ctFOut>timeToOut)
			{
				objLight.light.intensity=0.0f;
				color.r=0.2f;
				color.g=0.2f;
				color.b=0.2f;
				color.a=0.2f;
				setColor(color);
				ctFOut=0.0f;
				state=States.IS_DEATH;
				psystem.particleSystem.enableEmission=false;
				psystem.particleSystem.emissionRate=0;
				objLight.light.enabled = false;
				Color c;
				player[0].transform.FindChild ("PlayerLight1").gameObject.light.enabled = true;
				player[1].transform.FindChild ("PlayerLight2").gameObject.light.enabled = true;
				player[0].transform.FindChild ("PlayerLight1").gameObject.light.intensity = 1;
				player[1].transform.FindChild ("PlayerLight2").gameObject.light.intensity = 1;
				c = Color.red;
				GameObject.Find("Player1").transform.FindChild("Graphics").gameObject.GetComponent<Renderer>().material.color =  c;
				c.a = 0.5f;
				player[0].transform.FindChild ("PlayerLight1").gameObject.light.color = c;
				c = Color.green;
				GameObject.Find("Player2").transform.FindChild("Graphics").gameObject.GetComponent<Renderer>().material.color =  c;
				c.a = 0.5f;
				player[1].transform.FindChild ("PlayerLight2").gameObject.light.color = c;
		    }
			break;
		case States.IS_DEATH:
			break;
		default :
				break;
		}
	}
}
