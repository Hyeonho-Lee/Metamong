using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class Player_Chat : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject BubbleSpeechObject; //말풍선 오브젝트
    public GameObject UpdateText; // 말풍선 텍스트
    public GameObject Nickname_Text;
    public GameObject Emotional_Panel;

    public int emotion_value;

    public bool is_tab;
    public bool is_motion;

    private Text ChatText;
    private InputField ChatInputField; //체팅 하는 필드
    private Animator Chat_Ani;
    private Animator Player_Ani;

    private float chat_time;
    private float chat_realtime;
    private bool is_chat;
    private bool focus_chat;
    private bool focus_break;

    private PhotonView PV;
    private Photon_Player PP;
    private Player_Movement PM;
    private TextMeshProUGUI TMP;

    private void Awake()
    {
        ChatInputField = GameObject.Find("Chat_InputField").GetComponent<InputField>();
        ChatText = GameObject.Find("All_Chat").GetComponent<Text>();
        Chat_Ani = GameObject.Find("Chat_Panel").GetComponent<Animator>();
        Player_Ani = GetComponent<Animator>();

        PV = GetComponent<PhotonView>();
        PP = GetComponent<Photon_Player>();
        PM = GetComponent<Player_Movement>();
        TMP = UpdateText.GetComponent<TextMeshProUGUI>();

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name != "Main_World") {
            BubbleSpeechObject.SetActive(false);
            Nickname_Text.SetActive(false);
            Emotional_Panel.SetActive(false);
        }
    }

    private void Start()
    {
        emotion_value = 0;
        chat_time = 5.0f;
        chat_realtime = chat_time;

        BubbleSpeechObject.SetActive(false);
        Emotional_Panel.SetActive(false);
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

        if (ChatInputField.isFocused) {
            focus_chat = true;
        } else {
            focus_chat = false;
        }

        if (photonView.IsMine) {

            if (focus_chat) {
                PM.horizontal = 0;
                PM.vertical = 0;
                StartCoroutine(UI_Play_Ani("1"));
            }else {
                StartCoroutine(UI_Play_Ani("2"));
            }

            if (is_motion) {
                PM.horizontal = 0;
                PM.vertical = 0;
            }

            if (is_tab && !focus_chat && !is_motion) {
                Emotional_Panel.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    StartCoroutine(Player_Play_Ani("e1", 4.15f));
                }

                if (Input.GetKeyDown(KeyCode.Alpha2)) {
                    StartCoroutine(Player_Play_Ani("e2", 1.5f));
                }

                if (Input.GetKeyDown(KeyCode.Alpha3)) {
                    StartCoroutine(Player_Play_Ani("e3", 4.4f));
                }

                if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    StartCoroutine(Player_Play_Ani("e4", 13.45f));
                }

                if (Input.GetKeyDown(KeyCode.Alpha5)) {
                    StartCoroutine(Player_Play_Ani("e5", 3.45f));
                }
            } else {
                Emotional_Panel.SetActive(false);
            }

            if (ChatInputField.text != "" && ChatInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Return)) {
                PV.RPC("SendMassage", RpcTarget.All, ChatInputField.text);
                PV.RPC("ChatRPC", RpcTarget.All, "<color=white><b>" + PP.PlayerNickName + " :</b></color> " + ChatInputField.text);
                ChatInputField.text = "";
                focus_break = false;
            }else if (!focus_chat && Input.GetKeyDown(KeyCode.Return)) {
                ChatInputField.Select();
                focus_break = false;
            }

            if (Input.GetKeyDown(KeyCode.Tab)) {
                is_tab = true;
            }

            if (Input.GetKeyUp(KeyCode.Tab)) {
                is_tab = false;
            }
        }
    }

    IEnumerator UI_Play_Ani(string name)
    {
        if (!focus_break) {
            focus_break = true;
            Chat_Ani.Play(name);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Player_Play_Ani(string name, float delay)
    {
        if (!is_motion) {
            is_motion = true;
            Player_Ani.SetBool(name, is_motion);
            //Player_Ani.Play(name);
            yield return new WaitForSeconds(delay);
            is_motion = false;
            Player_Ani.SetBool(name, is_motion);
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
