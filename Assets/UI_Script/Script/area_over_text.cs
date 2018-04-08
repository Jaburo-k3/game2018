using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class area_over_text : MonoBehaviour {
    Text text;
    float initial_alpha;
    bool add = false;
	// Use this for initialization
	void Start () {
        text = this.GetComponent<Text>();
        initial_alpha = text.color.a;
	}
	
	// Update is called once per frame
	void Update () {
        if (text.enabled)
        {
            float alpha;
            if (!add)
            {
                alpha = -0.05f;
            }
            else {
                alpha = 0.05f;
            }
            text.color += new Color(0, 0, 0, alpha);
            if (text.color.a > initial_alpha)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, initial_alpha);
                add = false;
            }
            else if (text.color.a < 0)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                add = true;
            }
        }
        else {
            if (text.color.a != initial_alpha) {
                text.color = new Color(text.color.r, text.color.g, text.color.b, initial_alpha);
            }
        }
	}
}
