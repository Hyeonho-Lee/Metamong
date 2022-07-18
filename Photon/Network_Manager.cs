using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Network_Manager : MonoBehaviourPunCallbacks
{
    public Text info;
    public Text info_name;

    public GameObject follow_camera;
    public GameObject player;

    private string username;

    private GameObject console;
    Auth_Controller ac;

    private void Start()
    {
        console = GameObject.Find("Title_Console").gameObject;
        ac = console.GetComponent<Auth_Controller>();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        info.text = PhotonNetwork.NetworkClientState.ToString();
        info_name.text = PhotonNetwork.LocalPlayer.NickName;

        if (ac.userName != null) {
            username = ac.userName;
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = username;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.Instantiate(player.name, new Vector3(Random.Range(-5f, 3f), -2, 0), Quaternion.identity);
    }
}
