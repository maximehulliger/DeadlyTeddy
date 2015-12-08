using UnityEngine;
using System.Collections;

public class DieNKill : MonoBehaviour {
	private GameObject _key,_pole,_next,_next2;
	
	// Use this for initialization
	void Start () {
		this._key = GameObject.Find("Key");
		this._pole = GameObject.Find("pole");
		this._next = GameObject.Find("nextStage");
		this._next2 = GameObject.Find("nextStage2");
	}

	void OnCollisionEnter2D(Collision2D collisionInfo) {
		if (collisionInfo.gameObject.tag == "Enemy"|| collisionInfo.gameObject.tag == "needle") {        //when have a collision with Enemy K
			Application.LoadLevel (Level.scene);
		}
		
		if (collisionInfo.gameObject.tag == "Finish") {
			Level.scene = 1;
			Application.LoadLevel (Level.scene);
		}
		if (collisionInfo.gameObject.tag == "Key") {
			Destroy(this._key);
			Destroy(this._next2);
		}
		if (collisionInfo.gameObject.tag == "Flag") {
			this._pole.GetComponent<BoxCollider2D>().size = Vector2.zero;
			Level.scene = 2;
			Destroy(this._next);
		}
	}
}


