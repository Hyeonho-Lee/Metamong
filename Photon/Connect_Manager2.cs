using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Connect_Manager2 : MonoBehaviourPunCallbacks
{
    public string spawn_name;
    public string room_index;

    private bool is_spawn;
    private bool is_host;

    public Text info;
    public GameObject player;
    public InputField ChatInput;

    private GameObject Player_Object;

    private PhotonView PV;
    private Auth_Controller AC;
    private Room_Managers RM;

    private void Start()
    {
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();

        PV = GetComponent<PhotonView>();
        RM = GameObject.Find("Room_Console").GetComponent<Room_Managers>();

        is_spawn = false;
    }

    private void Update()
    {
        info.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public void Connect()
    {
        //PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (!is_host && !is_spawn && GameObject.Find("Send_Info") != null) {
            GameObject empty = GameObject.Find("Send_Info");
            AC = empty.gameObject.GetComponent<Auth_Controller>();
            spawn_name = AC.world_position;
            room_index = AC.room_index;
            Destroy(empty);
            is_spawn = true;
            is_host = true;
            StartCoroutine(RM.Load_RoomCustom());
            print("호스트 들어옴");
        }

        if (is_host && !is_spawn && GameObject.Find("Send_Info") != null) {
            GameObject empty = GameObject.Find("Send_Info");
            AC = empty.gameObject.GetComponent<Auth_Controller>();
            spawn_name = AC.world_position;
            room_index = AC.room_index;
            Destroy(empty);
            is_spawn = true;
            print("유저 들어옴");
        }

        PhotonNetwork.JoinRoom(room_index + "room");
    }

    public override void OnJoinedRoom()
    {
        if (spawn_name == "room_world") {
            Spawn_Player(new Vector3(-2.3f, 0, -3.8f));
        }
    }

    private void Spawn_Player(Vector3 spawn)
    {
        Player_Object = PhotonNetwork.Instantiate(player.name, spawn, Quaternion.identity);
        is_spawn = true;
        spawn_name = "";
        ChatInput.text = "";
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        CreateRoom(room_index);
    }

    public override void OnLeftRoom()
    {
        Destroy(Player_Object);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Destroy(Player_Object);
        //print(cause);
        Connect();
    }

    public void CreateRoom(string name)
    {
        print(name + "room");
        PhotonNetwork.CreateRoom(name + "room", new RoomOptions { MaxPlayers = 2 });
    }
}