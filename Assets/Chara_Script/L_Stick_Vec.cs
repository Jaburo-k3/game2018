using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Stick_Vec : MonoBehaviour {
    public Vector2 stick_vec = Vector2.zero;
    public float spinSpeed = 1f;


    private float S = 1f;
    private float E = 1f;

    public bool stick_lock = false;

    private float Clamp_S;//マウスy軸の最低値
    private float Clamp_E;//マウスy軸の最高値

    public void mouse_limit()
    {
        stick_vec.y = Mathf.Clamp(stick_vec.y, Clamp_S, Clamp_E);
    }


    // Use this for initialization
    void Start()
    {
        Clamp_S = S;
        Clamp_E = E;

    }

    // Update is called once per frame
    void Update()
    {
        if (stick_lock == false)
        {
            stick_vec = new Vector2(Input.GetAxis("Horizontal") * -1, Input.GetAxis("Vertical") );
        }
    }
}
