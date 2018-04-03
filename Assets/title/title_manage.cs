using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class title_manage : MonoBehaviour {

    int mode_number = 0;

    public bool button = false;

    public AudioClip title_sound;
    Vector2 cross_vec_save = Vector2.zero;
    Vector2 stick_vec_save = Vector2.zero;

    public GameObject back_flame;
    public Vector3 back_flame_pos_save;
    void crossorstick_buttondown_system()
    {
        if (cross_vec_save.y == 0 && Input.GetAxis("Cross_Vertical") >= 1.0f)
        {
            mode_number += 1;
            cross_vec_save.y = 1.0f;
        }
        else if (stick_vec_save.y == 0 && Input.GetAxis("Vertical") >= 0.8f)
        {
            mode_number -= 1;
            stick_vec_save.y = 1.0f;
        }
        else if (cross_vec_save.y == 0 && Input.GetAxis("Cross_Vertical") <= -1.0f)
        {
            mode_number -= 1;
            cross_vec_save.y = -1.0f;

        }
        else if (stick_vec_save.y == 0 && Input.GetAxis("Vertical") <= -0.8f)
        {
            mode_number += 1;
            stick_vec_save.y = -1.0f;
        }
        else if (cross_vec_save.y == 1.0f && Input.GetAxis("Cross_Vertical") <= 0.0f)
        {
            cross_vec_save.y = 0.0f;
        }
        else if (stick_vec_save.y == 1.0f && Input.GetAxis("Vertical") <= 0.0f) {
            stick_vec_save.y = 0.0f;
        }
        else if (cross_vec_save.y == -1.0f && Input.GetAxis("Cross_Vertical") >= 0.0f)
        {
            cross_vec_save.y = 0.0f;
        }
        else if (stick_vec_save.y == -1.0f && Input.GetAxis("Vertical") >= 0.0f)
        {
            stick_vec_save.y = 0.0f;
        }
    }

    void mode_number_limit()
    {
        if (mode_number >= 3)
        {
            mode_number = 0;
        }
        else if (mode_number < 0)
        {
            mode_number = 2;
        }
    }

    void UI_pos_update() {
        back_flame.transform.position = new Vector3(back_flame_pos_save.x, back_flame_pos_save.y - 100 * mode_number, back_flame_pos_save.z); 
    }
    public void ranking_world() {
        if (button == false)
        {
            StartCoroutine(world_change("rankingall_world"));
        }
    }
    public void assemble_world() {
        if (button == false)
        {
            StartCoroutine(world_change("assemble_world"));
        }
    }

    public void tutorial_world() {
        if (button == false)
        {
            StartCoroutine(world_change("tutorial_world"));
        }
    }
    public IEnumerator world_change(string world_name)
    {
        float delay_time = 0;
        button = true;
        if (world_name == "assemble_world") {
            AudioSource AudioSource;
            AudioSource = gameObject.AddComponent<AudioSource>();
            AudioSource.clip = title_sound;
            AudioSource.volume = 0.3f;
            AudioSource.Play();
            delay_time = 1.5f;
        }
        yield return new WaitForSeconds(delay_time);
        SceneManager.LoadScene(world_name);
    }


    // Use this for initialization
    void Start () {
        back_flame_pos_save = back_flame.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        crossorstick_buttondown_system();
        mode_number_limit();
        UI_pos_update();
        if (Input.GetButtonDown("button2") || Input.GetButtonDown("button9")) {
            if (mode_number == 0)
            {
                assemble_world();
            }
            else if (mode_number == 1)
            {
                tutorial_world();
            }
            else if (mode_number == 2) {
                ranking_world();
            }
        }
    }
}
