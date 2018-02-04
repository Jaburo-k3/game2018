using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ranking_update : MonoBehaviour {
    string str_score = "score";
    string str_name = "name";
    public string public_name;


    float[] score_list = new float[5];
    string[] name_list = new string[5];



    public Text[] ranking_text;
    public Text[] name_text = new Text[5];
    public InputField[] inputfield = new InputField[5];
    public InputField new_name;
    public ColorBlock inputfield_color;
    int update_point;

    float alpha = 1;
    bool input_end = false;
    bool alpha_add = false;

    void set_score_list() {
        for (int i = 0; i < score_list.Length; i++) {
            string key = str_score + "_" + i.ToString();
            score_list[i] = PlayerPrefs.GetFloat(key,100);
            //Debug.Log(PlayerPrefs.GetFloat(key, 100));
        }
    }
    void set_name_list() {
        for (int i = 0; i < name_list.Length; i++) {
            string key = str_name + "_" + i.ToString();
            name_list[i] = PlayerPrefs.GetString(key, "Name");
            Debug.Log(PlayerPrefs.GetString(key));
        }
    }

    void set_text() {
        for (int i = 0; i < name_list.Length; i++) {
            name_text[i].text = name_list[i];
        }
    }
    void set_new_name() {
        for (int i = 0; i < inputfield.Length; i++) {
            if (i == update_point)
            {
                inputfield[i].interactable = true;
                //new_name = inputfield[i];
                if (game_status.game_clear) {
                    new_name = inputfield[i];
                    inputfield_color = new_name.colors;
                    inputfield_color.normalColor = new Color(255f, 255f, 255f, 1f);
                    inputfield_color.highlightedColor = new Color(255f, 255f, 255f, 1f);
                    inputfield_color.pressedColor = new Color(255f, 255f, 255f, 1f);
                    inputfield_color.disabledColor = new Color(255f, 255f, 255f, 1f);
                }
                new_name.colors = inputfield_color;
            }
            else {
                inputfield[i].interactable = false;
            }
        }
    }


    void score_list_update(){
        update_point = score_list.Length;
        float[] save_list = new float[0];
        for (int i = 0; i < score_list.Length; i++) {
            if (score_list[i] > timer.time) {
                save_list = new float[score_list.Length - (i + 1)];
                for (int j = 0,start = i; j < save_list.Length; j++,start++) {
                    save_list[j] = score_list[start];
                }
                update_point = i;
                break;
            }
        }
        if (update_point != score_list.Length) {
            score_list[update_point] = timer.time;
            for (int i = update_point + 1,j = 0; j < save_list.Length; i++,j++) {
                score_list[i] = save_list[j];
            }
        }
        for (int i = 0; i < score_list.Length; i++) {
        }
    }

    void name_list_update() {
        string[] save_list = new string[0];
        if (update_point != score_list.Length)
        {
            save_list = new string[score_list.Length - (update_point + 1)];
            for (int j = 0, start = update_point; j < save_list.Length; j++, start++)
            {
                save_list[j] = name_list[start];
            }
            name_list[update_point] = "Name";
            for (int i = update_point + 1, j = 0; j < save_list.Length; i++, j++)
            {
                name_list[i] = save_list[j];
            }
        }
    }
    void new_name_update() {
        string key = str_name + "_" + update_point.ToString();
        Debug.Log(inputfield[update_point].text);
        PlayerPrefs.SetString(key, inputfield[update_point].text);
        Debug.Log(update_point);
    }
    void set_score_PlayerPrefs() {
        for (int i = 0; i < score_list.Length; i++) {
            string key = str_score + "_" + i.ToString();
            PlayerPrefs.SetFloat(key, score_list[i]);
        }
        PlayerPrefs.Save();
    }
    void set_name_PlayerPrefs() {
        for (int i = 0; i < name_list.Length; i++) {
            string key = str_name + "_" + i.ToString();
            PlayerPrefs.SetString(key, name_list[i]);
        }
        PlayerPrefs.Save();
    }

    void savedate() {
        update_point = score_list.Length;
        set_score_list();
        set_name_list();
        if (game_status.game_clear) {
            score_list_update();
            name_list_update();
        }
        set_score_PlayerPrefs();
        set_name_PlayerPrefs();
        set_text();
        set_new_name();
    }
    void text_update() {
        for (int i = 0; i < score_list.Length; i++) {
            ranking_text[i].text = score_list[i].ToString("F2");
        }
    }
    void constant_str_update(string key) {
        if (key == "ranking2_world") {
            str_name += "2";
            str_score += "2";
        }
        else if (key == "rankingall_world") {
            str_name += public_name;
            str_score += public_name;
        }
    }
    // Use this for initialization
    void Start () {
        constant_str_update(SceneManager.GetActiveScene().name);
        savedate();
        text_update();
        game_status.game_clear = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) && update_point != score_list.Length)
        {
            //SceneManager.LoadScene("assemble_world");
            new_name_update();
            input_end = true;
            inputfield_color.normalColor = new Color(255f, 255f, 255f, 0f);
            inputfield_color.highlightedColor = new Color(255f, 255f, 255f, 1f);
            inputfield_color.pressedColor = new Color(255f, 255f, 255f, 1f);
            inputfield_color.disabledColor = new Color(255f, 255f, 255f, 1f);
            new_name.colors = inputfield_color;
            input_end = true;
        }
        else if (Input.GetKey(KeyCode.Delete) && Input.GetKeyDown(KeyCode.Escape)) {
            PlayerPrefs.DeleteAll();
        }

        if (update_point != score_list.Length && input_end == false)
        {
            if (alpha_add == false)
            {
                alpha -= 0.01f;
            }
            else {
                alpha += 0.01f;
            }
            if (alpha < 0) {
                alpha = 0.0f;
                alpha_add = true;
            }
            else if (alpha > 0.8f) {
                alpha = 0.8f;
                alpha_add = false;
            }
            inputfield_color.normalColor = new Color(255f, 255f, 255f, alpha);
            new_name.colors = inputfield_color;
        }
    }
}
