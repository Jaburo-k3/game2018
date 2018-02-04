using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_slot_button : MonoBehaviour {
    public int mynumber;
    private assemble_weapon_manage As_weapon_manage;

    public void weapon_slot_updated() {
        if (As_weapon_manage.Exit_mode || As_weapon_manage.Stage_mode) {
            return;
        }

        if (mynumber < assemble_status.my_weapon_number.Length)
        {
            if (As_weapon_manage.mode_change)
            {
                As_weapon_manage.weapon_tab_close();
            }
            As_weapon_manage.weapon_slot = mynumber;
            As_weapon_manage.weapon_slot_move();
            As_weapon_manage.weapon_tab_open();
        }
        else {
            As_weapon_manage.weapon_tab_close();
            As_weapon_manage.weapon_slot = mynumber;
            As_weapon_manage.weapon_slot_move();
            As_weapon_manage.start_UI_open();
        }
    }
    

	// Use this for initialization
	void Start () {
        As_weapon_manage = GameObject.Find("Canvas").GetComponent<assemble_weapon_manage>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
