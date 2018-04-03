using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_status : MonoBehaviour {
    public int my_weapon_number;

    public float attack;

    public float bullet_speed;

    public float cool_time = 0;
    public float cool_const;

    public float bullet_counter = 100;
    public float bullet_one_shot = 10;
    private float bullet_counter_max = 100;


    public int reload_type;//0:単発リロード　1:撃ち切りリロード
    public float reload_time = 10f;
    public bool reload_now = false;
    public bool shot_lock = false;
    public GameObject parent_obj;
    public string parent_name;

    public float get_bullet_counter_max() {
        return bullet_counter_max;
    }
    public int get_my_weapon_number() {
        return my_weapon_number;
    }
    // Use this for initialization
    void Start () {
        parent_obj = transform.root.gameObject;
        bullet_counter_max = bullet_counter;
    }
	
	// Update is called once per frame
	void Update () {
	}
}
