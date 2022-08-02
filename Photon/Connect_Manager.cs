using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Connect_Manager : MonoBehaviourPunCallbacks
{
    public Text info;

    public GameObject follow_camera;
    public GameObject loading_camera;
    public GameObject player;

    public InputField ChatInput;

    private PhotonView PV;


    private void Start()
    {
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();

        PV = GetComponent<PhotonView>();
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
        loading_camera.SetActive(false);
        PhotonNetwork.Instantiate(player.name, new Vector3(Random.Range(-5f, 3f), -2, 0), Quaternion.identity);
        ChatInput.text = "";
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("main_room", new RoomOptions { MaxPlayers = 50 });
    }

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
        //print("새로운 플레이어 접속");
    //}
    //onplayerenterdroom player타입에 아무거나 
}
