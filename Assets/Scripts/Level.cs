using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	private GameObject _pole,_next,_next2,_key,_Head,_fireball;
	public static int scene = 0;
	private Vector2 pos1,pos2,lspeed;
	private int Enemy_num = 0;

	// Use this for initialization
	void Start () {
		this._pole = GameObject.Find("pole");
		this._next = GameObject.Find("nextStage");
		this._next2 = GameObject.Find("nextStage2");
		this._Head = GameObject.Find("Head");
		this._fireball = GameObject.Find("Fireball");
		this._key = GameObject.Find("Key");

		pos1 = new Vector2(-30,-16);
		pos2 = new Vector2(22,-16);
		lspeed = new Vector2 (-20, 20);
		Enemy_num = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Breakable" && coll.relativeVelocity.magnitude > 14) {
			Destroy(coll.gameObject);
		}
		if (coll.gameObject.tag == "Flag") {
			this._pole.GetComponent<BoxCollider2D>().size = Vector2.zero;
			scene = 2;
			Destroy(this._next);
		}
		if (coll.gameObject.tag == "Enemy") {
			if (scene == 2){
				Enemy_num = 3;
			}
			switch(Enemy_num){
			case 0:
				Destroy(coll.gameObject);
				Enemy_num += 1;
				break;
			case 1:
				coll.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				coll.gameObject.transform.localPosition = pos2;
				Enemy_num += 1;
				break;
			case 2:
				coll.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				coll.gameObject.transform.localPosition = pos1;
				Enemy_num -= 1;
				break;
			case 3:
				Destroy(coll.gameObject);
				break;
			}
		}
		if (coll.gameObject.tag == "needle") {
			Application.LoadLevel (scene);
		}
		if (coll.gameObject.tag == "Leftboundfloor") {
			this._Head.GetComponent<Rigidbody2D>().velocity = lspeed;
		}
		if (coll.gameObject.tag == "Switch") {
			Destroy(this._fireball);
			Enemy.Switch_num = 1;
		}
		if (coll.gameObject.tag == "Key") {
			Destroy(this._key);
			Destroy(this._next2);
		}
		if (coll.gameObject.tag == "Finish") {
			scene = 1;
			Application.LoadLevel (scene);
		}
		if (coll.gameObject.tag == "next") {
			scene = 3;
			Application.LoadLevel(scene);
		}

	}
}