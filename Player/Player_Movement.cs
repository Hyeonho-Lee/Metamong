using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Player_Movement : MonoBehaviourPunCallbacks, IPunObservable
{
    public float horizontal;
    public float vertical;

    public bool is_move;
    public bool is_dash;
    public bool is_room;

    private float t;
    private float timeStart;
    private float timeDuration;

    public float move_speed;
    private float first_speed;
    private float end_speed;

    public Vector3 movement;
    public Vector3 animation_movement;
    public Vector3 player_dir;
    public Vector3 player_forward;
    public Vector3 impact;
    private Vector3 dir_forward, dir_f_right, dir_right, dir_b_right, dir_back, dir_b_left, dir_left, dir_f_left;

    Vector3 Current_Pos;
    Quaternion Current_Rot;

    private Transform Bip001;

    private CharacterController cc;
    private Animator motion;
    private PhotonView PV;
    private Character_Info CI;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        motion = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
        CI = GetComponent<Character_Info>();

        //Bip001 = this.transform.GetChild(0);
        Bip001 = this.transform;
    }

    void Start()
    {
        is_move = false;

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Main_World") is_room = false;
        if (scene.name == "Room_World") is_room = true;
    }

    void Update()
    {
        if (PV.IsMine) {
            Check_Input();
        }
    }

    void FixedUpdate()
    {
        if (PV.IsMine) {
            Move();
        }else if ((transform.position - Current_Pos).sqrMagnitude >= 100.0f) {
            this.transform.position = Current_Pos;
            this.transform.rotation = Current_Rot;
        }else {
            this.transform.position = Vector3.Lerp(this.transform.position, Current_Pos, Time.smoothDeltaTime * 10.0f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Current_Rot, Time.smoothDeltaTime * 10.0f);
        }
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && is_move) {
            is_dash = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || !is_move) {
            timeStart = Time.time;
            t = 0;
            move_speed = (1 - t) * first_speed + t * end_speed;
            is_dash = false;
        }

        if (is_room) {
            player_dir = new Vector3(1, 0, 1);
            timeDuration = 1.0f;
            first_speed = 0.5f;
            end_speed = 1.5f;
        }else {
            if (is_dash) {
                timeDuration = 2.0f;
                first_speed = 6.0f;
                end_speed = 11.0f;
            }else {
                timeDuration = 2.0f;
                first_speed = 4.0f;
                end_speed = 7.0f;
            }
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
        motion.SetBool("is_dash", is_dash);

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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);
        }else {
            Current_Pos = (Vector3)stream.ReceiveNext();
            Current_Rot = (Quaternion)stream.ReceiveNext();
        }
    }
}
