using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_move : MonoBehaviour {
    public Transform enemy;
    public Transform start, goal;
    int hierarchy = 0;
    public float[] hierarchies = new float[3];
    Vector3 save_goal;
    Rigidbody rb;
    public GameObject cube;
    public GameObject cube_obj;


    //player_moves
    public GameObject Camera_obj;
    public Vector3 magnification = new Vector3(1.0f, 1.0f, 1.0f);
    float moveForceMultiplier = 1;
    bool boost_energy_consume = false;
    Coroutine hover_change_cor;


    //スクリプト
    private chara_status Chara_Status;
    private move_speed Move_Speed;
    private enemy_damage_count E_damage_count;

    // パスと、座標リスト。使い回す
    NavMeshPath path = null;
    Vector3[] positions;
    int currentPositionIndex = 0;

    float save_distance;

    //ジャンプ
    Vector3 jump_move(Vector3 vec)
    {
        vec.y = Move_Speed.rising_speed * 4.0f;
        return vec;
    }
    //ホバー
    Vector3 rising_move(Vector3 vec, float magnification)
    {
        vec.y = magnification * (Move_Speed.rising_speed);
        return vec;
    }
    public void hover_change()
    {
        if (hover_change_cor != null)
        {
            StopCoroutine(hover_change_cor);
        }
        hover_change_cor = StartCoroutine(h_change_cor());
    }
    IEnumerator h_change_cor()
    {
        yield return new WaitForSeconds(0.5f);
        if (Chara_Status.hover == 1)
        {
            Chara_Status.hover = 0;
        }
    }

    void move_direction(Vector3 targetPosition) {
        Vector3 EtoG = goal.transform.position - enemy.transform.position;
        Vector3 EtoT = targetPosition - enemy.transform.position;
        EtoG.Normalize();
        EtoT.Normalize();
        Vector3 vec = Vector3.Normalize(EtoG - EtoT);
        float max = Mathf.Abs(vec.x);
        Chara_Status.moving_state[0] = "forward";
        /*
        if (Mathf.Abs(vec.x) < Mathf.Abs(vec.z)) {
            if (vec.x > 0)
            {
                Chara_Status.moving_state[0] = "forward";
            }
            else {
                Chara_Status.moving_state[0] = "back";
            }
        }
        else
        {
            if (vec.y > 0)
            {
                Chara_Status.moving_state[0] = "left";
            }
            else {
                Chara_Status.moving_state[0] = "right";
            }
        }
        */

    }

    void speed_deceleration()
    {
        for (int i = 0; i < 2; i++)
        {
            float save_speed = Move_Speed.speed[i];
            Move_Speed.speed[i] -= Move_Speed.deceleration * Move_Speed.speed[i] / Move_Speed.speed_max[i];
            if (Move_Speed.speed[i] < Move_Speed.normal_speed_max)
            {
                Move_Speed.speed[i] = Move_Speed.normal_speed_max;
            }
            if (float.IsNaN(Move_Speed.speed[i]))
            {
                Move_Speed.speed[i] = save_speed;
            }
        }
    }

    void speed_max_deceleration(float speed)
    {
        for (int i = 0; i < Move_Speed.speed_max.Length; i++)
        {
            if (Move_Speed.speed_max[i] > speed)
            {
                Move_Speed.speed_max[i] -= Move_Speed.deceleration * Move_Speed.speed_max[i] / speed;
            }
            else {
                Move_Speed.speed_max[i] = speed;
            }
        }
    }

    //クイックブースト
    Vector3 quick_boost_move(Vector3 move_vec,Vector3 vec)
    {
        float x = 0;
        float y = 0;
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            Debug.Log(random);
            y = Move_Speed.quick_boost_speed_max;
        }
        else if (random == 1)
        {
            Debug.Log(random);
            x = Move_Speed.quick_boost_speed_max;
        }
        else if (random == 2)
        {
            Debug.Log(random);
            x = Move_Speed.quick_boost_speed_max * -1;
        }
        move_vec = y * vec + x * Vector3.Cross(vec, Vector3.up); //前後方向+左右方向
        Chara_Status.quick_boost = true;
        return move_vec;
    }

    void search_hierarchy() {
        int save_hierarchy = hierarchy;
        for (int i = 0; i < hierarchies.Length; i++) {
            if (enemy.transform.position.y > hierarchies[i] && hierarchy != i) {
                hierarchy = i;
            }
        }
        if (save_hierarchy != hierarchy) {
            root_update();
        }
    }
    void root_update() {
        path.ClearCorners();
        Vector3 goal_pos = goal.transform.position;
        Vector3 start_pos = start.transform.position;
        float y = 0;
        Debug.Log("hierarchy = " + hierarchy);
        if (hierarchy == 1) {
            y = 10;
        }
        else if (hierarchy == 2) {
            y = 20;
        }
        goal_pos.y = y;
        start_pos.y = y;
        NavMesh.CalculatePath(start_pos, goal_pos, NavMesh.AllAreas, path);
        if (path.corners.Length == 0)
        {
            Debug.Log("length 0");
            positions = new Vector3[1];
            positions[0] = goal_pos;
        }
        else {
            positions = new Vector3[path.corners.Length];
            path.GetCornersNonAlloc(positions);
        }
        Debug.Log(positions.Length);
        currentPositionIndex = 0;
        save_goal = goal.transform.position;
        if (positions.Length > 1)
        {
            save_distance = Vector3.Distance(enemy.transform.position, positions[1]);
        }
        else {
            save_distance = Vector3.Distance(enemy.transform.position, positions[0]);
        }
    }

    void Awake()
    {
        path = new NavMeshPath();
        rb = this.gameObject.GetComponent<Rigidbody>();
        Chara_Status = this.GetComponent<chara_status>();
        Move_Speed = this.GetComponent<move_speed>();
        E_damage_count = this.GetComponent<enemy_damage_count>();
    }
    void Start()
    {

        // パスの計算
        root_update();
        /*
        Vector3 goal_pos = goal.transform.position;
        Vector3 start_pos = start.transform.position;
        goal_pos.y = 0;
        start_pos.y = 0;
        NavMesh.CalculatePath(start_pos, goal_pos, NavMesh.AllAreas, path);
        positions = new Vector3[path.corners.Length];
        path.GetCornersNonAlloc(positions);
        save_goal = goal.transform.position;
        save_distance = Vector3.Distance(enemy.transform.position, positions[0]);
        cube_obj = Instantiate(cube, positions[0], Quaternion.identity);
        */
    }
    void Update() {

        Chara_Status.quick_boost = false;
        Vector3 enemy_pos = enemy.transform.position;
        Vector3 targetPosition = positions[currentPositionIndex];
        targetPosition.y = transform.position.y;

        if (Vector3.Distance(enemy_pos, targetPosition) < 1f)
        {
            Debug.Log("change_pos");
            if (currentPositionIndex + 1 < positions.Length) {
                currentPositionIndex = currentPositionIndex + 1;
                targetPosition = positions[currentPositionIndex];
                targetPosition.y = transform.position.y;

            }
            
        }
        if (save_distance - Vector3.Distance(enemy.transform.position, targetPosition) < -0.5f)
        {
            Debug.Log("save = " + save_distance);
            Debug.Log("new = " + Vector3.Distance(enemy.transform.position, targetPosition));
            Debug.Log("new system");
            root_update();
        }
        else if (save_distance - Vector3.Distance(enemy.transform.position, targetPosition) > 0)
        {
            Debug.Log("before_distance_update = " + save_distance);
            Debug.Log("save = " + save_distance);
            Debug.Log("new = " + Vector3.Distance(enemy.transform.position, targetPosition));
            save_distance = Vector3.Distance(enemy.transform.position, targetPosition);
            Debug.Log("distance_update = " + save_distance);
        }
        //ターゲットを見る
        Vector3 look_pos = goal.transform.position;
        look_pos.y = transform.position.y;
        transform.LookAt(look_pos);


        Vector3 vec = this.gameObject.transform.position - Camera_obj.transform.position;
        vec.y = 0f;
        vec = vec.normalized;

        //移動
        Vector3 moveVector = Vector3.zero;
        Vector3 Subtraction_vec = rb.velocity;

        string moving_state;


        moveVector = targetPosition - transform.position;
        moveVector.Normalize();
        moveVector.y = 0;

        ////////////
        //クイックブースト
        if (E_damage_count.evasive)
        {
            moveVector =  quick_boost_move(moveVector, vec);
            root_update();
            E_damage_count.evasive = false;
            if (Chara_Status.moving_state[1] == "boost")
            {
                moving_state = "boost";
            }
            else
            {
                moving_state = "quick_boost";
            }
            /*
            moving_state = "quick_boost";
            quick_boost_move();
            */
        }
        //ブースト
        else if (Vector3.Distance(enemy.transform.position ,goal.transform.position) > 10)
        {

            float speed_magnification = 1;

            float boost_acceleration = Move_Speed.boost_acceleration;

            moving_state = "boost";

            //空中時かつブーストに消費するエネルギーよりエネルギーが少ないとき
            if (Chara_Status.ground_condition == "air" && Chara_Status.boost_energy <= Chara_Status.boost_consume_energy)
            {
                //足りない分だけ加速スピード減少
                speed_magnification = Chara_Status.boost_energy / Chara_Status.boost_consume_energy;
                //Move_Speed.speed_max = Move_Speed.boost_speed_max;
                moveForceMultiplier = 100;
                Debug.Log("not_energy");
            }

            //減少した加速スピードが歩きより遅い際
            if (speed_magnification * boost_acceleration < boost_acceleration / 10)
            {
                //歩き
                speed_deceleration();
                speed_max_deceleration(Move_Speed.normal_speed_max);
                //Move_Speed.speed_max = Move_Speed.normal_speed_max;//変更点
                moveForceMultiplier = 50;
                moving_state = "walk";
                Debug.Log("not_boost_walk");

            }
            else {
                //ブースト
                boost_acceleration *= speed_magnification;
                speed_max_deceleration(Move_Speed.boost_speed_max);
                //Move_Speed.speed_max = Move_Speed.boost_speed_max;//変更点
                moveForceMultiplier = 100;
                //Debug.Log("boost_succes");

            }
            Move_Speed.speed[0] += boost_acceleration;
            //Move_Speed.speed[1] += boost_acceleration;

            for (int i = 0; i < 2; i++)
            {
                if (Move_Speed.speed[i] > Move_Speed.speed_max[i])
                {
                    Move_Speed.speed[i] = Move_Speed.speed_max[i];
                }
            }
            magnification.x = 1.0f;
            magnification.z = 1.0f;
        }
        //通常(歩き)状態
        else {
            speed_deceleration();
            speed_max_deceleration(Move_Speed.normal_speed_max);
            moveForceMultiplier = 50;
            magnification.x = 1.0f;
            magnification.z = 1.0f;
            moving_state = "walk";
        }



        ////////////



        //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, speed * Time.deltaTime);

        //上昇
        if (goal.transform.position.y - start.transform.position.y > 5f && Chara_Status.boost_energy > 30f)
        {
            if (Chara_Status.hover == 0)
            {
                Chara_Status.hover = 1;
            }
            hover_change();

            if (Chara_Status.hover == 1)
            {
                if (Chara_Status.ground_condition == "terrain")
                {
                    moveVector = jump_move(moveVector);
                }
                else if (Chara_Status.ground_condition == "air")
                {
                    float rising_magnification = 1;
                    if (Chara_Status.boost_energy < Chara_Status.boost_consume_energy)
                    {
                        rising_magnification = Chara_Status.boost_energy / Chara_Status.quick_boost_consume_energy;
                    }
                    moveVector = rising_move(moveVector, rising_magnification);

                    /*
                    if (!move_now)
                    {
                        moving_state = "rising";
                    }
                    */

                    Chara_Status.boost_energy -= Chara_Status.boost_consume_energy;
                    boost_energy_consume = true;
                }
            }
            else if (Chara_Status.hover == 2)
            {
                float rising_magnification = 1;
                if (Chara_Status.boost_energy < Chara_Status.boost_consume_energy)
                {
                    rising_magnification = Chara_Status.boost_energy / Chara_Status.quick_boost_consume_energy;
                }
                moveVector = rising_move(moveVector, rising_magnification);
                /*
                if (!move_now)
                {
                    moving_state = "rising";
                }
                */
                Chara_Status.boost_energy -= Chara_Status.boost_consume_energy;
                boost_energy_consume = true;
            }
        }


        //キャラ硬直時間減少
        if (Chara_Status.move_stun > 0)
        {
            Chara_Status.move_stun -= 1f;
        }

        //クイックブースト硬直時間減少
        if (Move_Speed.qboost_cool_time[0] > 0 || Move_Speed.qboost_cool_time[1] > 0 || Move_Speed.qboost_cool_time[2] > 0 || Move_Speed.qboost_cool_time[3] > 0)
        {
            for (int i = 0; i < Move_Speed.qboost_cool_time.Length; i++)
            {
                Move_Speed.qboost_cool_time[i] -= Time.deltaTime;
                if (Move_Speed.qboost_cool_time[i] < 0)
                {
                    Move_Speed.qboost_cool_time[i] = 0;
                }
            }
        }

        //ブーストエネルギー下限
        if (Chara_Status.boost_energy < 0)
        {
            Chara_Status.boost_energy = 0;
        }

        //ブースト回復
        if (Chara_Status.boost_energy < Chara_Status.boost_energy_max && !boost_energy_consume)
        {
            Chara_Status.boost_energy += Chara_Status.boost_recovery_speed;
            if (Chara_Status.boost_energy >= Chara_Status.boost_energy_max)
            {
                Chara_Status.boost_energy = Chara_Status.boost_energy_max;
            }
        }

        boost_energy_consume = false;

        Subtraction_vec.x *= magnification.x;
        Subtraction_vec.z *= magnification.z;

        if (Subtraction_vec.y < 0f)
        {
            Subtraction_vec.y = 0;
        }

        //移動
        rb.AddForce(Move_Speed.speed[0] * moveVector - Subtraction_vec);

        move_direction(targetPosition);
        Chara_Status.moving_state[1] = moving_state;

        search_hierarchy();

        if (Vector3.Distance(goal.transform.position, save_goal) > 5) {
            root_update();
        }
    }
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < positions.Length - 1; i++)
        {
            
            Gizmos.DrawLine(positions[i], positions[i + 1]);
        }
    }
}