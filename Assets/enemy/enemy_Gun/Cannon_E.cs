using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_E : MonoBehaviour {
    private GameObject parent;
    public GameObject bullet;
    public GameObject bullet_obj;
    public string button;

    float rotation = 25;
    //Vector3 rotation;

    private int cool_time;
    private int cool_const = 10;
    public float shot_time;
    public bool shot_now = false;

    public GameObject[] muzzle;
    private enemy_weapon_status E_W_status;
    private weapon_switching W_switching;
    private Attack attack;
    private bullet_status Bullet_status;
    private chara_status Chara_status;

    private bullet Bullet;

    private create_hit_marker C_Hit_Marker;

    private AudioSource AudioSource;
    public AudioClip Bullet_sound;
    public Rigidbody rb;
    public float knock_back;
    Coroutine C_bullet_cor;


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
    IEnumerator C_bullet()
    {
        yield return new WaitForSeconds(shot_time / 2);
        for (int i = 0; i < muzzle.Length; i++)
        {
            AudioSource.Play();
            bullet_obj = Instantiate(bullet, this.transform.position, Quaternion.identity);
            Bullet_status = bullet_obj.GetComponent<bullet_status>();
            Bullet_status.bullet_speed = E_W_status.bullet_speed;
            bullet_obj.tag = "bullet";
            bullet_obj.layer = gameObject.layer;
            bullet_obj.transform.position = muzzle[i].transform.position;
            Rigidbody bullet_rb = bullet_obj.GetComponent<Rigidbody>();
            bullet_obj.transform.LookAt(E_W_status.E_S_control.set_shot_pos(muzzle[i].transform.position, E_W_status.bullet_speed, bullet_rb.mass));
            //bullet_obj.transform.LookAt(A_sys.target);
            attack.attack = E_W_status.attack;

            E_W_status.cool_time = E_W_status.cool_const;

            rb = parent.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(parent.transform.forward.x * knock_back * -1, 0, parent.transform.forward.z * knock_back * -1));
            set_C_Hit_Marker(bullet_obj);

        }
        shot_now = false;
        transform.Rotate(-25f, 0, 0);
        C_bullet_cor = null;

    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        E_W_status = this.GetComponent<enemy_weapon_status>();
        Chara_status = parent.GetComponent<chara_status>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = bullet.GetComponent<Attack>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Bullet_sound;
        AudioSource.volume = 0.1f;
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

        if (E_W_status.E_S_control.shot_permission && C_bullet_cor == null && shot_permission() && shot_now == false)
        {
            Debug.Log("shot");
            shot_now = true;
            transform.Rotate(25, 0, 0);
            C_bullet_cor = StartCoroutine(C_bullet());
        }
    }
}

