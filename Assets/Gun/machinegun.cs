﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machinegun : MonoBehaviour {
    private GameObject parent;
    public GameObject bullets;
    public GameObject bullet_obj;
    public Vector2 mouse = Vector2.zero;

    public int burst;
    public float burst_time;

    private Attack attack;

    public GameObject muzzle;
    public float spinSpeed = 1.0f;
    private Aiming_system A_sys;
    private lockon Lockon;
    private weapon_status W_status;
    private weapon_switching W_switching;
    private bullet_status Bullet_status;

    private create_hit_marker C_Hit_Marker;


    bool shot_now = false;

    //private AudioSource AudioSource;
    public AudioClip machinegun_sound;

    Coroutine CC_bullet;

    void set_C_Hit_Marker(GameObject obj)
    {
        C_Hit_Marker = obj.GetComponent<create_hit_marker>();
        C_Hit_Marker.parent = this.parent;
        Debug.Log(C_Hit_Marker.parent.name);
    }
    //発射許可
    private bool shot_permission()
    {
        if (W_status.get_my_weapon_number() == W_switching.weapon_number && W_status.shot_lock == false 
            && W_status.cool_time == 0 && W_status.bullet_counter >= W_status.bullet_one_shot)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void lookat_bullet(GameObject bullet)
    {
        if (Lockon.target_obj != null)
        {
            bullet_obj.transform.LookAt(Lockon.deviation_shot(muzzle.transform.position, Bullet_status.bullet_speed, bullet.GetComponent<Rigidbody>().mass));
        }
        else {
            bullet_obj.transform.LookAt(A_sys.target);
        }
    }

    void C_bullet() {
        if (!shot_permission())
        {
            if (W_status.get_my_weapon_number() != W_switching.weapon_number && CC_bullet != null)
            {
                StopCoroutine(CC_bullet);
            }
            return;
        }

        AudioSource AudioSource;
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = machinegun_sound;
        AudioSource.volume = 0.3f;
        AudioSource.Play();
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        Bullet_status = bullet_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = W_status.bullet_speed;
        bullet_obj.tag = "bullet";
        bullet_obj.layer = gameObject.layer;
        bullet_obj.transform.position = muzzle.transform.position;
        //bullet_obj.transform.LookAt(A_sys.target);
        lookat_bullet(bullet_obj);
        attack.attack = W_status.attack;
        W_status.bullet_counter -= W_status.bullet_one_shot;
        set_C_Hit_Marker(bullet_obj);
    }

    //IEnumerator
    //
    IEnumerator C_bullet_system()
    {


        C_bullet();
        Debug.Log("Do");
        while (true) {
            if (W_status.get_my_weapon_number() != W_switching.weapon_number || W_status.bullet_counter < W_status.bullet_one_shot)
            {
                Debug.Log("break");
                StopCoroutine(CC_bullet);
            }
            yield return new WaitForSeconds(burst_time);
            C_bullet();
        }
        //Debug.Log("end");
    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        W_status = this.GetComponent<weapon_status>();
        A_sys = GameObject.Find("Main Camera").GetComponent<Aiming_system>();
        Lockon = GameObject.Find("Main Camera").GetComponent<lockon>();
        attack = bullets.GetComponent<Attack>();
        W_switching = parent.GetComponent<weapon_switching>();

        burst_time = W_status.cool_const;
    }

    // Update is called once per frame
    void Update()
    {
        if (W_status.cool_time > 0.0f)
        {
            W_status.cool_time -= 1 * Time.deltaTime;
            if (W_status.cool_time < 0)
            {
                W_status.cool_time = 0;
            }
        }

        if (Input.GetButtonDown("button5") && W_status.get_my_weapon_number() == W_switching.weapon_number)
        {
            CC_bullet = StartCoroutine(C_bullet_system());
        }
        else if (Input.GetButton("button5") && W_switching.weapon_change && W_status.get_my_weapon_number() == W_switching.weapon_number)
        {
            CC_bullet = StartCoroutine(C_bullet_system());
            W_switching.weapon_change = false;
        }
        else if (Input.GetButtonUp("button5") && CC_bullet != null && W_status.get_my_weapon_number() == W_switching.weapon_number) {
            StopCoroutine(CC_bullet);
            CC_bullet = null;
            W_status.cool_time = W_status.cool_const;
        }
        else if (Input.GetButton("button5") && W_status.get_my_weapon_number() != W_switching.weapon_number && CC_bullet != null)
        {
            StopCoroutine(CC_bullet);
            CC_bullet = null;
            if (W_status.cool_time == 0)
            {
                W_status.cool_time = W_status.cool_const;
            }
        }
    }
}
