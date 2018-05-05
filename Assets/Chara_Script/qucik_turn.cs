using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qucik_turn : MonoBehaviour {
    private L_Stick_Vec L_stick_vec;
    private R_Stick_Vec R_stick_vec;
    private chara_status Chara_status;
    float turn_time = 0.25f;
    bool turn_now = false;
    string turn_direction;
    float turn_value;


    GameObject camera_obj;

    string R_stick_direction() {
        if (Input.GetAxis("Horizontal2") * -1 > 0)
        {
            return "left";
        }
        else{
            return "right";
        }
    }

    IEnumerator quick_turn_move() {
        if (turn_direction == "left")
        {
            turn_value = R_stick_vec.stick_vec.x + 1f;
        }
        else {
            turn_value = R_stick_vec.stick_vec.x - 1f;
        }
        turn_now = true;
        Chara_status.move_lock = true;
        R_stick_vec.stick_lock = true;
        yield return new WaitForSeconds(turn_time);
        turn_now = false;
        Chara_status.move_lock = false;
        R_stick_vec.stick_lock = false;
        R_stick_vec.stick_vec.x = turn_value;
    }
    void FixedUpdate() {
    }
    // Use this for initialization
    void Start () {
        L_stick_vec = this.GetComponent<L_Stick_Vec>();
        R_stick_vec = this.GetComponent<R_Stick_Vec>();
        Chara_status = this.GetComponent<chara_status>();
        //camera_obj = this.transform.FindChild("Camera").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.SqrMagnitude(L_stick_vec.stick_vec) < 0.1f && Mathf.Abs(Input.GetAxis("Horizontal2")) != 0.0f
            && Input.GetButtonDown("button7") && !turn_now) {
            turn_direction = R_stick_direction();
            StartCoroutine("quick_turn_move",R_stick_direction());
        }
        if (turn_now)
        {
            if (turn_direction == "left")
            {
                R_stick_vec.stick_vec.x += 1f * (Time.deltaTime / turn_time);
                if (R_stick_vec.stick_vec.x > turn_value) {
                    R_stick_vec.stick_vec.x = turn_value;
                }
            }
            else {
                R_stick_vec.stick_vec.x -= 1f * (Time.deltaTime / turn_time);
                if (R_stick_vec.stick_vec.x < turn_value)
                {
                    R_stick_vec.stick_vec.x = turn_value;
                }
            }
        }
    }
}
