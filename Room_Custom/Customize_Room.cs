using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Room
{
    public int wall01; //벽1
    public int wall_accessory01; //벽 장식1
    public int ground_accessory01; //장식1

    public int wall02; //벽2
    public int wall_accessory02; //벽 장식2
    public int ground_accessory02; //장식2

    public int ground; //바닥 타일
    public int chair01; //상담사 의자
    public int chair02; //유저 의자
    public int table; //테이블
    public int table_accessory01; //테이블 위 장식물
}

[System.Serializable]
public class Room_Moudle
{
    public List<GameObject> wall01;
    public List<GameObject> wall_accessory01;
    public List<GameObject> ground_accessory01;

    public List<GameObject> wall02;
    public List<GameObject> wall_accessory02;
    public List<GameObject> ground_accessory02;

    public List<GameObject> ground;
    public List<GameObject> chair01;
    public List<GameObject> chair02;
    public List<GameObject> table;
    public List<GameObject> table_accessory01;

    public void Reset_Input()
    {
        wall01 = new List<GameObject>();
        wall_accessory01 = new List<GameObject>();
        ground_accessory01 = new List<GameObject>();

        wall02 = new List<GameObject>();
        wall_accessory02 = new List<GameObject>();
        ground_accessory02 = new List<GameObject>();

        ground = new List<GameObject>();
        chair01 = new List<GameObject>();
        chair02 = new List<GameObject>();
        table = new List<GameObject>();
        table_accessory01 = new List<GameObject>();
    }
}

public class Customize_Room : MonoBehaviour
{
    public Room room = new Room();
    public Room_Moudle room_moudle = new Room_Moudle(); //아이템 개체 생성

    private Room_Console RC;
    private Auth_Controller ac;

    private void Start()
    {
        RC = GetComponent<Room_Console>();

        if (GameObject.Find("Send_Info")) {
            ac = GameObject.Find("Send_Info").GetComponent<Auth_Controller>();
        }

        Check_Prefab();
        Check_Module();
    }

    public void Change_Module(int index)
    {
        string index_string;

        if (index >= 0) {
            index_string = index.ToString();
        } else {
            index_string = EventSystem.current.currentSelectedGameObject.name;
        }

        int index_int = int.Parse(index_string);

        switch(RC.label.text) {
            case "벽지1":
                Change_Part("Wall01_Part", index_int);
                break;
            case "벽장식1":
                Change_Part("Wall_Accessory01_Part", index_int);
                break;
            case "장식1":
                Change_Part("Ground_Accessory01_Part", index_int);
                break;
            case "벽지2":
                Change_Part("Wall02_Part", index_int);
                break;
            case "벽장식2":
                Change_Part("Wall_Accessory02_Part", index_int);
                break;
            case "장식2":
                Change_Part("Ground_Accessory02_Part", index_int);
                break;
            case "타일":
                Change_Part("Ground_Part", index_int);
                break;
            case "의자1":
                Change_Part("Chair01_Part", index_int);
                break;
            case "의자2":
                Change_Part("Chair02_Part", index_int);
                break;
            case "책상":
                Change_Part("Table_Part", index_int);
                break;
            case "소품":
                Change_Part("Table_Accessory01_Part", index_int);
                break;
        }
    }

    private void Change_Part(string value, int index)
    {
        Transform location = GameObject.Find(value.ToString()).transform; // 부모지정

        for (int j = 0; j < location.childCount; j++) {
            Destroy(location.GetChild(0).gameObject);
        }

        switch (value) {
            case "Wall01_Part":
                Instantiate(room_moudle.wall01[index], location);
                room.wall01 = index;
                ac.rc_user.wall01 = index;
                break;
            case "Wall_Accessory01_Part":
                Instantiate(room_moudle.wall_accessory01[index], location);
                room.wall_accessory01 = index;
                ac.rc_user.wall_accessory01 = index;
                break;
            case "Ground_Accessory01_Part":
                Instantiate(room_moudle.ground_accessory01[index], location);
                room.ground_accessory01 = index;
                ac.rc_user.ground_accessory01 = index;
                break;
            case "Wall02_Part":
                Instantiate(room_moudle.wall02[index], location);
                room.wall02 = index;
                ac.rc_user.wall02 = index;
                break;
            case "Wall_Accessory02_Part":
                Instantiate(room_moudle.wall_accessory02[index], location);
                room.wall_accessory02 = index;
                ac.rc_user.wall_accessory02 = index;
                break;
            case "Ground_Accessory02_Part":
                Instantiate(room_moudle.ground_accessory02[index], location);
                room.ground_accessory02 = index;
                ac.rc_user.ground_accessory02 = index;
                break;
            case "Ground_Part":
                Instantiate(room_moudle.ground[index], location);
                room.ground = index;
                ac.rc_user.ground = index;
                break;
            case "Chair01_Part":
                Instantiate(room_moudle.chair01[index], location);
                room.chair01 = index;
                ac.rc_user.chair01 = index;
                break;
            case "Chair02_Part":
                Instantiate(room_moudle.chair02[index], location);
                room.chair02 = index;
                ac.rc_user.chair02 = index;
                break;
            case "Table_Part":
                Instantiate(room_moudle.table[index], location);
                room.table = index;
                ac.rc_user.table = index;
                break;
            case "Table_Accessory01_Part":
                Instantiate(room_moudle.table_accessory01[index], location);
                room.table_accessory01 = index;
                ac.rc_user.table_accessory01 = index;
                break;
        }
    }

    public void Check_Module()
    {
        string[] value = new string[]
        {
            "Wall01_Part", "Wall_Accessory01_Part", "Ground_Accessory01_Part",
            "Wall02_Part", "Wall_Accessory02_Part", "Ground_Accessory02_Part",
            "Ground_Part", "Chair01_Part", "Chair02_Part", "Table_Part", "Table_Accessory01_Part"
        };

        for (int i = 0; i < value.Length; i++) {
            Transform location = GameObject.Find(value[i].ToString()).transform; // 부모지정

            for (int j = 0; j < location.childCount; j++) {
                Destroy(location.GetChild(0).gameObject);
            }

            switch (value[i].ToString()) {
                case "Wall01_Part":
                    Instantiate(room_moudle.wall01[room.wall01], location);
                    break;
                case "Wall_Accessory01_Part":
                    Instantiate(room_moudle.wall_accessory01[room.wall_accessory01], location);
                    break;
                case "Ground_Accessory01_Part":
                    Instantiate(room_moudle.ground_accessory01[room.ground_accessory01], location);
                    break;
                case "Wall02_Part":
                    Instantiate(room_moudle.wall02[room.wall02], location);
                    break;
                case "Wall_Accessory02_Part":
                    Instantiate(room_moudle.wall_accessory02[room.wall_accessory02], location);
                    break;
                case "Ground_Accessory02_Part":
                    Instantiate(room_moudle.ground_accessory02[room.ground_accessory02], location);
                    break;
                case "Ground_Part":
                    Instantiate(room_moudle.ground[room.ground], location);
                    break;
                case "Chair01_Part":
                    Instantiate(room_moudle.chair01[room.chair01], location);
                    break;
                case "Chair02_Part":
                    Instantiate(room_moudle.chair02[room.chair02], location);
                    break;
                case "Table_Part":
                    Instantiate(room_moudle.table[room.table], location);
                    break;
                case "Table_Accessory01_Part":
                    Instantiate(room_moudle.table_accessory01[room.table_accessory01], location);
                    break;
            }
        }
    }

    public void Check_Prefab()
    {
        GameObject[] wall01 = Resources.LoadAll<GameObject>("room_prefab/Wall01_All/Wall01");
        GameObject[] wall_accessory01 = Resources.LoadAll<GameObject>("room_prefab/Wall01_All/Wall_Accessory01");
        GameObject[] ground_accessory01 = Resources.LoadAll<GameObject>("room_prefab/Wall01_All/Ground_Accessory01");

        GameObject[] wall02 = Resources.LoadAll<GameObject>("room_prefab/Wall02_All/Wall02");
        GameObject[] wall_accessory02 = Resources.LoadAll<GameObject>("room_prefab/Wall02_All/Wall_Accessory02");
        GameObject[] ground_accessory02 = Resources.LoadAll<GameObject>("room_prefab/Wall02_All/Ground_Accessory02");

        GameObject[] ground = Resources.LoadAll<GameObject>("room_prefab/Ground_All/Ground");
        GameObject[] chair01 = Resources.LoadAll<GameObject>("room_prefab/Ground_All/Chair01");
        GameObject[] chair02 = Resources.LoadAll<GameObject>("room_prefab/Ground_All/Chair02");
        GameObject[] table = Resources.LoadAll<GameObject>("room_prefab/Ground_All/Table");
        GameObject[] table_accessory01 = Resources.LoadAll<GameObject>("room_prefab/Ground_All/Table_Accessory01");

        room_moudle.wall01 = wall01.OfType<GameObject>().ToList();
        room_moudle.wall_accessory01 = wall_accessory01.OfType<GameObject>().ToList();
        room_moudle.ground_accessory01 = ground_accessory01.OfType<GameObject>().ToList();

        room_moudle.wall02 = wall02.OfType<GameObject>().ToList();
        room_moudle.wall_accessory02 = wall_accessory02.OfType<GameObject>().ToList();
        room_moudle.ground_accessory02 = ground_accessory02.OfType<GameObject>().ToList();

        room_moudle.ground = ground.OfType<GameObject>().ToList();
        room_moudle.chair01 = chair01.OfType<GameObject>().ToList();
        room_moudle.chair02 = chair02.OfType<GameObject>().ToList();
        room_moudle.table = table.OfType<GameObject>().ToList();
        room_moudle.table_accessory01 = table_accessory01.OfType<GameObject>().ToList();
    }
}
