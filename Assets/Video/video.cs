using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class video : MonoBehaviour {
    VideoPlayer videoplayer;
    VideoClip videoclip;
    void play_byClip() {
    }
	// Use this for initialization
	void Start () {
        videoplayer = this.GetComponent<VideoPlayer>();
        videoplayer.Prepare();
    }

    private void Videoplayer_prepareCompleted(VideoPlayer source)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update () {
        //videoplayer.prepareCompleted += Videoplayer_prepareCompleted;
        videoplayer.Prepare();

    }
}
