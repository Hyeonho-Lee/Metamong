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
    public GameObject voice_onn;
    public GameObject sit_panel;
    public GameObject sit_panel2;

    private GameObject chair01;
    private GameObject chair02;

    public Sprite[] All_Icons;
    public Image voice_on;

    public int emotion_value;

    public bool is_tab;
    public bool is_motion;

    private Text ChatText;
    private InputField ChatInputField; //체팅 하는 필드
    private Scrollbar ChatScrol; //체팅 하는 필드
    private Animator Chat_Ani;
    private Animator Player_Ani;
    private Player_Console PC;

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
    private Auth_Controller AC;

    private void Awake()
    {
        ChatInputField = GameObject.Find("Chat_InputField").GetComponent<InputField>();
        ChatText = GameObject.Find("All_Chat").GetComponent<Text>();
        Chat_Ani = GameObject.Find("Chat_Panel").GetComponent<Animator>();

        Player_Ani = GetComponent<Animator>();

        PV = GetComponent<PhotonView>();
        PVV = GetComponent<PhotonVoiceView>();
        PP = GetComponent<Photon_Player>();
        PM = GetComponent<Player_Movement>();
        TMP = UpdateText.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Room_World") {
            PC = GameObject.Find("Room_Console").GetComponent<Player_Console>();
            AC = GameObject.Find("Room_Console").GetComponent<Auth_Controller>();
            StartCoroutine(Find_Chair(2.0f));
        } else if (scene.name == "Main_World")  {
            PC = GameObject.Find("World_Console").GetComponent<Player_Console>();
            AC = GameObject.Find("World_Console").GetComponent<Auth_Controller>();
        }

        emotion_value = 0;
        chat_time = 5.0f;
        chat_realtime = chat_time;

        BubbleSpeechObject.SetActive(false);
        Emotional_Panel.SetActive(false);
        sit_panel.SetActive(false);
        sit_panel2.SetActive(false);

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

            if (!focus_chat && !is_motion) {
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
            }

            if (ChatInputField.text != "" && ChatInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Return)) {
                Icons.GetComponent<Image>().sprite = null;
                PV.RPC("SendMessage", RpcTarget.All, ChatInputField.text);

                if (AC.user.is_counselor && AC.user.is_counselor_check && !AC.user.is_admin) {
                    PV.RPC("ChatRPC", RpcTarget.All, "<color=#FF9983>[상담사]</color> " + AC.user.username + " :  " + ChatInputField.text);
                } else if (AC.user.is_user && !AC.user.is_admin && !AC.user.is_counselor) {
                    PV.RPC("ChatRPC", RpcTarget.All, "<color=#FFFFFF>[유저]</color> " + AC.user.username + " :  " + ChatInputField.text);
                } else if (AC.user.is_admin) {
                    PV.RPC("ChatRPC", RpcTarget.All, "<color=#FF9983>[어드민]</color> " + AC.user.username + " :  " + ChatInputField.text);
                } else {
                    PV.RPC("ChatRPC", RpcTarget.All, "<color=#FFFFFF>[인증중]</color> " + AC.user.username + " :  " + ChatInputField.text);
                }

                //print(GameObject.Find("Scrollbar Vertical"));

                if (GameObject.Find("Scrollbar Vertical") != null) {
                    ChatScrol = GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
                    ChatScrol.value = 0;
                }

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

            if (chair01 != null) {
                Vector3 position = new Vector3(1.389f, 0.275f, 0.28f);
                float distance = Vector3.Distance(this.transform.position, position);
                //print("의자1 = " + distance.ToString());
                if (distance <= 1.2f) {
                    sit_panel2.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.F)) {
                        if (is_motion) {
                            StartCoroutine(Player_SitOff_Ani());
                            //print("일남");
                        }
                    }
                } else {
                    sit_panel2.SetActive(false);
                }

                if (distance <= 0.6f) {
                    if (Input.GetKeyDown(KeyCode.F)) {
                        if (!is_motion) {
                            StartCoroutine(Player_SitOn_Ani());
                            //print("앉음");
                        }
                    }
                }
            }

            if (chair02 != null) {
                Vector3 position = new Vector3(-1.21f, 0.275f, -0.078f);
                float distance = Vector3.Distance(this.transform.position, position);
                //print("의자2 = " + distance.ToString());
                if (distance <= 1.2f) {
                    sit_panel.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.F)) {
                        if (is_motion) {
                            StartCoroutine(Player_SitOff_Ani());
                            //print("일남");
                        }
                    }
                } else {
                    sit_panel.SetActive(false);
                }

                if (distance <= 0.6f) {
                    if (Input.GetKeyDown(KeyCode.F)) {
                        if (!is_motion) {
                            StartCoroutine(Player_SitOn_Ani());
                            //print("앉음");
                        }
                    }
                }
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

    IEnumerator Player_SitOn_Ani()
    {
        is_motion = true;
        Player_Ani.SetBool("stand_to_sit", true);
        yield return new WaitForSeconds(1.5f);
        Player_Ani.SetBool("siting", true);
        Player_Ani.SetBool("stand_to_sit", false);
    }

    IEnumerator Player_SitOff_Ani()
    {
        Player_Ani.SetBool("sit_to_stand", true);
        yield return new WaitForSeconds(1.5f);
        Player_Ani.SetBool("sit_to_stand", false);
        Player_Ani.SetBool("siting", false);
        Player_Ani.SetBool("stand_to_sit", false);
        is_motion = false;
    }

    IEnumerator Find_Chair(float delay)
    {
        yield return new WaitForSeconds(delay);

        chair01 = GameObject.FindWithTag("Chair01");
        chair02 = GameObject.FindWithTag("Chair02");
    }

    public void Start_Icons(int value)
    {
        PV.RPC("SendMessage", RpcTarget.All, "");
        PV.RPC("SendIcon", RpcTarget.All, value);
    }

    [PunRPC]
    private void SendMessage(string msg)
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
