using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming_system : MonoBehaviour {
    public Ray ray;
    public RaycastHit hit;
    public Vector3 target;
    public Transform Player_object;

    public LayerMask ignore_layer;

    public GameObject mouse_obj;
    private mouse_Vec mouse_vec;

    public GameObject center_point;

    private camera Camera_sc;

    // Use this for initialization
    void Start () {
        mouse_vec = mouse_obj.GetComponent<mouse_Vec>();
        Camera_sc = this.gameObject.GetComponent<camera>();
        center_point = GameObject.Find("Player/center_point");
        ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("Ignore Raycast"));

        ray = new Ray(new Vector3(Player_object.position.x, center_point.transform.position.y + 2f, Player_object.position.z), transform.forward);
    }
	
	// Update is called once per frame
	void Update () {
        if (Camera_sc.mode_snipe == false)
        {
            ray = new Ray(new Vector3(Player_object.position.x, center_point.transform.position.y + 2f, Player_object.position.z), transform.forward);
        }
        else if(Camera_sc.mode_snipe == true){
            //ray = new Ray(new Vector3(Player_object.position.x, Player_object.position.y, Player_object.position.z), transform.forward);
            ray = new Ray(transform.position, transform.forward);
        }
        if (Physics.SphereCast(ray, 0.1f, out hit, Mathf.Infinity,ignore_layer)) {
            target = hit.point;
            
        }
        

    }
}
