using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Pod : MonoBehaviour {
    private GameObject parent;
    public GameObject missile;
    public GameObject missile_obj;
    public string button;
    public float delay;


    private int cool_time;
    private int cool_const = 10;

    public GameObject[] muzzle;
    private Aiming_system A_sys;
    private lockon Lockon;
    private weapon_status W_status;
    private weapon_switching W_switching;
    private Attack attack;
    private bullet_status Bullet_status;
    private weapon_value_text W_value_text;

    private create_hit_marker C_Hit_Marker;

    //private AudioSource AudioSource;
    public AudioClip missile_sound;

    public int get_cool_const()
    {
        return cool_const;
    }
    public void set_cool_const(int value)
    {
        cool_const = value;
    }

    void set_C_Hit_Marker(GameObject obj)
    {
        C_Hit_Marker = obj.GetComponent<create_hit_marker>();
        C_Hit_Marker.parent = this.parent;
        Debug.Log(C_Hit_Marker.parent.name);
    }
    //発射許可
    private bool shot_permission()
    {
        if (W_status.get_my_weapon_number() == W_switching.weapon_number[W_status.my_arm_number] && 
            W_status.shot_lock == false && W_status.cool_time == 0 && W_status.bullet_counter >= W_status.bullet_one_shot)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void lookat_bullet(GameObject bullet,int muzzle_numer)
    {
        if (Lockon.target_obj != null)
        {
            missile_obj.transform.LookAt(Lockon.deviation_shot(muzzle[muzzle_numer].transform.position, Bullet_status.bullet_speed, bullet.GetComponent<Rigidbody>().mass));
        }
        else {
            missile_obj.transform.LookAt(A_sys.target);
        }
    }
    //
    void C_Missile(int number)
    {
        AudioSource AudioSource;
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = missile_sound;
        AudioSource.volume = 0.3f;
        AudioSource.Play();
        missile_obj = Instantiate(missile, this.transform.position, Quaternion.identity);
        Bullet_status = missile_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = W_status.bullet_speed;
        missile_obj.tag = "bullet";
        missile_obj.layer = gameObject.layer;
        missile_obj.transform.position = muzzle[number].transform.position;
        Vector3 target_pos = A_sys.target - transform.transform.position;
        lookat_bullet(missile_obj,number);
        //missile_obj.transform.LookAt(missile_obj.transform.position + target_pos);
        attack.attack = W_status.attack;
        W_status.bullet_counter -= W_status.bullet_one_shot;

        W_value_text = W_status.W_value_text;
        W_value_text.Weapon_status = this.GetComponent<weapon_status>();
        W_value_text.weapon_shot = true;

        W_status.cool_time = W_status.cool_const;

        set_C_Hit_Marker(missile_obj);
        Debug.Log(W_status.bullet_counter);

    }
    IEnumerator Missile() {
        C_Missile(0);
        for (int i = 1; i < muzzle.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            C_Missile(i);
            set_C_Hit_Marker(missile_obj);
        }
    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        W_status = this.GetComponent<weapon_status>();
        A_sys = W_status.camera_obj.GetComponent<Aiming_system>();
        Lockon = W_status.camera_obj.GetComponent<lockon>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = missile.GetComponent<Attack>();

        if (W_status.my_arm_number == 0)
        {
            button = "button5";
        }
        else {
            button = "button4";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (W_status.cool_time > 0)
        {
            W_status.cool_time -= 1 * Time.deltaTime;
            if (W_status.cool_time < 0)
            {
                W_status.cool_time = 0;
            }
        }

        if (Input.GetButtonDown(button) && shot_permission())
        {
            StartCoroutine(Missile());
        }
        else if (Input.GetButton(button) && W_switching.weapon_change && W_status.cool_time == 0 && shot_permission())
        {
            //Input.GetButton("button4") && W_switching.weapon_change && W_status.get_my_weapon_number() == W_switching.weapon_number && W_status.cool_time == 0
            StartCoroutine(Missile());
            W_switching.weapon_change = false;
        }
    }
}
