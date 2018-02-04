using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_camera : MonoBehaviour {
    public Transform target_object;
    public Vector3 target_pos;
    public float spinSpeed = 1.0f;
    public float radius;
    Vector3 nowPos;
    Vector3 pos = Vector3.zero;
    public Vector2 mouse = Vector2.zero;
    private float Clamp_S = -0.5f;//マウスy軸の最低値
    private float Clamp_E = 0.5f;//マウスy軸の最高値 
    // Use this for initialization
    void Start () {
        nowPos = transform.position;
        radius = Vector3.Distance(nowPos, target_object.position);
        //mouse.y = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        mouse += new Vector2(0, Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;

        mouse.y = Mathf.Clamp(mouse.y, Clamp_S, Clamp_E);
        //nowPos.y = radius* Mathf.Cos(mouse.y * Mathf.PI);
        transform.position = new Vector3(transform.position.x, target_object.position.y + radius *Mathf.Sin(mouse.y * Mathf.PI), transform.position.z);
        transform.LookAt(new Vector3(target_object.position.x, target_object.position.y + 1, target_object.position.z));

    }
}
