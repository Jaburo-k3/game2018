using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emission_change : MonoBehaviour {
    Renderer render;
    Material material;
    Color color;
    bool up_down = true;
    bool start_move = true;
    bool emission_start = false;
    float change_time = 2f;
    float start_time = 2f;
    IEnumerator change_updown() {
        while (true) {
            yield return new WaitForSeconds(change_time);
            if (up_down)
            {
                up_down = false;
            }
            else {
                up_down = true;
            }
        }
    }
    IEnumerator emission_switch()
    {
        yield return new WaitForSeconds(2f);
        emission_start = true;
        StartCoroutine(change_updown());
    }
    // Use this for initialization
    void Start () {
        render = this.GetComponent<Renderer>();
        material = this.GetComponent<Material>();
        render.material.EnableKeyword("_EMISSION");
        color = new Color(0.0f, 0f, 0f);
        render.material.SetColor("_EmissionColor", color);
        StartCoroutine(emission_switch());
    }
	
	// Update is called once per frame
	void Update () {
        if (!emission_start) {
            return;
        }
        if (start_move) {
            color.r += 2f * (Time.deltaTime / start_time);
            if (color.r > 2.0f) {
                color.r = 2.0f;
                start_move = false;
            }
        }
        if (up_down)
        {
            color.r += 1f * (Time.deltaTime / change_time);
        }
        else {
            color.r -= 1f * (Time.deltaTime / change_time);
        }
        render.material.SetColor("_EmissionColor", color);
    }
}
