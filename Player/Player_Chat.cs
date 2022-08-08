using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class Player_Chat : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject BubbleSpeechObject; //말풍선 오브젝트
    public GameObject UpdateText; // 말풍선 텍스트
    public GameObject Nickname_Text;
    public GameObject Emotional_Panel;
    public GameObject Icons;

    public Sprite[] All_Icons;
    public Image voice_on;

    public int emotion_value;

    public bool is_tab;
    public bool is_motion;

    private Text ChatText;
    private InputField ChatInputField; //체팅 하는 필드
    private Animator Chat_Ani;
    private Animator Player_Ani;
    private Player_Console PC;
    private Player_Cursor P_Cursor;

    private float chat_time;
    private float chat_realtime;
    private bool is_chat;
    private bool focus_chat;
    private bool focus_break;

    private PhotonView PV;
    private PhotonVoiceView PVV;
    private Photon_Player PP;
    private Player_Movement PM;
    private TextMeshProUGUI TMP;

    private void Awake()
    {
        ChatInputField = GameObject.Find("Chat_InputField").GetComponent<InputField>();
        ChatText = GameObject.Find("All_Chat").GetComponent<Text>();
        Chat_Ani = GameObject.Find("Chat_Panel").GetComponent<Animator>();
        PC = GameObject.Find("World_Console").GetComponent<Player_Console>();
        P_Cursor = GameObject.Find("World_Console").GetComponent<Player_Cursor>();
        Player_Ani = GetComponent<Animator>();

        PV = GetComponent<PhotonView>();
        PVV = GetComponent<PhotonVoiceView>();
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

        Icons.GetComponent<Image>().sprite = null;
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
                Icons.GetComponent<Image>().sprite = null;
                PV.RPC("SendMassage", RpcTarget.All, ChatInputField.text);
                PV.RPC("ChatRPC", RpcTarget.All, "<color=white><b> [유저] " + PP.PlayerNickName + " :</b></color>  " + ChatInputField.text);
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

            if (PC.button_value != 0) {
                Start_Icons(PC.button_value - 1);
                PC.button_value = 0;
            }

            if (P_Cursor.is_voice) {
                PV.RPC("VoiceIcon_on", RpcTarget.All);
            }else {
                PV.RPC("VoiceIcon_off", RpcTarget.All);
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

    public void Start_Icons(int value)
    {
        PV.RPC("SendMassage", RpcTarget.All, "");
        PV.RPC("SendIcon", RpcTarget.All, value);
    }

    [PunRPC]
    private void SendMassage(string msg)
    {
        is_chat = false;
        chat_realtime = 0;
        TMP.text = msg;
        BubbleSpeechObject.SetActive(true);
        Icons.GetComponent<Image>().sprite = null;
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        ChatText.text += msg + "\n";
    }

    [PunRPC]
    private void SendIcon(int value)
    {
        Icons.GetComponent<Image>().sprite = All_Icons[value];
    }

    [PunRPC]
    private void VoiceIcon_on()
    {
        voice_on.enabled = true;
    }

    [PunRPC]
    private void VoiceIcon_off()
    {
        voice_on.enabled = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
