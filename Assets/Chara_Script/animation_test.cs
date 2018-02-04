using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_test : MonoBehaviour {

    public bool animation_lock = false;
    private Player_move Player;
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("unitychan").GetComponent<Player_move>();
	}

    // Update is called once per frame
    void Update()
    {
        bool jump = false;
        bool left = false;
        bool right = false;
        bool back = false;
        bool fly = false;
        bool run = false;
        if (animation_lock == false)
        {
            if (Input.GetKey("w"))
            {
                jump = true;
            }
            else if (Input.GetKey("s"))
            {
                back = true;
            }
            else if (Input.GetKey("a"))
            {
                left = true;
            }
            else if (Input.GetKey("d"))
            {
                right = true;
            }
            if (Input.GetKey("space"))
            {
                fly = true;
            }
            string boost_style = Player.get_boost_style();
            if (boost_style != null)
            {
                run = true;
                jump = false;
                left = false;
                right = false;
                back = false;

            }
        }
        GetComponent<Animator>().SetBool("RUN", jump);
        GetComponent<Animator>().SetBool("left", left);
        GetComponent<Animator>().SetBool("right", right);
        GetComponent<Animator>().SetBool("fly", fly);
        GetComponent<Animator>().SetBool("back", back);
        GetComponent<Animator>().SetBool("run", run);
    }
}
