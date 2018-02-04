using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class snipe_UI : MonoBehaviour {
    public Image imag;

    private float alpha = 0;

    bool save_mode;
    bool Switch;

    public GameObject camera_obj;
    private camera camera_sc;


    IEnumerator UI_Transmission_plus() {
        //bool mode_snipe = camera_sc.mode_snipe;
        while (true) {
            yield return new WaitForSeconds(0.002f);
            alpha += 0.05f;
            imag.color = new Color(imag.color.r, imag.color.g, imag.color.b, alpha);
            if (alpha > 1)
            {
                alpha = 1;
                break;
            }
        }
    }
    IEnumerator UI_Transmission_minus() {
        //alpha = 255f;
        while (true) {
            
            yield return new WaitForSeconds(0.0001f);
            alpha -= 0.05f;
            //Debug.Log(alpha);
            imag.color = new Color(imag.color.r, imag.color.g, imag.color.b, alpha);
            if (alpha < 0)
            {
                alpha = 0;
                break;
            }
        }
    }
    // Use this for initialization
    void Start () {
        imag = this.gameObject.GetComponent<Image>();
        camera_sc = camera_obj.GetComponent<camera>();
        save_mode = camera_sc.mode_snipe;
	}
	


	// Update is called once per frame
	void Update () {
        if (save_mode == false && camera_sc.camera_move_now == true) {
            if (camera_sc.mode_snipe == false)
            {
                StartCoroutine("UI_Transmission_plus");
            }
            else {
                Debug.Log("end");
                StartCoroutine("UI_Transmission_minus");
            }
        }
        save_mode = camera_sc.camera_move_now;
	}
}
