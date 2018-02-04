using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_manage : MonoBehaviour {
    private camera_angle Camera_angle;
    public Vector2[] angle;


    public void camera_update(int number) {
        //Debug.Log(number);
        Camera_angle.update_angle(angle[number].x, angle[number].y);
    }
	// Use this for initialization
	void Start () {
        Camera_angle = this.GetComponent<camera_angle>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
