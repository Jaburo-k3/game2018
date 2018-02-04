using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_marker : MonoBehaviour {
    public GameObject parent;
    public GameObject child;
    private Vector3 look_pos;
    private Vector3 trans_pos;

    //private AudioSource AudioSource;
    //public AudioClip Hit_sound;



    IEnumerator Destroy() {
        yield return new WaitForSeconds(1f);
        Destroy(child);
        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start () {
        StartCoroutine("Destroy");
        child = transform.FindChild("Hit_UI").gameObject;
        look_pos = new Vector3(parent.transform.position.x, transform.position.y, parent.transform.position.z);
        transform.LookAt(look_pos);
        trans_pos = transform.position;
        //AudioSource = gameObject.AddComponent<AudioSource>();
        //AudioSource.clip = Hit_sound;
        //AudioSource.volume = 0.1f;
        //AudioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
        look_pos = new Vector3(parent.transform.position.x, transform.position.y, parent.transform.position.z);
        transform.LookAt(look_pos);
        trans_pos.y += 0.1f;
        transform.position = trans_pos;
    }
}
