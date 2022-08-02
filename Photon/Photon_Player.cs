using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Photon_Player : MonoBehaviourPunCallbacks, IPunObservable
{
    public string PlayerNickName;

    public GameObject player_username;
    public bool is_load;

    private PhotonView PV;
    private Connect_Manager CM;
    private Character_Info CI;
    private TextMeshProUGUI TMP;

    private void Awake()
    {
        CM = GameObject.Find("Server_Console").GetComponent<Connect_Manager>();

        CI = GetComponent<Character_Info>();
        PV = GetComponent<PhotonView>();

        TMP = player_username.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (PV.IsMine) {
            GameObject camera = Instantiate(CM.follow_camera, GameObject.Find("All_Camera").transform);
            camera.transform.GetChild(1).GetComponent<Player_Camera>().Player_Transform = this.transform;
            //camera.transform.parent = this.transform.parent;
        }
    }

    private void Update()
    {
        if (CI.userName != null && !is_load) {
            PhotonNetwork.LocalPlayer.NickName = CI.userName;
            PlayerNickName = CI.userName;
            TMP.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
            is_load = true;
        }

        if (PhotonNetwork.LocalPlayer.NickName == "") {
            PhotonNetwork.LocalPlayer.NickName = CI.userName;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
