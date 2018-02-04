using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class back_Screen : MonoBehaviour {
    private Game_over g_m;
    public GameObject g_m_obj;
    public Image image;
    public Text clear;
    public Text over;
    private float alpha;
    private float alpha_max = 0.7f;
    // Use this for initialization
    void Start () {
        clear.enabled = false;
        over.enabled = false;
        g_m = g_m_obj.GetComponent<Game_over>();
	}
	
	// Update is called once per frame
	void Update () {
        if (g_m.game_over == true || g_m.game_clear == true) {
            alpha += 0.05f;
            if (alpha > alpha_max) {
                alpha = alpha_max;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b,alpha);
        }
        if (alpha == alpha_max) {
            if (g_m.game_clear)
            {
                clear.enabled = true;
            }
            else if (g_m.game_over) {
                over.enabled = true;
            }
        }
	}
}
