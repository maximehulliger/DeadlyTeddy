using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class HeadHitManager : MonoBehaviour {
	
	public AudioClip defaultHitSound;
	public AudioClip rubbertHitSound;
	public AudioClip ironHitSound;
	public AudioClip woodHitSound;

	private MoveHead headMovment;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		headMovment = MoveHead.instance;
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D collisionInfo) {
		string collTag = collisionInfo.collider.tag;
		if (collTag != "Player") {
			headMovment.canBeSwiped = true;
			switch (collTag) {
			case "Wood":
				audio.PlayOneShot(woodHitSound);
				break;
			case "Rubber":
				audio.PlayOneShot(rubbertHitSound);
				break;
			case "Iron":
				audio.PlayOneShot(ironHitSound);
				break;
			default:
				audio.PlayOneShot(defaultHitSound);
				break;
			}
		}

	}
}
