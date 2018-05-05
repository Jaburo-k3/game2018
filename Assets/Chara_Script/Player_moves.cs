using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_moves : MonoBehaviour {
    Rigidbody rb;
    public GameObject stick_obj;
    private L_Stick_Vec L_stick_vec;
    private R_Stick_Vec R_stick_vec;
    public Vector2 save_stick_vec = Vector2.zero;
    public GameObject Chara_obj;
    public GameObject Camera_obj;
    float moveForceMultiplier = 200;
    public Vector3 magnification = new Vector3(1.0f,1.0f,1.0f);
    bool move_now = false;
    bool boost_energy_consume = false;
    private move_speed Move_Speed;

    private chara_status Chara_Status;

    Coroutine hover_change_cor;

    private AudioSource Q_AudioSource;
    public AudioClip quick_boost_sound;
    public void hover_change()
    {
        if (hover_change_cor != null)
        {
            StopCoroutine(hover_change_cor);
        }
        hover_change_cor = StartCoroutine(h_change_cor());
    }
    IEnumerator h_change_cor() {
        yield return new WaitForSeconds(0.5f);
        if (Chara_Status.hover == 1)
        {
            Chara_Status.hover = 0;
        }
    }

    void quick_boost_move() {
        float speed_magnification = 1;
        int q_boost_direction = 0;

        if (Chara_Status.boost_energy <= Chara_Status.quick_boost_consume_energy)
        {
            speed_magnification = Chara_Status.boost_energy / Chara_Status.quick_boost_consume_energy;
            //Debug.Log(speed_magnification);
        }
        if (Chara_Status.moving_state[0] == "forward" || Chara_Status.moving_state[0] == "back") {
            Move_Speed.speed[0] = speed_magnification * Move_Speed.quick_boost_speed_max;
            Move_Speed.speed_max[0] = speed_magnification * Move_Speed.quick_boost_speed_max;
            if (Chara_Status.moving_state[0] == "forward")
            {
                q_boost_direction = 0;
            }
            else {
                q_boost_direction = 1;
            }
        }
        else if (Chara_Status.moving_state[0] == "left" || Chara_Status.moving_state[0] == "right") {
            Move_Speed.speed[1] = speed_magnification * Move_Speed.quick_boost_speed_max;
            Move_Speed.speed_max[1] = speed_magnification * Move_Speed.quick_boost_speed_max;
            if (Chara_Status.moving_state[0] == "left")
            {
                q_boost_direction = 2;
            }
            else {
                q_boost_direction = 3;
            }
        }
        for (int i = 0; i < Move_Speed.qboost_cool_time.Length; i++) {
            if (i == q_boost_direction)
            {
                Move_Speed.qboost_cool_time[i] = Move_Speed.qboost_cool_time_const;
            }
            else {
                if (Move_Speed.qboost_cool_time[i] < 0.2f) {
                    Move_Speed.qboost_cool_time[i] = 0.2f;
                }
            }
        }
        //Move_Speed.speed[0] = speed_magnification * Move_Speed.quick_boost_speed_max;
        //Move_Speed.speed[1] = speed_magnification * Move_Speed.quick_boost_speed_max;
        moveForceMultiplier = 200;
        //Move_Speed.qboost_cool_time[0] = Move_Speed.qboost_cool_time_const;
        Chara_Status.boost_energy -= Chara_Status.quick_boost_consume_energy;
        boost_energy_consume = true;
        magnification.x = 0.05f;
        magnification.z = 0.05f;
        Q_AudioSource.Play();
    }
    Vector3 jump_move(Vector3 vec) {
        vec.y = Move_Speed.rising_speed * 4.0f;
        return vec;
    }
     Vector3 rising_move(Vector3 vec,float magnification) {
        float inclination = Mathf.Sqrt(L_stick_vec.stick_vec.x * L_stick_vec.stick_vec.x + L_stick_vec.stick_vec.y * L_stick_vec.stick_vec.y);
        vec.y =    magnification * (Move_Speed.rising_speed * (1.0f - inclination/2));
        Debug.Log(magnification * (Move_Speed.rising_speed * (1.0f - inclination / 2)));
        return vec;
    }
    void speed_deceleration() {
        for (int i = 0; i < 2; i++) {
            float save_speed = Move_Speed.speed[i];
            Move_Speed.speed[i] -= Move_Speed.deceleration * Move_Speed.speed[i] / Move_Speed.speed_max[i];
            if (Move_Speed.speed[i] < Move_Speed.normal_speed_max)
                {
                    Move_Speed.speed[i] = Move_Speed.normal_speed_max;
                }
            if (float.IsNaN(Move_Speed.speed[i])) {
                Move_Speed.speed[i] = save_speed;
            }
        }
    }
    void speed_max_deceleration(float speed)
    {
        for (int i = 0; i < Move_Speed.speed_max.Length; i++)
        {
            if (Move_Speed.speed_max[i] > speed)
            {
                Move_Speed.speed_max[i] -= Move_Speed.deceleration * Move_Speed.speed_max[i] / speed;
            }
            else {
                Move_Speed.speed_max[i] = speed;
            }
        }
    }

    void change_moving_state(string state) {
        /*
        float vec_x = Mathf.Abs(L_stick_vec.stick_vec.x);
        float vec_y = Mathf.Abs(L_stick_vec.stick_vec.y);
        */
        if (state == "wait" || state == "rising") {
            Chara_Status.moving_state[1] = state;
            Chara_Status.quick_boost = false;
            Chara_Status.moving_state[0] = null;
            return;
        }

        if (state == "quick_boost")
        {
            Chara_Status.quick_boost = true;
            if (Vector2.SqrMagnitude(L_stick_vec.stick_vec) < 0.1f) {
                Chara_Status.moving_state[0] = null;
                return;
            }
        }
        else {
            Chara_Status.moving_state[1] = state;
            Chara_Status.quick_boost = false;
        }

        /*
        if (vec_x > vec_y)
        {
            if (L_stick_vec.stick_vec.x > 0)
            {
                Chara_Status.moving_state[0] = "left";
            }
            else {
                Chara_Status.moving_state[0] = "right";
            }
        }
        else {
            if (L_stick_vec.stick_vec.y > 0)
            {
                Chara_Status.moving_state[0] = "forward";
            }
            else {
                Chara_Status.moving_state[0] = "back";
            }
        }
        */
        //Debug.Log(Chara_Status.moving_state[1]);
    }

    int change_direction_state()
    {
        float vec_x = Mathf.Abs(L_stick_vec.stick_vec.x);
        float vec_y = Mathf.Abs(L_stick_vec.stick_vec.y);

        if (vec_x > vec_y)
        {
            if (L_stick_vec.stick_vec.x > 0)
            {
                Chara_Status.moving_state[0] = "left";
                return 2;
            }
            else {
                Chara_Status.moving_state[0] = "right";
                return 3;
            }
        }
        else {
            if (L_stick_vec.stick_vec.y < 0)
            {
                Chara_Status.moving_state[0] = "back";
                return 1;
            }
            else {
                Chara_Status.moving_state[0] = "forward";
                return 0;
            }
        }
    }
    // Use this for initialization
    void Start () {
        stick_obj = this.gameObject;
        L_stick_vec = stick_obj.GetComponent<L_Stick_Vec>();
        R_stick_vec = stick_obj.GetComponent<R_Stick_Vec>();
        rb = this.GetComponent<Rigidbody>();
        Move_Speed = this.GetComponent<move_speed>();
        Chara_Status = this.GetComponent<chara_status>();

        Q_AudioSource = gameObject.AddComponent<AudioSource>();
        Q_AudioSource.clip = quick_boost_sound;
        Q_AudioSource.volume = 0.75f;
    }
	
	// Update is called once per frame
	void Update () {
        

        Vector3 moveVector = Vector3.zero;
        Vector3 Subtraction_vec = rb.velocity;
        string moving_state;
        //Lスティックの入力方向取得
        int stick_dir =  change_direction_state();

        if (Chara_Status.move_lock) {
            rb.AddForce(moveForceMultiplier * (moveVector - Subtraction_vec));
            change_moving_state("wait");
            return;
        }

        Vector3 vec = this.gameObject.transform.position - Camera_obj.transform.position;
        vec.y = 0f;
        vec = vec.normalized;



        //移動
        if (Vector2.SqrMagnitude(L_stick_vec.stick_vec) > 0.1f && Chara_Status.move_stun == 0)
        {
            //クイックブースト
            if (Input.GetButtonDown("button7") && Move_Speed.qboost_cool_time[stick_dir] == 0)
            {
                moving_state = "quick_boost";
                quick_boost_move();
            }
            //ブースト
            else if (Input.GetButton("button6"))
            {
                
                float speed_magnification = 1;

                float boost_acceleration = Move_Speed.boost_acceleration;

                moving_state = "boost";

                //空中時かつブーストに消費するエネルギーよりエネルギーが少ないとき
                if (Chara_Status.ground_condition == "air" && Chara_Status.boost_energy <= Chara_Status.boost_consume_energy)
                {
                    //足りない分だけ加速スピード減少
                    speed_magnification = Chara_Status.boost_energy / Chara_Status.boost_consume_energy;
                    //Move_Speed.speed_max = Move_Speed.boost_speed_max;
                    moveForceMultiplier = 100;
                    Debug.Log("not_energy");
                }

                //減少した加速スピードが歩きより遅い際
                if (speed_magnification * boost_acceleration < boost_acceleration/10)
                {
                    //歩き
                    speed_deceleration();
                    speed_max_deceleration(Move_Speed.normal_speed_max);
                    //Move_Speed.speed_max = Move_Speed.normal_speed_max;//変更点
                    moveForceMultiplier = 50;
                    moving_state = "walk";
                    Debug.Log("not_boost_walk");

                }
                else {
                    //ブースト
                    boost_acceleration *= speed_magnification;
                    speed_max_deceleration(Move_Speed.boost_speed_max);
                    //Move_Speed.speed_max = Move_Speed.boost_speed_max;//変更点
                    moveForceMultiplier = 100;
                    //Debug.Log("boost_succes");

                }
                Move_Speed.speed[0] += boost_acceleration;
                Move_Speed.speed[1] += boost_acceleration;

                for (int i = 0; i < 2; i++)
                {
                    if (Move_Speed.speed[i] > Move_Speed.speed_max[i])
                    {
                        Move_Speed.speed[i] = Move_Speed.speed_max[i];
                    }
                }
                magnification.x = 1.0f;
                magnification.z = 1.0f;
            }
            //通常(歩き)状態
            else {
                speed_deceleration();
                speed_max_deceleration(Move_Speed.normal_speed_max);
                moveForceMultiplier = 50;
                magnification.x = 1.0f;
                magnification.z = 1.0f;
                moving_state = "walk";
            }            //前後方向+左右方向
            moveVector = Move_Speed.speed[0] * vec * L_stick_vec.stick_vec.y + Move_Speed.speed[1] * Vector3.Cross(vec, Vector3.up) * L_stick_vec.stick_vec.x; //前後方向+左右方向
            move_now = true;
        }
        //Lスティック入力無し
        else {
            if (Input.GetButtonDown("button7") && Mathf.Abs(Input.GetAxis("Horizontal2")) == 0.0f && Chara_Status.move_stun == 0 && Move_Speed.qboost_cool_time[0] == 0)
            {
                //クイックブースト
                moving_state = "quick_boost";
                quick_boost_move();
                moveVector = Move_Speed.speed[0] * vec * 1; //前後方向+左右方向
                move_now = true;
            }
            else {
                speed_deceleration();
                speed_max_deceleration(Move_Speed.normal_speed_max);
                move_now = false;
                if (Chara_Status.moving_state[0] == "boost")
                {
                    Debug.Log("save");
                    moving_state = "boost";
                }
                else {
                    moving_state = "wait";
                }
                
            }
        }



        // 上昇

        if (Input.GetButtonDown("button6") && Chara_Status.hover == 1)
        {
            if (Chara_Status.ground_condition == "terrain")
            {
                moveVector = jump_move(moveVector);
            }
            else if (Chara_Status.ground_condition == "air")
            {
                float rising_magnification = 1;
                if (Chara_Status.boost_energy < Chara_Status.boost_consume_energy) {
                    rising_magnification = Chara_Status.boost_energy / Chara_Status.quick_boost_consume_energy;
                }
                moveVector = rising_move(moveVector,rising_magnification);

                if (!move_now) {
                    moving_state = "rising";
                }

                Chara_Status.boost_energy -= Chara_Status.boost_consume_energy;
                boost_energy_consume = true;
            }
        }
        else if (Input.GetButton("button6") && Chara_Status.hover == 2) {
            float rising_magnification = 1;
            if (Chara_Status.boost_energy < Chara_Status.boost_consume_energy)
            {
                rising_magnification = Chara_Status.boost_energy / Chara_Status.quick_boost_consume_energy;
            }
            moveVector = rising_move(moveVector,rising_magnification);
            if (!move_now)
            {
                moving_state = "rising";
            }
            Chara_Status.boost_energy -= Chara_Status.boost_consume_energy;
            boost_energy_consume = true;
        }



        //２回入力
        if (Input.GetButtonUp("button6"))
        {
            if (Chara_Status.hover == 0)
            {
                Chara_Status.hover = 1;
            }
            hover_change();
        }


        //キャラ硬直時間減少
        if (Chara_Status.move_stun > 0) {
            Chara_Status.move_stun -= 1f;
        }

        //クイックブースト硬直時間減少
        if (Move_Speed.qboost_cool_time[0] > 0 || Move_Speed.qboost_cool_time[1] > 0 || Move_Speed.qboost_cool_time[2] > 0 || Move_Speed.qboost_cool_time[3] > 0) {
            for (int i = 0; i < Move_Speed.qboost_cool_time.Length; i++) {
                Move_Speed.qboost_cool_time[i] -= Time.deltaTime;
                if (Move_Speed.qboost_cool_time[i] < 0)
                {
                    Move_Speed.qboost_cool_time[i] = 0;
                }
            }
            /*
            Move_Speed.qboost_cool_time[0] -= Time.deltaTime;
            if(Move_Speed.qboost_cool_time[0] < 0){
                Move_Speed.qboost_cool_time[0] = 0;
            }
            */
        }

        //ブーストエネルギー下限
        if (Chara_Status.boost_energy < 0) {
            Chara_Status.boost_energy = 0;
        }

        //ブースト回復
        if (Chara_Status.boost_energy < Chara_Status.boost_energy_max && !boost_energy_consume) {
            Chara_Status.boost_energy += Chara_Status.boost_recovery_speed;
            if (Chara_Status.boost_energy >= Chara_Status.boost_energy_max) {
                Chara_Status.boost_energy = Chara_Status.boost_energy_max;
            }
        }

        boost_energy_consume = false;

        Subtraction_vec.x *= magnification.x;
        Subtraction_vec.z *= magnification.z;

        if (Subtraction_vec.y < 0f) {
            Subtraction_vec.y = 0;
        }
        //移動
        rb.AddForce(moveForceMultiplier * (moveVector - Subtraction_vec));
        //移動状態変更
        //Debug.Log(moving_state);
        change_moving_state(moving_state);
    }
}
