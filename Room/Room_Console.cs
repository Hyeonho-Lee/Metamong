using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Room_Console : MonoBehaviour
{
    public GameObject Location;
    public GameObject RoomPanel;
    public Text label;

    private int state;
    public string label_text;

    private Room_Camera room_camera;

    void Start()
    {
        room_camera = GetComponent<Room_Camera>();

        state = 0;

        if (state == 0){
            Location.SetActive(false);
            RoomPanel.SetActive(false);
        }
    }

    void Update()
    {
        Input_key();

        if (!room_camera.is_camera) {
            switch (label_text) {
                case "����1":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_wall01;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    label_text = "";
                    break;
                case "�����1":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_wall_accessory01;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    label_text = "";
                    break;
                case "���1":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_ground_accessory01;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    label_text = "";
                    break;
                case "����2":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_wall02;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    label_text = "";
                    break;
                case "�����2":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_wall_accessory02;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    label_text = "";
                    break;
                case "���2":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_ground_accessory02;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    label_text = "";
                    break;
                case "Ÿ��":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_ground;
                    room_camera.camera_zoom = room_camera.camera_zoom_2;
                    label_text = "";
                    break;
                case "����1":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_chair01;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    label_text = "";
                    break;
                case "����2":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_chair02;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    label_text = "";
                    break;
                case "å��":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_table;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
                    label_text = "";
                    break;
                case "��ǰ":
                    room_camera.is_camera = true;
                    room_camera.camera_empty = room_camera.pos_table_accessory;
                    room_camera.camera_zoom = room_camera.camera_zoom_1;
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

    public void Test()
    {
        Debug.Log("Click test");
    }
}
