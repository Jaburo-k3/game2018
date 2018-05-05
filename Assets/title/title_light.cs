using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title_light : MonoBehaviour {
    private Light light;
    bool start_light = false;
    float wait_time = 8f;
    IEnumerator light_change() {
        yield return new WaitForSeconds(wait_time);
        start_light = true;
    }

	// Use this for initialization
	void Start () {
        light = this.GetComponent<Light>();
        StartCoroutine(light_change());
	}
	
	// Update is called once per frame
	void Update () {
        if (light.intensity < 1 && start_light) {
            light.intensity += Time.deltaTime  * 0.5f;
            if (light.intensity > 1) {
                light.intensity = 1;
            }
        }
	}
}
