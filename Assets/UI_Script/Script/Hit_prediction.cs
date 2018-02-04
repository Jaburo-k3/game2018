using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hit_prediction : MonoBehaviour {

    public GameObject AIM_obj;
    private Aiming_system AIM;

    public Image imag;
	// Use this for initialization
	void Start () {
        AIM = AIM_obj.GetComponent<Aiming_system>();
        imag = this.gameObject.GetComponent<Image>();
	}

    // Update is called once per frame
    void Update() {
        if (Physics.Raycast(AIM.ray, out AIM.hit, Mathf.Infinity))
        {
            {
                if (AIM.hit.collider.tag == "enemy")
                {
                    imag.enabled = true;
                }
                else {
                    imag.enabled = false;
                }
            }
        }
	}
}
