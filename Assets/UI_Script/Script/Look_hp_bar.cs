using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_hp_bar : MonoBehaviour {
    GameObject Player;
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
	}
}
