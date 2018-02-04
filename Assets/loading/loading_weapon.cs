using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loading_weapon : MonoBehaviour {
    public GameObject[] weapon;
    public List<GameObject> E_right_weapons;
    public bool E_right_weapon = false;

    void weapon_enable() {
        for (int i = 0; i < weapon.Length; i++) {
            weapon[i].SetActive(false);
        }
        for (int i = 0; i < assemble_status.my_weapon_number.Length; i++) {
            if (E_right_weapons.Contains(weapon[assemble_status.my_weapon_number[i]]))
            {
                if (E_right_weapon == false)
                {
                    weapon[assemble_status.my_weapon_number[i]].SetActive(true);
                    E_right_weapon = true;
                }
            }
            else {
                weapon[assemble_status.my_weapon_number[i]].SetActive(true);
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
