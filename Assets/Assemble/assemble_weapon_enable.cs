using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assemble_weapon_enable : MonoBehaviour {
    private assemble_weapon_manage As_Wea_Manage;
    public GameObject[] R_weapon;
    public GameObject[] L_weapon;
    public List<GameObject> E_R_weapon;
    public List<GameObject> E_L_weapon;
    public List<GameObject> E_right_weapon;
    public int E_right_arm = 0;

    public void set_weapon() {
        E_R_weapon.Clear();
        E_L_weapon.Clear();
        for (int i = 0; i < 4; i++) {
            if (As_Wea_Manage.weapon_slot == i)
            {
                if (As_Wea_Manage.weapon_slot == 0 || As_Wea_Manage.weapon_slot == 2)
                {
                    E_R_weapon.Add(R_weapon[As_Wea_Manage.weapon_number]);
                }
                else if (As_Wea_Manage.weapon_slot == 1 || As_Wea_Manage.weapon_slot == 3) {
                    E_L_weapon.Add(L_weapon[As_Wea_Manage.weapon_number]);
                }
            }
            else {
                if (i == 0 || i == 2)
                {
                    E_R_weapon.Add(R_weapon[assemble_status.my_weapon_number[i]]);
                }
                else if (i == 1 || i == 3) {
                    E_L_weapon.Add(L_weapon[assemble_status.my_weapon_number[i]]);
                }

            }
            
        }
    }
    public void weapon_enable(int weapon_slot)
    {
        Debug.Log("weapon_enable");
        R_weapon_enable();
        L_weapon_enable();
    }
    void R_weapon_enable() {
        for (int i = 0; i < R_weapon.Length; i++)
        {
            if (E_R_weapon.Contains(R_weapon[i]))
            {
                Debug.Log("true");
                R_weapon[i].SetActive(true);
            }


            else {
                Debug.Log("false");
                R_weapon[i].SetActive(false);
            }
        }
    }
    void L_weapon_enable()
    {
        for (int i = 0; i < L_weapon.Length; i++)
        {
            if (E_L_weapon.Contains(L_weapon[i]))
            {
                Debug.Log("true");
                L_weapon[i].SetActive(true);
            }
            else {
                Debug.Log("false");
                L_weapon[i].SetActive(false);
            }
        }
    }
    void Awake()
    {
        As_Wea_Manage = GameObject.Find("Canvas").GetComponent<assemble_weapon_manage>();
    }
    // Use this for initialization
    void Start () {
        //As_Wea_Manage = GameObject.Find("Canvas").GetComponent<assemble_weapon_manage>();
        set_weapon();
        weapon_enable(0);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
