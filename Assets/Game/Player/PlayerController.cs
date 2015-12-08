using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveForce = 365f;
	public float maxSpeed = 5f;

	public Rigidbody2D heroRB;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Cache the horizontal input.
		float h = ScreenInput.horizontal;
		
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		//anim.SetFloat("Speed", Mathf.Abs(h));
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * heroRB.velocity.x < maxSpeed)
			// ... add a force to the player.
			heroRB.AddForce(Vector2.right * h * moveForce);

	}
}
