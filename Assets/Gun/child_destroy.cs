using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class child_destroy : MonoBehaviour {
    public GameObject parent;
	// Use this for initialization
	void Start () {
        parent = transform.root.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        //parent = transform.root.gameObject;
        if (parent == null) {
            Destroy(this.gameObject);
        }
	}
}
