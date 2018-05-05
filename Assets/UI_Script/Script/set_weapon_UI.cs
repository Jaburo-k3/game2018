using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class set_weapon_UI : MonoBehaviour {
    public Sprite[] weapon;
    public Image[] weapon_UI;
    //public GameObject[] weapon_UI_gauge;
    void set_UI() {
        /*
        for (int i = 0; i < assemble_status.my_weapon_number.Length; i++) {
            weapon_UI[i].sprite = weapon[assemble_status.my_weapon_number[i]];
        }
        */
    }

	// Use this for initialization
	void Start () {
        set_UI();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
