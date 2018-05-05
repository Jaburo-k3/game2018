using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loading_weapon : MonoBehaviour {
    public GameObject[] R_weapon;
    public GameObject[] L_weapon;
    public List<GameObject> E_right_weapons;
    public bool E_right_weapon = false;

    void weapon_enable() {
        for (int i = 0; i < R_weapon.Length; i++) {
            R_weapon[i].SetActive(false);
            L_weapon[i].SetActive(false);
        }
        for (int i = 0; i < assemble_status.my_weapon_number.Length; i++) {
            if (i == 0 || i == 2)
            {
                R_weapon[assemble_status.my_weapon_number[i]].SetActive(true);
            }
            else if (i == 1 || i == 3) {
                L_weapon[assemble_status.my_weapon_number[i]].SetActive(true);
            }
        }
    }
	// Use this for initialization
	void Start () {
        weapon_enable();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
