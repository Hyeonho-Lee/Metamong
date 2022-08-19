using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Room_Console : MonoBehaviour
{
    public GameObject Location;
    public GameObject RoomPanel;
    public Text label;

    private int state;
    public string label_text;

    private Room_Camera room_camera;
    private Customize_Room_Bnt cr;
    private RoomManager rm;

    private void Awake()
    {
        room_camera = GetComponent<Room_Camera>();
        cr = GetComponent<Customize_Room_Bnt>();
        rm = GetComponent<RoomManager>();
    }

    private void Start()
    {
        state = 0;

        if (state == 0){
            Location.SetActive(false);
            RoomPanel.SetActive(false);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        if (state == 0) {
            state = 1;

            Location.SetActive(true);
            RoomPanel.SetActive(false);
        }
    }

    void Update()
    {
        //Input_key();

        if (!room_camera.is_camera) {
            switch (label_text) {
                case "벽지1":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_wall01;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    rm.Create_Button(cr.room_moudles.wall01.Count, label_text);
                    label_text = "";
                    break;
                case "벽장식1":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_wall_accessory01;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    rm.Create_Button(cr.room_moudles.wall_accessory01.Count, label_text);
                    label_text = "";
                    break;
                case "장식1":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_ground_accessory01;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    rm.Create_Button(cr.room_moudles.ground_accessory01.Count, label_text);
                    label_text = "";
                    break;
                case "벽지2":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_wall02;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    rm.Create_Button(cr.room_moudles.wall02.Count, label_text);
                    label_text = "";
                    break;
                case "벽장식2":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_wall_accessory02;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    rm.Create_Button(cr.room_moudles.wall_accessory02.Count, label_text);
                    label_text = "";
                    break;
                case "장식2":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_ground_accessory02;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    rm.Create_Button(cr.room_moudles.ground_accessory02.Count, label_text);
                    label_text = "";
                    break;
                case "타일":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_ground;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    rm.Create_Button(cr.room_moudles.ground.Count, label_text);
                    label_text = "";
                    break;
                case "의자1":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_chair01;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    rm.Create_Button(cr.room_moudles.chair01.Count, label_text);
                    label_text = "";
                    break;
                case "의자2":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_chair02;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    rm.Create_Button(cr.room_moudles.chair02.Count, label_text);
                    label_text = "";
                    break;
                case "책상":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_table;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    rm.Create_Button(cr.room_moudles.table.Count, label_text);
                    label_text = "";
                    break;
                case "소품":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_table_accessory;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    rm.Create_Button(cr.room_moudles.table_accessory01.Count, label_text);
                    label_text = "";
                    break;
            }
        }
    }

    public void Input_key()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(state==0)
            {
                state = 1;
                
                Location.SetActive(true);
                RoomPanel.SetActive(false);
            }
        }
    }

    public void Exit1()
    {
        state = 0;
        Location.SetActive(false);
    }

    public void Click_Loc()
    {
        state = 2;
        Location.SetActive(false);

        label_text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
        label.text = label_text;

        RoomPanel.SetActive(true);
    }
    
    public void Exit2()
    {
        state = 1;

        Location.SetActive(true);
        RoomPanel.SetActive(false);

        if (room_camera.is_camera) {
            room_camera.is_camera = false;
            room_camera.camera_empty = room_camera.camera_position;
        }
    }

    /*public void Create_Btn(int count)
    {
        Transform parents = GameObject.Find("Content").transform;

        for (int i = 0; i < count; i++) {
            GameObject btn = Instantiate(button, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            btn.name = i.ToString();
            btn.transform.SetParent(parents);
            btn.transform.localScale = new Vector3(1, 1, 1);
            btn.GetComponent<Button>().onClick.AddListener(() => Test());
        }
    }*/

    public void Change_Scene()
    {
        GameObject Send_Info = GameObject.Find("Send_Info");
        Send_Info.name = "Title_Console";

        GameObject Send_Spawn = Instantiate(Send_Info); ;
        Send_Spawn.name = "Send_Spawn";

        DontDestroyOnLoad(Send_Info);
        DontDestroyOnLoad(Send_Spawn);

        SceneManager.LoadScene("Main_World");
    }
}
