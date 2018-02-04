using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_enemy : MonoBehaviour {

    int move_direction = 1;

    IEnumerator direction_update()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            move_direction *= -1;
        }
    }
    // Use this for initialization
    void Start () {
        StartCoroutine(direction_update());
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + 0.1f * move_direction, transform.position.y, transform.position.z);
	}
}
