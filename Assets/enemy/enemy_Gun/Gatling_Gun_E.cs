using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling_Gun_E : MonoBehaviour {
    private GameObject parent;
    public GameObject bullets;
    public GameObject bullet_obj;
    public string button;

    private Attack attack;

    public int burst;
    public float burst_time;

    public GameObject[] muzzle;
    private enemy_weapon_status E_W_status;
    private weapon_switching W_switching;
    private bullet_status Bullet_status;

    private create_hit_marker C_Hit_Marker;

    private AudioSource AudioSource;
    public AudioClip Gatling_sound;

    Coroutine C_Gatling = null;
    //発射許可
    private bool shot_permission()
    {
        if (E_W_status.get_my_weapon_number() == W_switching.weapon_number[E_W_status.my_arm_number] &&  E_W_status.cool_time == 0 )
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
            return;
        }
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        Bullet_status = bullet_obj.GetComponent<bullet_status>();
        Bullet_status.bullet_speed = E_W_status.bullet_speed;
        bullet_obj.tag = "bullet";
        bullet_obj.layer = gameObject.layer;
        bullet_obj.transform.position = muzzle[number].transform.position;
        Rigidbody bullet_rb = bullet_obj.GetComponent<Rigidbody>();
        bullet_obj.transform.LookAt(E_W_status.E_S_control.set_shot_pos(muzzle[number].transform.position, E_W_status.bullet_speed, bullet_rb.mass));
        attack.attack = E_W_status.attack;
    }
    IEnumerator Gatling()
    {
        AudioSource.Play();
        for (int j = 0; j < muzzle.Length; j++)
        {
            C_bullet(j);
        }

        while (true)
        {
            if (E_W_status.get_my_weapon_number() != W_switching.weapon_number[E_W_status.my_arm_number])
            {
                Debug.Log("break");
                break;
            }
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

        if (E_W_status.E_S_control.shot_permission && C_Gatling == null)
        {
            C_Gatling = StartCoroutine(Gatling());
        }
        else if ((!E_W_status.E_S_control.shot_permission|| E_W_status.get_my_weapon_number() != W_switching.weapon_number[E_W_status.my_arm_number]) 
            && C_Gatling != null)
        {
            StopCoroutine(C_Gatling);
            E_W_status.cool_time = E_W_status.cool_const;
            C_Gatling = null;
            AudioSource.Stop();
        }
        /*
        if (W_status.bullet_counter <= 0) {
            test = Instantiate(test, this.transform.position, Quaternion.identity);
            test.transform.position = transform.position;
            test.transform.rotation = transform.rotation;
            Destroy(this.gameObject);
        }
        */
    }
}
