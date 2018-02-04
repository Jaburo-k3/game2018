using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class weapon_silhouette : MonoBehaviour {
    public Image[] silhouette_imag;
    public Sprite[] silhouette;

    public void silhouette_update(int slot,int number) {
        silhouette_imag[slot].sprite = silhouette[number];
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
