using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_text : MonoBehaviour {
    public Text[] text;

    public void text_update(int number) {
        for (int i = 0; i < text.Length; i++) {
            if (i == number) {
                text[i].enabled = true;
            }
            else
            {
                text[i].enabled = false;
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
