using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float health = 100.0f;
	public float healthDecayRate = 5.0f;

	public Texture tex;
	
	private float startingHealth;
	private float decayModifier;

	private float newAlpha = 0.0f;
	private float rndXoffset = 0.0f;
	private float rndYoffset = 0.0f;


	void OnGUI(){

		Color oldGUIColor = GUI.color;
		GUI.color = new Color(1f, 1f, 1f, newAlpha);
		GUI.DrawTexture(new Rect((10f - rndXoffset), (10f - rndYoffset), Screen.width, Screen.height), tex);
		GUI.color = oldGUIColor;
		//mainTextureOffset = new Vector2( rndXoffset, rndYoffset );
			
	}


	// Use this for initialization
	void Start () {
		startingHealth = health;

		decayModifier = startingHealth / healthDecayRate;

	}

	void Update () 
	{
		rndXoffset = Random.Range(0,10);
		rndYoffset = Random.Range(0,10);
	}
	
	public void DecreaseHealth()
	{
		health -= decayModifier * Time.deltaTime;

		newAlpha = 1.0f - (health / startingHealth);

		//LooseCondition
		if ( health <= 0.0f )
		{
			health = 0.0f;

			Debug.Log("Player is dead.");
			//load Game over Screen

		}
	}


	public void IncreaseHealth()
	{
		health += decayModifier * Time.deltaTime;
		
		newAlpha = 1.0f - (health / startingHealth);


		if ( health >= startingHealth )
		{
			health = startingHealth;
			
		}
	}
}
