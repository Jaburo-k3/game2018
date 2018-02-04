using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //transform.position = new Vector3(0, -0.8f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(0, -0.8f, 0);
        transform.Rotate(0, 0, 10);
        //Debug.Log("A");
    }
}
