using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
    public Transform target_object;
    public Vector3 target_pos;
    public float spinSpeed = 1.0f;
    private float radius = 0.9f;
    float x;
    public Vector3 nowPos;
    public Vector3 save_pos;
    public Vector3 save_vec;
    Vector3 pos = Vector3.zero;
    public Vector2 mouse = Vector2.zero;

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
    private Player_move player_move;

    private HP hp;

    public GameObject Aim_obj;
    private Aiming_system Aim;

    private chara_status Chara_Status;

    public GameObject center_point;




    public Camera camera_obj;

    public bool lockon = false;
    public bool camera_lock = false;
    public bool camera_move_now = false;

    private float Clamp_S = 0.05f;//マウスy軸の最低値
    private float Clamp_E = 0.75f;//マウスy軸の最高値



    IEnumerator camera_move()
    {
        player_move.gravity_lock = true;
        Vector3 C_move_vec = Vector3.zero;

        //Vector3 look_point;

        if (mode_snipe == false)
        {
            look_point = Aim.hit.point;
        }
        else {
            look_point = ray_hit.point;
        }
        while (true) {
            yield return new WaitForSeconds(0.0001f);
            float distance;
            //移動距離測定
            if (mode_snipe == false)
            {
                //C_move_vec = (player_obj.transform.position - transform.position) / 10;

                //C_move_vec = (player_obj.transform.position - transform.position) * Time.deltaTime * 6f;前
                C_move_vec = (center_point.transform.position - transform.position) * Time.deltaTime * 6f;
                distance = Vector3.Distance(center_point.transform.position, transform.position);
                if (distance < 0.05)
                {
                    transform.position = center_point.transform.position;
                }
            }
            else if(mode_snipe == true){
                //C_move_vec = (save_pos - transform.position) / 10;
                C_move_vec = (save_pos - transform.position) * Time.deltaTime * 6f;
                distance = Vector3.Distance(save_pos, transform.position);
                if (distance < 0.05)
                {
                    transform.position = save_pos;
                }
            }

            //カメラズーム
            if (mode_snipe == false)
            {
                camera_obj.fieldOfView -= 2f;
            }
            else if(mode_snipe == true){
                camera_obj.fieldOfView += 2.5f;
            }

            if (camera_obj.fieldOfView <= 20) {
                camera_obj.fieldOfView = 20;
            }
            if (camera_obj.fieldOfView >= 60) {
                camera_obj.fieldOfView = 60;
            }


            //移動終了
            if (mode_snipe == false && transform.position == center_point.transform.position)
            {
                Debug.Log("move_end");
                mode_snipe = true;

                camera_lock = false;

                camera_lock = false;

                R_stick_vec.spinSpeed = 0.25f;

                R_stick_vec.stick_lock = false;

                Lockon.lockon_lock = true;

                player_move.move_lock = true;
                player_move.gravity_lock = false;

                camera_move_now = false;

                break;
            }
            else if (mode_snipe == true && transform.position == save_pos)
            {

                mode_snipe = false;

                camera_lock = false;

                camera_lock = false;

                R_stick_vec.spinSpeed = 0.75f;

                R_stick_vec.stick_lock = false;

                Lockon.lockon_lock = false;

                player_move.move_lock = false;
                player_move.gravity_lock = false;

                camera_move_now = false;

                break;
            }


            //カメラ移動
            transform.position += C_move_vec;
            transform.LookAt(look_point);
        }
    }
    // Use this for initialization
    void Start()
    {
        

        nowPos = transform.position;
        //mouse.y = 0.3f;
        R_stick_vec = R_stick_obj.GetComponent<R_Stick_Vec>();
        Lockon = lockon_obj.GetComponent<lockon>();
        Aim = Aim_obj.GetComponent<Aiming_system>();
        player_move = player_obj.GetComponent<Player_move>();
        hp = player_obj.GetComponent<HP>();
        Chara_Status = player_obj.GetComponent<chara_status>();

        center_point = GameObject.Find("Player/center_point");
        //target_object = center_point.transform;
        ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("Ignore Raycast"));

        Chara_Status.camera_obj = this.gameObject;

        this.gameObject.transform.parent = null;
        //target_object = center_point.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //スナイプモード切り替え
        if (Input.GetKeyDown(KeyCode.LeftShift) && camera_move_now == false && hp.get_hp() > 0) {

            camera_lock = true;
            R_stick_vec.stick_lock = true;
            Lockon.lockon_lock = true;
            player_move.move_lock = true;
            player_move.gravity_lock = true;
            camera_move_now = true;
            StartCoroutine("camera_move");
            //TPSカメラ状態からFPSカメラへ移動
            //Debug.Log("Shift");
        }
        mouse = R_stick_vec.stick_vec;


        pos.x = Mathf.Sin(mouse.y * Mathf.PI)  * radius * Mathf.Cos(mouse.x * Mathf.PI);
        pos.y = Mathf.Cos(mouse.y * Mathf.PI);
        pos.z = Mathf.Sin(mouse.y * Mathf.PI) * radius * Mathf.Sin(mouse.x * Mathf.PI);

        //pos *= nowPos.z;
        pos *= -6f;

        //pos.y += nowPos.y;
        pos.y += 5.939f;
        

        save_pos = pos + target_object.position;

        
        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //通常モード
        if (camera_lock == false  && mode_snipe == false)
        {

            transform.position = pos + target_object.position;
            save_pos = pos + target_object.position;

            Vector3 look_pos = new Vector3(target_object.position.x, center_point.transform.position.y + 1f, target_object.position.z);

            transform.LookAt(look_pos);
        }
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //スナイプモード
        else if (camera_lock == false && mode_snipe == true)
        {

            //save_pos = pos + target_object.position;
            save_pos = pos + target_object.position;

            //Vector3 ray_pos = target_object.transform.position;
            Vector3 ray_pos = center_point.transform.position;
            ray_pos.y += 1;
            Vector3 ray_vec = ray_pos - save_pos;

            snipe_ray = new Ray(ray_pos, ray_vec);

            //Physics.Raycast(snipe_ray, out ray_hit, Mathf.Infinity);
            Physics.SphereCast(snipe_ray, 0.1f, out ray_hit, Mathf.Infinity,ignore_layer);

            transform.position = center_point.transform.position;
            transform.LookAt(ray_hit.point);
        }
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        //Debug.Log("save" + save_pos);
        //Debug.Log("trans" + transform.position);
    }
}
