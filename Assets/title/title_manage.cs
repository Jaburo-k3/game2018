using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class title_manage : MonoBehaviour {

    public bool button = false;

    public AudioClip title_sound;
    public void ranking_world() {
        if (button == false)
        {
            StartCoroutine(world_change("rankingall_world"));
        }
    }
    public void assemble_world() {
        if (button == false)
        {
            StartCoroutine(world_change("assemble_world"));
        }
    }

    public void tutorial_world() {
        if (button == false)
        {
            StartCoroutine(world_change("tutorial_world"));
        }
    }
    public IEnumerator world_change(string world_name)
    {
        float delay_time = 0;
        button = true;
        if (world_name == "assemble_world") {
            AudioSource AudioSource;
            AudioSource = gameObject.AddComponent<AudioSource>();
            AudioSource.clip = title_sound;
            AudioSource.volume = 0.3f;
            AudioSource.Play();
            delay_time = 1.5f;
        }
        yield return new WaitForSeconds(delay_time);
        SceneManager.LoadScene(world_name);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
