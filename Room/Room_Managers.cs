using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Room_Managers : MonoBehaviour
{
    public string uid;
    public string username;

    //public Button leftroomBnt;

    private Customize_Room2 customize_room;
    private Auth_Controller ac;
    private PhotonView PV;
    private Connect_Manager2 CM2;

    private void Start()
    {
        CM2 = GameObject.Find("Server_Console").GetComponent<Connect_Manager2>();
        customize_room = GetComponent<Customize_Room2>();
        ac = GetComponent<Auth_Controller>();
        PV = GetComponent<PhotonView>();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        //leftroomBnt.onClick.AddListener(() => LeftRoom());

        //StartCoroutine(Load_RoomCustom());
    }

    GameObject empty;

    public IEnumerator Load_RoomCustom()
    {
        ac.Get_User_Info();
        ac.Get_Room_Custom();

        if (GameObject.Find("Send_Spawn")) {
            empty = GameObject.Find("Send_Spawn");
            Auth_Controller AC = empty.gameObject.GetComponent<Auth_Controller>();
            username = AC.user.username;
            uid = AC.user.uid;
            ac.rc_user = AC.rc_user;
        }

        yield return new WaitForSeconds(2.0f);

        Destroy(empty);

        customize_room.room.wall01 = ac.rc_user.wall01;
        customize_room.room.wall_accessory01 = ac.rc_user.wall_accessory01;
        customize_room.room.ground_accessory01 = ac.rc_user.ground_accessory01;

        customize_room.room.wall02 = ac.rc_user.wall02;
        customize_room.room.wall_accessory02 = ac.rc_user.wall_accessory02;
        customize_room.room.ground_accessory02 = ac.rc_user.ground_accessory02;

        customize_room.room.ground = ac.rc_user.ground;
        customize_room.room.chair01 = ac.rc_user.chair01;
        customize_room.room.chair02 = ac.rc_user.chair02;
        customize_room.room.table = ac.rc_user.table;
        customize_room.room.table_accessory01 = ac.rc_user.table_accessory01;

        customize_room.Check_Module();
    }

    public void LeftRoom()
    {
        GameObject Send_Info = new GameObject("Send_Info");
        Send_Info.AddComponent<Auth_Controller>();
        Send_Info.name = "Title_Console";

        Auth_Controller info = Send_Info.GetComponent<Auth_Controller>();
        info.userName = ac.userName;
        info.localId = ac.localId;
        info.idToken = ac.idToken;
        info.world_position = "spawn";

        info.user = ac.user;
        info.cc_user = ac.cc_user;
        info.cc_db = ac.cc_db;

        GameObject Send_Spawn = Instantiate(Send_Info); ;
        Send_Spawn.name = "Send_Spawn";

        DontDestroyOnLoad(Send_Info);
        DontDestroyOnLoad(Send_Spawn);

        PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        CM2.OnLeftRoom();

        SceneManager.LoadScene("Main_World");
    }

    [PunRPC]
    public void DestroyRPC()
    {
        Destroy(this.gameObject);
    }
}
