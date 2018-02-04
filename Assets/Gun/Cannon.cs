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
    private Player_move player_move;
    private Aiming_system A_sys;
    private weapon_status W_status;
    private weapon_switching W_switching;
    private Attack attack;

    private bullet Bullet;

    private create_hit_marker C_Hit_Marker;

    private AudioSource AudioSource;
    public AudioClip Bullet_sound;
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
    //
    IEnumerator C_bullet()
    {
        player_move.move_lock = true;
        player_move.gravity_lock = true;
        yield return new WaitForSeconds(shot_time/2);
        for (int i = 0; i < muzzle.Length; i++) {
            AudioSource.Play();
            bullet_obj = Instantiate(bullet, this.transform.position, Quaternion.identity);
            bullet_obj.tag = "bullet";
            bullet_obj.layer = gameObject.layer;
            bullet_obj.transform.position = muzzle[i].transform.position;
            bullet_obj.transform.LookAt(A_sys.target);
            attack.attack = W_status.attack;
            W_status.bullet_counter -= W_status.bullet_one_shot / muzzle.Length;

            W_status.cool_time = W_status.cool_const;

            set_C_Hit_Marker(bullet_obj);
        }
        yield return new WaitForSeconds(shot_time / 2);
        player_move.move_lock = false;
        player_move.gravity_lock = false;
        shot_now = false;
        transform.Rotate(-25, 0, 0);
        Debug.Log(player_move.move_lock);

    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        player_move = parent.GetComponent<Player_move>();
        W_status = this.GetComponent<weapon_status>();
        A_sys = GameObject.Find("Main Camera").GetComponent<Aiming_system>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = bullet.GetComponent<Attack>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Bullet_sound;
        AudioSource.volume = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (W_status.cool_time > 0)
        {
            W_status.cool_time -= 1;
        }

        if (Input.GetMouseButtonDown(0) && shot_permission() && shot_now == false)
        {
            shot_now = true;
            transform.Rotate(25, 0, 0);
            StartCoroutine("C_bullet");
        }
    }
}

