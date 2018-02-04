using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boost_gauge : MonoBehaviour {
    private Player_move P_M;
    private camera camera_sc;
    public GameObject camera_obj;
    public Image Boost_gauge;
    public Image Boost_gauge_back;
    bool save_state = false;
    bool gauge_style = true;
    float alpha = 0;
    IEnumerator boost_gauge_up() {

        while (true)
        {
            alpha = Boost_gauge.color.a;
            yield return new WaitForSeconds(0.002f);
            alpha += 0.05f;
            if (alpha > 1.0f) {
                alpha = 1.0f;
                Boost_gauge.color = new Color(Boost_gauge.color.r, Boost_gauge.color.g, Boost_gauge.color.b, alpha);
                Boost_gauge_back.color = new Color(Boost_gauge_back.color.r, Boost_gauge_back.color.g, Boost_gauge_back.color.b, alpha);
                break;
            }
            if (gauge_style == false) {
                break;
            }
            Boost_gauge.color = new Color(Boost_gauge.color.r, Boost_gauge.color.g, Boost_gauge.color.b, alpha);
            Boost_gauge_back.color = new Color(Boost_gauge_back.color.r, Boost_gauge_back.color.g, Boost_gauge_back.color.b, alpha);
        }
    }
    IEnumerator boost_gauge_down()
    {
        while (true)
        {
            alpha = Boost_gauge.color.a;
            yield return new WaitForSeconds(0.002f);
            alpha -= 0.05f;
            if (alpha < 0)
            {
                alpha = 0;
                Boost_gauge.color = new Color(Boost_gauge.color.r, Boost_gauge.color.g, Boost_gauge.color.b, alpha);
                Boost_gauge_back.color = new Color(Boost_gauge_back.color.r, Boost_gauge_back.color.g, Boost_gauge_back.color.b, alpha);
                break;
            }
            if (gauge_style)
            {
                break;
            }
            Boost_gauge.color = new Color(Boost_gauge.color.r, Boost_gauge.color.g, Boost_gauge.color.b, alpha);
            Boost_gauge_back.color = new Color(Boost_gauge_back.color.r, Boost_gauge_back.color.g, Boost_gauge_back.color.b, alpha);
        }
    }

    // Use this for initialization
    void Start () {
        P_M = GameObject.Find("Player").GetComponent<Player_move>();
        camera_sc = camera_obj.GetComponent<camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Boost_gauge.fillAmount = P_M.boost_energy / P_M.boost_energy_max;
        if (P_M.boost_energy != P_M.boost_energy_max && save_state == true)
        {
            gauge_style = true;
            StartCoroutine("boost_gauge_up");
        }
        else if (P_M.boost_energy == P_M.boost_energy_max && save_state == false) {
            gauge_style = false;
            StartCoroutine("boost_gauge_down");
        }
        if (Boost_gauge.color.a >= 1.0f && camera_sc.camera_move_now == true) {
            Debug.Log("go");
            gauge_style = false;
            StartCoroutine("boost_gauge_down");
        }
        if (P_M.boost_energy == P_M.boost_energy_max)
        {
            save_state = true;
        }
        else
        {
            save_state = false;
        }
    }
}
