using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Lowest_P : MonoBehaviour {
    private GameObject parent;
    private Player_move P_M;
	// Use this for initialization
	void Start () {

        parent = transform.root.gameObject;
        //Debug.Log(transform.position.y);
        P_M = parent.GetComponent<Player_move>();
        P_M.L_P = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        P_M.L_P = transform.position;
    }
}
