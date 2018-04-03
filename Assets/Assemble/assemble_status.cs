using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assemble_status : MonoBehaviour {
    static public int[] my_weapon_number = {0,1,4};

    public float attack;
    public float save_attack;

    public float cool_time;
    public float save_cool_time;

    public float cool_const;
    public float save_cool_const;

    public float bullet_counter = 100;
    public float save_bullet_counter = 100;

    public float bullet_one_shot = 10;
    public float save_bullet_one_shot = 100;

    public float bullet_number;
    public float save_bullet_number;


    public int reload_type;//0:単発リロード　1:撃ち切りリロード
    public int save_reload_type;//0:単発リロード　1:撃ち切りリロード

    public float reload_time = 10f;
    public float save_reload_time = 10f;

    public void save_status_update() {
        save_attack = attack;
        save_cool_time = cool_time;
        save_bullet_counter = bullet_counter;
        save_bullet_one_shot = bullet_one_shot;
        save_reload_type = reload_type;
        save_reload_time = reload_time;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
