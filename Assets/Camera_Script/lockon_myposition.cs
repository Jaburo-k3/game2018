using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lockon_myposition : MonoBehaviour {


    private lockon Lockon;
    public Transform target; // 対象のオブジェクト
    public GameObject gui; // GUITextureもしくはGUIText
    public GUITexture Tex;
    private Vector3 standard_size;

    //ビューポート座標からキャンバス内の座標へ
    Vector3 ViewpostoCanvas(Vector3 viewPos)
    {
        return new Vector3(-1 * (1920/2) + (1920 * viewPos.x), -1 * (1080/2) + (1080 * viewPos.y), 0);
    }

    //ロックオン内にいるか
    public bool on_lockon(Vector3 viewPos, Vector3 target)
    {
        Vector3 can_pos = ViewpostoCanvas(viewPos);
        float lockon_area_x = Lockon.lockon_area_x;
        float lockon_area_y = Lockon.lockon_area_y;

        float centerpoint_x = Lockon.centerpoint_x;
        float centerpoint_y = Lockon.centerpoint_y;

        float r = lockon_area_x / 2;

        float point_val = (can_pos.x - centerpoint_x) * (can_pos.x - centerpoint_x) +
            (can_pos.y - centerpoint_y) * (can_pos.y - centerpoint_y);
        float r2 = r * r;

        return (point_val < r2);
    }

    //ロックオンマーカーの大きさを調整
    void size_change(GameObject camera) {
        float distance = Vector3.Distance(camera.transform.position, this.gameObject.transform.position);
        gui.transform.localScale =  new Vector3(standard_size.x * (13/distance), standard_size.y * (13/distance ),standard_size.z);
    }


    //ロックオン距離内にいるか
    bool on_lockonrange(GameObject camera) {
        float distance = Vector3.Distance(camera.transform.position, this.gameObject.transform.position);
        if (Lockon.lockon_range > distance) {
            return true;
        }
        return false;
    }
    void OnWillRenderObject()
    {
        if (Camera.current.name == "Main Camera")
        {
            Lockon = Camera.current.gameObject.GetComponent<lockon>();
            if (!on_lockonrange(Camera.current.gameObject))
            {
                Tex.enabled = false;
                if (Lockon.target_obj == this.gameObject)
                {
                    Lockon.target_delete();
                }
                return;
            }
            Vector3 viewPos = Camera.current.gameObject.GetComponent<Camera>().WorldToViewportPoint(gameObject.transform.position);
            //Debug.Log(viewPos);
            if (on_lockon(viewPos, gameObject.transform.position))
            {
                Tex.enabled = true;
                gui.transform.position = viewPos;
                size_change(Camera.current.gameObject);
                if (Lockon.target_update(this.gameObject))
                {
                    Tex.color = new Color(1, 0, 0);
                }
                else {
                    Tex.color = new Color(0.25f, 1, 0);
                }
            }
            else {
                Tex.enabled = false;
                if (Lockon.target_obj == this.gameObject) {
                    Lockon.target_delete();
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
        Tex = gui.GetComponent<GUITexture>();
        standard_size = gui.transform.localScale;
        Tex.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
