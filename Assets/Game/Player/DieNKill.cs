using UnityEngine;
using System.Collections;

public class DieNKill : MonoBehaviour { 
	
	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter2D(Collision2D collisionInfo) {
		if (collisionInfo.gameObject.tag == "Enemy"|| collisionInfo.gameObject.tag == "needle") {        //when have a collision with Enemy K
			Application.LoadLevel (Level.scene);
		}
	}
}


