using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockon : MonoBehaviour {
    public GameObject target_obj_save;
    public GameObject target_obj;
    public Transform target_obj_transform;
    public Vector3 target_obj_pos;

    public float angle_x;
    public float angle_y;

    public Vector2 Chara_Vec_old;
    public Vector2 Chara_Vec_new;

    public Vector3 Camera_Vec_old;
    public Vector3 Camera_Vec_new;

    public GameObject A_sys_obj;
    private Aiming_system A_sys;

    public GameObject Camera_obj;
    private camera Camera;

    public GameObject Camera_Vec_obj;

    public GameObject Chara_obj;
    private Character Chara;

    public GameObject Player_obj;
    private Player_move player_move;

    public GameObject mouse_obj;
    private mouse_Vec mouse_vec;

    public GameObject center_point;

    public bool LockOn = false;
    public int target_lock = 0;

    public float min_distance = 1000;

    float correction_value;
    float correction_value_const = 0.01f;

    private Rigidbody rb;

    private bool debug_switch = false;
    public bool lockon_lock = false;

    bool not_target = false;
    private float distance_DotandVec(Vector3 camera_vec, Vector3 target_pos) {
        Vector3 camera_focus = Chara_obj.transform.position;
        camera_focus.y = center_point.transform.position.y;
        camera_focus.y += 1f;
        Vector3 test = camera_focus - Camera_obj.transform.position;
        Vector3 CandT_distance = (target_pos - Camera_obj.transform.position);
        Vector3 cross = Vector3.Cross(test, CandT_distance);

        //Debug.DrawLine(Chara_obj.transform.position,Chara_obj.transform.position + test * 10);
        
        //Debug.DrawLine(Chara_obj.transform.position, Chara_obj.transform.position + CandT_distance * 10);

        float Distance = cross.magnitude;
        float Length = test.magnitude;
        float Height = Distance / Length;
        return Height;  
    }

    public void target_update(Collider other) {
        Vector3 other_pos = other.transform.position;

        float h = distance_DotandVec(Camera_obj.transform.forward, other_pos);

        if (h < min_distance && target_obj_save != other.gameObject)
        {
            min_distance = h;
            target_obj_save = other.gameObject;
        }
        else if (target_obj_save == other.gameObject)
        {
            min_distance = h;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "enemy")
        {
            not_target = false;
            target_update(other);
            if (target_lock == 1 && target_obj == null)
            {
                not_target = true;
                min_distance = 100000;
                target_lock = 2;
                Debug.Log("destroy");
            }
        }

        //Debug.Log(target_obj_save);
    }


    private void OnTriggerExit(Collider other) {
        if (other.tag == "enemy")
        {
            //Debug.Log("exit");
            //Debug.Log("exit_name = " + other.name);
            //Debug.Log("target_name = " + target_obj_save);
            if (target_obj_save == other.gameObject)
            {
                not_target = true;
                min_distance = Mathf.Infinity;
                //Debug.Log("Exit");
                target_obj_save = null;
                target_lock = 2;
                //Debug.Log(target_lock);
            }
        }
    }
    void Lockon() {
        angle_x = 0;
        angle_y = 0;
        if (target_obj_pos.x == transform.position.x &&
                target_obj_pos.z == transform.position.z)
        {
            return;
        }
        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //xz平面の更新
        /*
        Vector3 future_pos = Chara_obj.transform.position;
        future_pos += player_move.amount_move;
        Vector3 lock_pos = target_obj_pos - future_pos;
        Chara_Vec_old = new Vector2(Chara_obj.transform.forward.x, Chara_obj.transform.forward.z);

        Vector3 look_pos = new Vector3(target_obj_pos.x, Chara_obj.transform.position.y, target_obj_pos.z);
        //look_pos.x += player_move.amount_move.x;
        //look_pos.z += player_move.amount_move.z;
        Chara_obj.transform.LookAt(look_pos);

        Chara_Vec_new = new Vector2(lock_pos.x, lock_pos.z);

        */

        Chara_Vec_old = new Vector2(Chara_obj.transform.forward.x, Chara_obj.transform.forward.z);

        Vector3 look_pos = new Vector3(target_obj_pos.x, Chara_obj.transform.position.y, target_obj_pos.z);
        //look_pos.x += player_move.amount_move.x;
        //look_pos.z += player_move.amount_move.z;
        Chara_obj.transform.LookAt(look_pos);

        Chara_Vec_new = new Vector2(Chara_obj.transform.forward.x, Chara_obj.transform.forward.z);


        if (Chara_Vec_old != Chara_Vec_new)
        {
            float rad = Vector2.Dot(Chara_Vec_old, Chara_Vec_new);
            float d = Mathf.Clamp(rad, -1.0f, 1.0f);
            
            angle_x = Mathf.Acos(d / Chara_Vec_old.magnitude * Chara_Vec_new.magnitude);
            if (float.IsNaN(angle_x))
            {
                angle_x = 0;
            }

            Vector3 cross = Vector3.Cross(Chara_Vec_old, Chara_Vec_new);

            if (angle_x == 0)
            {
                correction_value = 0;
            }
            else {
                correction_value = correction_value_const;
            }

            if (cross.z < 0)
            {
                angle_x *= -1;
                if (player_move.get_boost_style() == player_move.right)
                {
                    mouse_vec.mouse.x += (angle_x + (correction_value * player_move.boostspeed/player_move.boost_max_speed) ) * (Time.deltaTime * 5f);
                }
                else if (player_move.get_boost_style() == player_move.left) {
                    mouse_vec.mouse.x += (angle_x - (correction_value * player_move.boostspeed / player_move.boost_max_speed)) * (Time.deltaTime * 5f);
                }
                else {
                    mouse_vec.mouse.x += (angle_x) * (Time.deltaTime * 5f);
                }
            }
            else {
                if (player_move.get_boost_style() == player_move.right)
                {
                    mouse_vec.mouse.x += (angle_x + (0.01f * player_move.boostspeed / player_move.boost_max_speed)) * (Time.deltaTime * 5f);
                }
                else if (player_move.get_boost_style() == player_move.left)
                {
                    mouse_vec.mouse.x += (angle_x - (0.01f * player_move.boostspeed / player_move.boost_max_speed)) * (Time.deltaTime * 5f);
                }
                else {
                    mouse_vec.mouse.x += (angle_x) * (Time.deltaTime * 5f);
                }
            }
            //mouse_vec.mouse.x += (angle_x) * (Time.deltaTime * 3f);
            //mouse_vec.snipe.x += angle_x * (Time.deltaTime * 4f);

        }
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲


        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //ｙ軸の更新

        /*
        Camera_Vec_old = new Vector3(Camera_obj.transform.up.x * Mathf.Cos(angle_x) - Camera_obj.transform.up.z * Mathf.Sin(angle_x),
            Camera_obj.transform.up.y, Camera_obj.transform.up.x * Mathf.Sin(angle_x) + Camera_obj.transform.up.z * Mathf.Cos(angle_x));

        Camera_Vec_obj.transform.LookAt(target_obj_pos);

        Camera_Vec_new = lock_pos;
        */

        Camera_Vec_old = new Vector3 (Camera_obj.transform.up.x * Mathf.Cos(angle_x) - Camera_obj.transform.up.z * Mathf.Sin(angle_x),
            Camera_obj.transform.up.y, Camera_obj.transform.up.x * Mathf.Sin(angle_x) + Camera_obj.transform.up.z * Mathf.Cos(angle_x));

        //target_obj_pos += player_move.amount_move;

        Camera_Vec_obj.transform.LookAt(target_obj_pos);
        //transform.LookAt(target_obj_pos);
        

        Camera_Vec_new = Camera_Vec_obj.transform.up;

        if (Camera_Vec_old != Camera_Vec_new)
        {
            float rad = Vector3.Dot(Camera_Vec_old, Camera_Vec_new);
            float d = Mathf.Clamp(rad, -1.0f, 1.0f);
            float Cos = d / Camera_Vec_old.magnitude * Camera_Vec_new.magnitude;

            angle_y = Mathf.Acos(d / Camera_Vec_old.magnitude * Camera_Vec_new.magnitude);

            if (float.IsNaN(angle_y))
            {
                angle_y = 0;
            }
            //Debug.Log(angle_y);
            Vector3 cross = Vector3.Cross(Camera_Vec_old, Camera_Vec_new);
            Vector2 cross_2d = new Vector2(cross.x, cross.z);


            Vector3 cross_cross = Vector3.Cross(Chara_Vec_new, cross_2d);
            //Debug.DrawLine(Chara_obj.transform.position, Chara_obj.transform.position + (cross_cross * 5));
            if (cross_cross.z > 0)
            {
                angle_y *= -1;
            }
            mouse_vec.mouse.y += angle_y * (Time.deltaTime * 3f);
            mouse_vec.mouse_limit();
            //mouse_vec.snipe.y += angle_y * (Time.deltaTime * 4f);
        }
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        //mouse_vec.mouse += new Vector2(angle_x,angle_y) * Time.deltaTime * 2f;
    }

    // Use this for initialization
    void Start () {
        A_sys = A_sys_obj.GetComponent<Aiming_system>();
        Camera = Camera_obj.GetComponent<camera>();
        Chara = Chara_obj.GetComponent<Character>();
        mouse_vec = mouse_obj.GetComponent<mouse_Vec>();
        player_move = Player_obj.GetComponent<Player_move>();
        rb = GetComponent<Rigidbody>();
        center_point = GameObject.Find("Player/center_point");

    }

    // Update is called once per frame
    void Update() {
        if (target_obj_save == null)
        {
            min_distance = Mathf.Infinity;
        }
        if (Input.GetMouseButtonDown(1)) {
            target_lock = 1;
            target_obj = target_obj_save;
        }
        if (Input.GetMouseButton(1)) {
            //Debug.Log("test");
        }
            //ロックオン実行
            if (Input.GetMouseButton(1) && not_target == false && lockon_lock == false)
            {
                if (target_lock == 2)
                {
                    target_obj = target_obj_save;
                    target_lock = 1;
                    Debug.Log("go");
                }
                if (target_obj != null)
                {
                    //Debug.Log("1");
                    target_obj_pos = target_obj.transform.position;
                    Lockon();
                    LockOn = true;
                }
            //Debug.Log("lockon");
            }
            else {
                LockOn = false;
                //Camera.lockon = false;
            }

            if (Input.GetMouseButtonUp(1))
            {
                target_lock = 0;
            }

            if (Input.GetKeyDown("g"))
            {
                if (debug_switch == true)
                {
                    debug_switch = false;
                }
                else {
                    debug_switch = true;
                }
                Debug.Log(target_obj_save);
            }

            //Debug.Log(target_lock);
            //target_object = C_test.hit.collider;
    }
}
