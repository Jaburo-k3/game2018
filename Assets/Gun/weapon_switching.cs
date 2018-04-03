using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_switching : MonoBehaviour {
    public int weapon_number = 0;

    public bool weapon_change = false;

    Vector2 cross_vec_save;

    void cross_buttondown_system() {
        if (cross_vec_save.y == 0 && Input.GetAxis("Cross_Vertical") >= 1.0)
        {
            weapon_number -= 1;
            weapon_change = true;
            cross_vec_save.y = Input.GetAxis("Cross_Vertical");
        }
        else if (cross_vec_save.y == 0 && Input.GetAxis("Cross_Vertical") <= -1.0)
        {
            weapon_number += 1;
            weapon_change = true;
            cross_vec_save.y = Input.GetAxis("Cross_Vertical");
        }
        else if (cross_vec_save.y == 1.0f && Input.GetAxis("Cross_Vertical") <= 0.0f)
        {
            cross_vec_save.y = 0.0f;
        }
        else if (cross_vec_save.y == -1.0f && Input.GetAxis("Cross_Vertical") >= 0.0f) {
            cross_vec_save.y = 0.0f;
        }
    }

    void weapon_number_limit() {
        if (weapon_number >= 3) {
            weapon_number = 0;
        }
        else if (weapon_number < 0) {
            weapon_number = 2;
        }
    }
	// Use this for initialization
	void Start () {
        //Debug.Log("Input");
	}
	
	// Update is called once per frame
	void Update () {
        cross_buttondown_system();
        weapon_number_limit();
	}
}
