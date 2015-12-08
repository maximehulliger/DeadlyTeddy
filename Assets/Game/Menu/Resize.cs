using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour {

	public float extraSize = .2f;
	public float frequency = 4;
	
	// Update is called once per frame
	void Update () {
		float f = 1 + extraSize * Mathf.Sin(Time.time * frequency);
		transform.localScale = new Vector3(f, f, 1);
	}
}
