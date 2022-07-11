using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Auth_Controller ac;

    private void Start()
    {
        if (GameObject.Find("Title_Console")) {
            ac = GameObject.Find("Title_Console").GetComponent<Auth_Controller>();
        } else {
            print("오프라인 입니다.");
        }

        Check_Module();
    }

    public void Check_Module()
    {
        string[] wall01_value = new string[]
        { 
            "Wall01", "Wall_Accessory01", "Ground_Accessory01"
        };

        string[] wall02_value = new string[]
        {
            "Wall02", "Wall_Accessory02", "Ground_Accessory02",
        };

        string[] ground_value = new string[]
        {
            "Ground", "Chair01", "Chair02", "Table", "Table_Accessory01"//바닥
        };
    }
}
