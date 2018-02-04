using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_blast : MonoBehaviour {

    private AudioSource AudioSource;
    public AudioClip blast_sound;
    IEnumerator Destroy_obj() {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start () {
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.clip = blast_sound;
        AudioSource.volume = 0.1f;
        AudioSource.Play();
        StartCoroutine("Destroy_obj");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
