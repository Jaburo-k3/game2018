using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_cannon : MonoBehaviour {
    private enemy_weapon_status Enemy_Weapon_Status;
    private GameObject parent;
    public GameObject bullet;
    public GameObject bullet_obj;

    float rotation = 25;
    //Vector3 rotation;

    public float shot_time;
    public bool shot_now = false;

    public GameObject[] muzzle;
    private Attack attack;

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
    IEnumerator C_bullet()
    {
        yield return new WaitForSeconds(shot_time);
        for (int i = 0; i < muzzle.Length; i++)
        {
            bullet_obj = Instantiate(bullet, this.transform.position, Quaternion.identity);
            bullet_obj.tag = "bullet";
            //bullet_obj.layer = gameObject.layer;
            bullet_obj.layer = LayerMask.NameToLayer("enemy_bullet");
            bullet_obj.transform.position = muzzle[i].transform.position;
            bullet_obj.transform.LookAt(Enemy_Weapon_Status.target.transform.position);
            attack.attack = Enemy_Weapon_Status.attack;

            Enemy_Weapon_Status.cool_time = Enemy_Weapon_Status.cool_const;

        }
        shot_now = false;

    }
    // Use this for initialization
    void Start()
    {
        parent = transform.root.gameObject;
        Enemy_Weapon_Status = this.GetComponent<enemy_weapon_status>();
        attack = bullet.GetComponent<Attack>();
        //rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_Weapon_Status.cool_time > 0)
        {
            Enemy_Weapon_Status.cool_time -= 1;
        }
        if (Enemy_Weapon_Status.target_lock && shot_permission()) {
            shot_now = true;
            StartCoroutine("C_bullet");
        }
    }
}