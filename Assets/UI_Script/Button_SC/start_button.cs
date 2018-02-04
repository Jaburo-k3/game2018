using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_button : MonoBehaviour {
    public bool yes_or_no;
    private assemble_weapon_manage As_weapon_manage;


    public void stage_UI_set() {
        
        As_weapon_manage.stage_UI_set(yes_or_no);
        As_weapon_manage.stage_UI_select_change(yes_or_no);
    }

	// Use this for initialization
	void Start () {
        As_weapon_manage = GameObject.Find("Canvas").GetComponent<assemble_weapon_manage>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
