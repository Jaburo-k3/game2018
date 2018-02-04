using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_beam_effect : MonoBehaviour {

    private laser_beam Laser_Beam;

    Vector3 firing_pos;
    Vector3 save_pos;
    GameObject parent_obj;
    public Vector3 parent_obj_pos;


    void save_pos_update() {
        //Vector3 pos = Vector3.Normalize(parent_obj_pos - firing_pos);
        Vector3 pos = (parent_obj_pos - save_pos);
        pos = pos * Vector3.Distance(parent_obj_pos, firing_pos)/ (Vector3.Distance(parent_obj_pos, save_pos) * 25);
        //Debug.Log("pos = " + pos);
        save_pos += pos;
        //Debug.Log("save_pos = " + save_pos);
        if (Mathf.Abs(Vector3.Distance(parent_obj_pos, save_pos)) < 0.1) {
            Destroy(this.gameObject);
        }
    }
    void size_update()
    {
        Vector3 pos = (parent_obj_pos - save_pos) / 2;
        this.transform.position = save_pos + pos;

        float scale_y = Vector3.Distance(parent_obj_pos, save_pos) / 2;
        Vector3 scale = new Vector3(this.transform.localScale.x, scale_y, this.transform.localScale.z);
        this.transform.localScale = scale;
    }
    // Use this for initialization
    void Start () {
        parent_obj = transform.root.gameObject;
        Laser_Beam = parent_obj.GetComponent<laser_beam>();
        firing_pos = this.transform.position;
        save_pos = firing_pos;
        //size_update();

    }
	
	// Update is called once per frame
	void Update () {
        if (parent_obj == null)
        {
            save_pos_update();
            size_update();
        }
        else {
            parent_obj_pos = Laser_Beam.destroy_pos;
        }
	}
}
