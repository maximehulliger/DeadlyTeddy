 using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	private GameObject _pole,_next,_next2,_key,_Head,_Body,_Thunder;
	private static bool as_Flag = false;
	private static Vector2 flag_pos;
	private Vector2 lspeed;
	public static int scene = 0; 
	private int Enemy_num = 0;

	// Use this for initialization
	void Start () {
		this._pole = GameObject.Find("pole");
		this._next = GameObject.Find("nextStage");
		this._next2 = GameObject.Find("nextStage2");
		this._Head = GameObject.Find("Head");
		this._Body = GameObject.Find("Body");
//		this._thunder = GameObject.Find("thunder");
		this._Thunder = GameObject.Find("Thunder");
		this._key = GameObject.Find("Key");

		if (as_Flag) {
			this._Body.transform.parent.position = flag_pos;
		}
		Enemy_num = 0;
		lspeed = new Vector2 (-20, 20);
		scene = Application.loadedLevel;
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Breakable" && coll.relativeVelocity.magnitude > 14) {
			Destroy(coll.gameObject);
		}
		if (coll.gameObject.tag == "needle") {
			Application.LoadLevel (scene);
		}
		if (coll.gameObject.tag == "Leftboundfloor") {
			this._Head.GetComponent<Rigidbody2D>().velocity = lspeed;
		}
		if (coll.gameObject.tag == "Switch") {
			Destroy(this._Thunder);
			Enemy.Switch_num = 1;
		}
		if (coll.gameObject.tag == "Key") {
			Destroy(this._key);
			Destroy(this._next2);
		}
		if (coll.gameObject.tag == "Finish") {
			scene = 1;
			as_Flag = false;
			Application.LoadLevel (scene);
		}
		if (coll.gameObject.tag == "next") {
			scene = 2;
			as_Flag = false;
			Application.LoadLevel(scene);
		}
		if (coll.gameObject.tag == "Flag") {
			this._pole.GetComponent<BoxCollider2D>().size = Vector2.zero;
			as_Flag = true;
			flag_pos = this._Body.transform.position;
			Destroy(this._next);
		}
		if (coll.gameObject.tag == "Flag_1") {
			this._pole.GetComponent<BoxCollider2D>().size = Vector2.zero;
			flag_pos = this._Body.transform.position;
			as_Flag = true; 
		}

	}
}