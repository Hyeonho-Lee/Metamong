using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomManager : MonoBehaviour
{
    public GameObject item_button;

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
            Bntt.GetComponent<Button>().onClick.AddListener(() => customize_room.Change_Module());

            GameObject item_name = Bntt.transform.Find("ItemName").gameObject;
            Text bntt_name = item_name.GetComponent<Text>();

            GameObject item_price = Bntt.transform.Find("Price").gameObject;
            Text bntt_price = item_price.GetComponent<Text>();

            GameObject bntImage = Bntt.transform.Find("ItemImage").gameObject;
            Image bnttImage = bntImage.GetComponent<Image>();

            switch (type) {
                case "����1":
                    bnttImage.sprite = cr.room_moudles.wall01[i];
                    bntt_name.text = ac.rc_db.Wall01[i].name_kr;
                    bntt_price.text = ac.rc_db.Wall01[i].price.ToString() + " G";
                    break;
                case "�����1":
                    bnttImage.sprite = cr.room_moudles.wall_accessory01[i];
                    bntt_name.text = ac.rc_db.Wall_Accessory01[i].name_kr;
                    bntt_price.text = ac.rc_db.Wall_Accessory01[i].price.ToString() + " G";
                    break;
                case "���1":
                    bnttImage.sprite = cr.room_moudles.ground_accessory01[i];
                    bntt_name.text = ac.rc_db.Ground_Accessory01[i].name_kr;
                    bntt_price.text = ac.rc_db.Ground_Accessory01[i].price.ToString() + " G";
                    break;
                case "����2":
                    bnttImage.sprite = cr.room_moudles.wall02[i];
                    bntt_name.text = ac.rc_db.Wall02[i].name_kr;
                    bntt_price.text = ac.rc_db.Wall02[i].price.ToString() + " G";
                    break;
                case "�����2":
                    bnttImage.sprite = cr.room_moudles.wall_accessory02[i];
                    bntt_name.text = ac.rc_db.Wall_Accessory02[i].name_kr;
                    bntt_price.text = ac.rc_db.Wall_Accessory02[i].price.ToString() + " G";
                    break;
                case "���2":
                    bnttImage.sprite = cr.room_moudles.ground_accessory02[i];
                    bntt_name.text = ac.rc_db.Ground_Accessory02[i].name_kr;
                    bntt_price.text = ac.rc_db.Ground_Accessory02[i].price.ToString() + " G";
                    break;
                case "Ÿ��":
                    bnttImage.sprite = cr.room_moudles.ground[i];
                    bntt_name.text = ac.rc_db.Ground[i].name_kr;
                    bntt_price.text = ac.rc_db.Ground[i].price.ToString() + " G";
                    break;
                case "����1":
                    bnttImage.sprite = cr.room_moudles.chair01[i];
                    bntt_name.text = ac.rc_db.Chair01[i].name_kr;
                    bntt_price.text = ac.rc_db.Chair01[i].price.ToString() + " G";
                    break;
                case "����2":
                    bnttImage.sprite = cr.room_moudles.chair02[i];
                    bntt_name.text = ac.rc_db.Chair02[i].name_kr;
                    bntt_price.text = ac.rc_db.Chair02[i].price.ToString() + " G";
                    break;
                case "å��":
                    bnttImage.sprite = cr.room_moudles.table[i];
                    bntt_name.text = ac.rc_db.Table[i].name_kr;
                    bntt_price.text = ac.rc_db.Table[i].price.ToString() + " G";
                    break;
                case "��ǰ":
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
        ac.Get_Room_Custom();

        yield return new WaitForSeconds(0.5f);

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
    }
}