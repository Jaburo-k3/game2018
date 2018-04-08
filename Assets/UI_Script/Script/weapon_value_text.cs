using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_value_text : MonoBehaviour {
    public Image Weapon_value_back;
    public Text Weapon_value_text;
    public weapon_status Weapon_status;
    Coroutine UI_cor;
    public bool weapon_shot = false;
    IEnumerator UI_enable()
    {
        string zero = "";
        Weapon_value_text.text = Weapon_status.bullet_counter.ToString();
        for (int i = 0; i < 4 - Weapon_value_text.text.Length; i++) {
            zero += "0";
        }
        Weapon_value_text.text = zero + Weapon_value_text.text;
        Weapon_value_back.enabled = true;
        Weapon_value_text.enabled = true;
        yield return new WaitForSeconds(1f);
        Weapon_value_back.enabled = false;
        Weapon_value_text.enabled = false;

    }
    // Use this for initialization
    void Start()
    {
        //Weapon_status = Weapon.GetComponent<weapon_status>();
        Weapon_value_back.enabled = false;
        Weapon_value_text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon_shot) {
            if (UI_cor == null)
            {
                UI_cor = StartCoroutine(UI_enable());
            }
            else {
                StopCoroutine(UI_cor);
                UI_cor = StartCoroutine(UI_enable());
            }
            weapon_shot = false;
        }
    }
}