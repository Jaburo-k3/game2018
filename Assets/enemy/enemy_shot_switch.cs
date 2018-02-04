using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_shot_switch : MonoBehaviour {

    private E_create_bullet E_Create_Bullet;
    //public GameObject E_Create_Bullet_obj;

    private water_field Water_Field;
    public GameObject Water_Field_obj;
    // Use this for initialization
    void Start () {
        Water_Field_obj = GameObject.FindGameObjectWithTag("environment");
        E_Create_Bullet = this.GetComponent<E_create_bullet>();
        Water_Field = Water_Field_obj.GetComponent<water_field>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Water_Field.in_water_counter > 0) {
            E_Create_Bullet.shot_permission = true;
        }
        else {
            E_Create_Bullet.shot_permission = false;
        }
    }
}
