using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class Photon_Player : MonoBehaviourPunCallbacks, IPunObservable
{
    public string PlayerNickName;

    public GameObject player_username;
    public bool is_load;

    private GameObject player_camera;

    private PhotonView PV;
    private Connect_Manager CM;
    private Character_Info CI;
    private TextMeshProUGUI TMP;
    private Auth_Controller AC;
    private World_Navigation WN;

    private void Awake()
    {
        CM = GameObject.Find("Server_Console").GetComponent<Connect_Manager>();
        AC = GameObject.Find("World_Console").GetComponent<Auth_Controller>();
        WN = AC.GetComponent<World_Navigation>();

        CI = GetComponent<Character_Info>();
        PV = GetComponent<PhotonView>();

        TMP = player_username.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (PV.IsMine) {
            player_camera = Instantiate(CM.follow_camera, GameObject.Find("All_Camera").transform);
            player_camera.transform.GetChild(1).GetComponent<Player_Camera>().Player_Transform = this.transform;
            //camera.transform.parent = this.transform.parent;

            PhotonNetwork.LocalPlayer.NickName = CI.userName;
            PlayerNickName = CI.userName;
            //TMP.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;

            if (PhotonNetwork.LocalPlayer.NickName == "") {
                PhotonNetwork.LocalPlayer.NickName = CI.userName;
            }

            StartCoroutine(N_All(1.0f));
        }
    }

    private void Update()
    {
        /*if (PV.IsMine) {
            PV.RPC("SendName", RpcTarget.All);
        }*/
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PV.IsMine) {
            StartCoroutine(N_All(1.0f));
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PV.IsMine) {
            StartCoroutine(N_All(1.0f));
        }
    }

    IEnumerator N_All(float delay)
    {
        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(delay);
            PV.RPC("SendName", RpcTarget.All);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PV.IsMine) {
            // 옷,가구,상담소 포탈
            if (other.transform.tag == "Portal") {
                if (other.name.ToString() == "house_store") {
                    GameObject Send_Info = new GameObject("Send_Info");
                    Send_Info.AddComponent<Auth_Controller>();

                    Auth_Controller info = Send_Info.GetComponent<Auth_Controller>();
                    info.userName = AC.userName;
                    info.localId = AC.localId;
                    info.idToken = AC.idToken;
                    info.world_position = "house_store";

                    info.user = AC.user;
                    info.rc_info = AC.rc_info;

                    DontDestroyOnLoad(Send_Info);
                    SceneManager.LoadScene("House_Custom");

                    PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    CM.OnLeftRoom();
                }

                if (other.name.ToString() == "character_store") {
                    GameObject Send_Info1 = new GameObject("Send_Info");
                    Send_Info1.AddComponent<Auth_Controller>();

                    Auth_Controller info1 = Send_Info1.GetComponent<Auth_Controller>();
                    info1.userName = AC.userName;
                    info1.localId = AC.localId;
                    info1.idToken = AC.idToken;
                    info1.world_position = "character_store";

                    info1.user = AC.user;
                    info1.cc_user = AC.cc_user;
                    info1.cc_db = AC.cc_db;

                    DontDestroyOnLoad(Send_Info1);
                    SceneManager.LoadScene("Character_Custom");

                    PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    CM.OnLeftRoom();
                }

                if (other.name.ToString() == "room_store") {
                    GameObject Send_Info2 = new GameObject("Send_Info");
                    Send_Info2.AddComponent<Auth_Controller>();

                    Auth_Controller info2 = Send_Info2.GetComponent<Auth_Controller>();
                    info2.userName = AC.userName;
                    info2.localId = AC.localId;
                    info2.idToken = AC.idToken;
                    info2.world_position = "room_store";

                    info2.user = AC.user;
                    info2.rc_info = AC.rc_info;
                    info2.rc_db = AC.rc_db;

                    DontDestroyOnLoad(Send_Info2);
                    SceneManager.LoadScene("Room_Custom");

                    PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
                    CM.OnLeftRoom();
                }
            }

            // 발판 밟을시 일어나는일
            if (other.transform.tag == "World_Portal") {
                for (int i = 1; i <= 42; i++) {
                    if (other.name.ToString() == i.ToString()) {

                        // 내가 밟은 포탈이 소유중일때
                        for (int j = 0; j < AC.h_position_index.Count; j++) {
                            if (AC.h_position_index[j] == i) {
                                if (AC.h_uid[j] == AC.user.uid) {
                                    print("내꺼");
                                }
                            }
                        }

                        // 발판 밟을시 일어나는일
                        // 해야할거: 발판을 밟을시 포톤 상담소씬(Room_World)로 이동
                        // 포톤방을 새로 생성하고 들어가는 파트까지만
                        // 캐릭터 생성이 되더라도 다른거 참조 해야하니까
                        // 방을 들어만 가게끔하고 오브젝트 생성은 나중에 작업예정
                        // 버튼 하나 만들어서 다시 메인 월드로 포톤 방이동이 되도록

                        print("상담소 이동 / " + i);
                    }
                }
            }
        }
    }


    [PunRPC]

    public void DestroyRPC()
    {
        Destroy(player_camera);
        Destroy(gameObject);
    }

    [PunRPC]

    public void SendName()
    {
        TMP.text = CI.userName;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
