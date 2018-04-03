using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_create_bullet : MonoBehaviour {
    public float attack;

    public GameObject bullets;
    public GameObject bullet_obj;

    private GameObject parent;

    public GameObject gun;
    public Vector3 gun_pos;

    private Attack Attack;

    public float spinSpeed = 1.0f;
    public float bullet_speed = 400f;
    private float bullet_mass;
    private bullet Bullet;
    private bullet_status Bullet_status;
    public GameObject target;
    public GameObject center_point;
    public Vector3 target_speed;
    public Vector3 target_save_pos;
    public float targer_distance;
    public Vector3 target_after_pos;


    Ray ray;
    RaycastHit hit;
    public bool shot_permission = true;
    private bool target_on = false;
    private float cool_time = 0.3f;
    void OnTriggerExit(Collider other) {
        target_on = false;
    }
    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            if (target != null)
            {
                target = other.transform.FindChild("center_point").gameObject;
                //Debug.Log(other.name);
                target_on = true;


                target_speed = target.transform.position - target_save_pos;
                target_save_pos = target.transform.position;
                targer_distance = Vector3.Distance(gun_pos, target.transform.position);

                target_after_pos = target.transform.position + target_speed * targer_distance
                    / (bullet_speed / bullet_mass) / Time.deltaTime;

                //Debug.Log("now" + other.transform.position);
                //Debug.Log("after" + target_after_pos);
            }
            else {
                target = other.transform.FindChild("center_point").gameObject;
                target_on = true;
            }
        }
    }
    IEnumerator create_bullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(cool_time);
            if (target_on && shot_permission)
            {
                gun_pos = gun.transform.position;
                Vector3 ray_vec = target.transform.position - gun_pos;
                ray = new Ray(gun_pos, ray_vec);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity) && 
                    hit.collider.gameObject.tag == "Player")
                {
                    //Debug.Log("now" + target.transform.position);
                    //Debug.Log("after" + target_after_pos);

                    bullet_obj = Instantiate(bullets, gun_pos, Quaternion.identity);
                    Bullet_status = bullet_obj.GetComponent<bullet_status>();
                    Bullet_status.bullet_speed = bullet_speed;
                    //bullet_obj.transform.position = transform.position;
                    bullet_obj.transform.LookAt(target_after_pos);
                    bullet_obj.layer = parent.layer;
                    Bullet = bullet_obj.GetComponent<bullet>();
                    Attack.attack = attack;
                    Bullet.target = target_after_pos;
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
        parent = transform.root.gameObject;
        bullet_mass = bullets.GetComponent<Rigidbody>().mass;

        Attack = bullets.GetComponent<Attack>();
        //Debug.Log(bullet_mass);
        StartCoroutine("create_bullet");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
