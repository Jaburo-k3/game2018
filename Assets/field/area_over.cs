using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class area_over : MonoBehaviour {
    GameObject Player;
    private Game_over game_over;
    public Vector3 terrain_size;
    public Vector3 line_range;
    public Vector3 range;
    public Vector3 start_line_point;
    public Vector3 end_line_point;
    public Vector3 start_point;
    public Vector3 end_point;
    public Text area_over_text;
    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
        start_point = -1 * range;
        end_point = terrain_size + range;
        start_line_point = -1 * line_range;
        end_line_point = terrain_size + line_range;
        game_over = GameObject.Find("Game_manage").GetComponent<Game_over>();
        area_over_text.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(Player.transform.position.x < start_line_point.x || Player.transform.position.z < start_line_point.z ||
            Player.transform.position.x > end_line_point.x || Player.transform.position.z > end_line_point.z)
        {
            area_over_text.enabled = true;
        }
        else
        {
            if (area_over_text.enabled) {
                area_over_text.enabled = false;
            }
        }

        if (Player.transform.position.x < start_point.x || Player.transform.position.z < start_point.z ||
            Player.transform.position.x > end_point.x || Player.transform.position.z > end_point.z) {
            game_over.game_over = true;
        }
	}
}
