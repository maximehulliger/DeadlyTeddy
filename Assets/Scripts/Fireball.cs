using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	private Vector2 pos;
	private GameObject _fireball;
	// Use this for initialization
	void Start () {
		pos = new Vector2(24.2f, 57.7f);
		this._fireball = GameObject.Find("Fireball");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			this._fireball.transform.localPosition = pos;
			this._fireball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}
