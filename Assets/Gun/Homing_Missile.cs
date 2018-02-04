using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_Missile : MonoBehaviour {
    public GameObject target;    // ロックオンしたターゲット
    private float speed = 20.0f;    
    private float rotSpeed = 180.0f;  // 1秒間に回転する角度


    public Vector3 nowPos;
    public Vector3 pos = Vector3.zero;
    public Vector3 camera_Vec = Vector3.zero;
    public Vector3 save_pos;
    Rigidbody rb;
    public float radius = 0.5f;
    LineRenderer lineRender;

    private GameObject Child_obj;
    GameObject hit_parent;

    public GameObject blast_objs;
    public GameObject blast_obj;
    public Vector3 blast_scale;
    private blast Blast;

    private Attack attack;
    private Attack blast_attack;

    private CapsuleCollider Capcollider;

    private Ray ray;
    public RaycastHit hit;
    public LayerMask ignore_layer;
    Vector3 ray_vec;
    private HP Hp;

    public GameObject Hit_Marker_obj;
    private create_hit_marker C_Hit_Marker;

    private create_hit_marker blast_C_Hit_Marker;
    public bool permission_hit_marker;
    void create_hit_marker()
    {
        Hit_Marker_obj = Instantiate(Hit_Marker_obj, this.transform.position, Quaternion.identity);
        Hit_Marker_obj.transform.position = hit.point;

    }

    void create_blast(Vector3 point)
    {
        blast_obj = Instantiate(blast_objs, this.transform.position, Quaternion.identity);
        blast_obj.tag = "bullet";
        blast_obj.layer = gameObject.layer;
        blast_obj.transform.position = point;
        blast_obj.transform.localScale = blast_scale;
        Blast = blast_obj.GetComponent<blast>();
        blast_attack = Blast.GetComponent<Attack>();
        blast_attack.attack = attack.attack / 6;
        Blast.Hit_obj = hit_parent;
        Blast.permission_hit_marker = permission_hit_marker;

        blast_C_Hit_Marker = blast_obj.GetComponent<create_hit_marker>();
        blast_C_Hit_Marker.parent = C_Hit_Marker.parent;

        save_pos = transform.position;
    }
    void obj_search()
    {
        ray = new Ray(save_pos, transform.forward);
        float ray_distance = Vector3.Distance(transform.position, save_pos);
        if (Physics.SphereCast(ray, Capcollider.radius, out hit, ray_distance, ignore_layer))
        {
            if (hit.collider.tag == "enemy" || hit.collider.tag == "Player")
            {
                hit_parent = hit.transform.root.gameObject;
                if (hit_parent == null)
                {
                    hit_parent = hit.collider.gameObject;
                }
                Hp = hit_parent.gameObject.GetComponent<HP>();
                Hp.set_hp(Hp.get_hp() - attack.attack);
                if (permission_hit_marker)
                {
                    C_Hit_Marker.create(hit.point);
                }
            }
            //Debug.Log(hit.collider.name);
            Destroy(Child_obj.gameObject);
            create_blast(hit.point);
            Destroy(this.gameObject);

        }
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }
    // Use this for initialization
    void Start()
    {

        Child_obj = transform.FindChild("Cylinder").gameObject;

        rb = GetComponent<Rigidbody>();

        C_Hit_Marker = this.gameObject.GetComponent<create_hit_marker>();

        Capcollider = this.gameObject.GetComponent<CapsuleCollider>();

        attack = this.GetComponent<Attack>();

        nowPos = transform.position;

        save_pos = transform.position;

        ray_vec = transform.forward;

        ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("Ignore Raycast"));
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // ターゲットまでの角度を取得
            Vector3 vecTarget = target.transform.position - transform.position; // ターゲットへのベクトル
            Vector3 vecForward = transform.TransformDirection(Vector3.forward);   // 弾の正面ベクトル
            float angleDiff = Vector3.Angle(vecForward, vecTarget);            // ターゲットまでの角度
            float angleAdd = (rotSpeed * Time.deltaTime);                    // 回転角
            Quaternion rotTarget = Quaternion.LookRotation(vecTarget);              // ターゲットへ向けるクォータニオン
            if (angleDiff <= angleAdd)
            {
                // ターゲットが回転角以内なら完全にターゲットの方を向く
                transform.rotation = rotTarget;
            }
            else
            {
                // ターゲットが回転角の外なら、指定角度だけターゲットに向ける
                float t = (angleAdd / angleDiff);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, t);
            }
        }
        /*
        // 前進
        transform.position += transform.TransformDirection(Vector3.forward) * _speed * Time.deltaTime;
        */
        rb.velocity = rb.velocity / 10;
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        obj_search();
        save_pos = transform.position;
    }
}
