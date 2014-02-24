using UnityEngine;
using System.Collections;

[AddComponentMenu("Light/Life")]


public class LightLife : MonoBehaviour {

	//attributes setting from editor
	public float energyPerTime=0.1f;	
	public GameObject[] player = new GameObject[2]{null, null}; 
	public int players_count = 2;
//	public float timeToOut;
//	public float pointFactor = 2.0f;

	//local attributes 
//	private float ctime;
//	private float ctFOut;
//	private float oldints;
	//states
//	public enum States{
//		IS_LIVE,
//		IS_TODEATH,
//		IS_DEATH
//	};
	//state
//	public States state = States.IS_LIVE;

	// Use this for initialization
	void Start () {
		player = new GameObject[]{GameObject.Find("Player1"),GameObject.Find("Player2")};
//		oldints = light.intensity;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!light.enabled)
		{
			return;
		}
		for(int i=0; i<players_count; i++)
		{
//			Debug.Log("Light triggered? "+light.intensity);
			if (Vector3.Distance (player[i].transform.position, transform.position) < light.range)
			{
//				Debug.Log("Light triggered!!! "+light.intensity);
			
				light.intensity -= player[i].GetComponent<PlayerLogic>().addStamina(30*Time.deltaTime*energyPerTime)/30;
			}
			if(light.intensity<=0.0)
			{
				player[i].GetComponent<PlayerLogic>().addStamina(-light.intensity*30);
				light.intensity = 0;
				light.enabled = false;
			}
		}
//		switch (state) {
//		case States.IS_LIVE:
//				if (Vector3.Distance (player.transform.position, transform.position) < light.range) {
//				    ctime += Time.deltaTime;
//					player[i].GetComponent<PlayerLogic>().addStamina(Time.deltaTime*pointFactor);
//					if (ctime > timeToLive)
//								state = States.IS_TODEATH;
//				}
//				break;
//		case States.IS_TODEATH:
//			light.intensity = oldints-(ctFOut/timeToOut)*oldints;
//			    ctFOut += Time.deltaTime;
//				player[i].GetComponent<PlayerLogic>().addStamina(Time.deltaTime*pointFactor);
//				if(light.intensity<=0.0)
//					state=States.IS_DEATH;
//				break;
//		case States.IS_DEATH:
//			
//			light.enabled = false;
//			break;
//		default :
//				break;
//		}
	}
}
