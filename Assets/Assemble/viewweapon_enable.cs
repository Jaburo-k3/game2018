using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewweapon_enable : MonoBehaviour {
    public GameObject[] weapon;

    public void weapon_enable(int number) {
        for (int i = 0; i < weapon.Length; i++) {
            if (i != number)
            {
                weapon[i].SetActive(false);
            }
            else {
                weapon[i].SetActive(true);
            }
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
