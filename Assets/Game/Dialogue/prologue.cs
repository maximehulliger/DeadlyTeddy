using UnityEngine;
using System.Collections;

public class prologue : MonoBehaviour {

	public string dialogueName = "test";
	public bool unique = false;
	private bool done = false;
//	public blackscreen_script darkness;
//	void Start()
//	{
//		// we can probably scrap this one if we ain't using system.action.
//		//darkness = GameObject.Find ("Black_screen").GetComponent<blackscreen_script>();
//	}

	void OnTriggerEnter2D(Collider2D other) {
		if ((!done || !unique) && other.tag == "Player" && MoveHead.attached) {
			DialogueManager.beginDialogue(dialogueName, true, null);
			//GameObject.Find("Black_screen").GetComponent<AudioSource>().Play();
			done = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			DialogueManager.endDialogue();
		}
	}
}
