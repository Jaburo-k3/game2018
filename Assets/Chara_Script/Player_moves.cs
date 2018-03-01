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
    float moveForceMultiplier = 50;
    public Vector3 magnification = new Vector3(1.0f,1.0f,1.0f);
    bool move_now = false;
    private move_speed Move_Speed;

    private chara_status Chara_Status;


    Vector3 jump_move(Vector3 vec) {
        vec.y = Move_Speed.rising_speed * 4.0f;
        return vec;
    }
     Vector3 rising_move(Vector3 vec) {
        vec.y = Move_Speed.rising_speed * 1.0f;
        return vec;
    }
    void speed_deceleration() {
        Move_Speed.speed -= Move_Speed.deceleration;
        if (Move_Speed.speed < Move_Speed.normal_speed_max)
        {
            Move_Speed.speed = Move_Speed.normal_speed_max;
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
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 moveVector = Vector3.zero;
        Vector3 Subtraction_vec = rb.velocity;

        Vector3 vec = this.gameObject.transform.position - Camera_obj.transform.position;
        vec.y = 0f;
        vec = vec.normalized;

        //移動
        if (Vector2.SqrMagnitude(L_stick_vec.stick_vec) > 0.1f && Chara_Status.move_lock == 0)
        {

            if (Move_Speed.speed <= Move_Speed.speed_max)
            {
                Vector3 rising_vec = Vector3.zero;

                //クイックブースト
                if (Input.GetButtonDown("button7"))
                {
                    Move_Speed.speed = Move_Speed.quick_boost_speed_max;
                    moveForceMultiplier = 200;

                    magnification.x = 0.05f;
                    magnification.z = 0.05f;
                    Debug.Log("Q_boost");
                }
                //ブースト
                else if (Input.GetButton("button6") && Move_Speed.speed <= Move_Speed.boost_speed_max)
                {
                    Move_Speed.speed += Move_Speed.boost_acceleration;
                    Move_Speed.speed_max = Move_Speed.boost_speed_max;
                    if (Move_Speed.speed > Move_Speed.speed_max)
                    {
                        Move_Speed.speed = Move_Speed.speed_max;
                    }

                    moveForceMultiplier = 100;

                    magnification.x = 1.0f;
                    magnification.z = 1.0f;
                    Debug.Log("boost");
                }
                //通常(歩き)状態
                else {
                    speed_deceleration();
                    Move_Speed.speed_max = Move_Speed.normal_speed_max;
                    moveForceMultiplier = 50;
                    magnification.x = 1.0f;
                    magnification.z = 1.0f;
                }
                moveVector = Move_Speed.speed * vec * L_stick_vec.stick_vec.y + Move_Speed.speed * Vector3.Cross(vec, Vector3.up) * L_stick_vec.stick_vec.x + rising_vec;
                move_now = true;
            }
            else {
                speed_deceleration();
                move_now = false;
            }
        }
        else {
            speed_deceleration();
            move_now = false;
        }

        //上昇
        if (Input.GetButtonDown("button6") && Chara_Status.ground_condition == "terrain" && Move_Speed.speed == Move_Speed.normal_speed_max && move_now == false)
        {
            moveVector = jump_move(moveVector);
        }
        else if (Input.GetButton("button6") && Chara_Status.ground_condition == "air")
        {
            moveVector = rising_move(moveVector);
        }
        else {
            Subtraction_vec.y += Physics.gravity.y * Time.deltaTime;
        }

        if (Chara_Status.move_lock > 0) {
            Chara_Status.move_lock -= 1f;
        }
        Subtraction_vec.x *= magnification.x;
        Subtraction_vec.z *= magnification.z;
        rb.AddForce(moveForceMultiplier * (moveVector - Subtraction_vec));
    }

}
