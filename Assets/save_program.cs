using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_program : MonoBehaviour {
    public LayerMask mask;

    public Transform target;
    Vector2 mouse = Vector2.zero;
    public Vector3 save_pos;
    public Rigidbody rb;
    public float spinSpeed = 1.0f;
    public float rotation;
    public float normalspeed = 0.1f;//半径
    public float back_radius = 0.1f;
    public float boostspeed = 0.1f;
    public float mouse_save;
    public int boost_style = 0;
    private float ray_P_angle = 0.166f;
    private float ray_N_angle = -0.166f;
    private float quarter_P = 0.25f;
    private float quarter_N = -0.25f;
    private float half = 0.5f;
    public string[] key_list = new string[4];
    public int list_number = 0;
    public int list_B_number = 0;
    public int list_D_number = 0;
    public string boost_switch;
    string forward = "forward";
    string back = "back";
    string left = "left";
    string right = "right";
    //■■■raycastを前方、その左右(30度)に飛ばすためのショートカット■■■
    public Vector3 ray_forward(float radius, float angle)
    {
        return new Vector3(radius * Mathf.Cos((angle + mouse.x) * Mathf.PI), 0, radius * Mathf.Sin((angle + mouse.x) * Mathf.PI));
    }

    //raycastを後方、その左右(30度)に飛ばすためのショートカット
    public Vector3 ray_back(float radius, float angle)
    {
        return new Vector3(-1 * radius * Mathf.Cos((angle + mouse.x) * Mathf.PI), 0, -1 * radius * Mathf.Sin((angle + mouse.x) * Mathf.PI));
    }

    //raycastを左、その左右(30度)に飛ばすためのショートカット
    public Vector3 ray_left(float radius, float angle)
    {
        return new Vector3(radius * Mathf.Sin((angle + mouse.x) * -1 * Mathf.PI), 0, radius * Mathf.Cos((angle + mouse.x) * Mathf.PI));
    }

    //raycastを後方、その左右(30度)に飛ばすためのショートカット
    public Vector3 ray_right(float radius, float angle)
    {
        return new Vector3(radius * Mathf.Sin((angle + mouse.x) * Mathf.PI), 0, -1 * radius * Mathf.Cos((angle + mouse.x) * Mathf.PI));
    }

    public bool move_permit(string direction, float radius, float angle)
    {
        Ray ray;
        Ray ray_P;
        Ray ray_N;
        RaycastHit hit;
        RaycastHit hit_P;
        RaycastHit hit_N;
        if (direction == "forward")
        {
            ray = new Ray(transform.position, ray_forward(radius, angle));
            ray_P = new Ray(transform.position, ray_forward(radius, angle + ray_P_angle));
            ray_N = new Ray(transform.position, ray_forward(radius, angle + ray_N_angle));
        }
        else if (direction == "back")
        {
            ray = new Ray(transform.position, ray_back(radius, angle));
            ray_P = new Ray(transform.position, ray_back(radius, angle + ray_P_angle));
            ray_N = new Ray(transform.position, ray_back(radius, angle + ray_N_angle));
        }
        else if (direction == "left")
        {
            ray = new Ray(transform.position, ray_left(radius, angle));
            ray_P = new Ray(transform.position, ray_left(radius, angle + ray_P_angle));
            ray_N = new Ray(transform.position, ray_left(radius, angle + ray_N_angle));
        }
        else {
            ray = new Ray(transform.position, ray_right(radius, angle));
            ray_P = new Ray(transform.position, ray_right(radius, angle + ray_P_angle));
            ray_N = new Ray(transform.position, ray_right(radius, angle + ray_N_angle));
        }
        if (Physics.Raycast(ray, out hit, 0.5f) == false && Physics.Raycast(ray_P, out hit_P, 0.5f) == false
                    && Physics.Raycast(ray_N, out hit_N, 0.5f) == false)
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 0, false);
            return true;
        }
        else {
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 5, false);
            return false;
        }

    }
    void ray_test()
    {
        Ray ray = new Ray(transform.position, new Vector3(normalspeed * Mathf.Cos(mouse.x * Mathf.PI),
            0, normalspeed * Mathf.Sin(mouse.x * Mathf.PI)));
        Ray ray_2 = new Ray(transform.position, ray_back(normalspeed, 0.5f));
        RaycastHit hit;
        RaycastHit hit_2;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(ray.origin, ray.direction, Color.red, 5, false);
            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
            //Debug.Log(hit.collider.gameObject.name);
            //Debug.DrawRay(ray.origin, ray.direction, Color.red, 5, false);
        }
        if (Physics.Raycast(ray_2, out hit_2, Mathf.Infinity))
        {
            //Debug.DrawRay(ray_2.origin, ray_back(radius, 0.5f), Color.red, 5, false);
        }
    }
    public void move(string direction, float speed, float angle)
    {
        Vector3 pos = transform.position;
        if (direction == forward)
        {
            pos.z += speed * Mathf.Sin((mouse.x + angle) * Mathf.PI);
            pos.x += speed * Mathf.Cos((mouse.x + angle) * Mathf.PI);
        }
        else if (direction == back)
        {
            pos.z -= speed * Mathf.Sin((mouse.x + angle) * Mathf.PI);
            pos.x -= speed * Mathf.Cos((mouse.x + angle) * Mathf.PI);
        }
        else if (direction == left)
        {
            pos.z += speed * Mathf.Cos((mouse.x + angle) * -1 * Mathf.PI);
            pos.x += speed * Mathf.Sin((mouse.x + angle) * -1 * Mathf.PI);
        }
        else {
            pos.z -= speed * Mathf.Cos((mouse.x + angle) * Mathf.PI);
            pos.x += speed * Mathf.Sin((mouse.x + angle) * Mathf.PI);
        }
        transform.position = pos;
    }
    //キャラ歩行
    public void character_move(float speed)
    {
        //直進
        if (Input.GetKey("w"))
        {
            //左斜め前
            if (Input.GetKey("a"))
            {
                //左斜め前
                if (move_permit(forward, speed, quarter_P))
                {
                    move(forward, speed, quarter_P);
                }
                //駄目なら左
                else {
                    for (float P = 0; P > -0.8f; P -= 0.05f)
                    {
                        if (move_permit(forward, speed, half + P))
                        {
                            move(forward, speed * half, half + P);
                            break;
                        }
                    }
                    /*
                    if (move_permit(left, speed, 0))
                    {
                        move(left, speed * half, 0);
                    }
                    //それも駄目なら前
                    else {
                        if (move_permit(forward, speed, 0))
                        {
                            move(forward, speed * half, 0);
                        }
                        else {
                            for (float P = 0; P > -0.6f; P -= 0.05f) {
                                if (move_permit(forward, speed, quarter_P + P)) {
                                    move(forward, speed * half, quarter_P + P);
                                    break;
                                }
                            }
                        }
                    }
                    */
                }
                //Debug.Log("左斜め前");
            }

            //右斜め前
            else if (Input.GetKey("d"))
            {
                if (move_permit(forward, speed, quarter_N))
                {
                    move(forward, speed, quarter_N);
                }
                else {
                    /*
                    if (move_permit(right,speed,0))
                    {
                        move(right, speed * half, 0);
                    }
                    else {
                        if (move_permit(forward,speed,0))
                        {
                            move(forward, speed * half, 0);
                        }
                        else {
                            for (float P = 0; P < 0.6f; P += 0.05f)
                            {
                                if (move_permit(forward, speed, quarter_N + P))
                                {
                                    move(forward, speed * half, quarter_N + P);
                                    break;
                                }
                            }
                        }
                    }
                    */
                    for (float P = 0; P < 0.8f; P += 0.05f)
                    {
                        if (move_permit(forward, speed, -1 * half + P))
                        {
                            move(forward, speed * half, -1 * half + P);
                            break;
                        }
                    }
                }
                //Debug.Log("右斜め前");
            }

            else
            {
                if (move_permit(forward, speed, 0))
                {
                    move(forward, speed, 0);
                }
            }
        }
        //後退
        else if (Input.GetKey("s"))
        {
            Vector3 pos = transform.position;
            //左斜め後ろ
            if (Input.GetKey("a"))
            {
                if (move_permit(back, speed, quarter_N))
                {
                    move(back, speed, quarter_N);
                }

                else {
                    for (float P = 0; P < 0.8f; P += 0.05f)
                    {
                        if (move_permit(back, speed, -1 * half + P))
                        {
                            move(back, speed * half, -1 * half + P);
                            break;
                        }
                    }
                    /*
                    if (move_permit(left,speed,0))
                    {
                        move(left, speed * half, 0);
                    }
                    else {
                        if (move_permit(back,speed,0))
                        {
                            move(back, speed * half, 0);
                        }
                        else {
                            for (float P = 0; P < 0.6f; P += 0.05f)
                            {
                                if (move_permit(back, speed, quarter_N + P))
                                {
                                    move(back, speed * half, quarter_N + P);
                                    break;
                                }
                            }
                        }
                    }
                    */
                }
                //Debug.Log("左斜め後ろ");
            }
            //右斜め後ろ
            else if (Input.GetKey("d"))
            {
                if (move_permit(back, speed, quarter_P))
                {
                    move(back, speed, quarter_P);
                    //pos.z -= radius * Mathf.Sin((0.25f + mouse.x) * Mathf.PI);
                    //pos.x -= radius * Mathf.Cos((0.25f + mouse.x) * Mathf.PI);
                }
                else {
                    /*
                    if (move_permit(right,speed,0))
                    {
                        move(right, speed * half, quarter_P);
                    }
                    else {
                        if (move_permit(back,speed,0))
                        {
                            move(back, speed * half, 0);
                        }
                        else {
                            for (float P = 0; P > -0.6f; P -= 0.05f)
                            {
                                if (move_permit(back, speed, quarter_P + P))
                                {
                                    move(back, speed * half, quarter_P + P);
                                    break;
                                }
                            }
                        }
                    }
                    */
                    for (float P = 0; P > -0.8f; P -= 0.05f)
                    {
                        if (move_permit(back, speed, half + P))
                        {
                            move(back, speed * half, half + P);
                            break;
                        }
                    }
                }
                //Debug.Log("右斜め後ろ");
            }
            //後退
            else
            {
                if (move_permit(back, speed, 0))
                {
                    move(back, speed, 0);
                }
                //Debug.Log("後退");
            }
            //transform.position = pos; 
        }
        //左
        else if (Input.GetKey("a"))
        {
            if (move_permit(left, speed, 0))
            {
                move(left, speed, 0);
            }
            //Debug.Log("左");
        }
        //右
        else if (Input.GetKey("d"))
        {
            if (move_permit(right, speed, 0))
            {
                move(right, speed, 0);
            }
            //Debug.Log("右");
        }
    }
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mouse += new Vector2(Input.GetAxis("Mouse X") * -1, 0) * Time.deltaTime * spinSpeed;
        if (Input.GetKeyDown("w") && key_list[list_B_number] == "w")
        {
            boost_switch = "w";
        }
        /*
        if (Input.GetKeyDown("w")) {
            key_list[list_number] = "w";
            list_number += 1;
            if (list_number > 3)
            {
                list_number = 0;
            }
            list_B_number = list_number - 1;
            list_D_number = list_number + 3;
            if (list_B_number < 0) {
                list_B_number = 3;
            }
            if (list_D_number > 3) {
                list_D_number = 0;
            }
        }
        if (boost_switch == "w" && Input.GetKeyUp("w")) {
            boost_switch = "normal";
            boostspeed = normalspeed;
            key_list[list_B_number] = null; 
        }
        if (boost_switch == "w")
        {
            boostspeed += 0.01f;
            if (boostspeed > normalspeed * 5) {
                boostspeed = normalspeed * 5;
            }
            character_move(boostspeed);
            Debug.Log("boost!!");
        }
        else {
            character_move(normalspeed);
        }
        */
        character_move(normalspeed);
        //ジャンプ
        if (Input.GetKey("space"))
        {
            rb.velocity = new Vector3(0, 7.5f, 0);
            if (this.rb.velocity.y < 0 && boost_style == 0)
            {
                rb.velocity = new Vector3(0, 7.5f, 0);
                boost_style = 1;
            }
            else if (this.rb.velocity.y < 0 && boost_style == 1)
            {
                rb.velocity = new Vector3(0, 2f, 0);
            }
        }
        mouse_save = Input.GetAxis("Mouse X") * -1;
        save_pos = transform.position;
        //Debug.Log(mouse.x);
        ray_test();
    }
}