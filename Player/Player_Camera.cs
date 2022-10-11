using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player_Camera : MonoBehaviourPunCallbacks, IPunObservable
{
    //[System.NonSerialized]
    public float mouse_horizontal, mouse_vertical, whell_value;

    public float camera_distance;
    private float input_y;

    [System.NonSerialized]
    public Vector3 camera_angle, camera_dir, player_dir;
    public Vector3 dir_nor;

    public Transform Reset_Transform;
    public Transform Player_Transform;

    private Transform Camera_Arm;
    //public Transform Player_Canvas;
    private GameObject Main_Camera;

    private RaycastHit camera_hit;
    private Player_Movement PM;
    private Player_Console PC;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        whell_value = 1f;
        input_y = 3.0f;

        Camera_Arm = this.transform.parent;
        Main_Camera = this.gameObject;

        PM = Player_Transform.GetComponent<Player_Movement>();
        PC = GameObject.Find("World_Console").GetComponent<Player_Console>();
        //Player_Canvas = Player_Transform.GetChild(9).transform;
    }

    private void Update()
    {
        Camera_Arm.position = Player_Transform.transform.position + new Vector3(0f, 6.2f, 0f);
        //Player_Canvas.rotation = Quaternion.LookRotation(dir_nor);
        Rotate_Canvas();

        Mouse_Movement();
        Input_whell();
        Raycast_Camera();

        if (!PM.is_room) {
            PM.player_dir = player_dir;
        }
    }

    private void Mouse_Movement()
    {
        mouse_horizontal = Input.GetAxis("Mouse X");
        mouse_vertical = Input.GetAxis("Mouse Y");

        camera_angle = Camera_Arm.rotation.eulerAngles;
        float x = camera_angle.x - mouse_vertical;

        if (x < 180.0f) {
            x = Mathf.Clamp(x, -1.0f, 50.0f);
        } else {
            x = Mathf.Clamp(x, 335.0f, 361.0f);
        }

        Camera_Arm.rotation = Quaternion.Euler(x, camera_angle.y + mouse_horizontal, camera_angle.z);

        camera_dir = Player_Transform.transform.position + new Vector3(0f, input_y, 0f) - Reset_Transform.transform.position;
        dir_nor = camera_dir.normalized;
        player_dir.Set(dir_nor.x, 0, dir_nor.z);
        Debug.DrawRay(Player_Transform.transform.position + new Vector3(0f, input_y, 0f), camera_dir * 2f, Color.red);
    }

    private void Input_whell()
    {
        float whell = Input.GetAxisRaw("Mouse ScrollWheel");
        if (whell > 0) {
            if (whell_value < 8.5)
                whell_value = whell_value + 0.5f;
        } else if (whell < 0) {
            if (whell_value > 1)
                whell_value = whell_value - 0.5f;
        }
    }

    private void Raycast_Camera()
    {
        if (Physics.Linecast(Player_Transform.transform.position + new Vector3(0f, input_y, 0f), Reset_Transform.transform.position, out camera_hit)) {
            camera_distance = Vector3.Distance(Player_Transform.transform.position + new Vector3(0f, input_y, 0f), camera_hit.point);
            if (camera_hit.point != Vector3.zero && camera_distance >= 3.5f) {
                Main_Camera.transform.position = camera_hit.point + (dir_nor * 2.0f);
            }
        }else {
            camera_distance = Vector3.Distance(Player_Transform.transform.position + new Vector3(0f, input_y, 0f), Reset_Transform.transform.position);
            Main_Camera.transform.position = Reset_Transform.transform.position + (dir_nor * whell_value);
        }

        Debug.DrawRay(Reset_Transform.transform.position, camera_dir, Color.blue);
    }

    public void Rotate_Canvas()
    {
        for (int i = 0; i < PC.all_player.Length; i++) {
            if (PC.all_player[i] != null) {
                Transform Player_Canvas = PC.all_player[i].transform.GetChild(9).transform;
                if (dir_nor != Vector3.zero) {
                    Player_Canvas.rotation = Quaternion.LookRotation(dir_nor);
                }
            }
        }

        for (int i = 0; i < PC.All_Canvas.Length; i++) {
            if (PC.All_Canvas[i] != null) {
                Transform All_Canvas = PC.All_Canvas[i].transform;
                if (dir_nor != Vector3.zero) {
                    All_Canvas.rotation = Quaternion.LookRotation(dir_nor);
                }
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
