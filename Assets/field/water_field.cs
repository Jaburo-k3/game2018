using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_field : MonoBehaviour {
    private water_UI Water_UI;
    private chara_status Chara_Status;
    public GameObject Water_UI_obj;
    public int in_water_counter;
    private GameObject camera_obj;

    private BoxCollider boxcollider;
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Player_move player_move = other.GetComponent<Player_move>();
            Chara_Status = other.GetComponent<chara_status>();
            player_move.Chara_Status.Terrain_State = "Water";
            camera_obj = Chara_Status.camera_obj;
            player_move.status_update();
            in_water_counter += 1;
            Debug.Log(player_move.Chara_Status.Terrain_State);
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player_move player_move = other.GetComponent<Player_move>();
            player_move.Chara_Status.Terrain_State = "Normal";
            player_move.status_update();
            //Water_UI.water_flame_enable(false);
            in_water_counter -= 1;

            Debug.Log(player_move.Chara_Status.Terrain_State);
        }

    }
    // Use this for initialization
    void Start () {
        Water_UI = Water_UI_obj.GetComponent<water_UI>();
        boxcollider = this.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (camera_obj != null)
        {
            if ((camera_obj.transform.position.y < transform.position.y + (boxcollider.size.y * transform.localScale.y) / 2) &&
                (camera_obj.transform.position.y > transform.position.y - (boxcollider.size.y * transform.localScale.y) / 2))
            {
                Water_UI.water_flame_enable(true);
            }
            else {
                Debug.Log("trans" + transform.localPosition.y);
                Debug.Log("collider" + (boxcollider.size.y * transform.localScale.y) / 2);
                Debug.Log(transform.position.y + (boxcollider.size.y * transform.localScale.y) / 2);
                Water_UI.water_flame_enable(false);
            }
           
        }
	}
}
