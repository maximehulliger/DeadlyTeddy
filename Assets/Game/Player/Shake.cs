using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {
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
			float sqrMag = collisionInfo.relativeVelocity.sqrMagnitude;
			float strengh = sqrMag / sqr(headMovment.maxSpeed);
			CameraManager.shake(strengh/2+0.5f, Mathf.Min(strengh, 1));
			//print("strengh: "+strengh/2+0.5f);
		}
	}
	
	float sqr(float f) { return f*f; }
}
