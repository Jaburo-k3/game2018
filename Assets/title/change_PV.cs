using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_PV : MonoBehaviour {
    private title_manage Title_manage;
    public float timer;
    AsyncOperation operation;
    // Use this for initialization
    void Start () {
        Title_manage = this.GetComponent<title_manage>();
        operation = SceneManager.LoadSceneAsync("PV_world");
        //operation.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= 1 * Time.deltaTime;
        if (timer < 0  && Title_manage.button == false) {
            //operation.allowSceneActivation = true;
        }
    }
}
