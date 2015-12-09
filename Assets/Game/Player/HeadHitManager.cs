using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class HeadHitManager : MonoBehaviour {
	
	public AudioClip defaultHitSound;
	public AudioClip rubbertHitSound;
	public AudioClip ironHitSound;
	public AudioClip woodHitSound;

	private MoveHead headMovment;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		headMovment = MoveHead.instance;
		audioSource = GetComponent<AudioSource>();
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
				audioSource.PlayOneShot(woodHitSound);
				break;
			case "Rubber":
				audioSource.PlayOneShot(rubbertHitSound);
				break;
			case "Iron":
				audioSource.PlayOneShot(ironHitSound);
				break;
			default:
				audioSource.PlayOneShot(defaultHitSound);
				break;
			}
		}

	}
}
