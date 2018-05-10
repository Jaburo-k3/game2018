using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machinegun_E : MonoBehaviour {
    private GameObject parent;
    public GameObject bullets;
    public GameObject bullet_obj;

    public int burst;
    public float burst_time;

    private Attack attack;

    public GameObject muzzle;
    private enemy_weapon_status E_W_status;
    private weapon_switching W_switching;
    private bullet_status Bullet_status;



    bool shot_now = false;

    //private AudioSource AudioSource;
    public AudioClip machinegun_sound;

    Coroutine CC_bullet;

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

    void C_bullet()
    {
        if (!shot_permission())
        {
            if (E_W_status.get_my_weapon_number() != W_switching.weapon_number[E_W_status.my_arm_number] && CC_bullet != null)
            {
                StopCoroutine(CC_bullet);
                CC_bullet = null;
            }
            return;
        }

        AudioSource AudioSource;
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = machinegun_sound;
        AudioSource.volume = 0.15f;
        AudioSource.Play();
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        Bullet_status = bullet_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = E_W_status.bullet_speed;
        bullet_obj.tag = "bullet";
        bullet_obj.layer = gameObject.layer;
        bullet_obj.transform.position = muzzle.transform.position;
        //bullet_obj.transform.LookAt(A_sys.target);
        Rigidbody bullet_rb = bullet_obj.GetComponent<Rigidbody>();
        bullet_obj.transform.LookAt(E_W_status.E_S_control.set_shot_pos(muzzle.transform.position, E_W_status.bullet_speed, bullet_rb.mass));
        attack.attack = E_W_status.attack;
    }

    //IEnumerator
    //
    IEnumerator C_bullet_system()
    {


        C_bullet();
        Debug.Log("Do");
        while (true)
        {
            if (E_W_status.get_my_weapon_number() != W_switching.weapon_number[E_W_status.my_arm_number])
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
        E_W_status = this.GetComponent<enemy_weapon_status>();
        attack = bullets.GetComponent<Attack>();
        W_switching = parent.GetComponent<weapon_switching>();

        burst_time = E_W_status.cool_const;

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

        if (E_W_status.E_S_control.shot_permission && E_W_status.get_my_weapon_number() == W_switching.weapon_number[E_W_status.my_arm_number]
            && CC_bullet == null)
        {
            CC_bullet = StartCoroutine(C_bullet_system());
        }
        else if ((!E_W_status.E_S_control.shot_permission || E_W_status.get_my_weapon_number() != W_switching.weapon_number[E_W_status.my_arm_number])
            && CC_bullet != null)
        {
            
            StopCoroutine(CC_bullet);
            CC_bullet = null;
            W_switching.weapon_change = false;
        }
    }
}
