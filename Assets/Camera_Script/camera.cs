using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
    public Transform target_object;
    public Vector3 target_pos;
    public Vector3 center_pos;
    public Vector3 center_look_pos;
    public float spinSpeed = 1.0f;
    private float radius = 0.9f;
    float lerp_value = 15f;
    public Vector3 nowPos;
    public Vector3 save_pos;
    public Vector3 save_vec;
    Vector3 pos = Vector3.zero;
    public Vector2 stick_vec = Vector2.zero;

    Vector3 look_point;
    public bool mode_snipe = false;

    public Ray snipe_ray;
    public RaycastHit ray_hit;

    public LayerMask ignore_layer;

    Vector3 ray_pos;
    Vector3 ray_vec;


    public GameObject R_stick_obj;
    public R_Stick_Vec R_stick_vec;

    public GameObject lockon_obj;
    private lockon Lockon;

    public GameObject player_obj;

    private HP hp;

    public GameObject Aim_obj;
    private Aiming_system Aim;

    private chara_status Chara_Status;

    public GameObject center_point;

    private chara_status Chara_status;


    public Camera camera_obj;

    public bool lockon = false;
    public bool camera_lock = false;
    public bool camera_move_now = false;

    private float Clamp_S = 0.05f;//マウスy軸の最低値
    private float Clamp_E = 0.75f;//マウスy軸の最高値

    // Use this for initialization
    void Start()
    {
        

        nowPos = transform.position;
        //mouse.y = 0.3f;
        R_stick_vec = R_stick_obj.GetComponent<R_Stick_Vec>();
        Lockon = lockon_obj.GetComponent<lockon>();
        Aim = Aim_obj.GetComponent<Aiming_system>();
        hp = player_obj.GetComponent<HP>();
        Chara_Status = player_obj.GetComponent<chara_status>();

        center_point = GameObject.Find("Player/center_point");
        //target_object = center_point.transform;
        ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("Ignore Raycast"));

        Chara_Status.camera_obj = this.gameObject;

        this.gameObject.transform.parent = null;

        center_pos = target_object.transform.position;
        center_look_pos = new Vector3(target_object.position.x, center_point.transform.position.y + 2f, target_object.position.z);
        //target_object = center_point.transform;
    }

    void FixedUpdate() {
        if (hp.get_hp() > 0)
        {
            stick_vec = R_stick_vec.stick_vec;
        }
        pos.x = Mathf.Sin(stick_vec.y * Mathf.PI) * radius * Mathf.Cos(stick_vec.x * Mathf.PI);
        pos.y = Mathf.Cos(stick_vec.y * Mathf.PI);
        pos.z = Mathf.Sin(stick_vec.y * Mathf.PI) * radius * Mathf.Sin(stick_vec.x * Mathf.PI);

        //pos *= nowPos.z;
        pos *= -6f;

        //pos.y += nowPos.y;
        pos.y += 5.939f;


        save_pos = pos + target_object.position;


        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //通常モード
        if (camera_lock == false && mode_snipe == false)
        {
            center_pos = Vector3.Lerp(center_pos, target_object.position, lerp_value * Time.deltaTime);

            //transform.position = pos + target_object.position;
            transform.position = pos + center_pos;
            save_pos = pos + target_object.position;

            //Vector3 look_pos = new Vector3(target_object.position.x, center_point.transform.position.y + 2f, target_object.position.z);
            center_look_pos = Vector3.Lerp(center_look_pos, new Vector3(target_object.position.x, center_point.transform.position.y + 2f, target_object.position.z), 
                lerp_value * Time.deltaTime);

            transform.LookAt(center_look_pos);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
