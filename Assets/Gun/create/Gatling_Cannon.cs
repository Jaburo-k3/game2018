using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling_Cannon : MonoBehaviour {
    private GameObject parent;
    public GameObject bullets;
    public GameObject bullet_obj;
    public Vector2 mouse = Vector2.zero;
    public GameObject[] barrel;
    public Vector3 barrel_roll;
    public string button;

    private Attack attack;


    public int burst;
    public float burst_time;

    public GameObject[] muzzle;
    public float spinSpeed = 1.0f;
    private Aiming_system A_sys;
    private lockon Lockon;
    private weapon_status W_status;
    private weapon_switching W_switching;
    private bullet_status Bullet_status;
    private barrel_roll Barrel_roll;
    private weapon_value_text W_value_text;

    private create_hit_marker C_Hit_Marker;

    private AudioSource AudioSource;
    public AudioClip Gatling_sound;

    Coroutine C_Gatling;

    void set_C_Hit_Marker(GameObject obj)
    {
        C_Hit_Marker = obj.GetComponent<create_hit_marker>();
        C_Hit_Marker.parent = this.parent;
        //Debug.Log(C_Hit_Marker.parent.name);
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
    void lookat_bullet(GameObject bullet, int muzzle_number)
    {
        if (Lockon.target_obj != null)
        {
            bullet_obj.transform.LookAt(Lockon.deviation_shot(muzzle[muzzle_number].transform.position, Bullet_status.bullet_speed, bullet.GetComponent<Rigidbody>().mass));
        }
        else {
            bullet_obj.transform.LookAt(A_sys.target);
        }
    }

    //IEnumerator
    //
    void C_bullet(int number)
    {
        if (!shot_permission())
        {
            barrel[0].transform.Rotate(-1 * barrel_roll);
            return;
        }
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        Bullet_status = bullet_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = W_status.bullet_speed;
        bullet_obj.tag = "bullet";
        bullet_obj.layer = gameObject.layer;
        bullet_obj.transform.position = muzzle[number].transform.position;
        attack.attack = W_status.attack;
        lookat_bullet(bullet_obj, number);
        //bullet_obj.transform.LookAt(A_sys.target);

        W_status.bullet_counter -= W_status.bullet_one_shot;

        W_value_text = W_status.W_value_text;
        W_value_text.Weapon_status = this.GetComponent<weapon_status>();
        W_value_text.weapon_shot = true;

        set_C_Hit_Marker(bullet_obj);
    }
    IEnumerator Gatling()
    {
        barrel[0].transform.Rotate(barrel_roll);
        AudioSource.Play();
        for (int j = 0; j < muzzle.Length; j++)
        {
            C_bullet(j);
        }

        while (true)
        {
            if (W_status.bullet_counter < W_status.bullet_one_shot)
            {
                barrel[0].transform.Rotate(-1 * barrel_roll);
                break;
            }
            yield return new WaitForSeconds(burst_time);

            for (int j = 0; j < muzzle.Length; j++)
            {
                C_bullet(j);
            }
        }
        AudioSource.Stop();
    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        W_status = this.GetComponent<weapon_status>();
        A_sys = W_status.camera_obj.GetComponent<Aiming_system>();
        Lockon = W_status.camera_obj.GetComponent<lockon>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = bullets.GetComponent<Attack>();
        Barrel_roll = this.GetComponent<barrel_roll>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Gatling_sound;
        AudioSource.volume = 0.2f;
        AudioSource.loop = true;

        burst_time = W_status.cool_const;

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
        if (W_status.cool_time > 0.0f)
        {
            W_status.cool_time -= 1 * Time.deltaTime;
            if (W_status.cool_time < 0)
            {
                W_status.cool_time = 0;
            }
        }

        if (Input.GetButtonDown(button))
        {
            if (shot_permission())
            {
                Barrel_roll.roll = true;
            }
            C_Gatling = StartCoroutine(Gatling());
        }
        else if (Input.GetButtonUp(button))
        {
            StopCoroutine(C_Gatling);
            barrel[0].transform.Rotate(-1 * barrel_roll);
            W_status.cool_time = W_status.cool_const;
            C_Gatling = null;
            Barrel_roll.roll = false;
            AudioSource.Stop();
        }
        else if (Input.GetButton(button) && C_Gatling == null && W_switching.weapon_change && shot_permission() && W_status.cool_time == 0) {
            C_Gatling = StartCoroutine(Gatling());
            Barrel_roll.roll = true;
            W_switching.weapon_change = false;
        }
        else if (Input.GetButton(button) && W_status.get_my_weapon_number() != W_switching.weapon_number[W_status.my_arm_number] && C_Gatling != null)
        {
            StopCoroutine(C_Gatling);
            W_status.cool_time = W_status.cool_const;
            Barrel_roll.roll = false;
            C_Gatling = null;
            AudioSource.Stop();
            if (W_status.cool_time == 0)
            {
                W_status.cool_time = W_status.cool_const;
            }
        }
        
    }
}
