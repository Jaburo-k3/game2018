using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blast : MonoBehaviour {
    private float attack = 5f;
    private HP Hp;
    public ParticleSystem effect;

    public GameObject Hit_Marker_obj;
    private create_hit_marker C_Hit_Marker;
    public GameObject Hit_obj;

    public bool permission_hit_marker;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if ((other.gameObject.tag == "enemy" || other.gameObject.tag == "Player") && other.gameObject != Hit_obj)
        {
            Debug.Log("hit");
            GameObject parent = other.transform.root.gameObject;
            if (parent == null)
            {
                parent = other.gameObject;
            }
            Hp = parent.gameObject.GetComponent<HP>();
            Hp.set_hp(Hp.get_hp() - attack);
            if (permission_hit_marker)
            {
                C_Hit_Marker.create(other.transform.position);
            }
        }
    }
    IEnumerator Destriy_obj()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        C_Hit_Marker = this.gameObject.GetComponent<create_hit_marker>();
        StartCoroutine("Destriy_obj");
        this.transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(this.gameObject);
        //this.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
}
