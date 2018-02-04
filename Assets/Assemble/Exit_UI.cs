using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit_UI : MonoBehaviour {
    public GameObject Exit_backflame;
    public Image[] Exit_imag;
    public Text[] Exit_text;

    Coroutine play;

    public bool Exit_mode = false;
    public bool Exit_select = true;
    public bool Pop_state = false;

    IEnumerator Exit_UI_Popup() {
        Exit_mode = true;
        Pop_state = false;
        Exit_imag[0].transform.localScale = new Vector3(0, 0, 1);
        Exit_imag[0].enabled = true;
        Exit_imag[1].enabled = true;
        for (float i = 0; Exit_imag[0].transform.localScale.x <= 1; i += 0.1f)
        {
            yield return new WaitForSeconds(0.01f);
            Exit_imag[0].transform.localScale = new Vector3(i, i, 1);
        }
        for (int i = 2; i < Exit_imag.Length - 2; i++) {
            Exit_imag[i].enabled = true;
        }
        for (int i = 0; i < Exit_text.Length; i++) {
            Exit_text[i].enabled = true;
        }
        set_back_flame();
        Pop_state = true;

    }
    IEnumerator Exit_UI_Popdown() {
        Debug.Log("test2");
        Pop_state = false;
        Exit_imag[4].enabled = false;
        Exit_imag[5].enabled = false;
        for (int i = 2; i < Exit_imag.Length - 2; i++)
        {
            Exit_imag[i].enabled = false;
        }
        for (int i = 0; i < Exit_text.Length; i++)
        {
            Exit_text[i].enabled = false;
        }

        for (float i = 1; Exit_imag[0].transform.localScale.x  >= 0; i -= 0.1f)
        {
            yield return new WaitForSeconds(0.01f);
            Exit_imag[0].transform.localScale = new Vector3(i, i, 1);
        }
        Exit_mode = false;
        Pop_state = true;
    }

    public void start_Exit() {
        if (play != null) {
            StopCoroutine(play);
        }

        if (Exit_mode == false)
        {
            play = StartCoroutine(Exit_UI_Popup());
        }
        else {
            play = StartCoroutine(Exit_UI_Popdown());
        }
    }

    public void set_back_flame() {
        if (Exit_select)
        {
            Exit_imag[4].enabled = true;
            Exit_imag[5].enabled = false;
        }
        else {
            Exit_imag[4].enabled = false;
            Exit_imag[5].enabled = true;
        }
    }
	// Use this for initialization
	void Start () {
        for (int i = 0; i < Exit_imag.Length; i++)
        {
            Exit_imag[i].enabled = false;
        }
        for (int i = 0; i < Exit_text.Length; i++)
        {
            Exit_text[i].enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
