using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel_roll : MonoBehaviour {
    public GameObject[] barrel;
    Quaternion[] default_rotate = new Quaternion[1];
    public Vector3 roll_value;
    public bool roll = false;
    bool roll_start = false;
	// Use this for initialization
	void Start () {
        Debug.Log(barrel[0].name);
        default_rotate[0] = barrel[0].transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (roll)
        {
            if (roll_start == false)
            {
                roll_start = true;
            }
            for (int i = 0; i < barrel.Length; i++)
            {
                barrel[i].transform.Rotate(roll_value);
            }
        }
        if (roll_start && !roll) {
            roll_start = false;
            for (int i = 0; i < barrel.Length; i++) {
               barrel[i].transform.localRotation = default_rotate[i];
            }
        }
    }
}
