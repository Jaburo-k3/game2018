using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar_Compass : MonoBehaviour {
    public Image Compass;
    public GameObject Player;
	// Use this for initialization
	void Start () {
        Compass = this.GetComponent<Image>();
        Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Compass.transform.rotation = Quaternion.Euler(Compass.transform.rotation.x,
            Compass.transform.rotation.y, Player.transform.eulerAngles.y);
	}
}
