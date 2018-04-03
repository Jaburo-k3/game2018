using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_judment : MonoBehaviour {

    private chara_status Chara_Status;
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "terrain") {
            if (Chara_Status.ground_condition == "air") {
                Chara_Status.move_stun = 10;
                Chara_Status.hover = 0;
            }
            Chara_Status.ground_condition = "terrain";
        }
        Debug.Log("enter");
    }

    void OnCollisionStay(Collision other) {
        Debug.Log("stay");
    }
    void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "terrain")
        {
            Chara_Status.ground_condition = "air";
            Chara_Status.hover = 2;
        }
        Debug.Log("exit");
    }

    // Use this for initialization
    void Start () {
        Chara_Status = this.GetComponent<chara_status>();
        Chara_Status.ground_condition = "air";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
