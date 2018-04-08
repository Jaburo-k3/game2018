using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class area_over_line : MonoBehaviour {
    Image line;
    public Image lineImag;
    GameObject compass;

    public GameObject Player;

    void OnDestroy()
    {
        Destroy(line);
    }
    // Use this for initialization
    void Start()
    {
        //compass = GameObject.Find("Radar_compass");

        Player = GameObject.Find("Player");

        compass = GameObject.Find("Radar_compassmask");

        line = Instantiate(lineImag, compass.transform.position, Quaternion.identity) as Image;

        line.transform.SetParent(compass.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position - Player.transform.position;
        line.transform.localPosition = new Vector3(position.x, position.z, 0);

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
