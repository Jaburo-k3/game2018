using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_hit_marker : MonoBehaviour {
    public GameObject hit_marker_obj;

    private hit_marker Hit_Marker;

    public GameObject parent;

    public void create(Vector3 pos) {
        hit_marker_obj = Instantiate(hit_marker_obj, this.transform.position, Quaternion.identity);
        Hit_Marker = hit_marker_obj.GetComponent<hit_marker>();
        Hit_Marker.parent = this.parent;
        hit_marker_obj.transform.position = pos;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
