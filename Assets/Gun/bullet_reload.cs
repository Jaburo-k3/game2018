using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_reload : MonoBehaviour {

    private weapon_status W_status;

    IEnumerator reload_type0()
    {

        while (true)
        {

            if (W_status.reload_type == 0)
            {
                yield return new WaitForSeconds(0f);
                W_status.bullet_counter += W_status.bullet_one_shot / (W_status.reload_time * 60);
                //Debug.Log(W_status.bullet_counter);
            }
            /*
            else
            {
                if (W_status.bullet_counter <= 0) {
                    W_status.shot_lock = true;
                    yield return new WaitForSeconds(W_status.reload_time / W_status.get_bullet_counter_max());
                    W_status.bullet_counter += 1;
                }
                
                //Debug.Log(W_status.bullet_counter);

            }
            */


            if (W_status.bullet_counter >= W_status.get_bullet_counter_max())
            {
                W_status.bullet_counter = W_status.get_bullet_counter_max();
                W_status.reload_now = false;
                Debug.Log("reloadend");
                break;
            }
        }
    }

    IEnumerator reload_type1()
    {
        W_status.shot_lock = true;

        while (true)
        {
            yield return new WaitForSeconds(W_status.reload_time / W_status.get_bullet_counter_max());
            W_status.bullet_counter += 1;
            

            if (W_status.bullet_counter >= W_status.get_bullet_counter_max())
            {
                W_status.bullet_counter = W_status.get_bullet_counter_max();
                W_status.reload_now = false;
                Debug.Log("reloadend");
                break;
            }
        }

        W_status.shot_lock = false;
    }
    // Use this for initialization
    void Start () {
        W_status = this.GetComponent<weapon_status>();
	}
	
	// Update is called once per frame
	void Update () {
        if (W_status.bullet_counter != W_status.get_bullet_counter_max() && W_status.reload_now == false && W_status.reload_type == 0)
        {
            W_status.reload_now = true;
            StartCoroutine("reload_type0");
        }
        else if (W_status.bullet_counter < W_status.bullet_one_shot && W_status.reload_now == false && W_status.reload_type == 1) {
            W_status.reload_now = true;
            StartCoroutine("reload_type1");
        }
	}
}
