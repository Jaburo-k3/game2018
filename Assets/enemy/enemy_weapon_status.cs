using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_weapon_status : MonoBehaviour {

    public float attack;

    public int cool_time;
    public int cool_const;

    public GameObject parent_obj;

    public GameObject target;

    public bool target_lock;
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
