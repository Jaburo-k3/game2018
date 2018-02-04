using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_tab_button : MonoBehaviour {
    public int mynumber;
    private assemble_weapon_manage As_weapon_manage;

    public void weapon_tab_update() {
        Debug.Log(this.gameObject.name);
        As_weapon_manage.weapon_number = mynumber;
        As_weapon_manage.weapon_tab_move();
        As_weapon_manage.weapon_tab_decision();
    }

	// Use this for initialization
	void Start () {
        As_weapon_manage = GameObject.Find("Canvas").GetComponent<assemble_weapon_manage>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
