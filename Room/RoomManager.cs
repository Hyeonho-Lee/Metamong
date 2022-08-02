using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomManager : MonoBehaviour
{
    public string input_value;

    public GameObject item_button;

    private Customize_Room_Bnt cr;

    private void Awake()
    {
        cr = GetComponent<Customize_Room_Bnt>();
    }


    #region 버튼 생성
    public void Create_Bnt()
    {
        if (input_value != null) {
            switch (input_value) {
                case "벽지1":
                    Create_Button(cr.room_moudles.wall01.Count);
                    break;
                case "벽장식1":
                    Create_Button(cr.room_moudles.wall_accessory01.Count);
                    break;
                case "장식1":
                    Create_Button(cr.room_moudles.ground_accessory01.Count);
                    break;
                case "벽지2":
                    Create_Button(cr.room_moudles.wall02.Count);
                    break;
                case "벽장식2":
                    Create_Button(cr.room_moudles.wall_accessory02.Count);
                    break;
                case "장식2":
                    Create_Button(cr.room_moudles.ground_accessory02.Count);
                    break;
                case "타일":
                    Create_Button(cr.room_moudles.ground.Count);
                    break;
                case "의자1":
                    Create_Button(cr.room_moudles.chair01.Count);
                    break;
                case "의자2":
                    Create_Button(cr.room_moudles.chair02.Count);
                    break;
                case "책상":
                    Create_Button(cr.room_moudles.table.Count);
                    break;
                case "소품":
                    Create_Button(cr.room_moudles.table_accessory01.Count);
                    break;
            }
        }
    }

    public void Create_Button(int count)
    {
        Transform Parents = GameObject.Find("Content").transform;

        for (int i = 0; i < count; i++) {
            GameObject Bntt = Instantiate(item_button, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Bntt.name = i.ToString();
            Bntt.transform.SetParent(Parents);
            Bntt.transform.localScale = new Vector3(1, 1, 1);
            Bntt.GetComponent<Button>().onClick.AddListener(() => Change_Module());

            GameObject item_name = Bntt.transform.Find("ItemName").gameObject;
            Text bntt_name = item_name.GetComponent<Text>();

            GameObject item_price = Bntt.transform.Find("Price").gameObject;
            Text bntt_price = item_price.GetComponent<Text>();

            GameObject bntImage = Bntt.transform.Find("ItemImage").gameObject;
            Image bnttImage = bntImage.GetComponent<Image>();

            /*if (input_value != null) {
                switch (input_value) {
                    case "눈썹":
                        bnttImage.sprite = cr.room_moudles.Item_eyebrow[i];
                        bntt_name.text = ac.cc_db.eyebrow[i].name_kr;
                        bntt_price.text = ac.cc_db.eyebrow[i].price.ToString() + " G";
                        break;
                    case "눈":
                        bnttImage.sprite = cs.item_moudle.Item_eye[i];
                        bntt_name.text = ac.cc_db.eye[i].name_kr;
                        bntt_price.text = ac.cc_db.eye[i].price.ToString() + " G";
                        break;
                    case "수염":
                        bnttImage.sprite = cs.item_moudle.Item_beard[i];
                        bntt_name.text = ac.cc_db.beard[i].name_kr;
                        bntt_price.text = ac.cc_db.beard[i].price.ToString() + " G";
                        break;
                    case "입":
                        bnttImage.sprite = cs.item_moudle.Item_mouth[i];
                        bntt_name.text = ac.cc_db.mouth[i].name_kr;
                        bntt_price.text = ac.cc_db.mouth[i].price.ToString() + " G";
                        break;
                    case "머리카락":
                        bnttImage.sprite = cs.item_moudle.Item_hair[i];
                        bntt_name.text = ac.cc_db.hair[i].name_kr;
                        bntt_price.text = ac.cc_db.hair[i].price.ToString() + " G";
                        break;
                    case "머리":
                        bnttImage.sprite = cs.item_moudle.Item_head[i];
                        bntt_name.text = ac.cc_db.head[i].name_kr;
                        bntt_price.text = ac.cc_db.head[i].price.ToString() + " G";
                        break;
                    case "상의":
                        bnttImage.sprite = cs.item_moudle.Item_top[i];
                        bntt_name.text = ac.cc_db.top[i].name_kr;
                        bntt_price.text = ac.cc_db.top[i].price.ToString() + " G";
                        break;
                    case "하의":
                        bnttImage.sprite = cs.item_moudle.Item_pants[i];
                        bntt_name.text = ac.cc_db.pants[i].name_kr;
                        bntt_price.text = ac.cc_db.pants[i].price.ToString() + " G";
                        break;
                    case "신발":
                        bnttImage.sprite = cs.item_moudle.Item_shoes[i];
                        bntt_name.text = ac.cc_db.shoes[i].name_kr;
                        bntt_price.text = ac.cc_db.shoes[i].price.ToString() + " G";
                        break;
                    case "손":
                        bnttImage.sprite = cs.item_moudle.Item_gloves[i];
                        bntt_name.text = ac.cc_db.gloves[i].name_kr;
                        bntt_price.text = ac.cc_db.gloves[i].price.ToString() + " G";
                        break;
                    case "장식1":
                        bnttImage.sprite = cs.item_moudle.Item_accessory01[i];
                        bntt_name.text = ac.cc_db.accessory01[i].name_kr;
                        bntt_price.text = ac.cc_db.accessory01[i].price.ToString() + " G";
                        break;
                    case "장식2":
                        bnttImage.sprite = cs.item_moudle.Item_accessory02[i];
                        bntt_name.text = ac.cc_db.accessory02[i].name_kr;
                        bntt_price.text = ac.cc_db.accessory02[i].price.ToString() + " G";
                        break;
                    case "모자":
                        bnttImage.sprite = cs.item_moudle.Item_helmet[i];
                        bntt_name.text = ac.cc_db.helmet[i].name_kr;
                        bntt_price.text = ac.cc_db.helmet[i].price.ToString() + " G";
                        break;
                }
            }*/
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

    #endregion

    #region 모듈 바꾸기
    public void Change_Module()
    {
        /*Transform Eyebrow_Part = GameObject.Find("Eyebrow_Part").transform;
        Transform Eye_Part = GameObject.Find("Eye_Part").transform;
        Transform Beard_Part = GameObject.Find("Beard_Part").transform;
        Transform Mouth_Part = GameObject.Find("Mouth_Part").transform;
        Transform Hair_Part = GameObject.Find("Hair_Part").transform;
        Transform Head_Part = GameObject.Find("Head_Part").transform;
        Transform Top_Part = GameObject.Find("Top_Part").transform;
        Transform Pants_Part = GameObject.Find("Pants_Part").transform;
        Transform Shoes_Part = GameObject.Find("Shoes_Part").transform;
        Transform Gloves_Part = GameObject.Find("Gloves_Part").transform;
        Transform Accessory01_Part = GameObject.Find("Accessory01_Part").transform;
        Transform Accessory02_Part = GameObject.Find("Accessory02_Part").transform;
        Transform Helmet_Part = GameObject.Find("Helmet_Part").transform;
        Transform Weapon_Part = GameObject.Find("Weapon_Part").transform;

        string index_string = EventSystem.current.currentSelectedGameObject.name;

        if (input_value != null) {
            switch (input_value) {
                case "눈썹":
                    if (Eyebrow_Part.childCount != 0) {
                        cc.character.eyebrow = int.Parse(index_string);
                        ac.cc_user.eyebrow = cc.character.eyebrow;
                        cc.Change_Character("Eyebrow_Part", "eyebrow");
                    }
                    break;
                case "눈":
                    if (Eye_Part.childCount != 0) {
                        cc.character.eye = int.Parse(index_string);
                        ac.cc_user.eye = cc.character.eye;
                        cc.Change_Character("Eye_Part", "eye");
                    }
                    break;
                case "수염":
                    if (Beard_Part.childCount != 0) {
                        cc.character.beard = int.Parse(index_string);
                        ac.cc_user.beard = cc.character.beard;
                        cc.Change_Character("Beard_Part", "beard");
                    }
                    break;
                case "입":
                    if (Mouth_Part.childCount != 0) {
                        cc.character.mouth = int.Parse(index_string);
                        ac.cc_user.mouth = cc.character.mouth;
                        cc.Change_Character("Mouth_Part", "mouth");
                    }
                    break;
                case "머리카락":
                    if (Hair_Part.childCount != 0) {
                        cc.character.hair = int.Parse(index_string);
                        ac.cc_user.hair = cc.character.hair;
                        cc.Change_Character("Hair_Part", "hair");
                    }
                    break;
                case "머리":
                    if (Head_Part.childCount != 0) {
                        cc.character.head = int.Parse(index_string);
                        ac.cc_user.head = cc.character.head;
                        cc.Change_Character("Head_Part", "head");
                    }
                    break;
                case "상의":
                    if (Top_Part.childCount != 0) {
                        cc.character.top = int.Parse(index_string);
                        ac.cc_user.top = cc.character.top;
                        cc.Change_Character("Top_Part", "top");
                    }
                    break;
                case "하의":
                    if (Pants_Part.childCount != 0) {
                        cc.character.pants = int.Parse(index_string);
                        ac.cc_user.pants = cc.character.pants;
                        cc.Change_Character("Pants_Part", "pants");
                    }
                    break;
                case "신발":
                    if (Shoes_Part.childCount != 0) {
                        cc.character.shoes = int.Parse(index_string);
                        ac.cc_user.shoes = cc.character.shoes;
                        cc.Change_Character("Shoes_Part", "shoes");
                    }
                    break;
                case "손":
                    if (Gloves_Part.childCount != 0) {
                        cc.character.gloves = int.Parse(index_string);
                        ac.cc_user.gloves = cc.character.gloves;
                        cc.Change_Character("Gloves_Part", "gloves");
                    }
                    break;
                case "장식1":
                    if (Accessory01_Part.childCount != 0) {
                        cc.character.accessory01 = int.Parse(index_string);
                        ac.cc_user.accessory01 = cc.character.accessory01;
                        cc.Change_Character("Accessory01_Part", "accessory01");
                    }
                    break;
                case "장식2":
                    if (Accessory02_Part.childCount != 0) {
                        cc.character.accessory02 = int.Parse(index_string);
                        ac.cc_user.accessory02 = cc.character.accessory02;
                        cc.Change_Character("Accessory02_Part", "accessory02");
                    }
                    break;
                case "모자":
                    if (Helmet_Part.childCount != 0) {
                        cc.character.helmet = int.Parse(index_string);
                        ac.cc_user.helmet = cc.character.helmet;
                        cc.Change_Character("Helmet_Part", "helmet");
                    }
                    break;
                case "무기":
                    if (Weapon_Part.childCount != 0) {
                        cc.character.weapon = int.Parse(index_string);
                        cc.Change_Character("Weapon_Part", "weapon");
                    }
                    break;
            }
        }*/
    }
    #endregion
}
