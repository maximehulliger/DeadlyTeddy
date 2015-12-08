using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ScreenInput.hasSwipe||Input.GetKey ("return")) {
			Level.scene = 4;
			Application.LoadLevel (Level.scene); 
		}
	}
}
