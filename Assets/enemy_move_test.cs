using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move_test : MonoBehaviour {
    private Rigidbody rb;

    bool go = false;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("enemy_move");


    }

    IEnumerable enemy_move()
    {
            yield return new WaitForSeconds(1f);


    }

    // Update is called once per frame
    void Update () {
        /*
        float x = Random.Range(-10, 10);
        float z = Random.Range(0, 10);
        rb.velocity = new Vector3(x, 0, z);
        */
        if (Input.GetKeyDown("x")) {
            if (go)
            {
                go = false;
            }
            else {
                go = true;
            }
        }
        if (go) {
            rb.velocity = new Vector3(0, 0, 10);
        }
    }
}
