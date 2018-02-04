using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_beam : MonoBehaviour {
    public Vector3 target;
    public Vector3 save_pos;
    public Vector3 destroy_pos;
    Rigidbody rb;
    public float radius = 0.5f;
    LineRenderer lineRender;
    private float speed = 400f;
    private Vector3 bullet_speed;
    private List<GameObject> Child_obj;
    public GameObject Hit_Marker_obj;

    public List<GameObject> hited_obj;

    private hit_marker Hit_Marker;

    private create_hit_marker C_Hit_Marker;
    public bool permission_hit_marker;

    private Attack attack;

    private SphereCollider Sphcollider;

    private Ray ray;
    public RaycastHit hit;
    public LayerMask ignore_layer;
    public LayerMask test;

    private HP Hp;

    public GameObject laser_obj;
    private Vector3 firing_pos;

    private AudioSource AudioSource;
    public AudioClip Bullet_sound;

    public int flame = 0;
    public int count = 0;
    bool Do = false;
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "object")
        {
            this.transform.DetachChildren();
            Destroy(this.gameObject);
        }
    }
    */

    Vector3 intersection(Vector3 start_pos,Vector3 hit_obj_point) {
        //A = 発射地点
        //B = 現在位置
        //X = 衝突したオブジェクトの位置

        Vector3 normalize =  Vector3.Normalize(transform.position - start_pos);
        //AXの距離 = ABの単位ベクトル ・ ベクトルAP
        float hit_distance = Vector3.Dot(normalize, hit_obj_point - start_pos);
        //点X =　点A + (ABの単位ベクトル * AXの距離)
        Vector3 intersec_pos = start_pos + (normalize * Vector3.Distance(hit_obj_point, start_pos));
        //Debug.Log(intersec_pos);
        return intersec_pos;
    }
    //■■■前フレームの位置と現在の位置の間にオブジェクトがないか探す■■■
    void obj_search(Vector3 pos)
    {
        count += 1;
        //Debug.Log(transform.position);
        //Debug.Log(pos);   
        ray = new Ray(pos, transform.forward);//前フレームの位置から現在向いてる向きにRayを飛ばす
        float ray_distance = Vector3.Distance(transform.position, pos);//Rayの距離を計算
        //オブジェクトがないか探す
        if (Physics.SphereCast(ray, Sphcollider.radius, out hit, ray_distance, ignore_layer))
        {
            if (hit.collider.tag == "enemy" || hit.collider.tag == "Player")
            {

                GameObject parent = hit.transform.root.gameObject;
                bool test = true;
                if (parent == null)
                {
                    parent = hit.collider.gameObject;
                }

                /*
                if (hited_obj.Contains(parent))
                {
                    Debug.Log("重複");
                }
                else {
                    Debug.Log(parent.name);
                    //Debug.Log(hited_obj);
                    Hp = parent.gameObject.GetComponent<HP>();
                    Hp.set_hp(Hp.get_hp() - attack.attack);
                    hited_obj.Add(parent);
                }
                */
                for (int i = 0; i < hited_obj.Count; i++) {
                    //Debug.Log(hited_obj[i]);
                    if (parent == hited_obj[i]) {
                        //Debug.Log(hited_obj[i]);
                        test = false;
                    }
                }
                if (test) {
                    Debug.Log(pos + parent.name + "  flame = " + flame);
                    Debug.Log("count = " + count);
                    
                    if (hited_obj.Count > 0) {
                        //Debug.Log("リスト = " + hited_obj[0]);
                    }
                    Hp = parent.gameObject.GetComponent<HP>();
                    Hp.set_hp(Hp.get_hp() - attack.attack);
                    hited_obj.Add(parent);
                    //Debug.Log("リスト = " + hited_obj[0]);
                }
                //ヒットマーカーの作成
                if (permission_hit_marker)
                {
                    C_Hit_Marker.create(hit.point);
                }
                AudioSource.Play();
                Vector3 Pos = intersection(pos, parent.transform.position);
                obj_search(Pos);
            }
            else if(hit.collider.tag == "object"){
                transform.position = hit.point;
                destroy_pos = hit.point;//オブジェクトと衝突した位置
                Debug.Log("建物 = " + hit.collider.name);
                //親子関係解除
                transform.DetachChildren();
                Destroy(this.gameObject);
            }

        }
    }
    //オブジェクトの大きさ設定
    void laser_obj_size_update() {
        Vector3 pos = (transform.position - firing_pos) / 2;
        laser_obj.transform.position = firing_pos + pos;

        float scale_y = Vector3.Distance(transform.position, firing_pos) /2;
        Vector3 scale = new Vector3(laser_obj.transform.localScale.x,scale_y ,laser_obj.transform.localScale.z);
        laser_obj.transform.localScale = scale;
    }
    // Use this for initialization
    void Start()
    {
        //■■■スクリプトにアクセス■■■
        rb = GetComponent<Rigidbody>();

        C_Hit_Marker = this.gameObject.GetComponent<create_hit_marker>();

        Sphcollider = this.gameObject.GetComponent<SphereCollider>();

        attack = this.GetComponent<Attack>();

        //■■■Rayで無視するlayerの設定■■■
        if (this.gameObject.layer == LayerMask.NameToLayer("enemy_bullet"))
        {
            ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("enemy") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
        }
        else {
            ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("Ignore Raycast"));
        }
        //■■■初期発射位置を記憶■■■
        save_pos = transform.position;
        firing_pos = transform.position;
        //■■■効果音の設定■■■
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Bullet_sound;

        laser_obj = transform.FindChild("laser_obj").gameObject;

        //■■■弾に力を加える■■■
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        laser_obj_size_update();
    }

    // Update is called once per frame
    void Update()
    {
        destroy_pos = transform.position;
        count = 0;
        obj_search(save_pos);
        laser_obj_size_update();
        save_pos = transform.position;
        flame += 1;
    }
}
