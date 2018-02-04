using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_over : MonoBehaviour {
    private Player_move P_M;
    public GameObject Player;
    private mouse_Vec M_V;
    //private animation_test A_T;
    public int enemy_number;
    public bool game_clear = false;
    public bool game_over = false;

    public void Game_Over() {

    }
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
        P_M = Player.GetComponent<Player_move>();
        M_V = Player.GetComponent<mouse_Vec>();
        //A_T = Player.GetComponent<animation_test>();
	}

    // Update is called once per frame
    void Update() {
        if (enemy_number <= 0) {
            game_clear = true;
            game_status.game_clear = true;
        }
        if (game_over) {
            game_clear = false;
            game_status.game_clear = false;

        }

        if (game_clear || game_over) {
            P_M.move_lock = true;
            M_V.mouse_lock = true;
            //A_T.animation_lock = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                string key = "ranking";
                if (SceneManager.GetActiveScene().name == "game_world")
                {
                    key = key + "_world";
                    SceneManager.LoadScene(key);
                }
                else if (SceneManager.GetActiveScene().name == "game2_world") {
                    key = key + "2" + "_world"; 
                    SceneManager.LoadScene(key);
                }
            }
        }
	}
}
