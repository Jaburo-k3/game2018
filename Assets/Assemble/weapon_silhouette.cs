using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class weapon_silhouette : MonoBehaviour {
    public Image[] silhouette_imag;
    public string[] silhouette_text_source;
    [SerializeField]
    public Text[] silhouette_text;
    public Sprite[] silhouette;

    public void silhouette_update(int number) {
        silhouette_imag[3].sprite = silhouette[1];
        for (int i = 0; i < assemble_status.my_weapon_number.Length; i++) {
            silhouette_imag[i].sprite = silhouette[assemble_status.my_weapon_number[number]];
            silhouette_text[i].text = silhouette_text_source[number];
            number += 1;
            if (number > assemble_status.my_weapon_number.Length - 1) {
                number = 0;
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
