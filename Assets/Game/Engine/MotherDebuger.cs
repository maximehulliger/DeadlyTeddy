using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MotherDebuger : MonoBehaviour {
	//public float i = 0;
	//public Rigidbody2D bodyRB;
	//public Rigidbody2D headRB;


	//ScreenInput sinput;
	//[Range(0,2)]
	public float timeScale = 1;

	// Use this for initialization
	void Start () {
		//sinput = GetComponent<ScreenInput>();
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = timeScale;
		//textSwipe.text = ScreenInput.hasSwipe ? "swipe: "+ScreenInput.swipe : "no swipe";
		//Vector2 midScreen = new Vector2(Screen.width/2, Screen.height/2);
		//Drawing.DrawLine(midScreen, midScreen + );
		//PDebug.log("hor: "+ScreenInput.horizontal);
		/*if (Input.GetKeyDown("t")) {
			CameraManager.shake(force);
		}*/
	}

	void OnGUI() {
		/*if (ScreenInput.hasSwipe) {
			Vector2 midScreen = new Vector2(Screen.width/2, Screen.height/2);
			Drawing.DrawLine(midScreen, midScreen + ScreenInput.swipe/8, Color.black);
		}*/
		/*PDebug.log2 ("rect left: "+sinput.leftArrow.rectTransform.rect);
		foreach (ScreenInput.Tap t in ScreenInput.taps) {
			PDebug.log1(" Cpos: "+t.canvasPos+" "+sinput.leftArrow.rectTransform.rect.Contains(t.canvasPos));

		}*/

	}

}
