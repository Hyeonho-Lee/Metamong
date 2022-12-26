using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Setting_Field : MonoBehaviour
{
    public bool is_esc;
    public bool is_b;

    [Header("Button")]
    public GameObject Room_Button;
    public GameObject All_House_Button;

    [Header("Canvas")]
    public GameObject Setting_Canvas;
    public GameObject RCInfo_Canvas;
    public GameObject All_House_Canvas;

    [Header("Input_Field")]
    public InputField title;
    public InputField info;
    public InputField time;

    [Header("Text")]
    public Text username;
    public Text uid;
    public Text rating;

    private Auth_Controller ac;
    private Connect_Manager2 cm2;
    private Player_Console pc;
    private World_Navigation wn;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Title") {
            ac = GameObject.Find("Title_Console").GetComponent<Auth_Controller>();
        } else if (scene.name == "Main_World") {
            GameObject wc = GameObject.Find("World_Console");

            ac = wc.GetComponent<Auth_Controller>();
            pc = wc.GetComponent<Player_Console>();
            wn = wc.GetComponent<World_Navigation>();
        } else if (scene.name == "Room_World") {
            ac = GameObject.Find("Room_Console").GetComponent<Auth_Controller>();
            cm2 = GameObject.Find("Server_Console").GetComponent<Connect_Manager2>();
        }

        username.text = "<color=#FF9983></color>";
        uid.text = "<color=#FF9983></color>";
        rating.text = "<color=#FF9983></color>";

        if (scene.name == "Room_World") {
            StartCoroutine(Check_Host(1.5f));
        }
    }

    IEnumerator Check_Host(float delay)
    {
        yield return new WaitForSeconds(delay);

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Room_World") {
            for (int i = 0; i < ac.h_position_index.Count; i++) {
                if (int.Parse(cm2.room_index) == ac.h_position_index[i]) {
                    if (ac.h_uid[i] == ac.localId) {
                        //print("방 주인임");
                    } else {
                        //print("유저임");
                        Room_Button.SetActive(false);
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (!is_esc) {
                Setting_Canvas.SetActive(true);
                is_esc = true;
                esc_update();
            } else {
                Setting_Canvas.SetActive(false);
                is_esc = false;
            }
        }


        if (Input.GetKeyUp(KeyCode.B)) {

            Scene scene = SceneManager.GetActiveScene();

            if (scene.name == "Room_World") {
                for (int i = 0; i < ac.h_position_index.Count; i++) {
                    if (int.Parse(cm2.room_index) == ac.h_position_index[i]) {
                        if (ac.h_uid[i] == ac.localId) {

                            if (!is_b) {
                                RCInfo_Canvas.SetActive(true);
                                is_b = true;

                                title.text = ac.rc_info.RC_Infos[int.Parse(cm2.room_index) - 1].title;
                                info.text = ac.rc_info.RC_Infos[int.Parse(cm2.room_index) - 1].info;
                                time.text = ac.rc_info.RC_Infos[int.Parse(cm2.room_index) - 1].time;
                            } else {
                                RCInfo_Canvas.SetActive(false);
                                is_b = false;
                            }

                            //print("방 주인임");
                        } else {
                            //print("유저임");
                        }
                    }
                }
            }

            if (scene.name == "Main_World") {
                if (!is_b) {
                    All_House_Canvas.SetActive(true);
                    is_b = true;

                    Transform Parents = GameObject.Find("House_Content").transform;

                    if (Parents.childCount != 0) {
                        for (int i = 0; i < Parents.childCount; i++) {
                            Destroy(Parents.GetChild(i).gameObject);
                        }
                    }

                    for (int i = 0; i < pc.all_room.Count; i++) {
                        if (pc.all_room[i] != "main_room") {
                            GameObject Bntt = Instantiate(All_House_Button, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            Bntt.name = i.ToString();
                            Bntt.transform.SetParent(Parents);
                            Bntt.transform.localScale = new Vector3(1, 1, 1);

                            string index = pc.all_room[i].Substring(0, pc.all_room[i].Length - 4);
                            //print(index);

                            GameObject item_title = Bntt.transform.Find("title").gameObject;
                            Text bntt_title = item_title.GetComponent<Text>();
                            bntt_title.text = ac.rc_info.RC_Infos[int.Parse(index) - 1].title;

                            GameObject item_info = Bntt.transform.Find("info").gameObject;
                            Text bntt_info = item_info.GetComponent<Text>();
                            bntt_info.text = ac.rc_info.RC_Infos[int.Parse(index) - 1].info;

                            GameObject info_button = Bntt.transform.Find("Info_Button").gameObject;
                            info_button.GetComponent<Button>().onClick.AddListener(() => wn.Click_Button(int.Parse(index) - 1));
                        }
                    }
                } else {
                    All_House_Canvas.SetActive(false);
                    is_b = false;
                }
            }
        }
    }

    private void esc_update()
    {
        if (ac.user.uid != "") {
            username.text = "<color=#FF9983>" + ac.user.username + "</color>";
            uid.text = "<color=#FF9983>" + ac.user.uid + "</color>";

            if (ac.user.is_counselor && ac.user.is_counselor_check && !ac.user.is_admin) {
                rating.text = "<color=#FF9983>" + "상담사" + "</color>";
            } else if (ac.user.is_user && !ac.user.is_admin && !ac.user.is_counselor) {
                rating.text = "<color=#FF9983>" + "유저" + "</color>";
            } else if (ac.user.is_admin) {
                rating.text = "<color=#FF9983>" + "어드민" + "</color>";
            } else {
                rating.text = "<color=#FF9983>" + "인증중" + "</color>";
            }
        }
    }
    public void is_esc_on()
    {
        Setting_Canvas.SetActive(true);
        is_esc = true;
        esc_update();
    }

    public void is_esc_off()
    {
        Setting_Canvas.SetActive(false);
        is_esc = false;
        esc_update();
    }

    public void update_info()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Room_World") {
            ac.rc_info.RC_Infos[int.Parse(cm2.room_index) - 1].title = title.text;
            ac.rc_info.RC_Infos[int.Parse(cm2.room_index) - 1].info = info.text;
            ac.rc_info.RC_Infos[int.Parse(cm2.room_index) - 1].time = time.text;
            ac.rc_info.RC_Infos[int.Parse(cm2.room_index) - 1].uid = ac.localId;
            ac.rc_info.RC_Infos[int.Parse(cm2.room_index) - 1].username = ac.userName;

            ac.Update_Room_Info();
        }
    }

    public void is_b_off()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Room_World") {
            RCInfo_Canvas.SetActive(false);
            is_b = false;
        }else if (scene.name == "Main_World") {
            All_House_Canvas.SetActive(false);
            is_b = false;
        }
    }

    public void GameOver()
    {
        Application.Quit();
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
