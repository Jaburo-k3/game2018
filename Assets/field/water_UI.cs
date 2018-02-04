using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class water_UI : MonoBehaviour {

    Image water_flame;

    public void water_flame_enable(bool status) {
        water_flame.enabled = status;
    }

	// Use this for initialization
	void Start () {
        water_flame = this.GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
