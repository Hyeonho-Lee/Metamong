using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float horizontal;
    public float vertical;

    public bool is_move;

    private float t;
    private float timeStart;
    private float timeDuration = 2.0f;

    public float move_speed;
    private float first_speed;
    private float end_speed;

    public Vector3 movement;
    public Vector3 animation_movement;
    public Vector3 player_dir;
    public Vector3 player_forward;
    public Vector3 impact;
    private Vector3 dir_forward, dir_f_right, dir_right, dir_b_right, dir_back, dir_b_left, dir_left, dir_f_left;

    CharacterController cc;
    Animator motion;
    //TPSCamera tpscamera;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        motion = GetComponent<Animator>();
        //tpscamera = GameObject.Find("TPS_Camera").GetComponent<TPSCamera>();
    }

    void Start()
    {
        first_speed = 4.0f;
        end_speed = 7.0f;

        is_move = false;
        player_dir = Vector3.forward;
    }

    void Update()
    {
        Check_Input();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Check_Input()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0) {
            is_move = true;
        }else {
            is_move = false;
        }

        if (!is_move) {
            timeStart = Time.time;
            t = 0;
        }else {
            t = (Time.time - timeStart) / timeDuration;
        }

        if (t <= 1.0f) {
            move_speed = (1 - t) * first_speed + t * end_speed;
        }

        motion.SetFloat("horizontal", movement.x);
        motion.SetFloat("vertical", movement.z);

        //player_dir = tpscamera.player_dir.normalized;
    }

    void Move()
    {
        movement.Set(horizontal, 0, vertical);
        movement = movement.normalized;

        dir_forward = Quaternion.Euler(0f, 0f, 0f) * player_dir;
        dir_f_right = Quaternion.Euler(0f, 45f, 0f) * player_dir;
        dir_right = Quaternion.Euler(0f, 90f, 0f) * player_dir;
        dir_b_right = Quaternion.Euler(0f, 135f, 0f) * player_dir;
        dir_back = Quaternion.Euler(0f, 180f, 0f) * player_dir;
        dir_b_left = Quaternion.Euler(0f, 225f, 0f) * player_dir;
        dir_left = Quaternion.Euler(0f, 270f, 0f) * player_dir;
        dir_f_left = Quaternion.Euler(0f, 315f, 0f) * player_dir;

        if (movement.z == 0 && movement.x == 0)
        {
            cc.Move(movement * (move_speed * Time.deltaTime) * Time.deltaTime);
        }

        // ¾Õ¿À
        if ((movement.z > 0 && movement.z <= 1) && (movement.x > 0 && movement.x <= 1))
        {
            Move_Vector(dir_f_right, dir_f_right);
        }

        // ¾Õ¿Þ
        if ((movement.z > 0 && movement.z <= 1) && (movement.x < 0 && movement.x >= -1))
        {
            Move_Vector(dir_f_left, dir_f_left);
        }

        // µÚ¿Þ
        if ((movement.z < 0 && movement.z >= -1) && (movement.x < 0 && movement.x >= -1))
        {
            Move_Vector(dir_b_left, dir_b_left);
        }

        // µÚ¿À
        if ((movement.z < 0 && movement.z >= -1) && (movement.x > 0 && movement.x <= 1))
        {
            Move_Vector(dir_b_right, dir_b_right);
        }

        // ¾Õ
        if ((movement.z > 0 && movement.z <= 1) && (movement.x == 0))
        {
            Move_Vector(dir_forward, dir_forward);
        }

        // µÚ
        if ((movement.z < 0 && movement.z >= -1) && (movement.x == 0))
        {
            Move_Vector(dir_back, dir_back);
        }

        // ¿À
        if ((movement.x > 0 && movement.x <= 1) && (movement.z == 0))
        {
            Move_Vector(dir_right, dir_right);
        }

        // ¿Þ
        if ((movement.x < 0 && movement.x >= -1) && (movement.z == 0))
        {
            Move_Vector(dir_left, dir_left);
        }
    }

    void Move_Vector(Vector3 input_vector, Vector3 rotate_vector)
    {
        Quaternion newRotation = Quaternion.LookRotation(rotate_vector);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, 6.0f * Time.deltaTime);
        cc.Move(input_vector * (move_speed * Time.deltaTime));
    }
}
