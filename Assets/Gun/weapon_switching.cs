using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_switching : MonoBehaviour {
    public int[] weapon_number = new int[2];

    public bool weapon_change = false;

    public GameObject[] Weapon_text;

    private weapon_value_text[] Weapon_value_text = new weapon_value_text[2];

    private equip_weapon E_weapon;
    Vector2 cross_vec_save;

    private AudioSource Change_AudioSource;
    public AudioClip Change_sound;

    void cross_buttondown_system() {
        if (cross_vec_save.y == 0 && Input.GetAxis("Cross_Vertical") >= 1.0)
        {
            Debug.Log("up");
            weapon_number[0] -= 1;
            weapon_number_limit(0);
            weapon_change = true;
            UI_update();
            cross_vec_save.y = Input.GetAxis("Cross_Vertical");
            Change_AudioSource.Play();
        }
        else if (cross_vec_save.y == 0 && Input.GetAxis("Cross_Vertical") <= -1.0)
        {
            Debug.Log("Down");
            weapon_number[1] -= 1;
            weapon_number_limit(1);
            weapon_change = true;
            UI_update();
            cross_vec_save.y = Input.GetAxis("Cross_Vertical");
            Change_AudioSource.Play();

        }
        else if (cross_vec_save.y == 1.0f && Input.GetAxis("Cross_Vertical") <= 0.0f)
        {
            cross_vec_save.y = 0.0f;
        }
        else if (cross_vec_save.y == -1.0f && Input.GetAxis("Cross_Vertical") >= 0.0f) {
            cross_vec_save.y = 0.0f;
        }
    }

    void weapon_number_limit(int arm_number) {
        if (weapon_number[arm_number] < 0) {
            weapon_number[arm_number] = 1;
        }
        /*
        if (weapon_number >= 2) {
            weapon_number = 0;
        }
        else if (weapon_number < 0) {
            weapon_number = 1;
        }
        */
    }
    void UI_update() {
        E_weapon.set_UI(0,weapon_number[0]);
        E_weapon.set_UI(1,weapon_number[1]);

        E_weapon.set_weapon_value(0,weapon_number[0]);
        E_weapon.set_weapon_value(1,weapon_number[1]);
    }
    void text_update() {

    }
	// Use this for initialization
	void Start () {
        Change_AudioSource = gameObject.AddComponent<AudioSource>();
        Change_AudioSource.clip = Change_sound;
        Change_AudioSource.volume = 0.5f;
        //Debug.Log("Input");
        E_weapon = this.GetComponent<equip_weapon>();

    }
	
	// Update is called once per frame
	void Update () {
        cross_buttondown_system();
	}
}
