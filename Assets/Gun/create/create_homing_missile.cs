using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_homing_missile : MonoBehaviour {
    private GameObject parent;
    public GameObject missile;
    public GameObject missile_obj;
    public GameObject target;
    public string button;
    public int multiple_firing;
    public float delay;


    private int cool_time;
    private int cool_const = 10;

    public GameObject[] muzzle;
    private Aiming_system A_sys;
    private lockon Lockon;
    private weapon_status W_status;
    private weapon_switching W_switching;
    private Homing_Missile homing_missile;
    private Attack attack;
    private bullet_status Bullet_status;
    private weapon_value_text W_value_text;

    private create_hit_marker C_Hit_Marker;

    //private AudioSource AudioSource;
    public AudioClip homingmissile_sound;

    Coroutine CC_Missile;
    bool one_shot = false;
    bool button_up = false;

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
            W_status.shot_lock == false && W_status.cool_time == 0 && W_status.bullet_counter >= W_status.bullet_one_shot )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //
    void C_Missile(int number)
    {
        if (!shot_permission())
        {
            if (CC_Missile != null)
            {
                StopCoroutine(CC_Missile);
            }
            return;
        }
        AudioSource AudioSource;
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = homingmissile_sound;
        AudioSource.volume = 0.1f;
        AudioSource.Play();
        missile_obj = Instantiate(missile, this.transform.position, Quaternion.identity);
        Bullet_status = missile_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = W_status.bullet_speed;
        missile_obj.tag = "bullet";
        missile_obj.layer = gameObject.layer;
        missile_obj.transform.position = muzzle[number].transform.position;
        /*
        Vector3 target_pos = A_sys.target - transform.transform.position;
        missile_obj.transform.LookAt(missile_obj.transform.position + target_pos);
        */
        Vector3 look_pos = missile_obj.transform.position;
        look_pos.x += parent.transform.forward.x/2;
        look_pos.z += parent.transform.forward.z/2;
        look_pos.y += 1;
        missile_obj.transform.LookAt(look_pos);
        homing_missile = missile_obj.GetComponent<Homing_Missile>();
        homing_missile.target = target;
        attack.attack = W_status.attack;
        W_status.bullet_counter -= W_status.bullet_one_shot;

        W_value_text = W_status.W_value_text;
        W_value_text.Weapon_status = this.GetComponent<weapon_status>();
        W_value_text.weapon_shot = true;

        set_C_Hit_Marker(missile_obj);

    }
    IEnumerator Missile()
    {
        for (int i = 0; i < muzzle.Length / multiple_firing; i++)
        {
            Debug.Log("first");
            target = Lockon.target_obj;
            yield return new WaitForSeconds(delay);
            for (int j = 0; j < multiple_firing; j++)
            {
                C_Missile(i * 2 + j);
            }
        }
        one_shot = true;


        while (true) {
            yield return new WaitForSeconds(W_status.cool_const);
            for (int i = 0; i < muzzle.Length / multiple_firing; i++)
            {
                target = Lockon.target_obj;
                yield return new WaitForSeconds(delay);
                for (int j = 0; j < multiple_firing; j++)
                {
                    C_Missile(i * 2 + j);
                }
            }
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
        if (W_status.cool_time > 0.0f)
        {
            W_status.cool_time -= 1 * Time.deltaTime;
            if (W_status.cool_time < 0)
            {
                W_status.cool_time = 0;
            }
        }

        if (Input.GetButtonDown(button) && CC_Missile == null && one_shot == false && shot_permission())
        {
            Debug.Log("Down");
            //Input.GetButtonDown("button4") && W_status.get_my_weapon_number() == W_switching.weapon_number
            CC_Missile = StartCoroutine(Missile());
        }
        else if (Input.GetButton(button) && W_switching.weapon_change && CC_Missile == null && W_status.cool_time == 0 && shot_permission())
        {
            //Input.GetButton("button4") && W_switching.weapon_change && W_status.get_my_weapon_number() == W_switching.weapon_number && W_status.cool_time == 0
            CC_Missile = StartCoroutine(Missile());
            W_switching.weapon_change = false;
        }
        else if (Input.GetButtonUp(button) && CC_Missile != null ) {
            button_up = true;
        }
        if (button_up && one_shot && CC_Missile != null) {
            StopCoroutine(CC_Missile);
            CC_Missile = null;
            target = null;
            W_status.cool_time = W_status.cool_const;
            button_up = false;
            one_shot = false;
        }
        /*
        else if (Input.GetButton("button4") && CC_Missile != null)
        {
            Debug.Log("end");
            //Input.GetButton("button4") && W_status.get_my_weapon_number() != W_switching.weapon_number && CC_Missile != null
            StopCoroutine(CC_Missile);
            CC_Missile = null;
            if (W_status.cool_time == 0)
            {
                W_status.cool_time = W_status.cool_const;
            }
        }
        */
    }
}
