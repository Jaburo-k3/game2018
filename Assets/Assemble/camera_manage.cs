using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_manage : MonoBehaviour {
    private camera_angle Camera_angle;
    public Vector2[] R_angle;
    public Vector2[] L_angle;


    public void camera_update(int number, int arm) {
        //Debug.Log(number);
        if (arm == 0)
        {
            Camera_angle.update_angle(R_angle[number].x, R_angle[number].y);
        }
        else if (arm == 1) {
            Camera_angle.update_angle(L_angle[number].x, L_angle[number].y);
        }
    }
    void Awake()
    {
        Camera_angle = this.GetComponent<camera_angle>();
    }
    // Use this for initialization
    void Start () {
        //Camera_angle = this.GetComponent<camera_angle>();//変更点
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
