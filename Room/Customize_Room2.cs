using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class Room2
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
public class Room_Moudle2
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

public class Customize_Room2 : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool is_lock;

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
    }

    public void Check_Module()
    {
        string[] value = new string[]
        {
            "Wall01_Part", "Wall_Accessory01_Part", "Ground_Accessory01_Part",
            "Wall02_Part", "Wall_Accessory02_Part", "Ground_Accessory02_Part",
            "Ground_Part", "Chair01_Part", "Chair02_Part", "Table_Part", "Table_Accessory01_Part"
        };

        if (!is_lock) {
            for (int i = 0; i < value.Length; i++) {
                Transform location = GameObject.Find(value[i].ToString()).transform; // 부모지정

                string path;

                switch (value[i].ToString()) {
                    case "Wall01_Part":
                        path = "photon_room_prefab/Wall01_All/Wall01/" + room_moudle.wall01[room.wall01].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Wall_Accessory01_Part":
                        path = "photon_room_prefab/Wall01_All/Wall_Accessory01/" + room_moudle.wall_accessory01[room.wall_accessory01].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Ground_Accessory01_Part":
                        path = "photon_room_prefab/Wall01_All/Ground_Accessory01/" + room_moudle.ground_accessory01[room.ground_accessory01].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Wall02_Part":
                        path = "photon_room_prefab/Wall02_All/Wall02/" + room_moudle.wall02[room.wall02].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Wall_Accessory02_Part":
                        path = "photon_room_prefab/Wall02_All/Wall_Accessory02/" + room_moudle.wall_accessory02[room.wall_accessory02].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Ground_Accessory02_Part":
                        path = "photon_room_prefab/Wall02_All/Ground_Accessory02/" + room_moudle.ground_accessory02[room.ground_accessory02].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Ground_Part":
                        path = "photon_room_prefab/Ground_All/Ground/" + room_moudle.ground[room.ground].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Chair01_Part":
                        path = "photon_room_prefab/Ground_All/Chair01/" + room_moudle.chair01[room.chair01].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Chair02_Part":
                        path = "photon_room_prefab/Ground_All/Chair02/" + room_moudle.chair02[room.chair02].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Table_Part":
                        path = "photon_room_prefab/Ground_All/Table/" + room_moudle.table[room.table].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                    case "Table_Accessory01_Part":
                        path = "photon_room_prefab/Ground_All/Table_Accessory01/" + room_moudle.table_accessory01[room.table_accessory01].name;
                        PhotonNetwork.Instantiate(path, location.position, Quaternion.identity);
                        break;
                }
            }

            is_lock = true;
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            stream.SendNext(is_lock);
        } else {
            is_lock = (bool)stream.ReceiveNext();
        }
    }
}
