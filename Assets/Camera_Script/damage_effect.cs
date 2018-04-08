using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage_effect : MonoBehaviour {
    public　GameObject parent;
    GameObject player_smoke;
    private chara_status Chara_status;
    private HP hp;
    private GlitchFx glitchfx;
    private UnityStandardAssets.ImageEffects.NoiseAndScratches Noise_a_Scratches;
    Coroutine D_effect_cor;
    private ParticleSystem P_system;
    float initial_intensity;
    float max_intensity = 0.02f;

    void Awake() {
        parent = transform.root.gameObject;
    }
    // Use this for initialization
    void Start () {
        //parent = transform.root.gameObject;
        player_smoke = parent.transform.FindChild("player_smoke").gameObject;
        player_smoke.SetActive(false);
        Chara_status = parent.GetComponent<chara_status>();
        hp = parent.GetComponent<HP>();
        glitchfx = this.GetComponent<GlitchFx>();
        P_system = player_smoke.GetComponent<ParticleSystem>();
        Noise_a_Scratches = this.GetComponent<UnityStandardAssets.ImageEffects.NoiseAndScratches>();
        initial_intensity = glitchfx.intensity;
        glitchfx.enabled = false;
        Noise_a_Scratches.enabled = false;


    }
	
	// Update is called once per frame
	void Update () {
        if (hp.get_hp() < hp.get_hp_max() / 2) {
            if (!glitchfx.enabled) {
                glitchfx.enabled = true;
            }
            glitchfx.intensity = max_intensity - ((max_intensity - initial_intensity) * hp.get_hp() / (hp.get_hp_max()/2));
            //煙発生
            if (!player_smoke.active)
            {
                player_smoke.SetActive(true);
            }
            Debug.Log("1/2");
        }

        //煙調整
        if (hp.get_hp() < hp.get_hp_max() / 3 && player_smoke.active) {
            var emission = P_system.emission;
            emission.rateOverTime = 200f;
            Debug.Log("1/3");
        }

        //ノイズ
        if (hp.get_hp() < hp.get_hp_max() / 5) {
            if (!Noise_a_Scratches.enabled) {
                Noise_a_Scratches.enabled = true;
            }
            Debug.Log("1/5");
        }

    }
}
