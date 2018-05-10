using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_weapon_status : MonoBehaviour {

    public int my_weapon_number;
    public int my_arm_number;
    public float bullet_speed;
    public float attack;

    public float cool_time;
    public float cool_const;

    public GameObject parent_obj;

    public GameObject target;
    public string parent_name;
    public bool target_lock;
    public bool shot_permission = false;

    public enemy_shot_control E_S_control;

    public int get_my_weapon_number() {
        return my_weapon_number;
    }

    // Use this for initialization
    void Start()
    {
        parent_obj = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
