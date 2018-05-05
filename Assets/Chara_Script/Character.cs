using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public Transform target;
    Vector2 mouse = Vector2.zero;
    public Rigidbody rb;
    public float spinSpeed = 1.0f;
    public float rotation;
    public float radius = 1f;
    public float mouse_save;
    public int boost_style = 0;


    public Vector3 initial_Vec = Vector3.zero;


    public GameObject R_stick_obj;
    private R_Stick_Vec R_stick_vec;

    public GameObject lockon_obj;
    private lockon LockOn;
    // Use this for initialization
    void Start () {
        R_stick_obj = this.gameObject;
        R_stick_vec = R_stick_obj.GetComponent<R_Stick_Vec>();
        LockOn = lockon_obj.GetComponent<lockon>();
        initial_Vec = transform.forward;
    }
    void FixedUpdate()
    {
        mouse = R_stick_vec.stick_vec;

        transform.LookAt(new Vector3(this.transform.position.x + radius * Mathf.Sin((0.5f + mouse.x) * Mathf.PI), this.transform.position.y,
            this.transform.position.z - radius * Mathf.Cos((0.5f + mouse.x) * Mathf.PI)));
    }
        // Update is called once per frame
        void Update () {
        /*
        mouse = R_stick_vec.stick_vec;

        transform.LookAt(new Vector3(this.transform.position.x + radius * Mathf.Sin((0.5f + mouse.x) * Mathf.PI), this.transform.position.y,
            this.transform.position.z - radius * Mathf.Cos((0.5f + mouse.x) * Mathf.PI)));
        */
    }
}
