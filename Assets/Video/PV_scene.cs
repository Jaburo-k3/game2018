using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PV_scene : MonoBehaviour {

    public IEnumerator world_change()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("title_world");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            Debug.Log("go");
            StartCoroutine(world_change());
        }
	}
}
