using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_switching : MonoBehaviour {
    public int weapon_number = 0;

	// Use this for initialization
	void Start () {
        //Debug.Log("Input");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon_number = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon_number = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon_number = 2;
        }
	}
}
