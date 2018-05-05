using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qucik_boost : MonoBehaviour {
    private Rigidbody rb;
    private move_speed Move_Speed;
    private chara_status Chara_Status;
    private L_Stick_Vec L_stick_vec;
    public GameObject Camera_obj;
    float moveForceMultiplier = 200;
    public Vector3 magnification = new Vector3(1.0f, 1.0f, 1.0f);
    int L_stick_direction() {
        float vec_x = Mathf.Abs(L_stick_vec.stick_vec.x);
        float vec_y = Mathf.Abs(L_stick_vec.stick_vec.y);
        if (vec_x > vec_y)
        {
            if (L_stick_vec.stick_vec.x > 0)
            {
                return 2;
            }
            else {
                return 3;
            }
        }
        else {
            if (L_stick_vec.stick_vec.y >= 0)
            {
                return 0;
            }
            else {
                return 1;
            }
        }
    }

    void qboost_cool_time_down()
    {
        for (int i = 0; i < Move_Speed.qboost_cool_time.Length; i++) {
            Move_Speed.qboost_cool_time[i] -= Time.deltaTime * 4;
            if (Move_Speed.qboost_cool_time[i] < 0) {
                Move_Speed.qboost_cool_time[i] = 0;
            }
        }
    }
    void quick_boost_move(int direction)
    {
        Vector3 vec = this.gameObject.transform.position - Camera_obj.transform.position;
        vec.y = 0f;
        vec = vec.normalized;

        if (direction == 0)
        {
            rb.AddForce(Move_Speed.quick_boost_speed_max * vec * 1);
        }
        else if (direction == 1)
        {
            rb.AddForce(Move_Speed.quick_boost_speed_max * vec * -1);
        }
        else if (direction == 2)
        {
            rb.AddForce(Move_Speed.quick_boost_speed_max * Vector3.Cross(vec, Vector3.up) * 1);
        }
        else if (direction == 3) {
            rb.AddForce(Move_Speed.quick_boost_speed_max * Vector3.Cross(vec, Vector3.up) * -1);
        }
        for (int i = 0; i < Move_Speed.qboost_cool_time.Length; i++) {
            if (i == direction)
            {
                Move_Speed.qboost_cool_time[i] = Move_Speed.qboost_cool_time_const;
            }
            else {
                Move_Speed.qboost_cool_time[i] = 0.1f;
            }
        }
        Chara_Status.quick_boost = true;
    }

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        Move_Speed = this.GetComponent<move_speed>();
        Chara_Status = this.GetComponent<chara_status>();
        L_stick_vec = this.GetComponent<L_Stick_Vec>();
	}
	
	// Update is called once per frame
	void Update () {
        Chara_Status.quick_boost = false;
        qboost_cool_time_down();
        int stick_direction = L_stick_direction();
        if (Input.GetButtonDown("button7")) {
            if (Move_Speed.qboost_cool_time[stick_direction] <= 0) {
                quick_boost_move(stick_direction);
                Debug.Log("Q_boost");
            }
        }
    }
}
