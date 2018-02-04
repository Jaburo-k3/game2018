using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {
    static public float time = 0;
    public Text text;
    private Game_over game_over;
	// Use this for initialization
	void Start () {
        game_over = GameObject.Find("Game_manage").GetComponent<Game_over>();
        time = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (game_over.game_clear == false)
        {
            time += Time.deltaTime;
        }
        else {
            if (text.transform.localPosition.x > 0)
            {
                //text.transform.localScale += new Vector3(0.04f,0.04f);
                text.fontSize += 2;
                text.transform.localPosition -= new Vector3(10,9f);

            }
        }
        text.text = time.ToString("F2");

	}
}
