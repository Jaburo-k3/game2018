using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_speed : MonoBehaviour {
    private chara_status Chara_status;

    public float speed;//これに加算していく
    public float speed_max;//スピード上限
    public float acceleration;//加速度
    public float deceleration = 0.5f;//減速度
    public float boost_acceleration;//ブースト加速度
    public float normal_speed_max;//通常時のスピード上限
    public float boost_speed_max;//ブースト時のスピード上限
    public float quick_boost_speed_max;//クイックブーストのスピード
    public float rising_speed;//上昇速度
    public float qboost_cool_time = 0;//クイックブーストのクールタイム
    public float qboost_cool_time_const;//クールタイム定数

    public void set_up_speed() {
        chara_status Chara_status = this.GetComponent<chara_status>();
        speed_max = Chara_status.speed_max;
        acceleration = Chara_status.acceleration;
        boost_acceleration = Chara_status.boost_acceleration;
        normal_speed_max = Chara_status.normal_speed_max;
        boost_speed_max = Chara_status.boost_speed_max;
        quick_boost_speed_max = Chara_status.quick_boost_speed_max;
        rising_speed = Chara_status.rising_speed;
        speed = normal_speed_max;
        qboost_cool_time_const = Chara_status.qboost_cool_time_const;
    }

    public void add_move_lock(float number) {
        if (Chara_status.move_stun < number)
        {
            Chara_status.move_stun = number;
        }
    }
    // Use this for initialization
    void Start () {
        Chara_status = this.GetComponent<chara_status>();
        set_up_speed();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
