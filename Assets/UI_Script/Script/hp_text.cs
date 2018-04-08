using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_text : MonoBehaviour {

    private HP hp;
    public GameObject Player;
    public Text Hp_text;
	// Use this for initialization
	void Start () {
        hp = Player.GetComponent<HP>();

    }
	
	// Update is called once per frame
	void Update () {
        Hp_text.text = hp.get_hp().ToString();
	}
}
