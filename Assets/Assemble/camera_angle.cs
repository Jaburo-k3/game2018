using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_angle : MonoBehaviour {
    public Vector3 target_vec;

    public float angle_x;
    public float angle_y;

    public Vector2 Chara_Vec_old;
    public Vector2 Chara_Vec_new;

    public Vector3 Camera_Vec_old;
    public Vector3 Camera_Vec_new;


    public GameObject Camera_obj;
    private camera Camera;

    public GameObject Camera_Vec_obj;

    public GameObject Chara_obj;
    private Character Chara;

    private assemble_camera Assem_Camera;

    public GameObject center_point;

    public bool LockOn = false;
    public int target_lock = 0;



    
    public void update_angle(float a,float b)
    {
        angle_x = 0;
        angle_y = 0;
        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //xz平面の更新
        angle_x = a;
        float x = 0;
        if (-1f * angle_x + Assem_Camera.angle.x < -2.0f)
        {
            x = 2f;
        }
        Assem_Camera.add_angle.x = -1f * angle_x + Assem_Camera.angle.x + x;
        Assem_Camera.save_angle.x = angle_x;
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲


        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //ｙ軸の更新
        angle_y = b;
        Assem_Camera.add_angle.y =  -1 * angle_y + Assem_Camera.angle.y;
        Assem_Camera.save_angle.y = angle_y;

        if (Assem_Camera.camera_lock == false) {
            Assem_Camera.end_coroutine();
            Assem_Camera.camera_lock = false;
            Assem_Camera.move();
        }
        else {
            Assem_Camera.camera_lock = false;
            Assem_Camera.move();
        }
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        //mouse_vec.mouse += new Vector2(angle_x,angle_y) * Time.deltaTime * 2f;
        //Debug.Log(angle_x);
        //Debug.Log(angle_y);
    }


    void Awake()
    {
       Assem_Camera = this.GetComponent<assemble_camera>();
        center_point = GameObject.Find("Player/center_point");
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            update_angle(1.0f,0.7f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            update_angle(-0.5f, 0.4f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            update_angle(0f, 0.7f);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            update_angle(1.0f, 0.4f);
        }
        */
    }
}
