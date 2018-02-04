using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_button : MonoBehaviour {
    public int stage_number;
    private assemble_weapon_manage As_weapon_manage;

    public void stage_load() {
        if (stage_number == 0)
        {
            As_weapon_manage.stage_load(true);
        }
        else if (stage_number == 1)
        {
            As_weapon_manage.stage_load(false);
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
