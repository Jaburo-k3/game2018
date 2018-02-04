using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_hide_obj : MonoBehaviour {
    public GameObject Chara;

    public GameObject hide_obj;

    public Vector3 ray_vec;

    public float distance;
    void hide(GameObject hit_obj= null) {
        if (hit_obj == null && hide_obj != null) {
            float r = hide_obj.GetComponent<Renderer>().material.color.r;
            float g = hide_obj.GetComponent<Renderer>().material.color.g;
            float b = hide_obj.GetComponent<Renderer>().material.color.b;
            hide_obj.GetComponent<Renderer>().material.color = new Color(r, g, b, 1f);
            //Debug.Log(1);
        }
        else if (hide_obj != null) {
            float r = hide_obj.GetComponent<Renderer>().material.color.r;
            float g = hide_obj.GetComponent<Renderer>().material.color.g;
            float b = hide_obj.GetComponent<Renderer>().material.color.b;
            hide_obj.GetComponent<Renderer>().material.color = new Color(r, g, b, 1f);

            r = hit_obj.GetComponent<Renderer>().material.color.r;
            g = hit_obj.GetComponent<Renderer>().material.color.g;
            b = hit_obj.GetComponent<Renderer>().material.color.b;
            hit_obj.GetComponent<Renderer>().material.color = new Color(r, g, b, 0.5f);
            hide_obj = hit_obj;
            //Debug.Log(2);
        }
    }
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update() {
        ray_vec = Chara.transform.position - this.transform.position;
        distance = Vector3.Distance(Chara.transform.position, this.transform.position);
        Ray camera_ray = new Ray(transform.position, ray_vec);
        RaycastHit hit;
        if (Physics.Raycast(camera_ray, out hit, distance) == true && (hit.collider.tag == "object" || hit.collider.tag == "enemy")) 
        {
            if (hit.collider.tag == "object")
            {
                if (hide_obj == null)
                {
                    hide_obj = hit.collider.gameObject;
                }
                hide(hit.collider.gameObject);
                //Debug.Log("start");
            }
        }
        else {
            hide();
        }
    }
}
