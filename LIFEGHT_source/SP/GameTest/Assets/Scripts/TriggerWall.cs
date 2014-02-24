using UnityEngine;
using System.Collections;

public class TriggerWall : MonoBehaviour {

	//publics 
	public int idColorObj=0;
	public GameObject player=null; 
	public bool eqColor=false;
	public float fedinTime=0.0f;
	public float fedoutTime=0.0f;
	public bool enableDistance=false;
	public float distance=0.0f;
	public enum States{
		EQUAL,
		NOT_EQUAL
	};
	public States typeTrigger;
	//
	public enum AnimStates{
		TO_SHOW,
		SHOW,
		TO_HIDE,
		HIDE
	};
	public AnimStates sanim=AnimStates.HIDE;
	private float atime=0.0f;

	void UpdateAnimation(){
		Color color;
		switch (sanim) {
		case AnimStates.TO_SHOW:
			atime+=Time.deltaTime;
			color=getColor();
			color.a=(atime/fedinTime);
			if(atime>fedinTime){
				color.a=1.0f;
				sanim=AnimStates.SHOW;
			}
			setColor(color);
			//enable collision and grf
			collider.enabled=renderer.enabled=true;
			//Debug.Log ("to show color:" + color);
			break;
		case AnimStates.SHOW: 
			atime=0.0f;
		break;
		case AnimStates.TO_HIDE:
			atime+=Time.deltaTime;
			color=getColor();
			color.a=1.0f-(atime/fedoutTime);
			if(atime>fedoutTime){
				color.a=0.0f;
				sanim=AnimStates.HIDE;
			}
			setColor(color);
			//Debug.Log ("to hide color:" + color);
			break;
		case AnimStates.HIDE: 
			//enable collision and grf
			atime=0.0f;
			collider.enabled=renderer.enabled=false;
			break;
		default: break;
		};
	}
	// Use this for initialization
	void Start () {
		if (player == null) {
			player=GameObject.Find("Player");		
		}
		//setcolor
		if(eqColor)
			renderer.material.SetColor("_Color",
			                           player.GetComponent<PlayerLogic>().getColorLight((int)idColorObj));

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("isHide:" + isHide ());
		//Debug.Log ("isShow:" + isShow ());
		if(!enableDistance || Vector3.Distance(transform.position,player.transform.position)<distance) {
		switch (typeTrigger) {
				case States.EQUAL:
					if(idColorObj==player.GetComponent<PlayerLogic>().getCurrentLight()){
						if(isHide())
							toShow();
					}
					else if(isShow())
						toHide();
					break;
				case States.NOT_EQUAL:
					if(idColorObj!=player.GetComponent<PlayerLogic>().getCurrentLight()){
						if(isHide())
							toShow();
					}
					else if(isShow())
						toHide();
					break;
				default: break;
				};
				UpdateAnimation();
		}
	}
	
	bool isShow(){
		return sanim == AnimStates.SHOW || sanim == AnimStates.TO_SHOW;
	}
	bool isHide(){
		return sanim == AnimStates.HIDE || sanim == AnimStates.TO_HIDE;
	}
	void toShow(){
		atime=0.0f;
		sanim = AnimStates.TO_SHOW;
	}
	void toHide(){
		atime=0.0f;
		sanim = AnimStates.TO_HIDE;
	}
	void setColor(Color c){
		renderer.material.SetColor ("_Color", c);
	}
	Color getColor(){
		return renderer.material.GetColor ("_Color");
	}


}
