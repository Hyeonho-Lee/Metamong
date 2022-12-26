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
    private Connect_Manager2 CM2;
    private Character_Info CI;
    private TextMeshProUGUI TMP;
    private Auth_Controller AC;
    private World_Navigation WN;
    private Player_Console PC;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Room_World") {
            CM2 = GameObject.Find("Server_Console").GetComponent<Connect_Manager2>();
            AC = GameObject.Find("Room_Console").GetComponent<Auth_Controller>();
            PC = GameObject.Find("Room_Console").GetComponent<Player_Console>();
        } else {
            CM = GameObject.Find("Server_Console").GetComponent<Connect_Manager>();
            AC = GameObject.Find("World_Console").GetComponent<Auth_Controller>();
            PC = GameObject.Find("World_Console").GetComponent<Player_Console>();
        }

        WN = AC.GetComponent<World_Navigation>();

        CI = GetComponent<Character_Info>();
        PV = GetComponent<PhotonView>();

        TMP = player_username.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (PV.IsMine) {
            if (SceneManager.GetActiveScene().name == "Room_World") {
                player_camera = null;
            } else {
                player_camera = Instantiate(CM.follow_camera, GameObject.Find("All_Camera").transform);
                player_camera.transform.GetChild(1).GetComponent<Player_Camera>().Player_Transform = this.transform;
                //camera.transform.parent = this.transform.parent;
            }

            StartCoroutine(N_All(1.0f));
        }
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
            if (AC.user.is_counselor && AC.user.is_counselor_check && !AC.user.is_admin) {
                PV.RPC("NicknameRPC", RpcTarget.All, "<color=#FF9983>"+ AC.user.username + "</color>");
            } else if (AC.user.is_user && !AC.user.is_admin && !AC.user.is_counselor) {
                PV.RPC("NicknameRPC", RpcTarget.All, "<color=#FFFFFF>" + AC.user.username + "</color>");
            } else if (AC.user.is_admin) {
                PV.RPC("NicknameRPC", RpcTarget.All, "<color=#FF9983>" + AC.user.username + "</color>");
            } else {
                PV.RPC("NicknameRPC", RpcTarget.All, "<color=#FFFFFF>" + AC.user.username + "</color>");
            }
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

                if (other.name.ToString() == "world_portal") {
                    Room_Managers RM = GameObject.Find("Room_Console").GetComponent<Room_Managers>();
                    RM.LeftRoom();
                }
            }

            if (other.transform.tag == "World_Portal") {
                for (int i = 1; i <= 42; i++) {
                    if (other.name.ToString() == i.ToString()) {

                        int stack = 0;

                        // 내가 밟은 포탈이 소유중일때
                        for (int j = 0; j < AC.h_position_index.Count; j++) {
                            if (AC.h_position_index[j] == i) {
                                if (AC.h_uid[j] == AC.user.uid) {
                                    GameObject Send_Info = new GameObject("Send_Info");
                                    Send_Info.AddComponent<Auth_Controller>();

                                    Auth_Controller info = Send_Info.GetComponent<Auth_Controller>();
                                    info.userName = AC.userName;
                                    info.localId = AC.localId;
                                    info.idToken = AC.idToken;
                                    info.world_position = "room_world";
                                    info.room_index = i.ToString();
                                    CM.spawn_name = "room_world";

                                    info.user = AC.user;
                                    info.cc_user = AC.cc_user;
                                    info.cc_db = AC.cc_db;

                                    GameObject Send_Spawn = Instantiate(Send_Info); ;
                                    Send_Spawn.name = "Title_Console";

                                    DontDestroyOnLoad(Send_Info);
                                    DontDestroyOnLoad(Send_Spawn);

                                    CM.spawn_name = "room_world";

                                    PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
                                    CM.OnLeftRoom();
                                }
                            } else {
                                stack++;
                            }
                        }

                        if (AC.h_position_index.Count != stack) {
                            //print("누군가 집");

                            for (int j = 0; j < PC.all_room.Count; j++) {
                                if (i.ToString()+"room" == PC.all_room[j]) {
                                    print(i + "번쨰 방이 존재 합니다");

                                    GameObject Send_Info2 = new GameObject("Send_Info");
                                    Send_Info2.AddComponent<Auth_Controller>();

                                    Auth_Controller info2 = Send_Info2.GetComponent<Auth_Controller>();
                                    info2.userName = AC.userName;
                                    info2.localId = AC.localId;
                                    info2.idToken = AC.idToken;
                                    info2.world_position = "room_world";
                                    info2.room_index = i.ToString();
                                    CM.spawn_name = "room_world";

                                    info2.user = AC.user;
                                    info2.cc_user = AC.cc_user;
                                    info2.cc_db = AC.cc_db;

                                    GameObject Send_Spawn2 = Instantiate(Send_Info2); ;
                                    Send_Spawn2.name = "Title_Console";

                                    DontDestroyOnLoad(Send_Info2);
                                    DontDestroyOnLoad(Send_Spawn2);

                                    PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
                                    CM.OnLeftRoom();
                                }
                            }
                        } else {
                            //print("빈 집");
                        }

                        //print(stack);
                        //print("상담소 이동 / " + i);
                        stack = 0;
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

    public void NicknameRPC(string msg)
    {
        TMP.text = msg;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
