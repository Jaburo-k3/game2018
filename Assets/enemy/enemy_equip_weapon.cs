using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_equip_weapon : MonoBehaviour {
    private enemy_weapon_status Weapon_Status;
    //private weapon_UI Weapon_UI;
    public int[] my_weapon_number = new int[4];

    public GameObject[] weapon;
    public GameObject[] R_weapon;
    public GameObject[] L_weapon;
    public GameObject[,] E_weapon = new GameObject[2, 2];
    public GameObject[] R_weapon_parent;
    public GameObject[] L_weapon_parent;
    //public GameObject[] weapon_UI_gauge;
    //public GameObject camera_obj;


    public GameObject[] Weapon_text;
    private weapon_value_text[] Weapon_value_text = new weapon_value_text[2];

    public void set_parent(GameObject child, int arm_number)
    {
        if (arm_number == 0)
        {
            if (Weapon_Status.parent_name == "25cn: base")
            {
                child.transform.parent = R_weapon_parent[0].transform;
            }
            else if (Weapon_Status.parent_name == "wall01_azalea:waist")
            {
                child.transform.parent = R_weapon_parent[1].transform;
            }
            else if (Weapon_Status.parent_name == "05azalea_back:topArm")
            {
                child.transform.parent = R_weapon_parent[2].transform;
            }
        }
        else {

            if (Weapon_Status.parent_name == "25cn: base")
            {
                child.transform.parent = L_weapon_parent[0].transform;
            }
            else if (Weapon_Status.parent_name == "wall01_azalea:waist")
            {
                child.transform.parent = L_weapon_parent[1].transform;
            }
            else if (Weapon_Status.parent_name == "05azalea_back:topArm")
            {
                child.transform.parent = L_weapon_parent[2].transform;
            }
        }
    }
    void set_weapon(int arm_number, int number, int slot_number)
    {
        if (arm_number == 0)
        {
            E_weapon[arm_number, slot_number] = Instantiate(R_weapon[my_weapon_number[number]]);
        }
        else {
            E_weapon[arm_number, slot_number] = Instantiate(L_weapon[my_weapon_number[number]]);
        }
        Weapon_Status = E_weapon[arm_number, slot_number].GetComponent<enemy_weapon_status>();
        Weapon_Status.my_weapon_number = slot_number;
        Weapon_Status.my_arm_number = arm_number;
        //Weapon_Status.camera_obj = camera_obj;
        Vector3 x = E_weapon[arm_number, slot_number].transform.localScale;
        Vector3 y = E_weapon[arm_number, slot_number].transform.localEulerAngles;
        //E_weapon[i].transform.parent = E_weapon_parent[0].transform;
        set_parent(E_weapon[arm_number, slot_number], arm_number);
        E_weapon[arm_number, slot_number].transform.localPosition = E_weapon[arm_number, slot_number].transform.position;
        E_weapon[arm_number, slot_number].transform.localScale = x;
        E_weapon[arm_number, slot_number].transform.localEulerAngles = y;
    }

    /*
    public void set_UI(int arm_number, int slot_number)
    {
        Weapon_UI = weapon_UI_gauge[arm_number].GetComponent<weapon_UI>();

        Weapon_UI.status_obj = E_weapon[arm_number, slot_number];
        Weapon_UI.getcomponet();
    }
    */
    public void set_weapon_value(int arm_number, int number)
    {
        if (arm_number == 1)
        {
            Weapon_value_text[1].Weapon_status = E_weapon[1, number].GetComponent<weapon_status>();
            Weapon_value_text[1].weapon_shot = true;
        }

        else if (arm_number == 0)
        {
            Weapon_value_text[0].Weapon_status = E_weapon[0, number].GetComponent<weapon_status>();
            Weapon_value_text[0].weapon_shot = true;
        }

    }
    // Use this for initialization
    void Start()
    {
        Weapon_value_text[0] = Weapon_text[0].GetComponent<weapon_value_text>();
        Weapon_value_text[1] = Weapon_text[1].GetComponent<weapon_value_text>();

        //右腕
        set_weapon(0, 0, 0);
        //左腕
        set_weapon(1, 1, 0);
        //右肩
        set_weapon(0, 2, 1);
        //左肩
        set_weapon(1, 3, 1);

        //set_UI(0, 0);
        //set_UI(1, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
