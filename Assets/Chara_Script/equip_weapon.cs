using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equip_weapon : MonoBehaviour {
    private weapon_status Weapon_Status;
    private weapon_UI Weapon_UI;

    public GameObject[] weapon;
    public GameObject[] E_weapon = new GameObject[3];
    public GameObject[] E_weapon_parent;
    public GameObject[] weapon_UI_gauge;

    public void set_parent(GameObject child) {
        if (Weapon_Status.parent_name == "25cn: base")
        {
            child.transform.parent = E_weapon_parent[0].transform;
        }
        else if (Weapon_Status.parent_name == "wall01_azalea:waist")
        {
            child.transform.parent = E_weapon_parent[1].transform;
        }
        else if (Weapon_Status.parent_name == "05azalea_back:topArm") {
            child.transform.parent = E_weapon_parent[2].transform;
        }
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < assemble_status.my_weapon_number.Length; i++) {
            E_weapon[i] = Instantiate(weapon[assemble_status.my_weapon_number[i]]);
            Weapon_Status = E_weapon[i].GetComponent<weapon_status>();
            Weapon_Status.my_weapon_number = i;
            Vector3 x = E_weapon[i].transform.localScale;
            Vector3 y = E_weapon[i].transform.localEulerAngles;
            //E_weapon[i].transform.parent = E_weapon_parent[0].transform;
            set_parent(E_weapon[i]);
            E_weapon[i].transform.localPosition = E_weapon[i].transform.position;
            E_weapon[i].transform.localScale = x;
            E_weapon[i].transform.localEulerAngles = y;
            Weapon_UI = weapon_UI_gauge[i].GetComponent<weapon_UI>();
            Weapon_UI.status_obj = E_weapon[i];
            Weapon_UI.getcomponet();
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
