using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boost_gauge : MonoBehaviour {
    public Image boost_bar;
    public GameObject Player;
    private chara_status Chara_status;
    float im_fill_default;
    // Use this for initialization
    void Start()
    {
        Chara_status = Player.GetComponent<chara_status>();
        im_fill_default = boost_bar.fillAmount;
    }

    // Update is called once per frame
    void Update()
    {
        boost_bar.fillAmount = im_fill_default * (Chara_status.boost_energy / Chara_status.boost_energy_max);
    }
}