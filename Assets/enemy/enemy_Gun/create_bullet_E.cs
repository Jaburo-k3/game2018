using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_bullet_E : MonoBehaviour {
    private GameObject parent;
    public GameObject bullets;
    public GameObject bullet_obj;

    private int cool_time;
    private float cool_const;

    public GameObject muzzle;
    private enemy_weapon_status E_W_status;
    private weapon_switching W_switching;
    private Attack attack;
    private bullet_status Bullet_status;

    private AudioSource AudioSource;
    public AudioClip Bullet_sound;

    Coroutine CC_bullet;

    public float get_cool_const()
    {
        return cool_const;
    }
    public void set_cool_const(float value)
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
    void C_bullet()
    {
        if (!shot_permission() || (E_W_status.get_my_weapon_number() != W_switching.weapon_number[E_W_status.my_arm_number] && CC_bullet != null))
        {
            StopCoroutine(CC_bullet);
            CC_bullet = null;
            return;
        }
        AudioSource.Play();
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        //変更点
        Bullet_status = bullet_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = E_W_status.bullet_speed;
        bullet_obj.tag = "bullet";
        bullet_obj.layer = gameObject.layer;
        bullet_obj.transform.position = muzzle.transform.position;
        Rigidbody bullet_rb = bullet_obj.GetComponent<Rigidbody>();
        bullet_obj.transform.LookAt(E_W_status.E_S_control.set_shot_pos(muzzle.transform.position,E_W_status.bullet_speed,bullet_rb.mass));
        attack.attack = E_W_status.attack;

    }

    IEnumerator C_bullet_system()
    {
        C_bullet();
        Debug.Log("C_bullet");
        while (true)
        {
            yield return new WaitForSeconds(E_W_status.cool_const);
            C_bullet();
        }

    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        E_W_status = this.GetComponent<enemy_weapon_status>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = bullets.GetComponent<Attack>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Bullet_sound;
        AudioSource.volume = 0.05f;
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

        if (E_W_status.E_S_control.shot_permission && shot_permission() && CC_bullet == null)
        {
            CC_bullet = StartCoroutine(C_bullet_system());
        }
        else if (!E_W_status.E_S_control.shot_permission && CC_bullet != null && E_W_status.get_my_weapon_number() == W_switching.weapon_number[E_W_status.my_arm_number])
        {
            StopCoroutine(CC_bullet);
            CC_bullet = null;
            if (E_W_status.cool_time <= 0.0f)
            {
                E_W_status.cool_time = E_W_status.cool_const;
            }
        }
    }
}
