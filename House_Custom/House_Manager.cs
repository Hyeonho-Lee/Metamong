using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class House_Manager : MonoBehaviour
{
    public GameObject item_button;

    private House_Console house_console;

    private void Start()
    {
        /*customize_room = GameObject.Find("Room_Console").GetComponent<Customize_Room>();
        cr = GetComponent<Customize_Room_Bnt>();

        if (GameObject.Find("Send_Info")) {
            ac = GameObject.Find("Send_Info").GetComponent<Auth_Controller>();
            ac.Get_Room_DB();
            ac.Get_Room_Custom();
            StartCoroutine(Load_RoomCustom());
        }*/

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

            GameObject item_name = Bntt.transform.Find("ItemName").gameObject;
            Text bntt_name = item_name.GetComponent<Text>();

            GameObject item_price = Bntt.transform.Find("Price").gameObject;
            Text bntt_price = item_price.GetComponent<Text>();

            GameObject bntImage = Bntt.transform.Find("ItemImage").gameObject;
            Image bnttImage = bntImage.GetComponent<Image>();

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
            GameObject Bntt = Instantiate(item_button, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Bntt.name = i.ToString();
            Bntt.transform.SetParent(Parents);
            Bntt.transform.localScale = new Vector3(1, 1, 1);
            Bntt.GetComponent<Button>().onClick.AddListener(() => house_console.Change_House());

            GameObject item_name = Bntt.transform.Find("ItemName").gameObject;
            Text bntt_name = item_name.GetComponent<Text>();

            GameObject item_price = Bntt.transform.Find("Price").gameObject;
            Text bntt_price = item_price.GetComponent<Text>();

            GameObject bntImage = Bntt.transform.Find("ItemImage").gameObject;
            Image bnttImage = bntImage.GetComponent<Image>();

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

    /*IEnumerator Load_RoomCustom()
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
    }*/
}
