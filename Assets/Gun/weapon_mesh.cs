using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_mesh : MonoBehaviour {
    private MeshRenderer[] my_mesh;
    private SkinnedMeshRenderer[] skin_my_mesh;
    public GameObject[] mesh_obj;
    private weapon_status W_status;
    private weapon_switching W_switching;
    private GameObject parent;
    public bool mesh_renderer;
    //public bool mesh_renderer;
    // Use this for initialization
    void Start () {
        parent = transform.root.gameObject;
        if (mesh_renderer)
        {
            my_mesh = new MeshRenderer[mesh_obj.Length];
            for (int i = 0; i < mesh_obj.Length; i++) {
                my_mesh[i] = mesh_obj[i].GetComponent<MeshRenderer>();
            }
        }
        else
        {
            skin_my_mesh = new SkinnedMeshRenderer[mesh_obj.Length];
            for (int i = 0; i < mesh_obj.Length; i++) {
                skin_my_mesh[i] = mesh_obj[i].GetComponent<SkinnedMeshRenderer>();
            }
            //mesh_renderer = false;
        }
        //my_mesh = this.GetComponent<MeshRenderer>();
        W_status = this.GetComponent<weapon_status>();
        W_switching = parent.GetComponent<weapon_switching>();

    }
	
	// Update is called once per frame
	void Update () {
        if (W_status.get_my_weapon_number() == W_switching.weapon_number[W_status.my_arm_number])
        {
            if (mesh_renderer)
            {
                for (int i = 0; i < mesh_obj.Length; i++) {
                    my_mesh[i].enabled = true;
                }

            }
            else {
                for (int i = 0; i < mesh_obj.Length; i++)
                {
                    skin_my_mesh[i].enabled = true;
                }
            }
        }
        else {
            if (mesh_renderer)
            {
                for (int i = 0; i < mesh_obj.Length; i++)
                {
                    my_mesh[i].enabled = false;
                }

            }
            else {
                for (int i = 0; i < mesh_obj.Length; i++)
                {
                    skin_my_mesh[i].enabled = false;
                }
            }
        }
	}
}
