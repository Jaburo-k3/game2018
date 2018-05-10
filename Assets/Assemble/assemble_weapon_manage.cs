using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class assemble_weapon_manage : MonoBehaviour {
    float scroll;
    float save_scroll;
    private weapon_UI_indicate W_UI_In;
    private weapon_flame_update W_FL_Up;
    private assemble_status As_Status;
    private camera_manage Camera_Manage;
    public GameObject camera_obj;
    private assemble_weapon_enable As_W_En;
    private viewweapon_enable View_enable;
    private weapon_silhouette Weapon_Sil;
    private weapon_text Weapon_Text;
    private status_text Status_Text;
    private Exit_UI Exit_Ui;
    private stage_select_UI Stage_Select_UI;
    private weapon_tab_button W_Tab_Button;

    public Image[] weapon;
    public Text[] E_text;

    public int shoulder_weapon_variation;

    public Image[] Exit;
    public Text[] Exit_text;

    public int weapon_slot = 0;
    public int weapon_number = 0;
    public int shoulder_weapon_number = 0;

    public int[] center_list = {1,3,5};
    public int center_number;

    public int[] top_distance = { 1, 1, 2 };

    public bool mode_change = false;
    public bool Exit_mode = false;
    public bool Stage_mode = false;
    public bool shoulder_weapon_select;

    public List<Image> weapon_list;
    public Sprite[] weapon_tex;

    public List<int> indicate_list;
    public List<int> E_weapon_number;
    public List<Text> E_text_list;

    Vector2 cross_vec_save = Vector2.zero;
    Vector2 stick_vec_save = Vector2.zero;

    public AudioSource SE;
    public AudioClip[] SE_clip;

    public GameObject BGM;
    

    //表示する番号のセット //変更済み
    void set_indicate_list()
    {
        //中身をリセット
        indicate_list.Clear();

        //入れる値の頭
        int number = weapon_number - top_distance[weapon_slot];
        int length;
        if (!shoulder_weapon_select)
        {
            length = weapon.Length - shoulder_weapon_variation;
            //上限のチェック
            if (number < 0) {
                int over = Mathf.Abs(number);
                number = length - over;
            }
        }
        else {
            length = shoulder_weapon_variation;
            //上限のチェック
            if (number < weapon.Length - shoulder_weapon_variation) {
                Debug.Log("number = " + number);
                Debug.Log("top_distance[weapon_slot] = " + top_distance[weapon_slot]);
                Debug.Log("over" + (weapon_number - top_distance[weapon_slot]));
                int over = Mathf.Abs((weapon_number - top_distance[weapon_slot]) - shoulder_weapon_variation);
                number = weapon.Length - over;
            }
        }
        //int number = assemble_status.my_weapon_number[weapon_slot] - top_distance[weapon_slot];
        //セット
        for (int i = 0; i < 4; i++)
        {
            indicate_list.Add(number);

            number += 1;
            if (!shoulder_weapon_select && number > weapon.Length - 1 - shoulder_weapon_variation)
            {
                number = 0;
            }
            else if (shoulder_weapon_select && number > weapon.Length - 1) {
                number = weapon.Length - shoulder_weapon_variation;
            }
        }

    }

    //
    public void E_text_set() {
        E_weapon_number.Clear();
        for (int i = 0; i < assemble_status.my_weapon_number.Length; i++) {
            for (int j = 0; j < indicate_list.Count; j++) {
                if (assemble_status.my_weapon_number[i] == indicate_list[j]) {
                    E_weapon_number.Add(j);
                }
            }
        }

        for (int i = 0; i < E_text.Length; i++) {
            E_text[i].enabled = false;
        }

        for (int i = 0; i < E_text.Length; i++) {
            if (E_weapon_number.Contains(i))
            {
                E_text_list[i].enabled = true;
            }
        }
    }
    //全テキスト不可視化
    public void all_E_text_clear() {
        for (int i = 0; i < E_text.Length; i++) {
            E_text[i].enabled = false;
        }
    }
    //武器のUI不可視化
    public void UI_enable()
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (weapon_list.Contains(weapon[i]))
            {
                weapon[i].enabled = true;
            }
            else {
                weapon[i].enabled = false;
            }
        }
    }

    //表示を変更するImagのセット
    void set_weapon_list()
    {
        //中身をリセット
        weapon_list.Clear();
        //どの範囲の「E」を取得するかもついでに行う
        E_text_list.Clear();
        //入れる値の頭
        int number = center_number - top_distance[weapon_slot];
        //上限のチェック
        if (number < 0)
        {
            int over = top_distance[weapon_slot] - center_number;
            number = (weapon.Length) - center_number;
        }
        for (int i = 0; i < 4; i++)
        {
            weapon_list.Add(weapon[number]);
            E_text_list.Add(E_text[number]);
            number += 1;
            if (!shoulder_weapon_select && number > weapon.Length - 1 - shoulder_weapon_variation)
            {
                number = 0;
            }
            else if (shoulder_weapon_select && number > weapon.Length - 1)
            {
                number = weapon.Length - shoulder_weapon_variation;
            }
        }
    }

    //武器のUI画像変更
    public void indicate()
    {
        for (int i = 0; i < 4; i++)
        {
            
            weapon_list[i].sprite = weapon_tex[indicate_list[i]];
        }
    }

    //武器タブボタンの番号更新
    public void W_T_B_number_update()
    {
        for (int i = 0; i < weapon_list.Count; i++)
        {
            W_Tab_Button = weapon_list[i].GetComponent<weapon_tab_button>();
            W_Tab_Button.mynumber = indicate_list[i];
        }
    }

    
    //中心点更新
    void center_update() {
        Debug.Log(weapon_slot);
        center_number = center_list[weapon_slot];
    }
    //全UIを見えなくする
    void all_W_UI_enable() {
        for (int i = 0; i < weapon.Length; i++) {
            weapon[i].enabled = false;
        }
    }

    //現在の武器番号
    void weapon_number_update() {
        weapon_number = assemble_status.my_weapon_number[weapon_slot];
    }

    //装備している武器番号の更新
    void my_weapon_number_update()
    {
        assemble_status.my_weapon_number[weapon_slot] = weapon_number;
    }

    //装備武器探索
    bool E_weapon_search(int number) {
        for (int i = 0; i < assemble_status.my_weapon_number.Length; i++) {
            if (number == assemble_status.my_weapon_number[i]) {
                return false;
            }
        }
        return true;
    }

    //機体カメラ更新
    void camera_update() {
        int arm;
        if (weapon_slot == 0 || weapon_slot == 2)
        {
            arm = 0;
        }
        else {
            arm = 1;
        }


        if (!Exit_mode)
        {
            Camera_Manage.camera_update(weapon_number,arm);
        }
        else {
            Camera_Manage.camera_update(weapon.Length,arm);
            //Debug.Log(weapon.Length);
        }
    }

    //機体に装備している武器を可視化
    void weapon_enable() {
        As_W_En.set_weapon();
        As_W_En.weapon_enable(weapon_slot);
    }

    //現在選択している武器のview
    void viewweapon_enable() {
        View_enable.weapon_enable(weapon_number);
    }

    //武器スロットのシルエット画像更新
    void slihoutte_update() {
        Weapon_Sil.silhouette_update(weapon_slot);
    }



    //テキストの更新関数を呼び出し
    void text_update() {
        Weapon_Text.text_update(weapon_number);
    }

    //ステータステキストの更新関数を呼び出し
    void status_text_update(int number, bool Do) {
        Status_Text.status_text_update(number,Do);
    }
    //装備している武器のステータスを保存する関数の呼び出し
    void save_status_update() {
        Status_Text.save_status_update();
    }

    //
    void status_text_getcomponent() {
        Status_Text.scripit_set();
    }


    //現在見ている武器のステータステキストを更新する関数呼び出し
    void now_status_update(int number) {
        Status_Text.now_status_update(number);
    }



    //StartUI作動
    void start_Exit() {
        Exit_Ui.start_Exit();
    }

    //ステージ選択UI作動
    　void start_Stage() {
        Stage_Select_UI.start_Stage();
    }

    //武器タブオープン
    public void weapon_tab_open()
    {
        if (Exit_mode || Stage_mode) {
            return;
        }
        mode_change = true;

        //W_FL_Up.backflame_update(mode_change);
        weapon_number_update();
        set_weapon_list();
        set_indicate_list();
        W_T_B_number_update();
        indicate();
        E_text_set();

        weapon_enable();

        viewweapon_enable();



        camera_update();

        UI_enable();

        //SE.clip = SE_clip[2];
        //SE.volume = 0.75f;
        //SE.Play();
    }

    //武器タブクローズ
    public void weapon_tab_close()
    {
        mode_change = false;
        if (weapon_slot < assemble_status.my_weapon_number.Length)
        {
            weapon_number_update();
        }
        //W_FL_Up.backflame_update(mode_change);
        all_W_UI_enable();
        weapon_enable();
        viewweapon_enable();
        all_E_text_clear();
        text_update();
        camera_update();

        if (weapon_slot < assemble_status.my_weapon_number.Length)
        {
            now_status_update(assemble_status.my_weapon_number[weapon_slot]);
        }

        save_status_update();

        if (weapon_slot < assemble_status.my_weapon_number.Length)
        {
            status_text_update(assemble_status.my_weapon_number[weapon_slot], false);
        }

        SE.clip = SE_clip[3];
        SE.volume = 0.75f;
        SE.Play();
    }

    //StartUIオープン
    public void start_UI_open() {
        start_Exit();
        Exit_mode = true;

        SE.clip = SE_clip[2];
        SE.volume = 0.75f;
        SE.Play();
    }

    //StartUIのYESNOチェンジ
    public void start_UI_set() {
        if (Exit_Ui.Exit_select)
        {
            Exit_Ui.Exit_select = false;
        }
        else
        {
            Exit_Ui.Exit_select = true;
        }
        Exit_Ui.set_back_flame();

        SE.clip = SE_clip[1];
        SE.volume = 0.75f;
        SE.Play();
    }

    //StageUIオープンクローズ
    public void stage_UI_set(bool select) {
        if (select)
        {
            Debug.Log("stage");
            SE.clip = SE_clip[2];
            SE.volume = 0.75f;
            SE.Play();
            start_Stage();
            Stage_Select_UI.Stage_select = true;
            Stage_mode = true;
            Exit_Ui.Exit_mode = false;
            //修正開始
            //Application.LoadLevel("loading_world");
            //SceneManager.LoadScene("loading_world");
        }
        else {
            start_Exit();
            Exit_Ui.Exit_select = true;
            Exit_mode = false;

            SE.clip = SE_clip[3];
            SE.volume = 0.75f;
            SE.Play();
        }
    }
    //武器スロット移動
    public void weapon_slot_move()
    {

        if (weapon_slot > assemble_status.my_weapon_number.Length - 1)
        {
            weapon_slot = 0;
        }
        else if (weapon_slot < 0)
        {
            weapon_slot = assemble_status.my_weapon_number.Length - 1;
        }

        if (weapon_slot == 2 || weapon_slot == 3)
        {
            shoulder_weapon_select = true;
        }
        else {
            shoulder_weapon_select = false;
        }

        center_update();

        weapon_number_update();


        set_weapon_list();
        set_indicate_list();

        weapon_enable();

        viewweapon_enable();

        text_update();

        now_status_update(assemble_status.my_weapon_number[weapon_slot]);

        save_status_update();

        status_text_update(assemble_status.my_weapon_number[weapon_slot], false);

        camera_update();

        /*
        //Startボタンにいない
        if (weapon_slot < assemble_status.my_weapon_number.Length)
        {
            center_update();

            weapon_number_update();


            set_weapon_list();
            set_indicate_list();

            weapon_enable();

            viewweapon_enable();

            text_update();

            now_status_update(assemble_status.my_weapon_number[weapon_slot]);

            save_status_update();

            status_text_update(assemble_status.my_weapon_number[weapon_slot], false);

            camera_update();
        }
        //Startボタンにいる
        else {
            //W_FL_Up.flame_update(assemble_status.my_weapon_number.Length);
            camera_update();
        }
        */

        SE.clip = SE_clip[1];
        SE.volume = 0.75f;
        SE.Play();
    }

    //武器タブ移動
    public void weapon_tab_move() {
        /*
        if (weapon_number > weapon.Length - 1)
        {
            weapon_number = 0;
        }
        else if (weapon_number < 0) {
            weapon_number = weapon.Length - 1;
        }
        */


        if (!shoulder_weapon_select && weapon_number > weapon.Length - 1 - shoulder_weapon_variation)
        {
            weapon_number = 0;
        }
        else if (!shoulder_weapon_select && weapon_number < 0)
        {
            weapon_number = weapon.Length - 1 - shoulder_weapon_variation;
        }
        else if (shoulder_weapon_select && weapon_number > weapon.Length - 1)
        {
            weapon_number = weapon.Length - shoulder_weapon_variation;
        }
        else if (shoulder_weapon_select && weapon_number < weapon.Length - shoulder_weapon_variation) {
            weapon_number = weapon.Length - 1;
        }

        set_indicate_list();
        //W_T_B_number_update();
        indicate();
        E_text_set();
        //my_weapon_number_update();

        weapon_enable();

        viewweapon_enable();

        slihoutte_update();

        text_update();

        status_text_update(weapon_number, true);

        camera_update();

        SE.clip = SE_clip[1];
        SE.volume = 0.75f;
        SE.Play();
    }

    //YESNO移動
    public void stage_UI_move() {
        if (Stage_Select_UI.Stage_select)
        {
            Stage_Select_UI.Stage_select = false;
        }
        else
        {
            Stage_Select_UI.Stage_select = true;
        }
        Stage_Select_UI.set_back_flame();

        SE.clip = SE_clip[1];
        SE.volume = 0.75f;
        SE.Play();
    }
    public void stage_UI_select_change(bool select) {
        if (select)
        {
        }
        else {
            Exit_Ui.Exit_mode = false;
        }
        
    }
    //武器装備更新
    public void weapon_tab_decision() {
        //new
        my_weapon_number_update();
        E_text_set();
        slihoutte_update();
        save_status_update();
        status_text_update(weapon_number, true);

        SE.clip = SE_clip[0];
        SE.volume = 0.75f;
        SE.Play();

        /*
        if (E_weapon_search(weapon_number))
        {
            my_weapon_number_update();
            E_text_set();
            slihoutte_update();
            save_status_update();
            status_text_update(weapon_number, true);

            SE.clip = SE_clip[0];
            SE.volume = 0.75f;
            SE.Play();
        }
        else {
            SE.clip = SE_clip[4];
            SE.volume = 0.75f;
            SE.Play();
        }
        */
    }

    //ステージロード
    public void stage_load(bool select) {
        SE.clip = SE_clip[2];
        SE.volume = 0.75f;
        SE.Play();
        if (select)
        {
            Stage_number.stage_number = 0;
            //Application.LoadLevel("loading_world");
            //SceneManager.LoadScene("loading_world");
        }
        else {
            Stage_number.stage_number = 0;
        }
        SceneManager.LoadScene("loading_world");
    }

    //十字キー、アナログパッドボタン化
    string crossorstick_buttondown_system()
    {
        //■■■■■■■■■
        //↓
        if (cross_vec_save.y == 0 && Input.GetAxis("Cross_Vertical") >= 1.0f)
        {
            cross_vec_save.y = 1.0f;
            return "up";
        }
        else if (stick_vec_save.y == 0 && Input.GetAxis("Vertical") <= -0.8f)
        {
            stick_vec_save.y = -1.0f;
            return "up";
        }

        //■■■■■■■■■
        //↑
        else if (cross_vec_save.y == 0 && Input.GetAxis("Cross_Vertical") <= -1.0f)
        {
            cross_vec_save.y = -1.0f;
            return "down";

        }
        else if (stick_vec_save.y == 0 && Input.GetAxis("Vertical") >= 0.8f)
        {
            stick_vec_save.y = 1.0f;
            return "down";
        }

        //■■■■■■■■■
        //→
        else if (cross_vec_save.x == 0 && Input.GetAxis("Cross_Horizontal") <= -1.0f)
        {
            cross_vec_save.x = -1.0f;
            return "right";

        }
        else if (stick_vec_save.x == 0 && Input.GetAxis("Horizontal") >= 0.8f)
        {
            stick_vec_save.x = 1.0f;
            Debug.Log("right");
            return "right";
        }

        //■■■■■■■■■
        //←
        else if (cross_vec_save.x == 0 && Input.GetAxis("Cross_Horizontal") >= 1.0f)
        {
            cross_vec_save.x = 1.0f;
            return "left";

        }
        else if (stick_vec_save.x == 0 && Input.GetAxis("Horizontal") <= -0.8f)
        {
            stick_vec_save.x = -1.0f;
            Debug.Log("left");
            return "left";
        }



        //■■■■■■■■■■■■■■■■■■
        //リセット
        //■■■■■■■■■
        //↓
        else if (cross_vec_save.y == 1.0f && Input.GetAxis("Cross_Vertical") <= 0.0f)
        {
            cross_vec_save.y = 0.0f;
        }
        else if (stick_vec_save.y == 1.0f && Input.GetAxis("Vertical") <= 0.0f)
        {
            Debug.Log("reset ↓");
            stick_vec_save.y = 0.0f;
        }

        //■■■■■■■■■
        //↑
        else if (cross_vec_save.y == -1.0f && Input.GetAxis("Cross_Vertical") >= 0.0f)
        {
            cross_vec_save.y = 0.0f;
        }
        else if (stick_vec_save.y == -1.0f && Input.GetAxis("Vertical") >= 0.0f)
        {
            Debug.Log("reset ↑");
            stick_vec_save.y = 0.0f;
        }
        //■■■■■■■■■
        //→
        else if (cross_vec_save.x == -1.0f && Input.GetAxis("Cross_Horizontal") >= 0.0f)
        {
            cross_vec_save.x = 0.0f;
        }
        else if (stick_vec_save.x == 1.0f && Input.GetAxis("Horizontal") <= 0.0f)
        {
            Debug.Log("reset →");
            stick_vec_save.x = 0.0f;
        }

        //■■■■■■■■■
        //←
        else if (cross_vec_save.x == 1.0f && Input.GetAxis("Cross_Horizontal") <= 0.0f)
        {
            cross_vec_save.x = 0.0f;
        }
        else if (stick_vec_save.x == -1.0f && Input.GetAxis("Horizontal") >= 0.0f)
        {
            Debug.Log("reset ←");
            stick_vec_save.x = 0.0f;
        }


        return null;
    }

    void Awake() {
        W_UI_In = this.GetComponent<weapon_UI_indicate>();
        As_Status = GameObject.Find("Assemble_manage").GetComponent<assemble_status>();
        As_W_En = GameObject.Find("Assemble_manage").GetComponent<assemble_weapon_enable>();
        Camera_Manage = camera_obj.GetComponent<camera_manage>();
        //Camera_Manage = GameObject.Find("Main Camera").GetComponent<camera_manage>();
        View_enable = GameObject.Find("Assemble_manage").GetComponent<viewweapon_enable>();
        Weapon_Sil = this.GetComponent<weapon_silhouette>();
        Weapon_Text = this.GetComponent<weapon_text>();
        Status_Text = this.GetComponent<status_text>();
        Exit_Ui = this.GetComponent<Exit_UI>();
        Stage_Select_UI = this.GetComponent<stage_select_UI>();
        SE = this.gameObject.AddComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
       


        all_W_UI_enable();
        //W_FL_Up.backflame_update(mode_change);
        center_update();



        for (int i = 0; i < 4; i++) {
            weapon_slot = i;
            weapon_number_update();
        }

        weapon_slot = 0;
        slihoutte_update();

        weapon_number_update();

        all_E_text_clear();

        text_update();

        status_text_getcomponent();

        now_status_update(assemble_status.my_weapon_number[weapon_slot]);

        save_status_update();

        status_text_update(assemble_status.my_weapon_number[weapon_slot],false);

        viewweapon_enable();



        weapon_tab_open();

    }
	// Update is called once per frame
	void Update () {

        string input_button = crossorstick_buttondown_system();
        if (input_button == "right")
        {
            if (!Exit_mode)
            {
                weapon_slot += 1;
                weapon_slot_move();
                weapon_tab_move();
                slihoutte_update();
            }
            else {
                start_UI_set();
            }
        }
        else if (input_button == "left")
        {
            if (!Exit_mode)
            {
                weapon_slot -= 1;
                weapon_slot_move();
                weapon_tab_move();
                slihoutte_update();
            }
            else {
                start_UI_set();
            }
        }
        else if (input_button == "up") {
            if (!Exit_mode)
            {
                weapon_number += 1;
                weapon_tab_move();
            }
        }
        else if (input_button == "down")
        {
            if (!Exit_mode)
            {
                weapon_number -= 1;
                weapon_tab_move();
            }
        }

        if (Input.GetButtonDown("button2"))
        {
            if (!Exit_mode)
            {
                weapon_tab_decision();
            }
            else if (Exit_mode && Exit_Ui.Pop_state)
            {
                if (Exit_Ui.Exit_select)
                {
                    //stage_UI_set(Exit_Ui.Exit_select);
                    stage_load(Stage_Select_UI.Stage_select);
                }
                else
                {
                    start_Exit();
                    Exit_Ui.Exit_select = true;
                    Exit_mode = false;

                    SE.clip = SE_clip[3];
                    SE.volume = 0.75f;
                    SE.Play();
                }
            }
        }
        else if (Input.GetButtonDown("button9") && !Exit_mode) {
            start_UI_open();
            camera_update();
        }

        if (Input.GetButtonDown("button1") && !Exit_mode) {
            Destroy(BGM);
            SceneManager.LoadScene("title_world");
        }

        /*
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //武器スロットチェンジ
        //武器スロット時の操作
        if (mode_change == false && Exit_mode == false && Stage_mode == false)
        {
            //武器スロットと武器の切り替え
            if ((input_button  == "right"|| Input.GetButtonDown("button2")) && weapon_slot < assemble_status.my_weapon_number.Length)
            {
                weapon_tab_open();
            } 

            
            
            //武器スロット+1移動
            else if (input_button == "up")
            {
                weapon_slot += 1;

                weapon_slot_move();
                
            }
            //武器スロット-1移動
            else if (input_button == "down")
            {
                weapon_slot -= 1;

                weapon_slot_move();
                
            }
            
            //スタートボタンにいる時
            else if (Input.GetButtonDown("button2") && weapon_slot == assemble_status.my_weapon_number.Length)
            {
                start_UI_open();
                
            }

            //やめる
            if (Input.GetButtonDown("button1"))
            {
                Destroy(BGM);
                SceneManager.LoadScene("title_world");
            }
        }


        //武器切り替え
        else if (mode_change == true && Exit_mode == false && Stage_mode == false)
        {

            //武器スロットと武器の切り替え
            if (input_button == "left" || Input.GetButtonDown("button1"))
            {
                weapon_tab_close();
            }
            //武器ナンバー+1移動
            else if (input_button == "up")
            {
                Debug.Log("+");
                weapon_number += 1;

                weapon_tab_move();
                
            }
            //武器ナンバー-1移動
            else if (input_button == "down")
            {
                Debug.Log("-");

                weapon_number -= 1;

                weapon_tab_move();

                Debug.Log(weapon_number);


            }
            //武器決定
            else if (Input.GetButtonDown("button2"))
            {
                if (weapon_slot < assemble_status.my_weapon_number.Length && mode_change)
                {
                    
                    weapon_tab_decision();
                }
            }
        }


        //決定UI
        else if (Exit_Ui.Exit_mode == true)
        {
            if ((input_button == "left" || input_button == "right") && Exit_Ui.Pop_state)
            {
                start_UI_set();
                
            }
            else if (Input.GetButtonDown("button2") && Exit_Ui.Pop_state)
            {
                if (Exit_Ui.Exit_select)
                {
                    //stage_UI_set(Exit_Ui.Exit_select);
                    stage_load(Stage_Select_UI.Stage_select);
                }
                else
                {
                    start_Exit();
                    Exit_Ui.Exit_select = true;
                    Exit_mode = false;

                    SE.clip = SE_clip[3];
                    SE.volume = 0.75f;
                    SE.Play();
                }
            }
        }

        //ステージUI
        else if (Stage_Select_UI.Stage_mode == true) {
            if ((input_button == "left" || input_button == "right") && Stage_Select_UI.Pop_state)
            {
                stage_UI_move();
                
            }
            else if (Input.GetButtonDown("button2") && Stage_Select_UI.Pop_state)
            {

                stage_load(Stage_Select_UI.Stage_select);
                
            }
        }
        */
    }
}
