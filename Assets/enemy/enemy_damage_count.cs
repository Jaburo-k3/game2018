using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_damage_count : MonoBehaviour {
    private HP hp;
    float save_hp;
    public bool evasive = false;
    float damage_count = 0;
    float hit_count = 0;
    Coroutine damage_count_cor;
    IEnumerator damage_reset()
    {
        damage_count = 0;
        hit_count = 0;
        yield return new WaitForSeconds(4f);
        damage_count = 0;
        hit_count = 0;
    }
	// Use this for initialization
	void Start () {
        hp = this.GetComponent<HP>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hp.get_hp() < save_hp) {
            damage_count += save_hp - hp.get_hp();
            hit_count += 1;
        }
        save_hp = hp.get_hp();
        if (damage_count > 50 || damage_count > 30) {
            Debug.Log("evasive");
            evasive = true;
            if (damage_count_cor == null)
            {
                damage_count_cor = StartCoroutine(damage_reset());
            }
            else {
                StopCoroutine(damage_count_cor);
                damage_count_cor = StartCoroutine(damage_reset());
            }
        }
	}
}
