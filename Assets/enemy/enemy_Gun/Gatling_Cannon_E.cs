using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling_Cannon_E : MonoBehaviour {
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
    private enemy_weapon_status E_W_status;
    private weapon_switching W_switching;
    private bullet_status Bullet_status;
    private barrel_roll Barrel_roll;

    private create_hit_marker C_Hit_Marker;

    private AudioSource AudioSource;
    public AudioClip Gatling_sound;

    Coroutine C_Gatling = null;

    void set_C_Hit_Marker(GameObject obj)
    {
        C_Hit_Marker = obj.GetComponent<create_hit_marker>();
        C_Hit_Marker.parent = this.parent;
        //Debug.Log(C_Hit_Marker.parent.name);
    }
    //発射許可
    private bool shot_permission()
    {
        if (E_W_status.get_my_weapon_number() == W_switching.weapon_number[E_W_status.my_arm_number] && E_W_status.cool_time == 0 )
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
    void C_bullet(int number)
    {
        if (!shot_permission())
        {
            barrel[0].transform.Rotate(-1 * barrel_roll);
            return;
        }
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        Bullet_status = bullet_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = E_W_status.bullet_speed;
        bullet_obj.tag = "bullet";
        bullet_obj.layer = gameObject.layer;
        bullet_obj.transform.position = muzzle[number].transform.position;
        attack.attack = E_W_status.attack;
        Rigidbody bullet_rb = bullet_obj.GetComponent<Rigidbody>();
        bullet_obj.transform.LookAt(E_W_status.E_S_control.set_shot_pos(muzzle[0].transform.position, E_W_status.bullet_speed, bullet_rb.mass));

        //bullet_obj.transform.LookAt(A_sys.target);

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
        E_W_status = this.GetComponent<enemy_weapon_status>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = bullets.GetComponent<Attack>();
        Barrel_roll = this.GetComponent<barrel_roll>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Gatling_sound;
        AudioSource.volume = 0.05f;
        AudioSource.loop = true;

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

        if (E_W_status.E_S_control.shot_permission && shot_permission() && C_Gatling == null)
        {
            if (shot_permission())
            {
                Barrel_roll.roll = true;
            }
            C_Gatling = StartCoroutine(Gatling());
        }
        else if ((!E_W_status.E_S_control.shot_permission || E_W_status.get_my_weapon_number() != W_switching.weapon_number[E_W_status.my_arm_number]) 
            && C_Gatling != null)
        {
            StopCoroutine(C_Gatling);
            barrel[0].transform.Rotate(-1 * barrel_roll);
            E_W_status.cool_time = E_W_status.cool_const;
            C_Gatling = null;
            Barrel_roll.roll = false;
            AudioSource.Stop();
        }

    }
}
