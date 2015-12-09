using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MoveBody : MonoBehaviour {
	private const string groundTag = "Ground";

	public float moveForce = 200;
	public float jumpForce = 200;
	public float maxSpeed = 50;		// px / sec, horizontal
	public float timeFixedAfterSwipe = 1;
	public float timeToTurn = 0.2f;
	[Space(10)]
	public IRope rope;
	public Transform spineBase;
	[Space(10)]
	public AudioClip jumpSound;


	private Rigidbody2D body;
	private bool grounded = false; 		// true if touch the ground
	private int groundContactCount = 0;	// nb of contact point with the ground
	private float detachedTime;
	private bool lastFixedd = true;
	private AudioSource audioSource;
	private bool goingRight = true;
	private float etatRot = 1;	//1 -> right, -1 -> left
	private Quaternion goingLeftIdentity = Quaternion.Euler(0,180,0);

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
	}


	// Update is called once per frame
	void Update () {
		// horizontal movement
		float h = ScreenInput.horizontal;
		//if he wants to move and doesn't move too fast
		if (h != 0 && Mathf.Sign(h) * body.velocity.x < maxSpeed) {
			body.AddForce(Vector2.right * h * moveForce * body.mass);
		}

		if (grounded) {
			// standing
			transform.rotation = Quaternion.Lerp(transform.rotation, 
			        goingRight ? Quaternion.identity : goingLeftIdentity, Time.deltaTime * 5f);

			// jumping
			if (ScreenInput.jumpDown) {
				body.AddForce(Vector2.up * jumpForce * body.mass);
				audioSource.PlayOneShot(jumpSound);
				groundContactCount = 0;
			}
		}
		
		// update rotation from horizontal
		if (ScreenInput.horizontal != 0)
			goingRight = ScreenInput.horizontal > 0;
		if (etatRot != etatRotGoal()) {
			etatRot += etatRotGoal() * Time.deltaTime * 2 / (timeToTurn);
			if (goingRight && etatRot > etatRotGoal() ||
			    !goingRight && etatRot < etatRotGoal())
				etatRot = etatRotGoal();
			rotateToEtatRot(etatRot);
		}

		if (!MoveHead.attached && Time.time > detachedTime + timeFixedAfterSwipe) {
			body.AddForceAtPosition(rope.rootForce, spineBase.position);
		}

		if (lastFixedd && MoveHead.fixedd) {
			detachedTime = Time.time;
		}
		lastFixedd = MoveHead.fixedd;
	}

	void OnCollisionEnter2D(Collision2D collisionInfo) {
		if (collisionInfo.gameObject.tag == groundTag && hasGroundContact(collisionInfo)) {
			groundContactCount++;
			if (groundContactCount <= 0)
				groundContactCount = 1;
			grounded = true;
			body.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}

	private bool hasGroundContact(Collision2D collisionInfo) {
		foreach(ContactPoint2D cp in collisionInfo.contacts)
			if (cp.normal.y > 0)
				return true;
		return false;
	}

	void OnCollisionExit2D(Collision2D collisionInfo) {
		if (collisionInfo.gameObject.tag == groundTag && hasGroundContact(collisionInfo)) {
			groundContactCount--;
			grounded = groundContactCount > 0;
			if (!grounded)
				body.constraints = RigidbodyConstraints2D.None;
		}
	}

	private void rotateToEtatRot(float etat) {
		Vector3 current = transform.rotation.eulerAngles;
		
		current.y = T.map(-etat, -1, 1, 0, 180, false);
		transform.rotation = Quaternion.Euler(current);
	}

	private int etatRotGoal() {
		return goingRight ? 1 : -1;
	}
}


