using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loading_text : MonoBehaviour {
    public Text loading;
    string text_source;
    string Now_Roading = "NOW LOADING ";
    string dot;

    IEnumerator text_update() {
        for (int i = 0; i < 30; i++) {
            yield return new WaitForSeconds(0.25f);
            dot = null;
            int count = i % 6;
            for (int j = 0; j < count; j++) {
                dot += ".";
            }
            text_source = Now_Roading + dot;
            loading.text = text_source;
        }
        if (Stage_number.stage_number == 0)
        {
            SceneManager.LoadScene("game_world");
        }
        else if (Stage_number.stage_number == 1) {
            SceneManager.LoadScene("game_world");
        }
        
        assemble_BGM.assemble_bgm.Stop();
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(text_update());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
