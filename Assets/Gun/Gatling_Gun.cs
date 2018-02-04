﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling_Gun : MonoBehaviour {
    private GameObject parent;
    public GameObject bullets;
    public GameObject bullet_obj;
    public Vector2 mouse = Vector2.zero;

    private Attack attack;

    public GameObject test;

    public int burst;
    public float burst_time;

    public GameObject[] muzzle;
    public float spinSpeed = 1.0f;
    private Aiming_system A_sys;
    private weapon_status W_status;
    private weapon_switching W_switching;

    private create_hit_marker C_Hit_Marker;

    private AudioSource AudioSource;
    public AudioClip Gatling_sound;

    void set_C_Hit_Marker(GameObject obj)
    {
        C_Hit_Marker = obj.GetComponent<create_hit_marker>();
        C_Hit_Marker.parent = this.parent;
        //Debug.Log(C_Hit_Marker.parent.name);
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
    //IEnumerator
    //
    void C_bullet(int number) {
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        bullet_obj.tag = "bullet";
        bullet_obj.layer = gameObject.layer;
        bullet_obj.transform.position = muzzle[number].transform.position;
        attack.attack = W_status.attack;
        bullet_obj.transform.LookAt(A_sys.target);

        W_status.bullet_counter -= W_status.bullet_one_shot;
        W_status.cool_time = W_status.cool_const;

        set_C_Hit_Marker(bullet_obj);
    }
    IEnumerator Gatling()
    {
        AudioSource.Play();
        //Debug.Log("Do");
        for (int i = 0; i < burst; i++)
        {
            if (W_status.get_my_weapon_number() != W_switching.weapon_number || W_status.bullet_counter < W_status.bullet_one_shot)
            {
                Debug.Log("break");
                break;
            }
            yield return new WaitForSeconds(burst_time);
            for (int j = 0; j < muzzle.Length; j++) {
                C_bullet(j);
                //Debug.Log("1");
            }
            //Debug.Log("for");
        }
        AudioSource.Stop();
        //Debug.Log("end");
    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        W_status = this.GetComponent<weapon_status>();
        A_sys = GameObject.Find("Main Camera").GetComponent<Aiming_system>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = bullets.GetComponent<Attack>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Gatling_sound;
        AudioSource.volume = 0.2f;
        //mouse.y = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (W_status.cool_time > 0)
        {
            W_status.cool_time -= 1;
        }

        if (Input.GetMouseButtonDown(0) && shot_permission())
        {
            
            StartCoroutine("Gatling");
        }
        /*
        if (W_status.bullet_counter <= 0) {
            test = Instantiate(test, this.transform.position, Quaternion.identity);
            test.transform.position = transform.position;
            test.transform.rotation = transform.rotation;
            Destroy(this.gameObject);
        }
        */
    }
}
