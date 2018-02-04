using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_moves : MonoBehaviour {
    Rigidbody rb;
    public GameObject stick_obj;
    private L_Stick_Vec L_stick_vec;
    private R_Stick_Vec R_stick_vec;
    public Vector2 stick_vec;
    public GameObject Chara_obj;
    public GameObject Camera_obj;
    public float speed = 20f;
	// Use this for initialization
	void Start () {
        stick_obj = this.gameObject;
        L_stick_vec = stick_obj.GetComponent<L_Stick_Vec>();
        R_stick_vec = stick_obj.GetComponent<R_Stick_Vec>();
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Vector2.SqrMagnitude(L_stick_vec.stick_vec) > 0.1f)
        {
            Vector3 vec = this.gameObject.transform.position - Camera_obj.transform.position;
            vec.y = 0f;
            vec = vec.normalized;
            rb.velocity = speed * vec * L_stick_vec.stick_vec.y + speed * Vector3.Cross(vec, Vector3.up) * L_stick_vec.stick_vec.x;
        }
        
    }

}
