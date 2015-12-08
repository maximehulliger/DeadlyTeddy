using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour {
	private const string dialogueFilePath = "Asset/Game/Dialogue/Dialogues";
	public DialogueFile file;
	private static DialogueManager instance;

	/// speed at wich the letters are displayed in the bubble, in letter / sec
	public float printSpeed = 5;

	private EasyDialogueManager manager;
	private Dialogue currentDialogue = null;
	private Dialogue.Choice currentChoice = null;
	private float startTime;	// start date of current bubble
	private string endText;		// text that should be displayed after some time
	private bool done = true;	// is the text fully displayed ?
	private Text uiText;
	private bool hasDialogue = false;
	private bool removedUserControl;
	private Image dialoguePanel;
	private int prefixLenght;
	private Action endAction;

	public static void beginDialogue(string dialogue, bool userControl) {
		beginDialogue(dialogue, userControl, null);
	}

	/// begin that dialogue with a specificted method to run when the dialogue is over 
	public static void beginDialogue(string dialogue, bool userControl, Action endAction) {
		instance.beginDialogueIn(dialogue, userControl, endAction);
	}
	
	/// end the current dialogue or comment
	public static void endDialogue() {
		instance.endDialogueIn();
	}

	// Use this for initialization
	void Start () {
		instance = this;
		manager = EasyDialogueManager.LoadDialogueFile(/*dialogueFilePath*/file);
		dialoguePanel = GameObject.Find("DialoguePanel").GetComponent<Image>();
		uiText = dialoguePanel.GetComponentInChildren<Text>();
		dialoguePanel.gameObject.SetActive(false);
		//dialoguePanelRect = ScreenInput.extractAbsoluteRect(dialoguePanel.rectTransform);
	}
	
	// Update is called once per frame
	void Update () {
		if (hasDialogue && ScreenInput.taps.Count > 0) {
			// if the text isn't totally displayed, display it. otherwise take the next talk
			if (!done) {
				uiText.text = endText;
				done = true;
			} else {
				Dialogue.Choice[] choices = currentDialogue.GetChoices();

				/*if (choices.Length > 1) {
					Debug.LogError("multi-choice dialogue not yet handled !");
				} else if (choices.Length == 1) {*/
				if (choices.Length >= 1) {
					currentChoice = choices[0];
					currentDialogue.PickChoice(currentChoice);
					displayNextText();
				} else {
					// over
					endDialogueIn();
				}
			}
		}

		// update the displayed text
		if (!done) {
			float passedTime = Time.time - startTime;
			int nbLetter = (int)(passedTime * printSpeed) + prefixLenght;
			uiText.text = endText.Substring(0, nbLetter);

			float totalWriteTime = (endText.Length-prefixLenght) / printSpeed;
			if (passedTime > totalWriteTime) {
				uiText.text = endText;
				done = true;
			}
		}
	}

	private void endDialogueIn() {
		dialoguePanel.gameObject.SetActive(false);
		hasDialogue = false;
		if (removedUserControl)
			ScreenInput.userControl = true;
		if (endAction != null)
			endAction();
	}


	// start displaying the next text
	private void displayNextText() {
		startTime = Time.time;

		prefixLenght = currentChoice.speaker.Length + 2;
		endText = currentChoice.speaker + ": " + currentChoice.dialogue;
		done = false;
	}

	private void beginDialogueIn(string dialogue, bool userControl, Action endAction) {
		if (!hasDialogue) {
			if (!userControl) {
				removedUserControl = ScreenInput.userControl;
				ScreenInput.userControl = false;
			}
			hasDialogue = true;
			currentDialogue = manager.GetDialogue(dialogue);
			currentChoice = currentDialogue.GetChoices()[0];
			currentDialogue.PickChoice(currentChoice);
			dialoguePanel.gameObject.SetActive(true);
			displayNextText();
			this.endAction = endAction;
		}
	}
}
