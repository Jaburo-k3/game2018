using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_test : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hit;
    public Vector3 target;
    public Transform Player_object;
    float radius = 1;
    Vector3 Vec = Vector3.zero;
    void Start()
    {
        ray = new Ray(transform.position, transform.forward);
    }
    void Update()
    {
        ray = new Ray(new Vector3(Player_object.position.x, Player_object.position.y + 1, Player_object.position.z), transform.forward);
        if (Physics.SphereCast(ray,0.01f, out hit, Mathf.Infinity))
        {
            target = hit.point;
            Debug.Log(hit.transform);
        }
        //Debug.Log(hit.collider.transform.position);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 0, false);
    }
}
