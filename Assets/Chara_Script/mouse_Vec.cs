using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_Vec : MonoBehaviour {
    public Vector2 mouse = Vector2.zero;
    public Vector2 snipe = Vector2.zero;
    public float spinSpeed = 0.75f;

    private lockon Lockon;
    public GameObject Lockon_obj;

    public bool mouse_lock = false;
    public bool mode_FPS = false;

    private float S = 0.1f;
    private float E = 0.75f;
    private float snipe_S = 0.05f;
    private float snipe_E = 0.75f;



    private float Clamp_S;//マウスy軸の最低値
    private float Clamp_E;//マウスy軸の最高値

    private float Clamp_snipe_S;//マウスy軸の最低値
    private float Clamp_snipe_E;//マウスy軸の最高値

    public void mouse_limit() {
        mouse.y = Mathf.Clamp(mouse.y, Clamp_S, Clamp_E);
    }


    // Use this for initialization
    void Start () {
        Clamp_S = S;
        Clamp_E = E;
        Clamp_snipe_S = snipe_S;
        Clamp_snipe_E = snipe_E;
        mouse.y = 0.4f;
        mouse.x = 0.5f;
        //snipe.y = 0.3f;
        Lockon = Lockon_obj.GetComponent<lockon>();
        //Debug.Log(Clamp_E);
        //Debug.Log(Clamp_snipe_E);
        //Debug.Log(Clamp_S);
        //Debug.Log(Clamp_snipe_S);

    }

    // Update is called once per frame
    void Update()
    {
        if (mouse_lock == false)
        {
            if (Lockon.LockOn == false)
            {
                mouse += new Vector2(Input.GetAxis("Mouse X") * -1, Input.GetAxis("Mouse Y") * -1) * Time.deltaTime * spinSpeed;
                snipe += new Vector2(Input.GetAxis("Mouse X") * -1, Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
                
            }

            mouse_limit();
            //mouse.y = Mathf.Clamp(mouse.y, Clamp_S, Clamp_E);
            snipe.y = Mathf.Clamp(snipe.y, Clamp_snipe_S, Clamp_snipe_E);
            //Debug.Log(mouse.x);
            //Debug.Log(Input.GetAxis("Mouse X") * -1);
        }

        /*
        if (mode_FPS) {
            FPS += new Vector2(Input.GetAxis("Mouse X") * -1, Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
            FPS.y = Mathf.Clamp(FPS.y, Clamp_S, Clamp_E);
        }
        */

    }
}
