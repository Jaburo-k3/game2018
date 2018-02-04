using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class status_text : MonoBehaviour {
    private weapon_status Weapon_Status;
    private assemble_status Assemble_Status;

    public GameObject[] weapon;
    public Text[] status_texts_source;
    public Text[,] status_texts;

    public void save_status_update() {
        Assemble_Status.save_attack = Assemble_Status.attack;
        Assemble_Status.save_cool_time = Assemble_Status.cool_time;
        Assemble_Status.save_bullet_counter = Assemble_Status.bullet_counter;
        Assemble_Status.save_bullet_one_shot = Assemble_Status.bullet_one_shot;
        Assemble_Status.save_bullet_number = Assemble_Status.bullet_counter / Assemble_Status.bullet_one_shot;
        Assemble_Status.save_reload_type = Assemble_Status.reload_type;
        Assemble_Status.save_reload_time = Assemble_Status.reload_time;
    }

    void text_update() {
        status_texts[0,0].text = Assemble_Status.save_attack.ToString("F1");
        status_texts[0,1].text = Assemble_Status.attack.ToString("F1");
        text_color_update(0, Assemble_Status.save_attack, Assemble_Status.attack);

        status_texts[1,0].text = Assemble_Status.save_bullet_number.ToString("F1");
        status_texts[1,1].text = Assemble_Status.bullet_number.ToString("F1");
        text_color_update(1, Assemble_Status.save_bullet_counter, Assemble_Status.bullet_counter);



        status_texts[2, 0].text = Assemble_Status.save_cool_time.ToString("F1");
        status_texts[2, 1].text = Assemble_Status.cool_time.ToString("F1");
        text_color_update(2, Assemble_Status.cool_time, Assemble_Status.save_cool_time);



        if (Assemble_Status.save_reload_type == 0)
        {
            status_texts[3, 0].text = "単発";
        }
        else {
            status_texts[3, 0].text = "撃ち切り";
        }
        if (Assemble_Status.reload_type == 0)
        {
            status_texts[3, 1].text = "単発";
        }
        else {
            status_texts[3, 1].text = "撃ち切り";
        }

        status_texts[4, 0].text = Assemble_Status.save_reload_time.ToString("F1");
        status_texts[4, 1].text = Assemble_Status.reload_time.ToString("F1");
        text_color_update(4, Assemble_Status.reload_time, Assemble_Status.save_reload_time);



    }

    void text_color_update(int number, float Old, float New) {
        if (Old < New)
        {
            status_texts[number, 1].color = new Color(0, 255, 255);
        }
        else if (Old > New)
        {
            status_texts[number, 1].color = new Color(255, 0, 0);
        }
        else {
            status_texts[number, 1].color = new Color(255, 255, 255);
        }
    }

    public void now_status_update(int number) {
        Weapon_Status = weapon[number].GetComponent<weapon_status>();
        Assemble_Status.attack = Weapon_Status.attack;
        Assemble_Status.cool_time = Weapon_Status.cool_const;
        Assemble_Status.bullet_counter = Weapon_Status.bullet_counter;
        Assemble_Status.bullet_one_shot = Weapon_Status.bullet_one_shot;
        Assemble_Status.bullet_number = Weapon_Status.bullet_counter / Weapon_Status.bullet_one_shot;
        Assemble_Status.reload_type = Weapon_Status.reload_type;
        Assemble_Status.reload_time = Weapon_Status.reload_time;
    }

    public void status_text_update(int number,bool Do) {

        now_status_update(number);

        /*
        if (Do)
        {
            //save_status_update();
            now_status_update(number);
        }
        else {
            now_status_update(number);
            //save_status_update();
        }
        */

        Assemble_Status.bullet_number = Assemble_Status.bullet_counter / Assemble_Status.bullet_one_shot;

        text_update();

        
    }

    public void scripit_set(){
        Assemble_Status =  GameObject.Find("Assemble_manage").GetComponent<assemble_status>();

        status_texts = new Text[status_texts_source.Length / 2, 2];

        status_texts[0, 0] = status_texts_source[0];
        for (int i = 0; i < status_texts_source.Length; i++)
        {
            //status_texts_source[i].enabled = false;
        }
        int number = 0;
        for (int i = 0; i < status_texts.GetLength(0); i++)
        {
            for (int j = 0; j < status_texts.GetLength(1); j++, number++)
            {
                status_texts[i, j] = status_texts_source[number];
            }
        }
    }

    // Use this for initialization
    void Start () {
        //Assemble_Status =  GameObject.Find("Assemble_manage").GetComponent<assemble_status>();

        /*
        status_texts = new Text[status_texts_source.Length/2,2];

        status_texts[0, 0] = status_texts_source[0];
        for (int i = 0; i < status_texts_source.Length; i++)
        {
            //status_texts_source[i].enabled = false;
        }
        int number = 0;
        for (int i = 0; i < status_texts.GetLength(0); i++) {
            for (int j = 0; j < status_texts.GetLength(1); j++, number++) {
                status_texts[i, j] = status_texts_source[number];
            }
        }
        */

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
