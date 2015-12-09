using UnityEngine;
using System.Collections;

/// stands for tools. contain useful static methods
static public class T {

	/// [min, max] => [0, 1]
	static public float map01(float v, float min, float max, bool clamp) {
		if (clamp)
			v = Mathf.Clamp(v, min, max);
		return (v - min) / (max - min);
	}
	
	/// [min, max] => [toMin, toMax]
	static public float map(float c, float min, float max, float toMin, float toMax, bool clamp) {
		return map01(c, min, max, clamp) * (toMax - toMin) + toMin;
	}
}
	