using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
	private static CameraManager instance;

	// camera follow
	public Transform headTransform;
	public Transform bodyTransform;

	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can be relative to the player.

	private Vector3 goalPos { get { return (headTransform.position + bodyTransform.position)/2; } }		// Reference to the player's transform.
	private Vector3 currentPos;

	// shaking
	public float shakeFrequency = 10;
	//public float shakeFrequency2;
	public float shakeRotFrequency = 7;
	private float from;
	private float till;
	private bool shaking = false;
	private float strengh;
	private Vector3 shakeVec;
	public float radiusFactor = 1f/10;

	public static void shake(float strengh) {
		shake(strengh, 1);
	}

	public static void shake(float strengh, float duration) {
		instance.shakeIn(strengh, duration);
	}

	private void shakeIn(float strengh, float duration) {
		if (Time.time > till || strengh >= this.strengh) {
			from = Time.time;
			till = Time.time + duration;
			shaking = true;
			this.strengh = strengh;
		}
	}

	void Start () {
		currentPos = transform.position;
		instance = this;
	}

	private void updateShaking() {
		if (shaking) {
			float diff = (Time.time - from) * 2 * Mathf.PI;
			float e1 = diff * shakeRotFrequency;
			shakeVec = new Vector2(Mathf.Cos(e1), Mathf.Sin(e1));
			shakeVec *= Mathf.Sin(diff * shakeFrequency * strengh) * strengh * radiusFactor;

			if (Time.time > till)
				shaking = false;
		}
	}


	void Update () {
		TrackPlayer();
		updateShaking();

		if (shaking) {
			transform.position = currentPos + shakeVec;
		} else {
			transform.position = currentPos;
		}
	}
	
	
	void TrackPlayer () {
		Vector3 diff = currentPos - goalPos;

		if(Mathf.Abs(diff.x) > xMargin)
			currentPos.x = Mathf.Lerp(currentPos.x, goalPos.x, xSmooth * Time.deltaTime);
		if(Mathf.Abs(diff.y) > yMargin)
			currentPos.y = Mathf.Lerp(currentPos.y, goalPos.y, ySmooth * Time.deltaTime);

		if (Mathf.Abs(diff.x) > maxXAndY.x)
			currentPos.x = goalPos.x + Mathf.Sign(diff.x) * maxXAndY.x;
		if (Mathf.Abs(diff.y) > maxXAndY.y)
			currentPos.y = goalPos.y + Mathf.Sign(diff.y) * maxXAndY.y;
	}
}
