using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boost_text : MonoBehaviour {
    private chara_status Chara_status;
    public GameObject Player;
    public Text Boost_text;
    // Use this for initialization
    void Start()
    {
        Chara_status = Player.GetComponent<chara_status>();

    }

    // Update is called once per frame
    void Update()
    {
        Boost_text.text = Chara_status.boost_energy.ToString("f2");
    }
}
