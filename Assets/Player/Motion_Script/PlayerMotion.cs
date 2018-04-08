using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour {
    private Animator animator;
    private Player_move player_move;
    private chara_status Chara_status;
    public GameObject[] L_arm;
    public GameObject[] R_arm;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        player_move = this.gameObject.GetComponent<Player_move>();
        Chara_status = this.GetComponent<chara_status>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Chara_status.moving_state[0] == "forward")
        {
            animator.SetInteger("Horizontal", 1);
            animator.SetInteger("Vertical", 0);
        }
        else if (Chara_status.moving_state[0] == "back")
        {
            animator.SetInteger("Horizontal", 2);
            animator.SetInteger("Vertical", 0);
            animator.SetBool("Jump", false);
        }
        else if (Chara_status.moving_state[0] == "left")
        {
            animator.SetInteger("Vertical", 2);
            animator.SetInteger("Horizontal", 0);
            animator.SetBool("Jump", false);
        }
        else if (Chara_status.moving_state[0] == "right")
        {
            animator.SetInteger("Vertical", 1);
            animator.SetInteger("Horizontal", 0);
            animator.SetBool("Jump", false);
        }
        else if (Chara_status.moving_state[1] == "rising") {
            animator.SetInteger("Horizontal", 0);
            animator.SetInteger("Vertical", 0);
            animator.SetBool("Jump",true);
        }
        else if (Chara_status.moving_state[0] == null)
        {
            animator.SetInteger("Horizontal", 0);
            animator.SetInteger("Vertical", 0);
            animator.SetBool("Jump", false);
        }

        //ブースト
        if (Chara_status.moving_state[1] == "boost")
        {
            animator.SetBool("Boost", true);
        }
        else {
            animator.SetBool("Boost", false);
        }
    }
    void LateUpdate() {
        L_arm[0].transform.localRotation = new Quaternion(R_arm[0].transform.localRotation.x, R_arm[0].transform.localRotation.y, R_arm[0].transform.localRotation.z * -1f, R_arm[0].transform.localRotation.w);
        L_arm[1].transform.localRotation = R_arm[1].transform.localRotation;
        L_arm[2].transform.localRotation = new Quaternion(R_arm[2].transform.localRotation.x, R_arm[2].transform.localRotation.y * -1, R_arm[2].transform.localRotation.z, R_arm[2].transform.localRotation.w);
        L_arm[3].transform.localRotation = R_arm[3].transform.localRotation;
        //arm.transform.localRotation = Quaternion.Euler(-10.0f, 0.0f, 0.0f);
    }
}
