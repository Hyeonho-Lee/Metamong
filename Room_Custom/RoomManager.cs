using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomManager : MonoBehaviour
{
    public int money;
    public string uid;
    public string username;

    public GameObject item_button;
    public GameObject buy_panel;
    public Text buy_text;
    public Text money_text;
    public Text money2_text;

    private Customize_Room customize_room;
    private Customize_Room_Bnt cr;
    private Auth_Controller ac;

    private void Start()
    {
        customize_room = GameObject.Find("Room_Console").GetComponent<Customize_Room>();
        cr = GetComponent<Customize_Room_Bnt>();

        if (GameObject.Find("Send_Info")) {
            ac = GameObject.Find("Send_Info").GetComponent<Auth_Controller>();
            ac.Get_Room_DB();
            ac.Get_Room_Custom();
            StartCoroutine(Load_RoomCustom());
        }
    }

    public void Create_Button(int count, string type)
    {
        Delete_Button();

        Transform Parents = GameObject.Find("Content").transform;

        for (int i = 0; i < count; i++) {
            GameObject Bntt = Instantiate(item_button, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Bntt.name = i.ToString();
            Bntt.transform.SetParent(Parents);
            Bntt.transform.localScale = new Vector3(1, 1, 1);
            Bntt.GetComponent<Button>().onClick.AddListener(() => customize_room.Change_Module(-1));

            GameObject item_name = Bntt.transform.Find("ItemName").gameObject;
            Text bntt_name = item_name.GetComponent<Text>();

            GameObject item_price = Bntt.transform.Find("Price").gameObject;
            Text bntt_price = item_price.GetComponent<Text>();

            GameObject bntImage = Bntt.transform.Find("ItemImage").gameObject;
            Image bnttImage = bntImage.GetComponent<Image>();

            GameObject buy_button = Bntt.transform.Find("Buy_button").gameObject;
            buy_button.GetComponent<Button>().onClick.AddListener(() => Buy_Room(int.Parse(Bntt.name)));
            buy_button.GetComponent<Button>().onClick.AddListener(() => customize_room.Change_Module(int.Parse(Bntt.name)));

            switch (type) {
                case "벽지1":
                    bnttImage.sprite = cr.room_moudles.wall01[i];
                    bntt_name.text = ac.rc_db.Wall01[i].name_kr;
                    bntt_price.text = ac.rc_db.Wall01[i].price.ToString() + " G";
                    break;
                case "벽장식1":
                    bnttImage.sprite = cr.room_moudles.wall_accessory01[i];
                    bntt_name.text = ac.rc_db.Wall_Accessory01[i].name_kr;
                    bntt_price.text = ac.rc_db.Wall_Accessory01[i].price.ToString() + " G";
                    break;
                case "장식1":
                    bnttImage.sprite = cr.room_moudles.ground_accessory01[i];
                    bntt_name.text = ac.rc_db.Ground_Accessory01[i].name_kr;
                    bntt_price.text = ac.rc_db.Ground_Accessory01[i].price.ToString() + " G";
                    break;
                case "벽지2":
                    bnttImage.sprite = cr.room_moudles.wall02[i];
                    bntt_name.text = ac.rc_db.Wall02[i].name_kr;
                    bntt_price.text = ac.rc_db.Wall02[i].price.ToString() + " G";
                    break;
                case "벽장식2":
                    bnttImage.sprite = cr.room_moudles.wall_accessory02[i];
                    bntt_name.text = ac.rc_db.Wall_Accessory02[i].name_kr;
                    bntt_price.text = ac.rc_db.Wall_Accessory02[i].price.ToString() + " G";
                    break;
                case "장식2":
                    bnttImage.sprite = cr.room_moudles.ground_accessory02[i];
                    bntt_name.text = ac.rc_db.Ground_Accessory02[i].name_kr;
                    bntt_price.text = ac.rc_db.Ground_Accessory02[i].price.ToString() + " G";
                    break;
                case "타일":
                    bnttImage.sprite = cr.room_moudles.ground[i];
                    bntt_name.text = ac.rc_db.Ground[i].name_kr;
                    bntt_price.text = ac.rc_db.Ground[i].price.ToString() + " G";
                    break;
                case "의자1":
                    bnttImage.sprite = cr.room_moudles.chair01[i];
                    bntt_name.text = ac.rc_db.Chair01[i].name_kr;
                    bntt_price.text = ac.rc_db.Chair01[i].price.ToString() + " G";
                    break;
                case "의자2":
                    bnttImage.sprite = cr.room_moudles.chair02[i];
                    bntt_name.text = ac.rc_db.Chair02[i].name_kr;
                    bntt_price.text = ac.rc_db.Chair02[i].price.ToString() + " G";
                    break;
                case "책상":
                    bnttImage.sprite = cr.room_moudles.table[i];
                    bntt_name.text = ac.rc_db.Table[i].name_kr;
                    bntt_price.text = ac.rc_db.Table[i].price.ToString() + " G";
                    break;
                case "소품":
                    bnttImage.sprite = cr.room_moudles.table_accessory01[i];
                    bntt_name.text = ac.rc_db.Table_Accessory01[i].name_kr;
                    bntt_price.text = ac.rc_db.Table_Accessory01[i].price.ToString() + " G";
                    break;
            }
        }
    }

    public void Delete_Button()
    {
        Transform Parents = GameObject.Find("Content").transform;

        if (Parents.childCount != 0) {
            for (int i = 0; i < Parents.childCount; i++) {
                Destroy(Parents.GetChild(i).gameObject);
            }
        }
    }

    IEnumerator Load_RoomCustom()
    {
        ac.Get_User_Info();
        ac.Get_Room_Custom();

        yield return new WaitForSeconds(1.5f);

        customize_room.room.wall01 = ac.rc_user.wall01;
        customize_room.room.wall_accessory01 = ac.rc_user.wall_accessory01;
        customize_room.room.ground_accessory01 = ac.rc_user.ground_accessory01;

        customize_room.room.wall02 = ac.rc_user.wall02;
        customize_room.room.wall_accessory02 = ac.rc_user.wall_accessory02;
        customize_room.room.ground_accessory02 = ac.rc_user.ground_accessory02;

        customize_room.room.ground = ac.rc_user.ground;
        customize_room.room.chair01 = ac.rc_user.chair01;
        customize_room.room.chair02 = ac.rc_user.chair02;
        customize_room.room.table = ac.rc_user.table;
        customize_room.room.table_accessory01 = ac.rc_user.table_accessory01;

        customize_room.Check_Module();
        Change_Value();
    }

    public void Buy_Room(int index)
    {
        buy_panel.SetActive(true);
        buy_text.text = index.ToString() + "번 외형을 변경하시겠습니까?";
    }

    public void Result_Room()
    {
        money -= 50;

        ac.user.money = money;

        ac.Update_User_Info();
        ac.Update_Room_Custom();

        Change_Value();
    }

    void Change_Value()
    {
        uid = ac.user.uid;
        username = ac.user.username;
        money = ac.user.money;

        money_text.text = money.ToString() + "G";
        money2_text.text = money.ToString() + "G";
    }
}
