using UnityEngine;
using System.Collections;

public class Waitsec : MonoBehaviour {
	private Rigidbody2D body;
	private float time;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		body.isKinematic = true;
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= time + 1){
			body.isKinematic = false;
	}
}
}