using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Connect_Manager : MonoBehaviourPunCallbacks
{
    public string spawn_name;

    private bool is_spawn;

    public Text info;

    public GameObject follow_camera;
    public GameObject loading_camera;
    public GameObject player;

    private GameObject Player_Object;

    public InputField ChatInput;

    private PhotonView PV;


    private void Start()
    {
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();

        PV = GetComponent<PhotonView>();

        is_spawn = false;
    }

    private void Update()
    {
        info.text = PhotonNetwork.NetworkClientState.ToString();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRoom("main_room");
    }

    public override void OnJoinedRoom()
    {
        spawn_name = "spawn";

        if (!is_spawn && GameObject.Find("Send_Spawn") != null) {
            GameObject empty = GameObject.Find("Send_Spawn");
            Auth_Controller AC = empty.gameObject.GetComponent<Auth_Controller>();
            spawn_name = AC.world_position;
            Destroy(empty);
            is_spawn = true;
        }

        if (spawn_name == "spawn") {
            //Spawn_Player(new Vector3(0, 0, -40.0f));
            Spawn_Player(new Vector3(160.0f, -4.5f, 290.0f));
        } else if (spawn_name == "character_store") {
            Spawn_Player(new Vector3(200.0f, -4.5f, 245.0f));
        } else if (spawn_name == "room_store") {
            Spawn_Player(new Vector3(160.0f, -4.5f, 290.0f));
        } else if (spawn_name == "house_store") {
            Spawn_Player(new Vector3(97.0f, -4.5f, 230.0f));
        }
    }

    private void Spawn_Player(Vector3 spawn)
    {
        Player_Object = PhotonNetwork.Instantiate(player.name, spawn, Quaternion.identity);
        is_spawn = true;
        spawn_name = "";
        loading_camera.SetActive(false);
        ChatInput.text = "";
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("main_room", new RoomOptions { MaxPlayers = 50 });
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Destroy(Player_Object);
    }

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //print("새로운 플레이어 접속");
    //}
    //onplayerenterdroom player타입에 아무거나 
}
