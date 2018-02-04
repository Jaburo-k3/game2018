using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Vec : MonoBehaviour {
    public GameObject camera_obj;
    public GameObject Chara_obj;
    Vector3 Chara_pos;
    private mouse_Vec mouse_vec;
    public GameObject mouse_obj;
	// Use this for initialization
	void Start () {
        transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
        Chara_pos = Chara_obj.transform.position;
        Chara_pos.y += 1f;

        transform.position = Chara_pos;
        //transform.LookAt(camera_obj.transform.up);
		
	}
}
