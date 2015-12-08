using UnityEngine;
using System.Collections;

public class HeadHitManager : MonoBehaviour {
	private MoveHead headMovment;

	// Use this for initialization
	void Start () {
		headMovment = MoveHead.instance;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D collisionInfo) {
		if (collisionInfo.collider.tag != "Player") {
			headMovment.canBeSwiped = true;
		}
	}
}
