using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class House_Manager : MonoBehaviour
{
    public string username;
    public string uid;
    public int money;

    public int index_forcus;
    public int position_index;
    public bool is_house;
    public bool find_house;

    public List<int> all_position_index = new List<int>();

    public GameObject item_button;
    public GameObject item_button2;
    public GameObject buy_panel;
    public GameObject buy2_panel;
    public Text buy_text;
    public Text buy2_text;

    public Text username_text;
    public Text money_text;
    public Text money2_text;
    public Text is_house_text;
    public Text position_index_text;
    public Text house_index_text;
    public Text house_date_text;

    private House_Console house_console;
    private Auth_Controller ac;

    private void Start()
    {
        /*customize_room = GameObject.Find("Room_Console").GetComponent<Customize_Room>();
        cr = GetComponent<Customize_Room_Bnt>();*/

        if (GameObject.Find("Send_Info")) {
            ac = GameObject.Find("Send_Info").GetComponent<Auth_Controller>();
            StartCoroutine(Load_House());
        }

        house_console = GetComponent<House_Console>();

        Create_Position_Button(42);
    }

    public void Create_Position_Button(int count)
    {
        Transform Parents = GameObject.Find("Position_Content").transform;

        if (Parents.childCount != 0) {
            for (int i = 0; i < Parents.childCount; i++) {
                Destroy(Parents.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < count; i++) {
            GameObject Bntt = Instantiate(item_button, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Bntt.name = i.ToString();
            Bntt.transform.SetParent(Parents);
            Bntt.transform.localScale = new Vector3(1, 1, 1);
            Bntt.GetComponent<Button>().onClick.AddListener(() => house_console.zoom_on());
            Bntt.GetComponent<Button>().onClick.AddListener(() => house_console.Change_House(ac.house.house_index));

            GameObject item_name = Bntt.transform.Find("ItemName").gameObject;
            Text bntt_name = item_name.GetComponent<Text>();

            GameObject item_price = Bntt.transform.Find("Price").gameObject;
            Text bntt_price = item_price.GetComponent<Text>();

            GameObject bntImage = Bntt.transform.Find("ItemImage").gameObject;
            Image bnttImage = bntImage.GetComponent<Image>();

            GameObject buy_button = Bntt.transform.Find("Buy_button").gameObject;
            buy_button.GetComponent<Button>().onClick.AddListener(() => Buy_House(int.Parse(Bntt.name) + 1));

            if (0 <= i && i <= 14) {
                bnttImage.sprite = house_console.Rank_Sprite[0];
            } else if (15 <= i && i <= 26) {
                bnttImage.sprite = house_console.Rank_Sprite[1];
            } else if (27 <= i && i <= 41) {
                bnttImage.sprite = house_console.Rank_Sprite[2];
            }

            bntt_name.text = (i + 1).ToString() + "번 위치";
            //bntt_price.text = ac.rc_db.Wall01[i].price.ToString() + " G";
        }
    }

    public void Create_House_Button(int count)
    {
        Transform Parents = GameObject.Find("House_Content").transform;

        if (Parents.childCount != 0) {
            for (int i = 0; i < Parents.childCount; i++) {
                Destroy(Parents.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < count; i++) {
            GameObject Bntt = Instantiate(item_button2, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Bntt.name = i.ToString();
            Bntt.transform.SetParent(Parents);
            Bntt.transform.localScale = new Vector3(1, 1, 1);
            Bntt.GetComponent<Button>().onClick.AddListener(() => house_console.Change_House(-1));

            GameObject item_name = Bntt.transform.Find("ItemName").gameObject;
            Text bntt_name = item_name.GetComponent<Text>();

            GameObject item_price = Bntt.transform.Find("Price").gameObject;
            Text bntt_price = item_price.GetComponent<Text>();

            GameObject bntImage = Bntt.transform.Find("ItemImage").gameObject;
            Image bnttImage = bntImage.GetComponent<Image>();

            GameObject buy_button = Bntt.transform.Find("Buy_button").gameObject;
            buy_button.GetComponent<Button>().onClick.AddListener(() => Buy2_House(int.Parse(Bntt.name) + 1));
            buy_button.GetComponent<Button>().onClick.AddListener(() => house_console.Change_House(int.Parse(Bntt.name)));

            switch (house_console.Level_Label) {
                case "A":
                    bnttImage.sprite = house_console.A_House_Sprite[i];
                    break;
                case "B":
                    bnttImage.sprite = house_console.B_House_Sprite[i];
                    break;
                case "C":
                    bnttImage.sprite = house_console.C_House_Sprite[i];
                    break;
            }

            bntt_name.text = (i + 1).ToString() + "번 건물";
            //bntt_price.text = ac.rc_db.Wall01[i].price.ToString() + " G";
        }
    }

    IEnumerator Load_House()
    {
        ac.Get_User_Info();
        ac.Get_House();
        ac.Get_All_House();

        yield return new WaitForSeconds(1.0f);

        if (ac.house.uid == null) {
            ac.house.uid = ac.user.uid;
            ac.house.username = ac.user.username;
            ac.house.is_house = false;
            ac.house.position_index = 0;
            ac.house.house_index = 0;
            ac.house.house_date = "X";
            ac.house.house_price = 0;
            ac.Update_House();
        }

        Change_Value();
    }

    void Change_Value()
    {
        uid = ac.user.uid;
        username = ac.user.username;
        money = ac.user.money;
        is_house = ac.house.is_house;
        position_index = ac.house.position_index;

        username_text.text = username;
        money_text.text = money.ToString() + "G";
        money2_text.text = money.ToString() + "G";

        if (!ac.house.is_house) {
            is_house_text.text = "No";
        } else {
            is_house_text.text = "Yes";
        }

        if (ac.house.position_index == 0) {
            position_index_text.text = "X";
        } else {
            position_index_text.text = ac.house.position_index.ToString();
        }

        if (ac.house.house_index == 0) {
            house_index_text.text = "X";
        } else {
            house_index_text.text = (ac.house.house_index + 1).ToString();
        }

        house_date_text.text = ac.house.house_date;
    }

    public void Buy_House(int index)
    {
        find_house = false;
        index_forcus = index;

        for (int i = 0; i < ac.h_position_index.Count; i++) {
            if (ac.h_position_index[i] == index) {
                find_house = true;
            }
        }

        if (!is_house && !find_house) {
            buy_panel.SetActive(true);
            buy_text.text = index.ToString() + "번 위치 건물을 구매하시겠습니까?\n가격: 1000G";
        }
    }

    public void Result_House()
    {
        money -= 1000;

        ac.user.money = money;
        ac.Update_User_Info();

        ac.house.is_house = true;
        ac.house.position_index = index_forcus;
        ac.house.house_date = DateTime.Now.ToString("yyyy-MM-dd");
        ac.Update_House();

        ac.rc_info.RC_Infos[ac.house.position_index - 1].info = ac.house.position_index.ToString();
        ac.rc_info.RC_Infos[ac.house.position_index - 1].title = ac.house.position_index.ToString() + "번 상담소 입니다";
        ac.rc_info.RC_Infos[ac.house.position_index - 1].time = "00:00 ~ 00:00";
        ac.rc_info.RC_Infos[ac.house.position_index - 1].uid = ac.localId;
        ac.rc_info.RC_Infos[ac.house.position_index - 1].username = ac.userName;
        ac.Update_Room_Info();

        Change_Value();
    }

    public void Reset_House()
    {
        ac.rc_info.RC_Infos[ac.house.position_index - 1].info = "";
        ac.rc_info.RC_Infos[ac.house.position_index - 1].title = "";
        ac.rc_info.RC_Infos[ac.house.position_index - 1].time = "";
        ac.rc_info.RC_Infos[ac.house.position_index - 1].uid = "";
        ac.rc_info.RC_Infos[ac.house.position_index - 1].username = "";
        ac.Update_Room_Info();

        ac.house.uid = ac.user.uid;
        ac.house.username = ac.user.username;
        ac.house.is_house = false;
        ac.house.position_index = 0;
        ac.house.house_index = 0;
        ac.house.house_date = "X";
        ac.house.house_price = 0;
        ac.Update_House();

        Change_Value();
    }

    public void Buy2_House(int index)
    {
        index_forcus = index;
        buy2_panel.SetActive(true);
        buy2_text.text = index.ToString() + "번 외형을 변경하시겠습니까?\n가격: 1000G";
    }

    public void Result2_House()
    {
        money -= 1000;

        ac.user.money = money;
        ac.house.uid = ac.user.uid;
        ac.house.username = ac.user.username;
        ac.house.is_house = true;
        ac.house.house_index = index_forcus - 1;

        ac.Update_User_Info();
        ac.Update_House();

        Change_Value();
    }
}
