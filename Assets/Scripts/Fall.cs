using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour {
	private Rigidbody2D fall;
	private BoxCollider2D fallb;

	// Use this for initialization
	void Start () {
		fall = GetComponent<Rigidbody2D>();
		fallb = GetComponent<BoxCollider2D>();
		fall.isKinematic = false;
	}
	
	// Update is called once per frame
	IEnumerator OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			yield return new WaitForSeconds(0.3f);
			fallb.enabled = false;
		}
	}
}
