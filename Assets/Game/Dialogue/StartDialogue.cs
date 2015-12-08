using UnityEngine;
using System.Collections;

public class StartDialogue : MonoBehaviour {

	public string dialogueName = "test";
	public bool userControl = false;
	public bool unique = false;

	private bool done = false;

	void OnTriggerEnter2D(Collider2D other) {
		if ((!done || !unique) && other.tag == "Player" && MoveHead.attached) {
			DialogueManager.beginDialogue(dialogueName, userControl);
			done = true;
		}
	}
}
