using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_flame_update : MonoBehaviour {
    public Image flame;
    public Image backflame;
    public Image Exit_back_flame;
    public Vector3 top_pos;
    public Vector3 pos;
    public float pos_distance;

    public void flame_update(int number)
    {
            pos = top_pos;
            pos.y = top_pos.y - pos_distance * number;
            flame.transform.localPosition = pos;
        if (number == assemble_status.my_weapon_number.Length)
        {
            Exit_back_flame.enabled = true;
            flame.enabled = false;
        }
        else {
            Exit_back_flame.enabled = false;
            flame.enabled = true;
        }
    }
    public void backflame_update(bool mode) {
        if (mode)
        {
            backflame.enabled = true;
        }
        else {
            backflame.enabled = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        top_pos = flame.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

