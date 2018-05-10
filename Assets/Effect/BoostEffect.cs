using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : MonoBehaviour {

    public GameObject[] boostLight = new GameObject[2];
    public GameObject[] boostLight_Lleg = new GameObject[2];
    public GameObject[] boostLight_Rleg = new GameObject[2];
    public GameObject[] boostLight_Lbackpack = new GameObject[2];
    public GameObject[] boostLight_Rbackpack = new GameObject[2];
    public GameObject[] quickboostside = new GameObject[2];
    public GameObject[] quick_turn_Light = new GameObject[2];
    private chara_status Chara_status;

    Coroutine Q_boost_effect_cor;
    Coroutine Q_boostside_effect_cor;
    Coroutine Q_turn_effect_cor;
    public GameObject player;


    IEnumerator Q_boost_effect()
    {
        //クイックブーストエフェクト
        boostLight[1].SetActive(true);
        boostLight_Lleg[1].SetActive(true);
        boostLight_Rleg[1].SetActive(true);
        boostLight_Lbackpack[1].SetActive(true);
        boostLight_Rbackpack[1].SetActive(true);
        //ブーストエフェクト
        boostLight[0].SetActive(false);
        boostLight_Lleg[0].SetActive(false);
        boostLight_Rleg[0].SetActive(false);
        boostLight_Lbackpack[0].SetActive(false);
        boostLight_Rbackpack[0].SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //クイックブーストエフェクト
        boostLight[1].SetActive(false);
        boostLight_Lleg[1].SetActive(false);
        boostLight_Rleg[1].SetActive(false);
        boostLight_Lbackpack[1].SetActive(false);
        boostLight_Rbackpack[1].SetActive(false);
        Q_boost_effect_cor = null;


    }
    IEnumerator Q_boostside_effect(string direction)
    {
        //クイックブーストエフェクト
        if (direction == "left")
        {
            quickboostside[0].SetActive(true);
            quickboostside[1].SetActive(false);
        }
        else {
            quickboostside[1].SetActive(true);
            quickboostside[0].SetActive(false);
        }
        //ブーストエフェクト
        boostLight[0].SetActive(false);
        boostLight_Lleg[0].SetActive(false);
        boostLight_Rleg[0].SetActive(false);
        boostLight_Lbackpack[0].SetActive(false);
        boostLight_Rbackpack[0].SetActive(false);
        yield return new WaitForSeconds(0.5f);
        //クイックブーストエフェクト
        quickboostside[0].SetActive(false);
        quickboostside[1].SetActive(false);
        Q_boostside_effect_cor = null;


    }
    IEnumerator Q_trun_effect()
    {
        quick_turn_Light[0].SetActive(false);
        quick_turn_Light[1].SetActive(false);
        quick_turn_Light[Chara_status.quick_turn].SetActive(true);
        yield return new WaitForSeconds(0.25f);
        quick_turn_Light[0].SetActive(false);
        quick_turn_Light[1].SetActive(false);
        Q_turn_effect_cor = null;
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
        quickboostside[0].SetActive(false);
        quickboostside[1].SetActive(false);
        quick_turn_Light[0].SetActive(false);
        quick_turn_Light[1].SetActive(false);
        //player = GameObject.Find("Player");
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
            if (Chara_status.moving_state[0] == "forward" || Chara_status.moving_state[0] == "back" || Chara_status.moving_state[1] == "wait")
            {
                if (Q_boost_effect_cor == null)
                {
                    Q_boost_effect_cor = StartCoroutine(Q_boost_effect());
                }
                else {
                    StopCoroutine(Q_boost_effect_cor);
                    Q_boost_effect_cor = StartCoroutine(Q_boost_effect());
                }
            }
            else if (Chara_status.moving_state[0] == "left" || Chara_status.moving_state[0] == "right") {
                if (Q_boostside_effect_cor == null)
                {
                    Q_boostside_effect_cor = StartCoroutine(Q_boostside_effect(Chara_status.moving_state[0]));
                }
                else {
                    StopCoroutine(Q_boostside_effect_cor);
                    Q_boostside_effect_cor = StartCoroutine(Q_boostside_effect(Chara_status.moving_state[0]));
                }
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
        if (Chara_status.quick_turn != 2 && Q_turn_effect_cor ==  null)
        {
            Q_turn_effect_cor = StartCoroutine(Q_trun_effect());
        }
    }
}
