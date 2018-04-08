using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_gauge : MonoBehaviour {
    public GameObject hp_obj;
    private HP hp;
    public Image hp_bar;
    float im_fill_default;
    // Use this for initialization
    void Start () {
        hp = hp_obj.GetComponent<HP>();
        im_fill_default = hp_bar.fillAmount;
	}
	
	// Update is called once per frame
	void Update () {
        hp_bar.fillAmount = im_fill_default *(hp.get_hp() / hp.get_hp_max());
        if (hp.get_hp() < hp.get_hp_max()/3) {
            hp_bar.color = new Color(Color.red.r, Color.red.g, Color.red.b, hp_bar.color.a);
        }
	}
}
