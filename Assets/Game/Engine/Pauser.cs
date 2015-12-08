using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {
	public static bool paused { get; private set; }

	void Start() {
		paused = false;
	}
	
	void Update () {
		if(Input.GetKeyDown("p")) {
			setPause(!paused);
		}
	}

	public static void setPause(bool paused) {
		Pauser.paused = paused;
		Time.timeScale = paused ? 0 : 1;
	}
}
