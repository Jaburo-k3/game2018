using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assemble_weapon_enable : MonoBehaviour {
    private assemble_weapon_manage As_Wea_Manage;
    public GameObject[] weapon;
    public List<GameObject> E_weapon;
    public List<GameObject> E_right_weapon;
    public int E_right_arm = 0;

    public void set_weapon() {
        E_weapon.Clear();
        for (int i = 0; i < 3; i++) {
            if (As_Wea_Manage.weapon_slot == i)
            {
                E_weapon.Add(weapon[As_Wea_Manage.weapon_number]);
            }
            else {
                E_weapon.Add(weapon[assemble_status.my_weapon_number[i]]);
            }
            
        }
    }
    public void weapon_enable(int weapon_slot)
    {
        E_right_arm = 0;
        for (int i = 0; i < weapon.Length; i++)
        {
            if (E_weapon.Contains(weapon[i]))
            {
                if (E_right_weapon.Contains(weapon[i]))
                {
                    if (As_Wea_Manage.weapon_number == i)
                    {
                        weapon[i].SetActive(true);
                        E_right_arm = 2;
                    }
                    else {
                        weapon[i].SetActive(false);
                        if (E_right_arm != 2) {
                            E_right_arm = 1;
                        }
                    }
                }
                else {
                    weapon[i].SetActive(true);
                }
            }


            else {
                weapon[i].SetActive(false);
            }
        }


        if (E_right_arm == 1)
        {
            for (int i = 0; i < weapon.Length; i++)
            {
                if (E_weapon.Contains(weapon[i]))
                {
                    if (E_right_weapon.Contains(weapon[i]))
                    {
                        if (E_right_arm == 1)
                        {
                            weapon[i].SetActive(true);
                            E_right_arm = 2;
                        }
                        else {
                            weapon[i].SetActive(false);
                        }
                    }
                    else {
                        weapon[i].SetActive(true);
                    }
                }


                else {
                    weapon[i].SetActive(false);
                }
            }
        }
    }
	// Use this for initialization
	void Start () {
        As_Wea_Manage = GameObject.Find("Canvas").GetComponent<assemble_weapon_manage>();
        set_weapon();
        weapon_enable(0);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
