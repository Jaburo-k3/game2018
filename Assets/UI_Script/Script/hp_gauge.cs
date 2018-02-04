using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_gauge : MonoBehaviour {
    public GameObject hp_obj;
    private HP hp;
    public Image hp_bar;
    // Use this for initialization
    void Start () {
        hp = hp_obj.GetComponent<HP>();
	}
	
	// Update is called once per frame
	void Update () {
        hp_bar.fillAmount = hp.get_hp() / hp.get_hp_max();
	}
}
