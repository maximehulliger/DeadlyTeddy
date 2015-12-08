using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// Used to debug on the phone. display a text for 'showTime' seconds
/// use log1(text) to dispatch a prioritary message over log2(text).
public class PDebug : MonoBehaviour {

	public Text textArea;
	public float showTime = 2;

	private string debugText1, debugText2;
	private bool showText1 = false, showText2 = false;
	private float showTill1 = 0, showTill2 = 0;
	static private PDebug instance;

	// Use this for initialization
	void Start () {
		textArea.enabled = false;
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		//log1("attached: "+MoveHead.attached+" fixed: "+MoveHead.fixedd);
		
		if (showText1 && Time.time > showTill1) {
			showText1 = false;
			if (showText2) {
				textArea.text = debugText2;
			} else {
				textArea.enabled = false;
			}
		} else if (showText2 && Time.time > showTill2) {
			textArea.enabled = false;
			showText2 = false;
		}
	}

	static public void log1(string text) {
		instance.log1p(text);
	}

	static public void log2(string text) {
		instance.log2p(text);
	}

	private void log1p(string text) {
		showTill1 = Time.time + showTime;
		textArea.enabled = true;
		textArea.text = text;
		showText1 = true;
		if (showText2) {
			showTill1 += showTime;
		}
	}

	private void log2p(string text) {
		showTill2 = Time.time + showTime;
		debugText2 = text;
		textArea.enabled = true;
		showText2 = true;
		if (!showText1) {
			textArea.text = text;
		}
	}
}
