using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assemble_BGM : MonoBehaviour {
    public bool DontDestroyEnabled = true;
    public static AudioSource assemble_bgm;

    // Use this for initialization
    void Start()
    {
        assemble_bgm = this.GetComponent<AudioSource>();
        assemble_bgm.Play();
        if (DontDestroyEnabled)
        {
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
