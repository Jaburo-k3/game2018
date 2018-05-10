using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Stick_Vec : MonoBehaviour {
    public Vector2 stick_vec = Vector2.zero;
    public float spinSpeed = 1f;


    private float S = 0.1f;
    private float E = 0.75f;

    public bool stick_lock = false;

    private float Clamp_S;//Lスティックy軸の最低値
    private float Clamp_E;//Lスティックy軸の最高値

    private HP hp;

    public void vec_limit() {
        stick_vec.y = Mathf.Clamp(stick_vec.y, Clamp_S, Clamp_E);
    }

    void Awake() {
        hp = this.GetComponent<HP>();
    }

    // Use this for initialization
    void Start () {
        Clamp_S = S;
        Clamp_E = E;
        stick_vec.y = 0.4f;
        stick_vec.x = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.get_hp() <= 0) {
            return;
        }
        if (stick_lock == false)
        {
            stick_vec += new Vector2(Input.GetAxis("Horizontal2") * -1, Input.GetAxis("Vertical2") * -1) * Time.deltaTime * spinSpeed;
            vec_limit();
        }
    }
}
