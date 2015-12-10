using UnityEngine;
using System.Collections;
using System;

public class blackscreen_script : MonoBehaviour {

	public Rigidbody2D teddyBody;	// I'll just add the rigidbody manually in the editor
	public SpriteRenderer sRend; 	// sprite renderer
	public float alphaLvl; 			// alpha visible in editor
	public bool toggleHatch = false;
	private const float speed = 0.15f;
	
	void Start () 
	{
		sRend = GetComponent<SpriteRenderer>();		// Can only get the component once the game starts. 
		sRend.color = new Color (1f, 1f, 1f, 1f);
		GameObject.Find("Body").GetComponent<Rigidbody2D>().isKinematic = true;
	}

	void Update () 
	{
		// ... i regret this already...
		if (toggleHatch == true) {
			updateAlpha ();
			GameObject.Find("Body").GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}
	
	public void updateAlpha()
	{
		if (alphaLvl > 0) 
		{
			alphaLvl -= Time.deltaTime*speed;
			alphaLvl = (float)System.Math.Round (alphaLvl, 3);	// rounding of alpha a little bit
			sRend.color = new Color (1f, 1f, 1f, alphaLvl);		// setting new color. 
		}else if(alphaLvl == 0)
		{ //... fml
			toggleHatch = false;
		}
	}

}
