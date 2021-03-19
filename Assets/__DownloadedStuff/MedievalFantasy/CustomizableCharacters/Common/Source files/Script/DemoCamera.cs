using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCamera : MonoBehaviour {
    float rot;
    float xrot;

    public bool autoRot;
    public bool zoom;
    public Camera cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rot, xrot), 10 * Time.deltaTime);
        if (autoRot) rot += 100 * Time.deltaTime;
        if (zoom) {
            cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, Quaternion.Euler(22, 90, 0), 5 * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 10, 5 * Time.deltaTime);
        } else {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 35, 5 * Time.deltaTime);
            cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, Quaternion.Euler(27, 90, 0), 5 * Time.deltaTime);
        }
	}
    private void OnGUI() {
        if (autoRot == false) {
            if (GUI.Button(new Rect(Screen.width * 0.5f - 175 - 16, Screen.height * 0.5f - 32, 32, 32), "<")) {
                rot -= 45;
            }
            if (GUI.Button(new Rect(Screen.width * 0.5f + 175 - 16, Screen.height * 0.5f - 32, 32, 32), ">")) {
                rot += 45;
            }
            if (GUI.Button(new Rect(Screen.width * 0.5f - 16, Screen.height * 0.5f - 32 - 250, 32, 32), "^")) {
                xrot -= 15;
            }
            if (GUI.Button(new Rect(Screen.width * 0.5f - 16, Screen.height * 0.5f - 32 + 250, 32, 32), "v")) {
                xrot += 15;
            }
        }

        if (GUI.Button(new Rect(Screen.width - 128, 0, 128, 32), "Auto rotation")) {
            autoRot = !autoRot;
        }
        if (GUI.Button(new Rect(Screen.width - 128, 0+32, 128, 32), "Zoom")) {
            zoom= !zoom;
        }
    }
}
