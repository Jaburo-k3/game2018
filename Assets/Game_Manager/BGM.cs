using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {
    private AudioSource bgm;
    public AudioClip[] BGM_sound;
	// Use this for initialization
	void Start () {
        int number = Random.RandomRange(0, BGM_sound.Length - 1);
        //Debug.Log(number);
        bgm = this.gameObject.AddComponent<AudioSource>();
        bgm.clip = BGM_sound[number];
        bgm.loop = true;
        bgm.volume = 0.45f;
        bgm.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
