using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
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

    private AudioSource AudioSource;
    public AudioClip Bullet_sound;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "object") {
            this.transform.DetachChildren();
            Destroy(this.gameObject);
        }
        /*
        if (other.tag == "enemy" || other.tag == "Player")
        {
            AudioSource.Play();
            GameObject parent = other.transform.root.gameObject;
            if (parent == null)
            {
                parent = other.gameObject;
            }
            Hp = parent.gameObject.GetComponent<HP>();
            Hp.set_hp(Hp.get_hp() - attack.attack);
            //ヒットマーカーの作成
            if (permission_hit_marker)
            {
                C_Hit_Marker.create(hit.point);
            }
            //AudioSource.Play();
        }
        */
        destroy_pos = other.transform.position;
    }
    /*
    private void OnTriggerStay(Collider other) {
        if (other.tag == "enemy" || other.tag == "Player")
        {
            AudioSource.Play();
            GameObject parent = other.transform.root.gameObject;
            if (parent == null)
            {
                parent = other.gameObject;
            }
            Hp = parent.gameObject.GetComponent<HP>();
            Hp.set_hp(Hp.get_hp() - attack.attack);
            //ヒットマーカーの作成
            if (permission_hit_marker)
            {
                C_Hit_Marker.create(hit.point);
            }
            //AudioSource.Play();
        }
        destroy_pos = other.transform.position;
        //オブジェクトと衝突した位置
        //子オブジェクトを探し親子関係を探し衝突した位置に移動
        foreach (Transform child in transform)
        {
            child.transform.position = destroy_pos;
        }
        //親子関係解除
        transform.DetachChildren();



        Destroy(this.gameObject);
        Debug.Log("hit");
    }
    */

    //■■■前フレームの位置と現在の位置の間にオブジェクトがないか探す■■■
    void obj_search()
    {

        ray = new Ray(save_pos, transform.forward);//前フレームの位置から現在向いてる向きにRayを飛ばす
        float ray_distance = Vector3.Distance(transform.position, save_pos);//Rayの距離を計算
        //オブジェクトがないか探す
        if (Physics.SphereCast(ray, Sphcollider.radius, out hit, ray_distance, ignore_layer))
        {
            if (hit.collider.tag == "enemy" || hit.collider.tag == "Player")
            {
                Debug.Log("ray");
                AudioSource.Play();
                GameObject parent = hit.transform.root.gameObject;
                if (parent == null)
                {
                    parent = hit.collider.gameObject;
                }
                Hp = parent.gameObject.GetComponent<HP>();
                Hp.set_hp(Hp.get_hp() - attack.attack);
                //ヒットマーカーの作成
                if (permission_hit_marker) {
                    C_Hit_Marker.create(hit.point);
                }
                //AudioSource.Play();
            }
            destroy_pos = hit.point;//オブジェクトと衝突した位置

            Debug.Log(hit.collider.name);
            //子オブジェクトを探し親子関係を探し衝突した位置に移動
            foreach (Transform child in transform)
            {
                child.transform.position = destroy_pos;
            }
            //親子関係解除
            transform.DetachChildren();



            Destroy(this.gameObject);

        }
    }

    // Use this for initialization
    void Start () {
        //■■■スクリプトにアクセス■■■
        rb = GetComponent<Rigidbody>();

        C_Hit_Marker = this.gameObject.GetComponent<create_hit_marker>();

        Sphcollider = this.gameObject.GetComponent<SphereCollider>();

        attack = this.GetComponent<Attack>();

        speed = this.GetComponent<bullet_status>().bullet_speed;

        //■■■Rayで無視するlayerの設定■■■
        if (this.gameObject.layer == LayerMask.NameToLayer("enemy_bullet")){
            ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("enemy") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
        }
        else {
            ignore_layer = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("Ignore Raycast"));
        }
        //■■■初期発射位置を記憶■■■
        save_pos = transform.position;

        //■■■効果音の設定■■■
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = Bullet_sound;

        //■■■弾に力を加える■■■
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
        obj_search();
        save_pos = transform.position;
    }
}
