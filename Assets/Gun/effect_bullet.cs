using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_bullet : MonoBehaviour {
    public Vector3 save_pos;
    public Vector3 line_vec = Vector3.zero;
    public Vector3 line_vec_dec; 
    LineRenderer lineRender;
    private GameObject Parent_obj;
    private Vector3 Parent_pos;

    private bullet Bullet;
    Vector3[] pos_array = new Vector3[100];
    int number = 0;
    /*
    IEnumerator update_setpos()
    {

        int i = 0;
        float update_time = 0.04f;
        while (true)
        {
            yield return new WaitForSeconds(update_time);
            Vector3 set_pos = pos_array[i];
            lineRender.SetPosition(0, set_pos);
            if (set_pos == transform.position && Parent_obj == null) {
                Destroy(this.gameObject);
            }
            i += 1;
            if (i > 99) {
                i = 0;
            }
            update_time -= 0.001f;
            if (update_time < 0.02f) {
                update_time = 0.02f;
            }
        }
    }
    IEnumerator update_savepos()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            save_pos = transform.position;
            pos_array[number] = save_pos;
            number += 1;
            if (number > 99) {
                number = 0;
            }
        }
    }
    */
    IEnumerator update_savepos() {
        yield return new WaitForSeconds(0.002f);
        line_vec = transform.position - save_pos;
        line_vec_dec = line_vec / 5;
    }
    // Use this for initialization
    void Start () {

        Parent_obj = transform.root.gameObject;

        lineRender = GetComponent<LineRenderer>();

        save_pos = Parent_obj.transform.position;
        lineRender.SetPosition(0, save_pos);
        lineRender.SetPosition(1, save_pos);

        Bullet = Parent_obj.GetComponent<bullet>();

        pos_array[number] = save_pos;
        number += 1;
        //StartCoroutine("update_savepos");
        //StartCoroutine("update_setpos");
        StartCoroutine("update_savepos");
    }
	
	// Update is called once per frame
	void Update () {
        Parent_pos = Bullet.hit.point;
        lineRender.SetPosition(1, transform.position);
        if (line_vec != Vector3.zero && Parent_obj != null) {
            lineRender.SetPosition(0, transform.position - line_vec);
        }
        if (Bullet.hit.point != Vector3.zero) {
            lineRender.SetPosition(1, Parent_pos);
            //save_pos = Bullet.hit.point;
        }
        if (Parent_obj == null) {
            lineRender.SetPosition(1, Parent_pos);
            line_vec = line_vec / 2f;
            lineRender.SetPosition(0, Parent_pos - line_vec);
            if (Vector3.Distance(lineRender.GetPosition(0), lineRender.GetPosition(1)) <= 0.01f) {
                Destroy(this.gameObject);
            }
        }
        //Debug.Log(Parent_obj);
    }
}
