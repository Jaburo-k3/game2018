using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class stage_select_UI : MonoBehaviour {
    public GameObject Exit_backflame;
    public Image[] Stage_imag;
    public Text[] Stage_text;

    Coroutine play;

    public bool Stage_mode = false;
    public bool Stage_select = true;
    public bool Pop_state = false;

    IEnumerator Stage_UI_Popup()
    {
        Debug.Log("test");
        Stage_mode = true;
        Pop_state = false;
        Stage_imag[0].transform.localScale = new Vector3(0, 0, 1);
        Stage_imag[0].enabled = true;
        Stage_imag[1].enabled = true;
        for (float i = 0; Stage_imag[0].transform.localScale.x <= 1; i += 0.1f)
        {
            yield return new WaitForSeconds(0.01f);
            Stage_imag[0].transform.localScale = new Vector3(i, i, 1);
        }
        for (int i = 2; i < Stage_imag.Length - 2; i++)
        {
            Stage_imag[i].enabled = true;
        }
        for (int i = 0; i < Stage_text.Length; i++)
        {
            Stage_text[i].enabled = true;
        }
        set_back_flame();
        Pop_state = true;

    }
    IEnumerator Stage_UI_Popdown()
    {
        Debug.Log("test2");
        Pop_state = false;
        Stage_imag[4].enabled = false;
        Stage_imag[5].enabled = false;
        for (int i = 2; i < Stage_imag.Length - 2; i++)
        {
            Stage_imag[i].enabled = false;
        }
        for (int i = 0; i < Stage_text.Length; i++)
        {
            Stage_text[i].enabled = false;
        }

        for (float i = 1; Stage_imag[0].transform.localScale.x >= 0; i -= 0.1f)
        {
            yield return new WaitForSeconds(0.01f);
            Stage_imag[0].transform.localScale = new Vector3(i, i, 1);
        }
        Stage_mode = false;
        Pop_state = true;
    }

    public void start_Stage()
    {
        if (play != null)
        {
            StopCoroutine(play);
        }

        if (Stage_mode == false)
        {
            play = StartCoroutine(Stage_UI_Popup());
        }
        else {
            play = StartCoroutine(Stage_UI_Popdown());
        }
    }

    public void set_back_flame()
    {
        if (Stage_select)
        {
            Stage_imag[4].enabled = true;
            Stage_imag[5].enabled = false;
        }
        else {
            Stage_imag[4].enabled = false;
            Stage_imag[5].enabled = true;
        }
    }
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Stage_imag.Length; i++)
        {
            Stage_imag[i].enabled = false;
        }
        for (int i = 0; i < Stage_text.Length; i++)
        {
            Stage_text[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
