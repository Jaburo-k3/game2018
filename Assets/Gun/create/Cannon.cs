using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    private GameObject parent;
    public GameObject bullet;
    public GameObject bullet_obj;

    float rotation = 25;
    //Vector3 rotation;

    private int cool_time;
    private int cool_const = 10;
    public float shot_time;
    public bool shot_now = false;

    public GameObject[] muzzle;
    private Aiming_system A_sys;
    private lockon Lockon;
    private weapon_status W_status;
    private weapon_switching W_switching;
    private Attack attack;
    private bullet_status Bullet_status;
    private chara_status Chara_status;
    private weapon_value_text W_value_text;

    private bullet Bullet;

    private create_hit_marker C_Hit_Marker;

    private AudioSource AudioSource;
    public AudioClip Bullet_sound;
    public Rigidbody rb;
    public float knock_back;
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
        if (W_status.shot_lock == false && W_status.cool_time == 0 && W_status.bullet_counter >= W_status.bullet_one_shot)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void lookat_bullet(GameObject bullet,int muzzle_number)
    {
        if (Lockon.target_obj != null)
        {
            bullet_obj.transform.LookAt(Lockon.deviation_shot(muzzle[muzzle_number].transform.position, Bullet_status.bullet_speed, bullet.GetComponent<Rigidbody>().mass));
        }
        else {
            bullet_obj.transform.LookAt(A_sys.target);
        }
    }
    //
    IEnumerator C_bullet()
    {
        yield return new WaitForSeconds(shot_time/2);
        for (int i = 0; i < muzzle.Length; i++) {
            AudioSource.Play();
            bullet_obj = Instantiate(bullet, this.transform.position, Quaternion.identity);
            Bullet_status = bullet_obj.GetComponent<bullet_status>();
            Bullet_status.bullet_speed = W_status.bullet_speed;
            bullet_obj.tag = "bullet";
            bullet_obj.layer = gameObject.layer;
            bullet_obj.transform.position = muzzle[i].transform.position;
            lookat_bullet(bullet_obj,i);
            //bullet_obj.transform.LookAt(A_sys.target);
            attack.attack = W_status.attack;
            W_status.bullet_counter -= W_status.bullet_one_shot / muzzle.Length;

            W_status.cool_time = W_status.cool_const;

            W_value_text = W_status.W_value_text;
            W_value_text.Weapon_status = this.GetComponent<weapon_status>();
            W_value_text.weapon_shot = true;

            rb = parent.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(parent.transform.forward.x * knock_back * -1,0, parent.transform.forward.z * knock_back * -1));
            set_C_Hit_Marker(bullet_obj);

        }
        shot_now = false;
        transform.Rotate(-25, 0, 0);

    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        W_status = this.GetComponent<weapon_status>();
        Chara_status = parent.GetComponent<chara_status>();
        A_sys = W_status.camera_obj.GetComponent<Aiming_system>();
        Lockon = W_status.camera_obj.GetComponent<lockon>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = bullet.GetComponent<Attack>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Bullet_sound;
        AudioSource.volume = 0.2f;
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

        if (Input.GetButtonDown("button4") && shot_permission() && shot_now == false)
        {
            shot_now = true;
            transform.Rotate(25, 0, 0);
            StartCoroutine(C_bullet());
        }
        else if (Input.GetButton("button4") && W_switching.weapon_change && W_status.cool_time == 0)
        {
            //Input.GetButton("button4") && W_switching.weapon_change && W_status.get_my_weapon_number() == W_switching.weapon_number && W_status.cool_time == 0
            StartCoroutine(C_bullet());
            W_switching.weapon_change = false;
        }
    }
}

