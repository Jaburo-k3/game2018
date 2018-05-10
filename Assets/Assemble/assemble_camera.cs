using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assemble_camera : MonoBehaviour {
    public Transform target_object;
    public Vector3 target_pos;
    public float spinSpeed = 1.0f;
    private float radius = 1f;
    Vector3 nowPos;
    public Vector3 save_pos;
    public Vector3 save_vec;
    Vector3 pos = Vector3.zero;
    public Vector2 angle = Vector2.zero;
    public Vector2 save_angle = Vector2.zero;
    public Vector2 save_add_angle = Vector2.zero;
    Vector2 test_vec;

    public Coroutine Cor;

    Vector3 look_point;
    public bool mode_snipe = false;

    public Ray snipe_ray;
    public RaycastHit ray_hit;

    Vector3 ray_pos;
    Vector3 ray_vec;

    public GameObject center_point;

    //private float x = 0.01f;
    public Vector2 add_angle = Vector2.zero;
    private float y = 0;

    public Camera camera_obj;

    public bool lockon = false;
    public bool camera_move_now = false;

    private float Clamp_S = 0.01f;//マウスy軸の最低値
    private float Clamp_E = 0.99f;//マウスy軸の最高値

    public bool camera_lock = true;

    public void move() {
        Cor = StartCoroutine(camera_move());
        //StartCoroutine("test");
    }
    public void end_coroutine() {
        StopCoroutine(Cor);
    }

    //
    IEnumerator camera_move()
    {
        yield return new WaitForSeconds(1f / 2f);
        angle.x = save_angle.x;
        
        angle.y = save_angle.y;
        camera_lock = true;
    }
    void Awake() {
        angle.y = 0.4f;
        center_point = GameObject.Find("Player/center_point");
    }
    // Use this for initialization
    void Start()
    {
        this.gameObject.transform.parent = null;

        nowPos = transform.position;

        //center_point = GameObject.Find("Player/center_point");

        //angle.y = 0.4f;
        //StartCoroutine("test");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.SqrMagnitude(new Vector2(Input.GetAxis("Horizontal2") * -1, Input.GetAxis("Vertical2"))) > 0.1f) {
            angle += new Vector2(Input.GetAxis("Horizontal2") * -1, Input.GetAxis("Vertical2")) * Time.deltaTime * spinSpeed;
        }
        //angle = mouse_vec.mouse;
        if (Input.GetMouseButton(1))
        {
            angle += new Vector2(Input.GetAxis("Mouse X") * -1, Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;
        }
        if (camera_lock == false) {
            Debug.Log("test");
            angle -= add_angle * Time.deltaTime * 2f;
            save_add_angle -= add_angle * Time.deltaTime * 2f;
            //Debug.Log(x);
        }


        if (angle.x < -2.0f)
        {
            angle.x = 0;
        }
        else if (angle.x > 2.0f) {
            angle.x = 0;
        }
        
        //angle.y += x * Time.deltaTime;
        angle.y = Mathf.Clamp(angle.y, Clamp_S, Clamp_E);
        /*
        mouse.x *= 1000;
        mouse.x = Mathf.Floor(mouse.x);
        mouse.x *= 0.001f;
        */


        pos.x = Mathf.Sin(angle.y * Mathf.PI) * radius * Mathf.Cos(angle.x * Mathf.PI);
        pos.y = Mathf.Cos(angle.y * Mathf.PI);
        pos.z = Mathf.Sin(angle.y * Mathf.PI) * radius * Mathf.Sin(angle.x * Mathf.PI);
        pos *= nowPos.z;

        pos.y += nowPos.y;


        save_pos = pos + target_object.position;


        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //通常モード
        transform.position = pos + target_object.position;
        save_pos = pos + target_object.position;

        Vector3 look_pos = center_point.transform.position;
        transform.LookAt(look_pos);
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        //Debug.Log("save" + save_pos);
        //Debug.Log("trans" + transform.position);
    }
}
