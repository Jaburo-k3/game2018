using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_type2 : MonoBehaviour {
    private enemy_weapon_status Enemy_weapon_status;
    public GameObject target_obj;
    public Transform target;
    float rotation_speed = 5f;
    public bool move_permission = true;
    Vector3 destination;
    NavMeshAgent agent;


    void Start()
    {
        target = target_obj.transform;
        agent = GetComponent<NavMeshAgent>();
        Enemy_weapon_status = this.GetComponent<enemy_weapon_status>();
        destination = agent.destination;
        agent.speed = 20f;
    }

    void Update()
    {
        Vector3 target_distance = target_obj.transform.position;
        target_distance.y = transform.position.y;
        if (Vector3.Distance(transform.position, target_distance) > 10)
        {

            //Debug.Log(true);
            if (move_permission)
            {
                destination = target.position;
                destination.y = transform.position.y;
                agent.destination = destination;
                Enemy_weapon_status.target_lock = false;
                agent.isStopped = false;
            }
            
        }

        else {

            //transform.LookAt(new Vector3(target_obj.transform.position.x,transform.position.y,target_obj.transform.position.z));
            agent.isStopped = true;
            Vector3 target_angle = target_obj.transform.position;
            target_angle.y = transform.position.y;
            target_angle = target_angle - transform.position;
            target_angle.Normalize();
            float angle = Vector3.Dot(transform.forward, target_angle);
            if (angle >= 0.75 && angle <= 1.0f)
            {
                //弾を撃つ
                Enemy_weapon_status.target_lock = true;
                Enemy_weapon_status.target = target.transform.FindChild("center_point").gameObject;
            }
            else {
                Enemy_weapon_status.target_lock = false;
            }

            
            Vector3 relativePos = target_obj.transform.position - transform.position;
            relativePos.y = transform.position.y;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotation_speed);
            
            //Debug.Log(angle);
        }
    }
}