using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Photon_Player : MonoBehaviourPunCallbacks, IPunObservable
{
    public Text player_username;
    public bool is_load;

    private PhotonView PV;
    private Connect_Manager CM;
    private Character_Info CI;

    private void Awake()
    {
        CM = GameObject.Find("Server_Console").GetComponent<Connect_Manager>();
        CI = GetComponent<Character_Info>();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine) {
            GameObject camera = Instantiate(CM.follow_camera, this.transform);
        }
    }

    private void Update()
    {
        if (CI.userName != null && !is_load) {
            PhotonNetwork.LocalPlayer.NickName = CI.userName;
            player_username.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
            is_load = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
