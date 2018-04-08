using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : MonoBehaviour {

    public GameObject[] boostLight = new GameObject[2];
    public GameObject[] boostLight_Lleg = new GameObject[2];
    public GameObject[] boostLight_Rleg = new GameObject[2];
    public GameObject[] boostLight_Lbackpack = new GameObject[2];
    public GameObject[] boostLight_Rbackpack = new GameObject[2];
    private chara_status Chara_status;

    Coroutine Q_boost_effect_cor;
    private GameObject player;

    IEnumerator Q_boost_effect()
    {
        boostLight[1].SetActive(true);
        boostLight_Lleg[1].SetActive(true);
        boostLight_Rleg[1].SetActive(true);
        boostLight_Lbackpack[1].SetActive(true);
        boostLight_Rbackpack[1].SetActive(true);
        boostLight[0].SetActive(false);
        boostLight_Lleg[0].SetActive(false);
        boostLight_Rleg[0].SetActive(false);
        boostLight_Lbackpack[0].SetActive(false);
        boostLight_Rbackpack[0].SetActive(false);
        yield return new WaitForSeconds(0.5f);
        boostLight[1].SetActive(false);
        boostLight_Lleg[1].SetActive(false);
        boostLight_Rleg[1].SetActive(false);
        boostLight_Lbackpack[1].SetActive(false);
        boostLight_Rbackpack[1].SetActive(false);
        Q_boost_effect_cor = null;


    }

    // Use this for initialization
    void Start () {
        boostLight[0].SetActive(false);
        boostLight_Lleg[0].SetActive(false);
        boostLight_Rleg[0].SetActive(false);
        boostLight_Lbackpack[0].SetActive(false);
        boostLight_Rbackpack[0].SetActive(false);
        boostLight[1].SetActive(false);
        boostLight_Lleg[1].SetActive(false);
        boostLight_Rleg[1].SetActive(false);
        boostLight_Lbackpack[1].SetActive(false);
        boostLight_Rbackpack[1].SetActive(false);
        player = GameObject.Find("Player");
        Chara_status = player.GetComponent<chara_status>();
	}
	
	// Update is called once per frame
	void Update () {
        bool Boost = false;
        if (Chara_status.moving_state[1] == "boost" || Chara_status.moving_state[1] == "rising")
        {
            Boost = true;
        }

        if (Chara_status.quick_boost) {
            if (Q_boost_effect_cor == null)
            {
                Q_boost_effect_cor = StartCoroutine(Q_boost_effect());
            }
            else {
                StopCoroutine(Q_boost_effect_cor);
                Q_boost_effect_cor = StartCoroutine(Q_boost_effect());
            }
        }
        if (Q_boost_effect_cor == null)
        {
            boostLight[0].SetActive(Boost);
            boostLight_Lleg[0].SetActive(Boost);
            boostLight_Rleg[0].SetActive(Boost);
            boostLight_Lbackpack[0].SetActive(Boost);
            boostLight_Rbackpack[0].SetActive(Boost);
        }
    }
}
