using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour {
    private chara_status Chara_Status;
    private float hp;
    private float hp_max;
    private Game_over g_m;

    public float get_hp_max() {
        return hp_max;
    }
    public float get_hp() {
        return hp;
    }
    public void set_hp(float damage) {
        hp = damage;
    }
	// Use this for initialization
	void Start () {
        g_m = GameObject.Find("Game_manage").GetComponent<Game_over>();
        Chara_Status = this.GetComponent<chara_status>();
        hp = Chara_Status.HP;
        hp_max = Chara_Status.HP;
        if (this.tag == "enemy") {
            g_m.enemy_number += 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0) {
            if (this.gameObject.tag == "enemy")
            {
                g_m.enemy_number -= 1;
                Destroy(this.gameObject);
            }
            if (this.gameObject.tag == "Player") {
                g_m.game_over = true;
            }

        }
	}
}
