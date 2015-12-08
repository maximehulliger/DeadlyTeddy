using UnityEngine;
using System.Collections;

public class DeviceRotation : MonoBehaviour {
	
	private static bool initialized = false;
	
	public static Quaternion get() {
		if (!initialized) {
			init();
		}

		//return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
		return SystemInfo.supportsGyroscope ? Input.gyro.attitude : Quaternion.identity;
	}
	
	private static void init() {
		if (SystemInfo.supportsGyroscope) {
			Input.gyro.enabled = true;                // enable the gyroscope
			Input.gyro.updateInterval = 1f/60;    // set the update interval to it's highest value (60 Hz)
		}
		initialized = true;
	}
}
