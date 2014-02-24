using UnityEngine;
using System.Collections;

public class LightLife : MonoBehaviour {

	//attributes setting from editor
	public float timeToLive;	
	public GameObject player=null; 
	public float timeToOut;
	public float pointFactor = 2.0f;

	//local attributes 
	private float ctime;
	private float ctFOut;
	private float oldints;
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
		if (player == null) {
			player=GameObject.Find("Player");		
		}
		oldints = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case States.IS_LIVE:
				if (Vector3.Distance (player.transform.position, transform.position) < light.range) {
				    ctime += Time.deltaTime;
					player.GetComponent<PlayerLogic>().addStamina(Time.deltaTime*pointFactor);
					if (ctime > timeToLive)
								state = States.IS_TODEATH;
				}
				break;
		case States.IS_TODEATH:
			light.intensity = oldints-(ctFOut/timeToOut)*oldints;
			    ctFOut += Time.deltaTime;
				player.GetComponent<PlayerLogic>().addStamina(Time.deltaTime*pointFactor);
				if(light.intensity<=0.0)
					state=States.IS_DEATH;
				break;
		case States.IS_DEATH:
			
			light.enabled = false;
			break;
		default :
				break;
		}
	}
}
