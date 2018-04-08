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
    public GameObject camera_obj;

    public GameObject[] Weapon_text;
    private weapon_value_text[] Weapon_value_text = new weapon_value_text[2];

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

    void set_weapon(int number) {
        E_weapon[number] = Instantiate(weapon[assemble_status.my_weapon_number[number]]);
        Weapon_Status = E_weapon[number].GetComponent<weapon_status>();
        Weapon_Status.my_weapon_number = number;
        Weapon_Status.camera_obj = camera_obj;
        Vector3 x = E_weapon[number].transform.localScale;
        Vector3 y = E_weapon[number].transform.localEulerAngles;
        //E_weapon[i].transform.parent = E_weapon_parent[0].transform;
        set_parent(E_weapon[number]);
        E_weapon[number].transform.localPosition = E_weapon[number].transform.position;
        E_weapon[number].transform.localScale = x;
        E_weapon[number].transform.localEulerAngles = y;
        
        if (number == 2)
        {
            Weapon_Status.W_value_text = Weapon_value_text[0];
        }
        else {
            Weapon_Status.W_value_text = Weapon_value_text[1];
        }
    }

    public void set_UI(int number) {
        if (number == 2)
        {
            Weapon_UI = weapon_UI_gauge[1].GetComponent<weapon_UI>();
        }
        else {
            Weapon_UI = weapon_UI_gauge[0].GetComponent<weapon_UI>();
        }
        Weapon_UI.status_obj = E_weapon[number];
        Weapon_UI.getcomponet();
    }
    public void set_weapon_value(int number) {
        if (number != 2)
        {
            Weapon_value_text[1].Weapon_status = E_weapon[number].GetComponent<weapon_status>();
            Weapon_value_text[1].weapon_shot = true;
        }
    }
	// Use this for initialization
	void Start () {
        Weapon_value_text[0] = Weapon_text[0].GetComponent<weapon_value_text>();
        Weapon_value_text[1] = Weapon_text[1].GetComponent<weapon_value_text>();
        for (int i = 0; i < E_weapon.Length; i++) {
            set_weapon(i);
        }
        set_UI(0);
        set_UI(2);


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
