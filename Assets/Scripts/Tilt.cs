using UnityEngine;
using System.Collections;

public class Tilt : MonoBehaviour {
	// Use this for initialization
	void Start () {
        //check if the gyro is enabled
#if UNITY_IPHONE
    /*if(Input.gyro.enabled){
        enabledGyro = true;
 
        //debug
        ToDebug("Gyro Enabled");
    } else {
        //TODO: show a warning message
    }*/
#endif

#if UNITY_ANDROID
    if(Input.gyro.enabled){
//        enabledGyro = true;
 
        //debug
    } else {
        //TODO: show a warning message
    }
#endif

#if UNITY_WINDOWS_PHONE
    //TODO : Windows Phone Gyro Version
#endif

#if UNITY_EDITOR

#endif

    }
	
	// Update is called once per frame
	void Update () {
        float speed = 0.5f;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(speed,0,0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -speed);
        }

	}
}
