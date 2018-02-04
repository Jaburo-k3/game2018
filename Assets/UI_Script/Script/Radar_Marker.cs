using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar_Marker : MonoBehaviour {
    Image marker;
    public Image markerImag;
    GameObject compass;

    public GameObject Player;

    void OnDestroy() {
        Destroy(marker);
    }
	// Use this for initialization
	void Start () {
        //compass = GameObject.Find("Radar_compass");

        Player = GameObject.Find("Player");

        compass = GameObject.Find("Radar_compassmask");

        marker = Instantiate(markerImag, compass.transform.position, Quaternion.identity) as Image;

        marker.transform.SetParent(compass.transform, false);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position - Player.transform.position;
        marker.transform.localPosition = new Vector3(position.x, position.z, 0);

        /*
        if (Vector3.Distance(Player.transform.position, transform.position) <= 150)
        {
            marker.enabled = true;
        }
        else
        {
            marker.enabled = false;
        }
        */
	}
}
