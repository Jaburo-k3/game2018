using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Pod_E : MonoBehaviour {
    private GameObject parent;
    public GameObject missile;
    public GameObject missile_obj;
    public float delay;

    public GameObject[] muzzle;
    private enemy_weapon_status E_W_status;
    private weapon_switching W_switching;
    private Attack attack;
    private bullet_status Bullet_status;

    Coroutine Missile_cor = null;

    //private AudioSource AudioSource;
    public AudioClip missile_sound;

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
        AudioSource AudioSource;
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = missile_sound;
        AudioSource.volume = 0.1f;
        AudioSource.Play();
        missile_obj = Instantiate(missile, this.transform.position, Quaternion.identity);
        Bullet_status = missile_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = E_W_status.bullet_speed;
        missile_obj.tag = "bullet";
        missile_obj.layer = gameObject.layer;
        missile_obj.transform.position = muzzle[number].transform.position;
        Rigidbody bullet_rb = missile_obj.GetComponent<Rigidbody>();
        missile_obj.transform.LookAt(E_W_status.E_S_control.set_shot_pos(muzzle[number].transform.position, E_W_status.bullet_speed, bullet_rb.mass));
        //missile_obj.transform.LookAt(missile_obj.transform.position + target_pos);
        attack.attack = E_W_status.attack;

    }
    IEnumerator Missile()
    {
        C_Missile(0);
        for (int i = 1; i < muzzle.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            C_Missile(i);
        }
        E_W_status.cool_time = E_W_status.cool_const;
        Missile_cor = null;
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
        if (E_W_status.cool_time > 0)
        {
            E_W_status.cool_time -= 1 * Time.deltaTime;
            if (E_W_status.cool_time < 0)
            {
                E_W_status.cool_time = 0;
            }
        }

        if (E_W_status.E_S_control.shot_permission && shot_permission() && Missile_cor == null)
        {
            Missile_cor =  StartCoroutine(Missile());
        }
    }
}
