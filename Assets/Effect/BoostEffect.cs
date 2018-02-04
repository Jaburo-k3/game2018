using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : MonoBehaviour {

    public GameObject boostLight;
    public GameObject boostLight_Lleg;
    public GameObject boostLight_Rleg;
    public GameObject boostLight_Lbackpack;
    public GameObject boostLight_Rbackpack;
    private Player_move player_move;

    private GameObject player;
	// Use this for initialization
	void Start () {
        boostLight.SetActive(false);
        boostLight_Lleg.SetActive(false);
        boostLight_Rleg.SetActive(false);
        boostLight_Lbackpack.SetActive(false);
        boostLight_Rbackpack.SetActive(false);
        player = GameObject.Find("Player");
        player_move = player.GetComponent<Player_move>();
	}
	
	// Update is called once per frame
	void Update () {
        bool Boost = false;

        if (player_move.get_boost_style() != null) {
            Boost = true;
        }
        boostLight.SetActive(Boost);
        boostLight_Lleg.SetActive(Boost);
        boostLight_Rleg.SetActive(Boost);
        boostLight_Lbackpack.SetActive(Boost);
        boostLight_Rbackpack.SetActive(Boost);
    }
}
