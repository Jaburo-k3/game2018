using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_move : MonoBehaviour {
    public Transform target;
    public float spinSpeed = 1.0f;

    Vector3 nowPos;
    Vector3 pos = Vector3.zero;
    Vector2 mouse = Vector2.zero;
    // Use this for initialization
    void Start()
    {
        nowPos = transform.position;

        /*
        if (target = null) {
            target = GameObject.FindWithTag("object").transform;
            Debug.Log("not");
        }
        */

        mouse.y = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        mouse += new Vector2(Input.GetAxis("Mouse X") * -1, 0) * Time.deltaTime * spinSpeed;

        mouse.y = Mathf.Clamp(mouse.y, -0.3f + 0.5f, 0.3f + 0.5f);

        pos.x = Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Cos(mouse.x * Mathf.PI);
        //pos.y = Mathf.Cos(mouse.y * Mathf.PI);
        pos.z = Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Sin(mouse.x * Mathf.PI);

        pos *= nowPos.z;

        pos.y = 0;

        transform.position = pos + target.position;
        transform.LookAt(target.position);
        //Debug.Log(mouse.y);

    }
}