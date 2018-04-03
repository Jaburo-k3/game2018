using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_UI : MonoBehaviour {
    private weapon_switching W_switching;
    public GameObject switching_obj;

    private weapon_status W_status;
    public GameObject status_obj;

    private float im_fill_initial;
    private Image im;
    private Color Primary_Color;
    public Color Red_Color = new Color(255, 40, 90, 143);
    public Vector3 equipment_pos;
    public Vector3 save_pos;

    public void getcomponet() {
        W_status = status_obj.GetComponent<weapon_status>();
    }
    // Use this for initialization
    void Start () {
        save_pos = transform.position;
        equipment_pos = transform.position + new Vector3(0, 40, 0);
        W_switching = switching_obj.GetComponent<weapon_switching>();
        //W_status = status_obj.GetComponent<weapon_status>();
        im = this.GetComponent<Image>();
        Primary_Color = im.color;
        im_fill_initial = im.fillAmount;
        Debug.Log(im_fill_initial);
        Red_Color = new Color(1.0f,0.2f,0.35f,0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        im.fillAmount =im_fill_initial *  (W_status.bullet_counter / W_status.get_bullet_counter_max());
        if ((W_status.bullet_counter < W_status.bullet_one_shot) || (W_status.reload_type == 1 && W_status.reload_now))
        {
            im.color = Red_Color;
        }
        else {
            im.color = Primary_Color;
        }
        /*
        if (W_switching.weapon_number == W_status.get_my_weapon_number())
        {
            this.transform.position = equipment_pos;
        }
        else {
            this.transform.position = save_pos;
        }
        */
		
	}
}
