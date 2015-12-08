using UnityEngine;
using System.Collections;

public class StartComment : MonoBehaviour {
	
	public string dialogueName = "test";
	public bool unique = false;

	private bool done = false;
	
	void OnTriggerEnter2D(Collider2D other) {
		if ((!done || !unique) && other.tag == "Player" && MoveHead.attached) {
			DialogueManager.beginDialogue(dialogueName, true);
			done = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			DialogueManager.endDialogue();
		}
	}
}
