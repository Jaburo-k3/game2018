using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motion_blur_switch : MonoBehaviour {
    GameObject parent;
    private chara_status Chara_status;
    private UnityStandardAssets.ImageEffects.MotionBlur motion_blur;
    Coroutine blur_switch_cor;

    IEnumerator blur_switch()
    {
        motion_blur.enabled = true;
        yield return new WaitForSeconds(0.3f);
        motion_blur.enabled = false;
    }
    void Awake() {
        parent = transform.root.gameObject;
    }
	// Use this for initialization
	void Start () {
        Chara_status = parent.GetComponent<chara_status>();
        motion_blur = this.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
        motion_blur.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Chara_status.quick_boost) {
            if (blur_switch_cor == null) {
                blur_switch_cor = StartCoroutine(blur_switch());
            }
            else {
                StopCoroutine(blur_switch_cor);
                blur_switch_cor = StartCoroutine(blur_switch());
            }
        }
	}
}
