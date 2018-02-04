using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_UI_indicate : MonoBehaviour {

    public Image[] weapon_UI;
    public List<Image> weapon_list;
    public Sprite[] weapon_tex;

    public int weapon_UI_number;
    public List<int> indicate_list;

    public int center_number;


    void number_limit() {
        if (weapon_UI_number > weapon_UI.Length - 1) {
            weapon_UI_number = 0;
        }
        else if (weapon_UI_number < 0) {
            weapon_UI_number = weapon_UI.Length - 1;
        }
    }
    //表示する番号のセット
    void set_indicate_list() {
        //中身をリセット
        indicate_list.Clear();

        //入れる値の頭
        int number = weapon_UI_number - 1;

        //上限のチェック
        if (number < 0)
        {
            number = weapon_UI.Length - 1;
        }
        //セット
        for (int i = 0; i < 4; i++) {
            indicate_list.Add(number);

            number += 1;
            if (number > weapon_UI.Length - 1) {
                number = 0;
            }
        }

    }

    public void UI_enable() {
        for (int i = 0; i < weapon_UI.Length; i++) {
            if (weapon_list.Contains(weapon_UI[i]))
            {
                weapon_UI[i].enabled = true;
            }
            else {
                weapon_UI[i].enabled = false;
            }
        }
    } 

    //表示を変更するImagのセット
    void set_weapon_list() {
        //中身をリセット
        weapon_list.Clear();
        //入れる値の頭
        int number = center_number - 1;

        //上限のチェック
        if (number < 0)
        {
            number = weapon_UI.Length - 1;
        }
        for (int i = 0; i < 4; i++) {
            weapon_list.Add(weapon_UI[number]);
            number += 1;
            if (number > weapon_UI.Length - 1)
            {
                number = 0;
            }
        }
    }


    public void indicate() {
        for (int i = 0; i < 4; i++) {
            weapon_list[i].sprite = weapon_tex[indicate_list[i]];
        }
    }
	// Use this for initialization
	void Start () {
        set_weapon_list();
        weapon_UI_number = center_number;
        set_indicate_list();
        UI_enable();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G)) {
            weapon_UI_number += 1;
            number_limit();
            set_indicate_list();
            indicate();
        }
	}
}
