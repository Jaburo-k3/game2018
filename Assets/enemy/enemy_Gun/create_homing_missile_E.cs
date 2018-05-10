using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_homing_missile_E : MonoBehaviour {
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
    private enemy_weapon_status E_W_status;
    private weapon_switching W_switching;
    private Homing_Missile homing_missile;
    private Attack attack;
    private bullet_status Bullet_status;

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
    //発射許可
    private bool shot_permission()
    {
        if (E_W_status.get_my_weapon_number() == W_switching.weapon_number[E_W_status.my_arm_number] && E_W_status.cool_time == 0)
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
        AudioSource.volume = 0.05f;
        AudioSource.Play();
        missile_obj = Instantiate(missile, this.transform.position, Quaternion.identity);
        Bullet_status = missile_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = E_W_status.bullet_speed;
        missile_obj.tag = "bullet";
        missile_obj.layer = gameObject.layer;
        missile_obj.transform.position = muzzle[number].transform.position;
        /*
        Vector3 target_pos = A_sys.target - transform.transform.position;
        missile_obj.transform.LookAt(missile_obj.transform.position + target_pos);
        */
        Vector3 look_pos = missile_obj.transform.position;
        look_pos.x += parent.transform.forward.x / 2;
        look_pos.z += parent.transform.forward.z / 2;
        look_pos.y += 1;
        missile_obj.transform.LookAt(look_pos);
        homing_missile = missile_obj.GetComponent<Homing_Missile>();
        homing_missile.target = target;
        attack.attack = E_W_status.attack;
    }
    IEnumerator Missile()
    {
        for (int i = 0; i < muzzle.Length / multiple_firing; i++)
        {
            Debug.Log("first");
            target = E_W_status.E_S_control.center_point;
            yield return new WaitForSeconds(delay);
            for (int j = 0; j < multiple_firing; j++)
            {
                C_Missile(i * 2 + j);
            }
        }
        one_shot = true;


        while (true)
        {
            yield return new WaitForSeconds(E_W_status.cool_const);
            for (int i = 0; i < muzzle.Length / multiple_firing; i++)
            {
                target = E_W_status.E_S_control.center_point;
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
        E_W_status = this.GetComponent<enemy_weapon_status>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = missile.GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (E_W_status.cool_time > 0.0f)
        {
            E_W_status.cool_time -= 1 * Time.deltaTime;
            if (E_W_status.cool_time < 0)
            {
                E_W_status.cool_time = 0;
            }
        }

        if (E_W_status.E_S_control.shot_permission && CC_Missile == null && one_shot == false && shot_permission())
        {
            Debug.Log("Down");
            //Input.GetButtonDown("button4") && W_status.get_my_weapon_number() == W_switching.weapon_number
            CC_Missile = StartCoroutine(Missile());
        }
        else if ((!E_W_status.E_S_control.shot_permission || E_W_status.get_my_weapon_number() != W_switching.weapon_number[E_W_status.my_arm_number]) 
            && CC_Missile != null)
        {
            button_up = true;
        }
        if (button_up && one_shot && CC_Missile != null)
        {
            StopCoroutine(CC_Missile);
            CC_Missile = null;
            target = null;
            E_W_status.cool_time = E_W_status.cool_const;
            button_up = false;
            one_shot = false;
        }
    }
}

