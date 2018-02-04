using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_bullet : MonoBehaviour {
    private GameObject parent;
    public GameObject bullets;
    public GameObject bullet_obj;
    public Vector2 mouse = Vector2.zero;

    private int cool_time;
    private int cool_const = 10;

    public GameObject muzzle;
    public float spinSpeed = 1.0f;
    private Aiming_system A_sys;
    private weapon_status W_status;
    private weapon_switching W_switching;
    private Attack attack;

    private bullet Bullet;

    private create_hit_marker C_Hit_Marker;

    private AudioSource AudioSource;
    public AudioClip Bullet_sound;

    public int get_cool_const() {
        return cool_const;
    }
    public void set_cool_const(int value) {
        cool_const = value;
    }

    void set_C_Hit_Marker(GameObject obj)
    {
        C_Hit_Marker = obj.GetComponent<create_hit_marker>();
        C_Hit_Marker.parent = this.parent;
        //Debug.Log(C_Hit_Marker.parent.name);
    }
    //発射許可
    private bool shot_permission() {
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
    void C_bullet() {
        AudioSource.Play();
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        //bullet_obj.tag = parent.tag;
        bullet_obj.tag = "bullet";
        bullet_obj.layer = gameObject.layer;
        bullet_obj.transform.position = muzzle.transform.position;
        //Debug.Log(A_sys.target);
        bullet_obj.transform.LookAt(A_sys.target);
        attack.attack = W_status.attack;
        W_status.bullet_counter -= W_status.bullet_one_shot;

        W_status.cool_time = W_status.cool_const;

        set_C_Hit_Marker(bullet_obj);
        //Debug.Log(W_status.bullet_counter);
        
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(0.1f);
        C_bullet();
    }
    // Use this for initialization
    void Start () {
        parent = transform.root.gameObject;
        W_status = this.GetComponent<weapon_status>();
        A_sys = GameObject.Find("Main Camera").GetComponent<Aiming_system>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = bullets.GetComponent<Attack>();

        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Bullet_sound;
        AudioSource.volume = 0.2f;
        //Debug.Log(parent.name);
    }
	
	// Update is called once per frame
	void Update () {
        if(W_status.cool_time > 0)
        {
            W_status.cool_time -= 1;
        }

        if (Input.GetMouseButtonDown(0) && shot_permission())
        {
            C_bullet();
            //StartCoroutine(test());
        }
    }
}
