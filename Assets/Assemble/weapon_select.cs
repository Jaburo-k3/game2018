using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_select : MonoBehaviour {
    public Image flame;
    public int number = 0;
    public Vector3 top_pos;
    public Vector3 pos;
    public float pos_distance;

    void flame_indicate() {
        pos = top_pos;
        pos.y = top_pos.y - pos_distance * number;
        flame.transform.localPosition = pos;
    }

	// Use this for initialization
	void Start () {
        top_pos = flame.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            number += 1;
            if (number > 2) {
                number = 0;
            }
            flame_indicate();
        }
	}
}
