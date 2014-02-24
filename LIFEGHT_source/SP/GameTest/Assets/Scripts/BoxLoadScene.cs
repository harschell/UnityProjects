using UnityEngine;
using System.Collections;

public class BoxLoadScene : MonoBehaviour {
	
	public GameObject player=null; 
	public string nextlivel;
	public bool hide=false;

	// Use this for initialization
	void Start () {
		if (player == null) {
			player=GameObject.Find("Player");		
		}
		if (hide) renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(player.transform.position.x-transform.position.x) < transform.localScale.x
		    &&
		    Mathf.Abs(player.transform.position.y-transform.position.y) < transform.localScale.y
		    &&
		    Mathf.Abs(player.transform.position.z-transform.position.z) < transform.localScale.z)
		{
			//Application.dataPath+'/
			Application.LoadLevel (nextlivel);
		}
	}
}
