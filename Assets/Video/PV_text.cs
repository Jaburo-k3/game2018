using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PV_text : MonoBehaviour {
    public Text text;
    public bool up_down = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (text.color.a <= 0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            up_down = true;
        }
        else if (text.color.a >= 1) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
            up_down = true;
            up_down = false;
        }
        if (up_down == false)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.025f);
        }
        else {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + 0.025f);
        }
	}
}
