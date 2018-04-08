using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_bullet : MonoBehaviour {
    public GameObject camera;
    private GameObject parent;
    public GameObject bullets;
    public GameObject bullet_obj;
    public Vector2 mouse = Vector2.zero;

    private int cool_time;
    private float cool_const;

    public GameObject muzzle;
    public float spinSpeed = 1.0f;
    private Aiming_system A_sys;
    private lockon Lockon;//変更点
    private weapon_status W_status;
    private weapon_switching W_switching;
    private Attack attack;
    private bullet_status Bullet_status;
    private weapon_value_text W_value_text;//変更点

    private bullet Bullet;

    private create_hit_marker C_Hit_Marker;

    private AudioSource AudioSource;
    public AudioClip Bullet_sound;

    Coroutine CC_bullet;

    public float get_cool_const() {
        return cool_const;
    }
    public void set_cool_const(float value) {
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
        if (W_status.get_my_weapon_number() == W_switching.weapon_number
            && W_status.cool_time == 0 && W_status.bullet_counter >= W_status.bullet_one_shot)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //変更点
    //
    void lookat_bullet(GameObject bullet) {
        if (Lockon.target_obj != null)
        {
            bullet_obj.transform.LookAt(Lockon.deviation_shot(muzzle.transform.position, Bullet_status.bullet_speed, bullet.GetComponent<Rigidbody>().mass));
        }
        else {
            bullet_obj.transform.LookAt(A_sys.target);
        }
    }
    //
    void C_bullet() {
        if (!shot_permission() || (W_status.get_my_weapon_number() != W_switching.weapon_number && CC_bullet != null)) {
            Debug.Log("cancel" + this.gameObject.name);
            StopCoroutine(CC_bullet);
            CC_bullet = null;
            //Debug.Log("cancel");
            return;
        }
        AudioSource.Play();
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        //変更点
        Bullet_status = bullet_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = W_status.bullet_speed;
        bullet_obj.tag = "bullet";
        bullet_obj.layer = gameObject.layer;
        bullet_obj.transform.position = muzzle.transform.position;
        //Debug.Log(A_sys.target);
        //bullet_obj.transform.LookAt(A_sys.target);
        lookat_bullet(bullet_obj);//変更点
        attack.attack = W_status.attack;
        W_status.bullet_counter -= W_status.bullet_one_shot;

        //変更点
        W_value_text = W_status.W_value_text;
        W_value_text.Weapon_status = this.GetComponent<weapon_status>();
        W_value_text.weapon_shot = true;

        set_C_Hit_Marker(bullet_obj);
        
    }

    IEnumerator C_bullet_system()
    {
        C_bullet();
        Debug.Log("C_bullet");
        while (true)
        {
            yield return new WaitForSeconds(W_status.cool_const);
            C_bullet();
        }

    }
    // Use this for initialization
    void Start () {
        parent = transform.root.gameObject;
        W_status = this.GetComponent<weapon_status>();
        A_sys = W_status.camera_obj.GetComponent<Aiming_system>();
        Lockon = W_status.camera_obj.GetComponent<lockon>();
        W_switching = parent.GetComponent<weapon_switching>();
        attack = bullets.GetComponent<Attack>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Bullet_sound;
        AudioSource.volume = 0.2f;
        //Debug.Log(parent.name);
    }
	
	// Update is called once per frame
	void Update () {

        if(W_status.cool_time > 0.0f)
        {
            W_status.cool_time -= 1 * Time.deltaTime;
            if (W_status.cool_time < 0) {
                W_status.cool_time = 0;
            }
        }

        if (Input.GetButton("button5") && shot_permission() && CC_bullet == null)
        {
            CC_bullet = StartCoroutine(C_bullet_system());
        }
        else if (Input.GetButtonUp("button5") && CC_bullet != null && W_status.get_my_weapon_number() == W_switching.weapon_number)
        {
            StopCoroutine(CC_bullet);
            CC_bullet = null;
            if (W_status.cool_time <= 0.0f)
            {
                Debug.Log("cool_time");
                W_status.cool_time = W_status.cool_const;
            }
        }
        /*
        else if (Input.GetButton("button5") && W_status.get_my_weapon_number() != W_switching.weapon_number && CC_bullet != null) {
            StopCoroutine(CC_bullet);
            CC_bullet = null;
            if (W_status.cool_time <= 0.0f)
            {
                Debug.Log("cool_time");
                W_status.cool_time = W_status.cool_const;
            }
        }
        */
    }
}
