using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_moves : MonoBehaviour {
    Rigidbody rb;
    public GameObject L_stick_obj;
    private L_Stick_Vec L_stick_vec;

    public Vector2 stick_vec;

    public float speed = 1.0f;
	// Use this for initialization
	void Start () {
        L_stick_obj = this.gameObject;
        L_stick_vec = L_stick_obj.GetComponent<L_Stick_Vec>();
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        stick_vec = L_stick_vec.stick_vec;
        if (stick_vec.y >= 1.0) {
            rb.velocity = new Vector3(speed * Mathf.Sin((stick_vec.x) * Mathf.PI), 0, speed * Mathf.Cos((stick_vec.x) * Mathf.PI));
        }
	}
}
