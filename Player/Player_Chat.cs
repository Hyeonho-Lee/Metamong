using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Player_Chat : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject BubbleSpeechObject; //말풍선 오브젝트
    public GameObject UpdateText; // 말풍선 텍스트

    private Text ChatText;
    private InputField ChatInputField; //체팅 하는 필드

    private float chat_time;
    private float chat_realtime;
    private bool is_chat;

    private PhotonView PV;
    private Photon_Player PP;
    private TextMeshProUGUI TMP;

    private void Awake()
    {
        ChatInputField = GameObject.Find("Chat_InputField").GetComponent<InputField>();
        ChatText = GameObject.Find("All_Chat").GetComponent<Text>();

        PV = GetComponent<PhotonView>();
        PP = GetComponent<Photon_Player>();
        TMP = UpdateText.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        chat_time = 5.0f;
        chat_realtime = chat_time;

        BubbleSpeechObject.SetActive(false);
    }

    private void Update()
    {
        if (!is_chat) {
            if (chat_realtime <= chat_time) {
                chat_realtime += Time.deltaTime;
            } else {
                is_chat = true;
            }
        }else {
            BubbleSpeechObject.SetActive(false);
        }

        if (photonView.IsMine) {
            if (ChatInputField.text != "" && ChatInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Return)) {
                PV.RPC("SendMassage", RpcTarget.All, ChatInputField.text);
                PV.RPC("ChatRPC", RpcTarget.All, "<color=black><b>" + PP.PlayerNickName + " :</b></color> " + ChatInputField.text);
                ChatInputField.text = "";
            }
        }
    }

    [PunRPC]
    private void SendMassage(string msg)
    {
        is_chat = false;
        chat_realtime = 0;
        TMP.text = msg;
        BubbleSpeechObject.SetActive(true);
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        ChatText.text += msg + "\n";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
