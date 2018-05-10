using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Missile_Pod : MonoBehaviour {
    private enemy_weapon_status Enemy_Weapon_Status;
    private GameObject parent;
    public GameObject missile;
    public GameObject missile_obj;
    public float delay;


    private int cool_time;
    private int cool_const = 10;

    public GameObject[] muzzle;
    private Attack attack;

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
    //
    void C_Missile(int number)
    {
        missile_obj = Instantiate(missile, this.transform.position, Quaternion.identity);
        missile_obj.tag = "bullet";
        //missile_obj.layer = gameObject.layer;
        missile_obj.layer = LayerMask.NameToLayer("enemy_bullet");
        missile_obj.transform.position = muzzle[number].transform.position;
        missile_obj.transform.LookAt(Enemy_Weapon_Status.target.transform.position);
        attack.attack = Enemy_Weapon_Status.attack;

        Enemy_Weapon_Status.cool_time = Enemy_Weapon_Status.cool_const;

    }
    IEnumerator Missile()
    {
        shot_now = true;
        C_Missile(0);
        for (int i = 1; i < muzzle.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            C_Missile(i);
        }
        shot_now = false;
    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        Enemy_Weapon_Status = this.GetComponent<enemy_weapon_status>();
        attack = missile.GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_Weapon_Status.cool_time > 0 && shot_now == false)
        {
            Enemy_Weapon_Status.cool_time -= 1;
        }
        //発射
        if (Enemy_Weapon_Status.target_lock && shot_permission()) {
            StartCoroutine("Missile");
        }
    }
}
