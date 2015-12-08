using UnityEngine;
using System.Collections;

public class MoveHead : MonoBehaviour {
	public static MoveHead instance;

	public enum SwipeMapping { Force, Velocity };
	public SwipeMapping swipeMapping = SwipeMapping.Velocity;
	public float forceRatio = 0.1f;
	public float velocityRatio = 0.03f;
	public float maxSpeed = 100;	// px / sec
	[Space(10)]
	public Rigidbody2D body;
	public Transform headPos;
	public float attractDist = 5f;	// distance from wich the head begin to be attracted to his body position.
	public float attractVelocity = 3;
	public float lockDist = 0.4f;	// distance from wich the grad will be locked (fixed) in his attached position.
	[Space(10)]
	public IRope rope;
	public Transform spineEnd;

	private Rigidbody2D head;
	//private Collider2D headCollHead;
	private Collider2D bodyCollHead;

	static public bool attached { get; private set; } 	// more like attracted.. but if true the head move relatively to the body
	static public bool fixedd { get; private set; } 	// -> attached

	[System.NonSerialized]
	public bool canBeSwiped = true; 	// only 1 shot of the head before being able to shot it;
	private bool isSwiping = false;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		head = GetComponent<Rigidbody2D>();
		//Collider2D[] headColls = GetComponents<Collider2D>();
		//headCollHead = headColls[0];
		bodyCollHead = body.GetComponent<Collider2D>();
		setAttached(true);
		fixedd = true;
	}
	
	// Update is called once per frame
	void Update () {
		// apply swipe force
		if (ScreenInput.hasSwipe && canBeSwiped && head.velocity.sqrMagnitude < maxSpeed*maxSpeed) {
			isSwiping = true;
			switch (swipeMapping) {
			case SwipeMapping.Force:
				head.AddForce(ScreenInput.swipe * forceRatio);
				break;
			case SwipeMapping.Velocity:
				head.velocity = ScreenInput.swipe * velocityRatio;
				break;
			}
			setAttached(false);
		}

		// prevent second swipe
		if (isSwiping && !ScreenInput.hasSwipe) {
			canBeSwiped = false;
			isSwiping = false;
		}

		// attract or reject the head when not fixed
		if (!fixedd && !ScreenInput.hasSwipe) {
			Vector2 diff = headPos.position - transform.position;
			float distSqr = diff.sqrMagnitude;
			if (distSqr < lockDist*lockDist) {
				setAttached(true);
				fixedd = true;
				head.velocity = Vector2.zero;
				rope.minNbOfPoint = 2;
			} else if (distSqr > attractDist*attractDist) {
				setAttached(false);
			} else if ( distSqr < attractDist*attractDist/4) {
				//print ("très pres");
				setAttached(true);
			} else if ((distSqr < attractDist*attractDist && goingTowardBody())) {
				//print ("va contre!");
				setAttached(true);
			} else {
				//print ("mauvais client (dist: "+Mathf.Sqrt(distSqr));
				setAttached(false);
			}
		}

		// move the object
		if (attached) {
			if (fixedd) {
				transform.position = headPos.position;
				transform.rotation = Quaternion.Lerp(
					transform.rotation, body.transform.rotation, Time.deltaTime * 15f);
			} else {
				transform.rotation = Quaternion.Lerp(
					transform.rotation, body.transform.rotation, Time.deltaTime * 5f);
				Vector2 diff = headPos.position - transform.position;
				head.velocity = diff.normalized * attractVelocity + body.velocity;
			}
		} else {
			head.AddForceAtPosition(rope.endForce, spineEnd.position);
		}
	}

	private bool goingTowardBody() {
		Vector2 v = head.velocity - body.velocity;
		Vector2 x = body.position - head.position;
		/*print("dot: "+Vector2.Dot(v,x));
		if (!(Vector2.Dot(v,x) >= 0))
			print("dot: "+Vector2.Dot(v,x)+" dv: "+v);*/
		return Vector2.Dot(v,x) >= 0;
	}

	void setAttached(bool isAttached) {
		if (attached != isAttached) {
			attached = isAttached;
			//print ("attached: "+attached);
			bodyCollHead.enabled = attached;
			//headCollHead.enabled = !attached;
			head.isKinematic = attached;
			/*Physics2D.IgnoreLayerCollision(
				LayerMask.NameToLayer("PlayerBody"),
				LayerMask.NameToLayer("PlayerHead"),
				attached);*/
			head.constraints = attached ? RigidbodyConstraints2D.FreezeRotation : RigidbodyConstraints2D.None;
			head.gravityScale = attached ? 0 : 1;
			if (attached) {	
				//Debug.Assert(!fixedd, "fixed but not attached :$");
				rope.minNbOfPoint = 3;
				canBeSwiped = true;
			} else {
				fixedd = false;
			}
		}
	}
}
