using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour {
    public chara_status Chara_Status;
    public LayerMask mask;
    public LayerMask ignore_layer;
    public Transform target;
    Vector2 mouse = Vector2.zero;
    public Vector3 save_pos;
    public GameObject center_point;
    public Vector3 center_point_pos;
    public Rigidbody rb;

    public float rotation;
    public float normalspeed;//半径
    public float boostspeed;
    public float boost_max_speed;

    public float mouse_save;

    public GameObject T_P_obj;
    public Vector3 T_P;
    public GameObject L_P_obj;
    public Vector3 L_P;
    private CapsuleCollider Cap_Collider;
    private float C_H;
    //private float oblique_line;

    public float boost_energy;
    public float boost_energy_max = 240f;
    private string boost_style = null;
    private int hover_style = 0;// 0:初期状態  1:1段目　2:2段目  3:3段目(上昇せず浮くのみ)
    private int hover_time = 0;
    private int h_t_max = 120;

    public bool Ground_condition = false; //接地状態

    //0.084
    private float ray_P_angle = 0.084f;
    private float ray_N_angle = -0.084f;

    private float quarter_P = 0.25f;
    private float quarter_N = -0.25f;
    private float half = 0.5f;

    private string save_B_S = null; 

    public string[] key_list = new string[4];
    public int list_number = 0;
    private int list_D_number = 3;
    private int list_countdown = 0;

    public string forward = "forward";
    public string back = "back";
    public string left = "left";
    public string right = "right";


    public GameObject R_stick_obj;
    private R_Stick_Vec R_stick_vec;

    public bool move_lock = false;
    public bool gravity_lock = false;

    public Vector3 save_chara_pos;
    public float amount_move;



    public string get_boost_style() {
        return boost_style;
    }

    void OnCollisionEnter(Collision other) {
        //Debug.Log("hit");
    }

    private float C_Radian_S(float O_line,float line) {
        return Mathf.Asin(line / O_line);
    }
    private float C_Radian_C(float O_line, float line) {
        return Mathf.Acos(line / O_line);
    }


    //■■■raycastを前方、その左右(30度)に飛ばすためのショートカット■■■
    public Vector3 ray_forward(float radius, float angle) {
        return new Vector3(radius * Mathf.Cos((angle + mouse.x) * Mathf.PI),0,radius * Mathf.Sin((angle + mouse.x) * Mathf.PI));
    }

    //raycastを後方、その左右(30度)に飛ばすためのショートカット
    public Vector3 ray_back(float radius, float angle) {
        return new Vector3(-1 * radius * Mathf.Cos((angle + mouse.x) * Mathf.PI), 0, -1 * radius * Mathf.Sin((angle + mouse.x) * Mathf.PI));
    }

    //raycastを左、その左右(30度)に飛ばすためのショートカット
    public Vector3 ray_left(float radius, float angle) {
        return new Vector3(radius * Mathf.Sin((angle + mouse.x) * -1 * Mathf.PI), 0, radius * Mathf.Cos((angle + mouse.x) * Mathf.PI));
    }

    //raycastを後方、その左右(30度)に飛ばすためのショートカット
    public Vector3 ray_right(float radius, float angle) {
        return new Vector3(radius * Mathf.Sin((angle + mouse.x) * Mathf.PI), 0, -1 * radius * Mathf.Cos((angle + mouse.x) * Mathf.PI));
    }


    private Vector3 transform_foward(Vector3 pos ,float speed, float angle) {
        pos.z += speed * Mathf.Sin((mouse.x + angle) * Mathf.PI);
        pos.x += speed * Mathf.Cos((mouse.x + angle) * Mathf.PI);
        return pos;
    }
    private Vector3 transform_back(Vector3 pos, float speed, float angle)
    {
        pos.z -= speed * Mathf.Sin((mouse.x + angle) * Mathf.PI);
        pos.x -= speed * Mathf.Cos((mouse.x + angle) * Mathf.PI);
        return pos;
    }
    private Vector3 transform_left(Vector3 pos, float speed, float angle) {
        pos.z += speed * Mathf.Cos((mouse.x + angle) * -1 * Mathf.PI);
        pos.x += speed * Mathf.Sin((mouse.x + angle) * -1 * Mathf.PI);
        return pos;
    }
    private Vector3 transform_right(Vector3 pos, float speed, float angle) {
        pos.z -= speed * Mathf.Cos((mouse.x + angle) * Mathf.PI);
        pos.x += speed * Mathf.Sin((mouse.x + angle) * Mathf.PI);
        return pos;
    }


    //何を入力しているか返す
    public string GetInput()
    {
        if (Input.GetKey("w"))
        {
            return forward;
        }
        else if (Input.GetKey("s"))
        {
            return back;
        }
        else if (Input.GetKey("a"))
        {
            return left;
        }
        else if (Input.GetKey("d"))
        {
            return right;
        }
        else {
            return null;
        }
    }
    //何を入力したか返す
    public string GetInputDown()
    {
        if (Input.GetKeyDown("w"))
        {
            return forward;
        }
        else if (Input.GetKeyDown("s"))
        {
            return back;
        }
        else if (Input.GetKeyDown("a"))
        {
            return left;
        }
        else if (Input.GetKeyDown("d"))
        {
            return right;
        }
        else {
            return null;
        }
    }
    //何を入力して離したか
    public string GetInputUp()
    {
        if (Input.GetKeyUp("w"))
        {
            return forward;
        }
        else if (Input.GetKeyUp("s"))
        {
            return back;
        }
        else if (Input.GetKeyUp("a"))
        {
            return left;
        }
        else if (Input.GetKeyUp("d"))
        {
            return right;
        }
        else {
            return null;
        }
    }
    
    
    //リストの中身を一つずつ下げる
    void List_Copy(string key)
    {
        for (int i = 3; i > 0; i--)
        {
            key_list[i] = key_list[i - 1];
        }
        key_list[0] = key;
        list_D_number += 1;
    }
    //キーの入力を記憶
    void Key_Memory(string Key)
    {
        bool Not_Inputting = true;//現在入力していないかを判断するため

        //ボタンを押してる間は一番新しい入力を消さないため
        if (GetInput() != null)
        {
            Not_Inputting = false;
        }

        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //入力したキーに合わせてリストにstringを入れていき、古いキーをリスト内の一つ下に下げていく
        if (Key == forward && boost_style == null)//「ｗ(前)」を入力
        {
            List_Copy(forward);
        }
        if (Key == back && boost_style == null)//「s(後ろ)」を入力
        {
            List_Copy(back);
        }
        if (Key == left && boost_style == null)//「a(右)」を入力
        {
            List_Copy(left);
        }
        if (Key == right && boost_style == null)//「d(左)」を入力
        {
            List_Copy(right);
        }
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //同時入力を行っていて片方の入力を離した際にリストを更新する
        if (Not_Inputting == false && (GetInputUp() != null))
        {
            if (GetInput() == forward && key_list[0] != forward)
            {
                List_Copy(forward);
            }
            else if (GetInput() == back && key_list[0] != back)
            {
                List_Copy(back);
            }
            else if (GetInput() == left && key_list[0] != left)
            {
                List_Copy(left);
            }
            else if (GetInput() == right && key_list[0] != right)
            {
                List_Copy(right);
            }
        }
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

        //リスト番号のオーバーを回避する
        if (list_D_number > 3)
        {
            list_D_number = 3;
        }

        //リストのどこかに入力が記憶されているならばカウントダウンする
        if (key_list[0] != null || key_list[1] != null || key_list[2] != null || key_list[3] != null)
        {
            list_countdown += 1;
        }

        //▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        //数フレームごとに一番古い入力を削除していく
        if (list_countdown >= 5 && (key_list[0] != null || key_list[1] != null || key_list[2] != null || key_list[3] != null))//リストの中身に何か残っていれば実行する
        {
            if (list_D_number != 0)
            {
                key_list[list_D_number] = null;
                list_D_number -= 1;
            }
            //何か入力していなければ
            else if (Not_Inputting)
            {
                key_list[list_D_number] = null;
                list_D_number -= 1;
            }
            //リスト番号を越えないため
            if (list_D_number < 0)
            {
                list_D_number = 3;
            }
            //リセット
            list_countdown = 0;
        }
        //▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

    }

    //キーのリストの中身を参照し、それにあったコマンドを探す
    void boost_style_change(string[] Key_List)
    {
        if (Key_List[0] == Key_List[1])
        {
            if (Key_List[0] == forward)
            {
                boost_style = forward;
            }
            else if (Key_List[0] == back)
            {
                boost_style = back;
            }
            else if (Key_List[0] == left)
            {
                boost_style = left;
            }
            else if (Key_List[0] == right)
            {
                boost_style = right;
            }
        }
    }
    //ブースト状態を終了させる
    void boost_style_end()
    {
        if (boost_style == forward && GetInput() != forward)
        {
            boost_style = null;
            boostspeed = 0;
        }
        else if (boost_style == back && GetInput() != back)
        {
            boost_style = null;
            boostspeed = 0;
        }
        else if (boost_style == left && GetInput() != left)
        {
            boost_style = null;
            boostspeed = 0;
        }
        else if (boost_style == right && GetInput() != right)
        {
            boost_style = null;
            boostspeed = 0;
        }
        else if (move_lock) {
            boost_style = null;
            boostspeed = 0;
        }

        if (boost_energy <= 0)
        {
            boost_style = null;
            boostspeed = 0;
        }
    }

    //接地判定
    void Ground_judgment() {
        bool save_G_C = Ground_condition;
        if (gravity_permission(0.01f) == false)
        {
            hover_style = 0;
            Ground_condition = false;
            if (boost_style == null) {
                boost_energy += 3;
                if (boost_energy > boost_energy_max) {
                    boost_energy = boost_energy_max;
                }
            }
        }
        else {
            Ground_condition = true;
        }
    }

    //グラビティ許可
    public bool gravity_permission(float speed) {
        Ray G_ray; RaycastHit G_hit; Vector3 Vec_ray = new Vector3(0, -1, 0);
        Ray G_ray_A; RaycastHit G_hit_A; Vector3 Vec_ray_A = new Vector3(0, -1, Mathf.Sin(C_Radian_S(1.0f, 0.1f)));
        Ray G_ray_B; RaycastHit G_hit_B; Vector3 Vec_ray_B = new Vector3(0, -1, Mathf.Sin(C_Radian_S(1.0f, -0.1f)));
        Ray G_ray_C; RaycastHit G_hit_C; Vector3 Vec_ray_C = new Vector3(Mathf.Sin(C_Radian_S(1.0f, 0.1f)), -1, 0);
        Ray G_ray_D; RaycastHit G_hit_D; Vector3 Vec_ray_D = new Vector3(Mathf.Sin(C_Radian_S(1.0f, -0.1f)), -1, 0);
        G_ray = new Ray(center_point.transform.position, Vec_ray);
        G_ray_A = new Ray(center_point.transform.position, Vec_ray_A);
        G_ray_B = new Ray(center_point.transform.position, Vec_ray_B);
        G_ray_C = new Ray(center_point.transform.position, Vec_ray_C);
        G_ray_D = new Ray(center_point.transform.position, Vec_ray_D);
        //Debug.Log(transform.position.y);
        if (Physics.Raycast(G_ray, out G_hit, Cap_Collider.height / 2 * transform.localScale.x + speed, ignore_layer) == false && 
            Physics.Raycast(G_ray_A, out G_hit_A, Cap_Collider.height / 2 * transform.localScale.x + speed, ignore_layer) == false && 
            Physics.Raycast(G_ray_B, out G_hit_B, Cap_Collider.height / 2 * transform.localScale.x + speed, ignore_layer) == false && 
            Physics.Raycast(G_ray_C, out G_hit_C, Cap_Collider.height / 2 * transform.localScale.x + speed, ignore_layer) == false && 
            Physics.Raycast(G_ray_D, out G_hit_D, Cap_Collider.height / 2 * transform.localScale.x + speed, ignore_layer) == false)
        {
            /*
            Debug.DrawRay(G_ray.origin, G_ray.direction, Color.red, 0, false);
            Debug.DrawRay(G_ray_A.origin, G_ray_A.direction, Color.red, 0, false);
            Debug.DrawRay(G_ray_B.origin, G_ray_B.direction, Color.red, 0, false);
            Debug.DrawRay(G_ray_C.origin, G_ray_C.direction, Color.red, 0, false);
            Debug.DrawRay(G_ray_D.origin, G_ray_D.direction, Color.red, 0, false);
            */
            return true;
        }
        else
        {
            /*
            Debug.Log(G_hit.collider.name);
            Debug.DrawRay(G_ray.origin, G_ray.direction, Color.red, 0, false);
            Debug.DrawRay(G_ray_A.origin, G_ray_A.direction, Color.red, 0, false);
            Debug.DrawRay(G_ray_B.origin, G_ray_B.direction, Color.red, 0, false);
            Debug.DrawRay(G_ray_C.origin, G_ray_C.direction, Color.red, 0, false);
            Debug.DrawRay(G_ray_D.origin, G_ray_D.direction, Color.red, 0, false);
            */
            return false;
        }
    }
    //ホバー許可
    public bool hover_permission() {
        Ray H_ray; RaycastHit H_hit; Vector3 Vec_ray = new Vector3(0, 1, 0);
        Ray H_ray_A; RaycastHit H_hit_A; Vector3 Vec_ray_A = new Vector3(0, 1, Mathf.Sin(C_Radian_S(1.0f, 0.1f)));
        Ray H_ray_B; RaycastHit H_hit_B; Vector3 Vec_ray_B = new Vector3(0, 1, Mathf.Sin(C_Radian_S(1.0f, -0.1f)));
        Ray H_ray_C; RaycastHit H_hit_C; Vector3 Vec_ray_C = new Vector3(Mathf.Sin(C_Radian_S(1.0f, 0.1f)), 1, 0);
        Ray H_ray_D; RaycastHit H_hit_D; Vector3 Vec_ray_D = new Vector3(Mathf.Sin(C_Radian_S(1.0f, -0.1f)), 1, 0);
        H_ray = new Ray(center_point.transform.position, Vec_ray);
        H_ray_A = new Ray(center_point.transform.position, Vec_ray_A);
        H_ray_B = new Ray(center_point.transform.position, Vec_ray_B);
        H_ray_C = new Ray(center_point.transform.position, Vec_ray_C);
        H_ray_D = new Ray(center_point.transform.position, Vec_ray_D);
        if (Physics.Raycast(H_ray, out H_hit, Cap_Collider.height / 2 * transform.localScale.x + 0.1f,ignore_layer) == false && 
            Physics.Raycast(H_ray_A, out H_hit_A, Cap_Collider.height / 2 * transform.localScale.x + 0.1f, ignore_layer) == false && 
            Physics.Raycast(H_ray_B, out H_hit_B, Cap_Collider.height / 2 * transform.localScale.x + 0.1f, ignore_layer) == false && 
            Physics.Raycast(H_ray_C, out H_hit_C, Cap_Collider.height / 2 * transform.localScale.x + 0.1f, ignore_layer) == false && 
            Physics.Raycast(H_ray_D, out H_hit_D, Cap_Collider.height / 2 * transform.localScale.x + 0.1f, ignore_layer) == false)
        {
            /*
            Debug.DrawRay(H_ray.origin, H_ray.direction, Color.red, 0, false);
            Debug.DrawRay(H_ray_A.origin, H_ray_A.direction, Color.red, 0, false);
            Debug.DrawRay(H_ray_B.origin, H_ray_B.direction, Color.red, 0, false);
            Debug.DrawRay(H_ray_C.origin, H_ray_C.direction, Color.red, 0, false);
            Debug.DrawRay(H_ray_D.origin, H_ray_D.direction, Color.red, 0, false);
            */
            return true;
        }
        else
        {
            /*
            Debug.DrawRay(H_ray.origin, H_ray.direction, Color.red, 0, false);
            Debug.DrawRay(H_ray_A.origin, H_ray_A.direction, Color.red, 0, false);
            Debug.DrawRay(H_ray_B.origin, H_ray_B.direction, Color.red, 0, false);
            Debug.DrawRay(H_ray_C.origin, H_ray_C.direction, Color.red, 0, false);
            Debug.DrawRay(H_ray_D.origin, H_ray_D.direction, Color.red, 0, false);
            */
            return false;
        }
    } 
    //移動許可
    public bool move_permission(string direction, float radius, float angle) {
        Ray ray;
        Ray ray_P;
        Ray ray_N;
        RaycastHit hit;
        RaycastHit hit_P;
        RaycastHit hit_N;
        float hit_height = 0;
        bool hited = false;
        for (float i = center_point.transform.position.y - (Cap_Collider.height/2 * transform.localScale.x); i <= (center_point.transform.position.y + (Cap_Collider.height/2 * transform.localScale.x)); i += 0.1f) {
            //Debug.Log(i);
            Vector3 ray_pos = new Vector3(center_point.transform.position.x, i, center_point.transform.position.z);
            if (direction == forward)
            {
                ray = new Ray(ray_pos, ray_forward(radius, angle));
                ray_P = new Ray(ray_pos, ray_forward(radius, angle + ray_P_angle));
                ray_N = new Ray(ray_pos, ray_forward(radius, angle + ray_N_angle));
            }
            else if (direction == back)
            {
                ray = new Ray(ray_pos, ray_back(radius, angle));
                ray_P = new Ray(ray_pos, ray_back(radius, angle + ray_P_angle));
                ray_N = new Ray(ray_pos, ray_back(radius, angle + ray_N_angle));
            }
            else if (direction == left)
            {
                ray = new Ray(ray_pos, ray_left(radius, angle));
                ray_P = new Ray(ray_pos, ray_left(radius, angle + ray_P_angle));
                ray_N = new Ray(ray_pos, ray_left(radius, angle + ray_N_angle));
            }
            else
            {
                ray = new Ray(ray_pos, ray_right(radius, angle));
                ray_P = new Ray(ray_pos, ray_right(radius, angle + ray_P_angle));
                ray_N = new Ray(ray_pos, ray_right(radius, angle + ray_N_angle));
            }
            if (Physics.Raycast(ray, out hit, Cap_Collider.radius * transform.localScale.x + radius, ignore_layer) == false
            && Physics.Raycast(ray_P, out hit_P, Cap_Collider.radius * transform.localScale.x + radius, ignore_layer) == false
            && Physics.Raycast(ray_N, out hit_N, Cap_Collider.radius * transform.localScale.x + radius, ignore_layer) == false)
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 0, true);
                Debug.DrawRay(ray_P.origin, ray_P.direction, Color.red, 0, true);
                Debug.DrawRay(ray_N.origin, ray_N.direction, Color.red, 0, true);
            }
            else {
                hit_height = i;
                hited = true;
                //return step_permission(direction,transform.position,radius,angle);
            }
        }
        if (hited) {
            //Debug.Log("hit_height = " + hit_height);
            //Debug.Log("centerpoint = " + center_point.transform.position);
            if (hit_height < center_point.transform.position.y)
            {
                Debug.Log("step_permission");
                return step_permission(direction, transform.position, radius, angle);
            }
            else {
                return false;
            }
        }
        return true;
    }
    //段差
    public bool step_permission(string direction ,Vector3 pos, float speed, float angle) {
        Ray S_ray; RaycastHit S_hit; Vector3 Vec_ray = new Vector3(0, -1, 0);
        Ray S_ray_top; RaycastHit S_hit_top; Vector3 Vec_ray_top = new Vector3(0, 1, 0);

        Vector3 test;
        if (direction == forward)
        {
            test = transform_foward(pos, speed, angle);
        }
        else if (direction == back)
        {
            test = transform_back(pos, speed, angle);
        }
        else if (direction == left)
        {
            test = transform_left(pos, speed, angle);
        }
        else {
            test = transform_right(pos, speed, angle);
        }
        test.y = center_point.transform.position.y;
        S_ray = new Ray(test, Vec_ray);
        S_ray_top = new Ray(test, Vec_ray_top);
        float distance = Cap_Collider.height / 2 * transform.localScale.x;
        Debug.DrawRay(S_ray.origin, S_ray.direction, Color.red, 0, true);
        Debug.DrawRay(S_ray_top.origin, S_ray_top.direction, Color.blue, 0, true);

        if (Physics.Raycast(S_ray_top, out S_hit_top, 0.1f + distance, ignore_layer) == false) {
            Debug.Log("not_hit_top");
            if (Physics.Raycast(S_ray, out S_hit,distance, ignore_layer))
            {
                //Debug.Log(S_hit.collider.name);
                
                //Debug.Log(S_hit.distance);
                if (distance - S_hit.distance < 0.3 && distance - S_hit.distance > 0)
                {
                    
                    Vector3 position = transform.position;
                    position.y += distance - S_hit.distance;
                    transform.position = position;
                    return true;
                }
                else {
                    
                    return false;
                }
            }
            else {
                
                return false;
            }
            //Debug.Log(S_hit.collider.name);
            //Debug.Log("distance = " + S_hit.distance);
            //Debug.Log("height = " + Cap_Collider.height / 2 * transform.localScale.x);

        }
        else {
            
            return false;
        }
    }
    //壁に向きに合わせて移動させる
    bool simple_move(string direction, float speed) {
        bool M_P = true;
        bool D_M = false;
        for (float P = 0; P > -0.2f; P -= 0.05f)
        {
            if (move_permission(direction, speed * half, P))
            {
                move(direction, speed * half, P);
                M_P = false;
                D_M = true;
                //Debug.Log(P);
                return true;
            }
        }

        if (M_P)
        {
            for (float P = 0; P < 0.2f; P += 0.05f)
            {
                if (move_permission(direction, speed * half, P))
                {
                    move(direction, speed * half, P);
                    M_P = false;
                    D_M = true;
                    //Debug.Log("B");
                    return true;
                }
            }
        }
        if (D_M == false)
        {

            if (direction == forward)
            {
                move(back, speed * 0.5f, 0);
            }
            else if (direction == back)
            {
                move(forward, speed * 0.5f, 0);
            }
            else if (direction == left)
            {
                move(right, speed * 0.5f, 0);
            }
            else if (direction == right)
            {
                move(left, speed * 0.5f, 0);
            }


            M_P = true;
            D_M = false;
            for (float P = 0; P > -0.2f; P -= 0.05f)
            {
                if (move_permission(direction, speed * half, P))
                {
                    move(direction, speed * half, P);
                    M_P = false;
                    D_M = true;
                    return true;
                }
            }
            if (M_P)
            {
                for (float P = 0; P < 0.2f; P += 0.05f)
                {
                    if (move_permission(direction, speed * half, P))
                    {
                        move(direction, speed * half, P);
                        M_P = false;
                        D_M = true;
                        //Debug.Log("D");
                        return true;
                    }
                }
            }

            if (direction == forward)
            {
                move(forward, speed * 0.5f, 0);
            }
            else if (direction == back)
            {
                move(back, speed * 0.5f, 0);
            }
            else if (direction == left)
            {
                move(left, speed * 0.5f, 0);
            }
            else if (direction == right)
            {
                move(right, speed * 0.5f, 0);
            }
        }
        /*
        for (float P = 0; P > -0.2f; P -= 0.05f)
        {
            if (move_permission(direction, speed * half, P))
            {
                move(direction, speed * half, P);
                M_P = false;
                D_M = true;
                //Debug.Log(P);
                return true;
            }
        }
        if (M_P)
        {
            for (float P = 0; P < 0.2f; P += 0.05f)
            {
                if (move_permission(direction, speed * half, P))
                {
                    move(direction, speed * half, P);
                    M_P = false;
                    D_M = true;
                    //Debug.Log("B");
                    return true;
                }
            }
        }
        if (D_M == false)
        {

            if (direction == forward)
            {
                move(back, speed * 0.5f, 0);
            }
            else if (direction == back)
            {
                move(forward, speed * 0.5f, 0);
            }
            else if (direction == left)
            {
                move(right, speed * 0.5f, 0);
            }
            else if (direction == right)
            {
                move(left, speed * 0.5f, 0);
            }


            M_P = true;
            D_M = false;
            for (float P = 0; P > -0.2f; P -= 0.05f)
            {
                if (move_permission(direction, speed * half, P))
                {
                    move(direction, speed * half, P);
                    M_P = false;
                    D_M = true;
                    return true;
                }
            }
            if (M_P)
            {
                for (float P = 0; P < 0.2f; P += 0.05f)
                {
                    if (move_permission(direction, speed * half, P))
                    {
                        move(direction, speed * half, P);
                        M_P = false;
                        D_M = true;
                        //Debug.Log("D");
                        return true;
                    }
                }
            }
        }
        */
        return false;
    }
    //移動制御関数
    public void move(string direction,float speed,float angle)
    {
        Vector3 pos = transform.position;
        if (direction == forward)
        {
            pos = transform_foward(pos, speed, angle);
        }
        else if (direction == back)
        {
            pos = transform_back(pos, speed, angle);
        }
        else if (direction == left)
        {
            pos = transform_left(pos, speed, angle);
        }
        else {
            pos = transform_right(pos, speed, angle);
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
                if (move_permission(forward,speed,quarter_P))
                {
                    move(forward, speed, quarter_P);
                }
                //駄目なら探す
                else {
                    for (float P = 0; P > -0.8f; P -= 0.05f)
                    {
                        if (move_permission(forward, speed, half + P))
                        {
                            move(forward, speed * half, half + P);
                            break;
                        }
                    }
                }
                //Debug.Log("左斜め前");
            }

            //右斜め前
            else if (Input.GetKey("d"))
            {
                if (move_permission(forward,speed,quarter_N)) {
                    move(forward, speed, quarter_N);
                }
                //駄目なら探す
                else {
                    for (float P = 0; P < 0.8f; P += 0.05f)
                    {
                        if (move_permission(forward, speed, -1*half +P))
                        {
                            move(forward, speed * half, -1*half + P);
                            break;
                        }
                    }
                }
                //Debug.Log("右斜め前");
            }

            else
            {
                if (move_permission(forward, speed, 0))
                {
                    move(forward, speed, 0);
                }
                else {
                    simple_move(forward, speed);
                }
            }
        }
        //後退
        else if (Input.GetKey("s"))
        {
            //左斜め後ろ
            if (Input.GetKey("a"))
            {
                if (move_permission(back,speed,quarter_N))
                {
                    move(back, speed, quarter_N);
                }
                //駄目なら探す
                else {
                    for (float P = 0; P < 0.8f; P += 0.05f)
                    {
                        if (move_permission(back, speed, -1*half + P))
                        {
                            move(back, speed * half, -1*half + P);
                            break;
                        }
                    }
                }
                //Debug.Log("左斜め後ろ");
            }
            //右斜め後ろ
            else if (Input.GetKey("d"))
            {
                if (move_permission(back,speed,quarter_P))
                {
                    move(back, speed, quarter_P);
                    //pos.z -= radius * Mathf.Sin((0.25f + mouse.x) * Mathf.PI);
                    //pos.x -= radius * Mathf.Cos((0.25f + mouse.x) * Mathf.PI);
                }
                else {
                    //駄目なら探す
                    for (float P = 0; P > -0.8f; P -= 0.05f)
                    {
                        if (move_permission(back, speed, half + P))
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
                if (move_permission(back,speed,0)) {
                    move(back, speed, 0);
                }
                else {
                    simple_move(back, speed);
                }
                //Debug.Log("後退");
            }
        }
        //左
        else if (Input.GetKey("a"))
        {
            if (move_permission(left,speed,0))
            {
                move(left, speed, 0);
            }
            else
            {
                simple_move(left, speed);
            }
            //Debug.Log("左");
        }
        //右
        else if (Input.GetKey("d"))
        {
            if (move_permission(right,speed,0))
            {
                move(right, speed, 0);
            }
            else
            {
                simple_move(right, speed);
            }
            //Debug.Log("右");
        }
    }

    //ブースト制御
    public void boost_move(float speed)
    {
        if (boost_energy > 0)
        {
            if (Input.GetKey("w") && boost_style == forward)
            {
                if (move_permission(forward, speed, 0))
                {
                    move(forward, speed, 0);

                }
                else {
                    simple_move(forward, speed * 0.5f);
                }
            }
            else if (Input.GetKey("s") && boost_style == back)
            {
                if (move_permission(back, speed, 0))
                {
                    move(back, speed, 0);
                }
                else {
                    simple_move(back, speed * 0.5f);
                }
            }
            else if (Input.GetKey("a") && boost_style == left)
            {
                if (move_permission(left, speed, 0))
                {
                    move(left, speed, 0);
                }
                else
                {
                    simple_move(left, speed * 0.5f);
                }
            }
            else if (Input.GetKey("d") && boost_style == right)
            {
                if (move_permission(right, speed, 0))
                {
                    move(right, speed, 0);
                }
                else
                {
                    simple_move(right, speed * 0.5f);
                }
            }
        }
        boost_energy -= 2;
    }

    //ホバー制御
    public void Hover_move(float speed) {
        if (Input.GetKeyDown("space")) {
            if (hover_style == 0)
            {
                hover_style = 1;
            }
            else if (hover_style == 1)
            {
                hover_style = 2;
            }
            else if (hover_style == 2)
            {
                if (Chara_Status.Terrain_State == "Water")
                {
                    hover_style = 2;
                }
                else {
                    hover_style = 3;
                }
            }
            else if (hover_style == 3) {
                if (Chara_Status.Terrain_State == "Water") {
                    hover_style = 2;
                }
            }

                hover_time = h_t_max;
        }

        if (Input.GetKey("space")) {
            if((hover_style == 1||hover_style == 2) && hover_time > 0 && boost_energy > 0)
            {
                if (hover_permission()) {
                    Vector3 pos = transform.position;
                    pos.y += 0.1f;
                    transform.position = pos;
                }
            }
            hover_time -= 1;
            boost_energy -= 1;
        }
    }

    //グラビティ制御
    public void gravity_move(float speed) {
        
        Vector3 pos = transform.position;
        if (Input.GetKey("space"))
        {
            if (boost_energy <= 0)
            {
                for (float i = speed; i >= 0.01f; i -= 0.01f) {
                    
                    if (gravity_permission(i))
                    {
                        pos.y -= i;
                        transform.position = pos;
                        break;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKey("v") && move_lock == false) {
                speed += 0.15f;
            }
            for (float i = speed; i >= 0.01f; i -= 0.01f)
            {
                if (gravity_permission(i))
                {
                    pos.y -= i;
                    transform.position = pos;
                    
                    break;
                }
            }
        }
    }

    //ステータス更新
    public void status_update() {
        float magnification = 1.0f;
        if (Chara_Status.Terrain_State == "Water") {
            magnification = 0.75f;
        }
        this.normalspeed = Chara_Status.normalspeed * magnification;
        this.boostspeed = Chara_Status.boostspeed * magnification;
        this.boost_max_speed = Chara_Status.boost_max_speed * magnification;
    }

    // Use this for initialization
    void Start()
    {
        Chara_Status = this.GetComponent<chara_status>();

        status_update();

        boost_energy = boost_energy_max;

        center_point = transform.FindChild("center_point").gameObject;

        center_point_pos = center_point.transform.position;

        rb = GetComponent<Rigidbody>();

        R_stick_obj = this.gameObject;

        R_stick_vec = R_stick_obj.GetComponent<R_Stick_Vec>();
        ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("enemy_bullet") | 1 << LayerMask.NameToLayer("Ignore Raycast")|1 << LayerMask.NameToLayer("Water") );
        Cap_Collider = this.GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        Key_Memory(GetInputDown());
        boost_style_change(key_list);
        boost_style_end();
        if (move_lock == false)
        {
            mouse = R_stick_vec.stick_vec;
            //mouse += new Vector2(Input.GetAxis("Mouse X") * -1,0) * Time.deltaTime * spinSpeed;

            /*
            Key_Memory(GetInputDown());
            boost_style_change(key_list);
            boost_style_end();
            */


            if (boost_style != null)
            {
                boostspeed += 0.015f;
                if (boostspeed > boost_max_speed)
                {
                    boostspeed = boost_max_speed;
                }
                boost_move(boostspeed);
            }
            else {
                character_move(normalspeed);
            }
            Ground_judgment();
            gravity_move(normalspeed);
            Hover_move(normalspeed);

            mouse_save = Input.GetAxis("Mouse X") * -1;
            save_pos = transform.position;
            //Debug.Log(mouse.x);[
            if (boost_style == null)
            {
                //Debug.Log(key_list[0] + "," + key_list[1] + "," + key_list[2] + "," + key_list[3]);
            }
            else {
                //Debug.Log(boost_style);
            }
            save_B_S = boost_style;
            //Debug.Log(key_list[0] + "," + key_list[1] + "," + key_list[2] + "," + key_list[3]);
            //Debug.Log(hover_style);
        }
        else {
            Ground_judgment();
            if (gravity_lock == false) {
                gravity_move(normalspeed);
            }
        }
        amount_move = Vector3.Distance(transform.position, save_chara_pos);
        save_chara_pos = transform.position;
        //Debug.Log(normalspeed);
    }
}