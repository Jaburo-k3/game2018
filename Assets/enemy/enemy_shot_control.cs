using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_shot_control : MonoBehaviour {
    public enemy_weapon_status[] E_W_status = new enemy_weapon_status[2];
    public GameObject enemy;
    public GameObject E_center_point;
    public GameObject target;
    public GameObject center_point;

    public bool shot_permission = false;

    public Vector3 target_speed;
    public Vector3 target_save_pos;
    public float targer_distance;
    public Vector3 target_after_pos;

    private Ray ray;
    public RaycastHit hit;
    public LayerMask ignore_layer;

    private weapon_switching W_switching;

    private HP hp;
    public Vector3 set_shot_pos(Vector3 gun_pos, float bullet_speed, float bullet_mass) {

        target_speed = center_point.transform.position - target_save_pos;
        targer_distance = Vector3.Distance(gun_pos, center_point.transform.position);

        return target_after_pos = center_point.transform.position + target_speed * targer_distance
            / (bullet_speed / bullet_mass) / Time.deltaTime;
    }
    void Awake() {
        target = GameObject.Find("Player");
        W_switching = this.GetComponent<weapon_switching>();
        hp = target.GetComponent<HP>();
    }
    // Use this for initialization
    void Start () {
        ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("enemy") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
        center_point = target.transform.FindChild("center_point").gameObject;
        E_center_point = enemy.transform.FindChild("center_point").gameObject;
    }
    void FixedUpdate() {
        Vector3 ray_vec = center_point.transform.position - E_center_point.transform.position;
        ray = new Ray(E_center_point.transform.position, ray_vec);//前フレームの位置から現在向いてる向きにRayを飛ばす
        float ray_distance = Vector3.Distance(E_center_point.transform.position, center_point.transform.position);//Rayの距離を計算
        Physics.Raycast(ray, out hit, ray_distance, ignore_layer);
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < 75 && distance > 50) {
            W_switching.weapon_number[0] = 1;
            W_switching.weapon_number[1] = 1;
        }
        else if (distance < 50) {
            W_switching.weapon_number[0] = 0;
            W_switching.weapon_number[1] = 0;
        }
        if (distance < 100 && hit.collider.tag == "Player" && hp.get_hp() > 0)
        {
            shot_permission = true;
        }
        else {
            shot_permission = false;
        }
        target_save_pos = center_point.transform.position;
    }
    // Update is called once per frame
    void Update () {
    }
}
