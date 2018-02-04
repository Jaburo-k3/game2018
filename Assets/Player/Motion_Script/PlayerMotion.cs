using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour {
    private Animator animator;
    private Player_move player_move;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        player_move = this.gameObject.GetComponent<Player_move>();
	}
	
	// Update is called once per frame
	void Update () { 
        if (player_move.GetInput() == "forward" && player_move.move_lock == false)
        {
            animator.SetInteger("Horizontal", 1);
        }
        else if (player_move.GetInput() == "back" && player_move.move_lock == false)
        {
            animator.SetInteger("Horizontal", 2);
        }
        else
        {
            animator.SetInteger("Horizontal", 0);
        }


        if (player_move.GetInput() == "right" && player_move.move_lock == false)
        {
            animator.SetInteger("Vertical", 1);
        }
        else if (player_move.GetInput() == "left" && player_move.move_lock == false)
        {
            animator.SetInteger("Vertical", 2);
        }
        else
        {
            animator.SetInteger("Vertical", 0);
        }

        //ジャンプモーション
        if (player_move.move_lock == false)
        {
            animator.SetBool("Jump", Input.GetKey(KeyCode.Space));
        }
        else {
            animator.SetBool("Jump", false);
        }
        //ブースト
        if (player_move.get_boost_style() != null && player_move.move_lock == false)
        {
            animator.SetBool("Boost", true);
        }
        else
        {
            animator.SetBool("Boost", false);
        }

	}
}
