using UnityEngine;
using System.Collections;

public class EnemyReverse : MonoBehaviour {
	public int Enem_num;
	private int direc;
	private Rigidbody2D body;
	// Use this for initialization
	void Start () {
		direc = 2;
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Enemy.Switch_num == Enem_num) {
			float h;
//			h = (ScreenInput.hasSwipe ? 1 : 0) + ScreenInput.horizontal;
			h = 1.0f;
			transform.Translate (Vector2.right * h * Time.deltaTime * direc);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			direc = direc * -1;
		}
		if (coll.gameObject.tag == "Return") {
			direc = direc * -1;
		}
		if (coll.gameObject.tag == "Player") {
			Invoke("die",0.5f);
		} 
	}

	void die(){
		Destroy(gameObject);
	}
}