using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player_Console : MonoBehaviourPunCallbacks
{
    public List<string> all_room = new List<string>();
    public List<GameObject> All_Info = new List<GameObject>();

    public GameObject[] all_player;
    public GameObject[] All_Canvas;

    private TextMeshProUGUI username;
    private TextMeshProUGUI title;
    private TextMeshProUGUI info;
    private TextMeshProUGUI open_info;

    public Button[] Emotion_All;
    public GameObject Icon_Panel;

    Action<List<RoomInfo>> callback = null;
    LoadBalancingClient client = null;

    public int button_value;

    private float update_realtime;
    private float update_realtime2;
    private float update_time;
    private float update_time2;

    private bool is_update;
    private bool is_update2;

    private Auth_Controller ac;

    private void Start()
    {
        ac = GetComponent<Auth_Controller>();

        update_time = 1.0f;
        update_time2 = 3.0f;

        Update_PlayerList();
        //Update_Custom();

        Icon_Panel = GameObject.Find("Icon_Panel");
        Emotion_All = Icon_Panel.transform.GetChild(2).GetComponentsInChildren<Button>();
        All_Canvas = GameObject.FindGameObjectsWithTag("All_Canvas");

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Main_World") {
            GameObject World_Info = GameObject.Find("World_Info");

            for (int i = 0; i < World_Info.transform.childCount; i++) {
                All_Info.Add(World_Info.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < Emotion_All.Length; i++) {
            Button emotion = Emotion_All[i].GetComponent<Button>();
            emotion.onClick.AddListener(() => Start_Icons(emotion));
        }

        Icon_Panel.SetActive(false);
    }

    private void Update()
    {
        if (!is_update) {
            if (update_realtime <= update_time) {
                update_realtime += Time.deltaTime;
            } else {
                is_update = true;
                Update_PlayerList();
            }
        }

        if (!is_update2) {
            if (update_realtime2 <= update_time2) {
                update_realtime2 += Time.deltaTime;
            } else {
                is_update2 = true;

                Reload_Info();

                OnGetRoomsInfo(
                    (roomInfos) => {
                        all_room.Clear();

                        //print("방 갯수: " + roomInfos.Count.ToString());

                        for (int i = 0; i < roomInfos.Count; i++) {
                            all_room.Add(roomInfos[i].Name);
                            //print(roomInfos[i].Name);
                        }

                        update_realtime2 = 0.0f;
                        is_update2 = false;
                    }
                );
            }
        }

        if (client != null) {
            client.Service();
        }
    }

    public void Reload_Info()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Main_World")
        {
            for (int i = 0; i < All_Info.Count; i++)
            {
                Transform parent = All_Info[i].transform.GetChild(0);

                username = parent.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                title = parent.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                info = parent.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                open_info = parent.transform.GetChild(6).GetComponent<TextMeshProUGUI>();

                username.text = ac.rc_info.RC_Infos[i].time;
                title.text = ac.rc_info.RC_Infos[i].title;
                info.text = ac.rc_info.RC_Infos[i].info;

                open_info.gameObject.SetActive(false);
            }

            for (int i = 0; i < all_room.Count; i++)
            {
                if (all_room[i] != "main_room")
                {
                    string index = all_room[i].Substring(0, all_room[i].Length - 4);

                    for (int j = 0; j < All_Info.Count; j++)
                    {
                        Transform parent = All_Info[j].transform.GetChild(0);

                        open_info = parent.transform.GetChild(6).GetComponent<TextMeshProUGUI>();

                        if (j == (int.Parse(index) - 1))
                        {
                            open_info.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    public void Update_PlayerList()
    {
        update_realtime = 0.0f;
        is_update = false;
        all_player = new GameObject[1];
        all_player = GameObject.FindGameObjectsWithTag("Player");
    }

    public void Start_Icons(Button emotion)
    {
        button_value = int.Parse(emotion.name) + 1;
    }

    public void OnGetRoomsInfo(Action<List<RoomInfo>> callback)
    {
        this.callback = callback;
        client = new LoadBalancingClient();
        client.AddCallbackTarget(this);
        client.StateChanged += OnStateChanged;
        client.AppId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;
        client.AppVersion = PhotonNetwork.NetworkingClient.AppVersion;
        client.EnableLobbyStatistics = true;

        // 포톤 세팅에서 접속 지역을 설정해주어야 한다. (FixedRegion 세팅하기)
        client.ConnectToRegionMaster(PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion);
    }

    void OnStateChanged(ClientState previousState, ClientState state)
    {
        //Debug.Log("서브 클라이언트 상태 : " + state);

        // 서브클라이언트가 마스터 서버에 접속하면 로비로 접속 시킨다.
        if (state == ClientState.ConnectedToMasterServer) {
            client.OpJoinLobby(null);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //Debug.Log("서브 클라이언트 룸리스트 업데이트");

        if (callback != null) {
            callback(roomList);
        }

        // 모든 작업이 끝나면 서브 클라이언트 연결 해제
        //client.Disconnect();
    }
}
