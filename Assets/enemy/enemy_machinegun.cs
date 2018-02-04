using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_machinegun : MonoBehaviour {
    private enemy_weapon_status Enemy_Weapon_Status;
    private GameObject parent;
    public GameObject bullets;
    public GameObject bullet_obj;

    private Attack attack;

    public GameObject test;

    public float burst_time;

    public GameObject[] muzzle;
    public float spinSpeed = 1.0f;

    public bool shot_now = false;

    //発射許可
    private bool shot_permission()
    {
        if (Enemy_Weapon_Status.cool_time == 0 && shot_now == false)
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
        bullet_obj = Instantiate(bullets, this.transform.position, Quaternion.identity);
        bullet_obj.tag = "bullet";
        //bullet_obj.layer = gameObject.layer;
        bullet_obj.layer = LayerMask.NameToLayer("enemy_bullet");
        bullet_obj.transform.position = muzzle[number].transform.position;
        attack.attack = Enemy_Weapon_Status.attack;
        bullet_obj.transform.LookAt(Enemy_Weapon_Status.target.transform.position);


        Enemy_Weapon_Status.cool_time = Enemy_Weapon_Status.cool_const;

    }
    IEnumerator Gatling()
    {
        shot_now = true;
        for (int i = 0; i < muzzle.Length; i++)
        {
            yield return new WaitForSeconds(burst_time);
            C_bullet(i);
        }
        shot_now = false;
    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        Enemy_Weapon_Status = this.GetComponent<enemy_weapon_status>();
        attack = bullets.GetComponent<Attack>();
        //mouse.y = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_Weapon_Status.cool_time > 0)
        {
            Enemy_Weapon_Status.cool_time -= 1;
        }
        if (Enemy_Weapon_Status.target_lock && shot_permission()) {
            StartCoroutine("Gatling");
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

