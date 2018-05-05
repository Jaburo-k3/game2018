using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class tutorial_step : MonoBehaviour {
    private tutorial_text Tutorial_text;


    private mouse_Vec Mouse_Vec;
    public GameObject Mouse_Vec_obj;

    private lockon Lockon;
    public GameObject Lockon_obj;

    private weapon_switching W_switching;

    private camera Camera;

    public GameObject enemy_obj;

    public int step = 0;
    private bool step_clear = false;
    public bool put_switch = false;
    bool step_stop = false;
    bool step_stop_now = false;
    public Vector2 save_mouse = Vector2.zero;

     string[] key_list = { "w", "s", "a", "d", "space" };
    public float put_time;
    public float lockon_time;
    public int shot_counter;
    public int save_weapon_number;
    GameObject target;

    public void step_lock() {
        //StartCoroutine()
    }
    IEnumerator text_update()
    {
        yield return new WaitForSeconds(1.0f);
        Tutorial_text.SetNextLine();

    }
    //ステップ移行前準備
    public void step_preparation(int number) {
        if (number == 5)
        {
            save_mouse = Mouse_Vec.mouse;
        }
        else if (number == 7)
        {
            target = Instantiate(enemy_obj);
        }
        else if (number == 9)
        {
            shot_counter = 0;
            //修正点
            //save_weapon_number = W_switching.weapon_number[];
        }
        else if (number == 10) {
            //Destroy(target);
        }
    }
    //ステップ管理
    public void step_manage(int number) {
        if (number >= 0 && number < 5)
        {
            step_move(key_list[number]);
        }
        else if (number >= 5 && number < 7)
        {
            //Debug.Log("mouse");
            step_camera();
        }
        else if (number == 7)
        {
            step_lockon();
        }
        else if (number == 8)
        {
            step_shot();
        }
        else if (number == 9)
        {
            step_weapon_change();
        }
        else if (number >= 10 && number < 12) {
            step_snipe();
        }
    }
    //基本動作チュートリアル
    public void step_move(string key) {
        //Debug.Log("start");
        if (Input.GetKeyDown(key))
        {
            //Debug.Log("go");
            put_switch = true;
        }
        else if (Input.GetKeyUp(key))
        {
            put_switch = false;
        }
        else if (Input.GetKey(key)) {
            put_time += 1 * Time.deltaTime;
        }

        if (put_switch && put_time >= 1.0) {
            step_clear = true;
            put_switch = false;
            put_time = 0;
            Debug.Log("Clear");
        }
    }

    //カメラチュートリアル
    public void step_camera() {
        //Debug.Log(Mathf.Abs(save_mouse.x - Mouse_Vec.mouse.x));
        if (step == 5 && Mathf.Abs(save_mouse.x - Mouse_Vec.mouse.x) > 0.2f)
        {
            //Debug.Log("x");
            step_clear = true;
            save_mouse = Mouse_Vec.mouse;
        }
        else if (step == 6 && Mathf.Abs(save_mouse.y - Mouse_Vec.mouse.y) > 0.15f) {
            //Debug.Log("y");
            step_clear = true;
            save_mouse = Mouse_Vec.mouse;
            step_stop_now = false;
        }
    }

    //ロックオンチュートリアル
    public void step_lockon() {
        if (Lockon.LockOn) {
            lockon_time += 1 * Time.deltaTime;
        }
        if (lockon_time > 2) {
            step_clear = true;
            step_stop_now = false;
        }
    }

    //射撃チュートリアル
    public void step_shot() {
        if (Input.GetMouseButtonDown(0)) {
            shot_counter += 1;
            Debug.Log(shot_counter);
        }
        if (shot_counter > 2) {
            Debug.Log(shot_counter);
            step_clear = true;
            step_stop_now = false;
        }
    }

    //武器チェンジチュートリアル
    public void step_weapon_change()
    {
        //修正点
        /*
        if (W_switching.weapon_number != save_weapon_number && Input.GetMouseButtonDown(0)) {
            shot_counter += 1;
        }
        if (shot_counter > 0) {
            step_clear = true;
            step_stop_now = false;
        }
        */
    }

    //スナイプチュートリアル
    public void step_snipe() {
        if (step == 10 && Camera.mode_snipe) {
            step_clear = true;
        }

        if (step == 11 && Camera.mode_snipe == false) {
            step_clear = true;
            step_stop_now = false;
        }
    }
    IEnumerator scene_change() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("title_world");
    }
    // Use this for initialization
    void Start () {
        Debug.Log(key_list[0]);
        Tutorial_text = this.GetComponent<tutorial_text>();
        Mouse_Vec = Mouse_Vec_obj.GetComponent<mouse_Vec>();
        W_switching = Mouse_Vec.GetComponent<weapon_switching>();
        Lockon = Lockon_obj.GetComponent<lockon>();
        Camera = Lockon_obj.GetComponent<camera>();
        //step_manage(step);
    }
	
	// Update is called once per frame
	void Update () {
        if (step_clear) {
            step += 1;
            step_preparation(step);
            Tutorial_text.SetNextLine();
            step_clear = false;
        }
        if ((step == 5 || step == 7 || step == 8 || step == 9 || step == 10 || step ==  12) && step_stop_now == false) {
            step_stop = true;
            if (Tutorial_text.currentText.Length == Tutorial_text.displayCharacterCount && step_stop == true) {
                step_stop = false;
                step_stop_now = true;
                StartCoroutine(text_update());
            }
        }

        if (step_stop == false)
        {
            //Debug.Log("step");
            step_manage(step);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            StartCoroutine(scene_change());
        }
        //Debug.Log(step);
    }
}
