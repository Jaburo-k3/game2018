using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_type2_lock : MonoBehaviour {

    private water_field Water_Field;
    private enemy_type2 Enemy_Type2;
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Water_Field.in_water_counter > 0) {
            Enemy_Type2.move_permission = true;
        }
    }
    // Use this for initialization
    void Start () {
        Enemy_Type2 = transform.root.gameObject.GetComponent<enemy_type2>();
        Water_Field = GameObject.FindGameObjectWithTag("environment").GetComponent<water_field>();
        Enemy_Type2.move_permission = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
