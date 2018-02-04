using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
    GameObject target;
    public GameObject main; 
    void OnTriggerStay(Collider other) {
        
        if (other.tag == "Player") {
            //Debug.Log("test");
            target = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject == target) {
            target = null;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null) {
            //Debug.Log("test");
            Vector3 target_pos = target.transform.position;
            main.transform.LookAt(new Vector3(target_pos.x, transform.position.y, target_pos.z));
        }
	}
}
