//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.18051
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

[AddComponentMenu("Level1 Cheat Start")]
public class Level1_cheatStart : MonoBehaviour
{
	public GameObject[] player = new GameObject[]{null,null};
	public bool active = false;
	void Start()
	{
		if (!active)
			return;

		player = new GameObject[]{GameObject.Find("Player1"),GameObject.Find("Player2")};

		for (int i=0; i<player.Length; i++)
		{
			player[i].transform.position = GameObject.Find("Level1/Player"+(i+1)+"_StartingPos").gameObject.transform.position;
		}
	}
}
