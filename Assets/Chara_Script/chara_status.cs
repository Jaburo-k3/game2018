using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chara_status : MonoBehaviour {
    public float HP;
    public float speed_max;//スピード上限
    public float acceleration;//加速度
    public float boost_acceleration;//ブースト加速度
    public float normal_speed_max;//通常時のスピード上限
    public float boost_speed_max;//ブースト時のスピード上限
    public float quick_boost_speed_max;//クイックブーストのスピード
    public float qboost_cool_time_const;//クールタイム定数
    public float rising_speed;
    public string Terrain_State;

    public float normalspeed;
    public float boostspeed;
    public float boost_max_speed;

    public int hover = 0;

    public string ground_condition;

    public float move_stun = 0;
    public bool move_lock = false;


    public GameObject camera_obj;

    public float lockon_range;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
