using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreManager : MonoBehaviour
{
    public int money;
    public string input_value;
    public string uid;
    public string username;

    public GameObject item_button;
    public GameObject buy_panel;

    public Text buy_text;
    public Text money_text;

    private Custom_Character_Offline cc;
    private Customize_StoreBnt cs;

    private Auth_Controller ac;

    private void Start()
    {
        cc = GameObject.Find("Custom_Character").GetComponent<Custom_Character_Offline>();
        cs = GetComponent<Customize_StoreBnt>();

        if (GameObject.Find("Send_Info")) {
            ac = GameObject.Find("Send_Info").GetComponent<Auth_Controller>();
            StartCoroutine(Load_Characters());
        } else {
            print("오프라인 입니다.");
        }
    }


    #region 버튼 생성
    public void Create_Btn()
    {
        if (input_value != null) {
            switch (input_value) {
                case "눈썹":
                    Create_Button(cc.character_moudle.eyebrow.Count);
                    break;
                case "눈":
                    Create_Button(cc.character_moudle.eye.Count);
                    break;
                case "수염":
                    Create_Button(cc.character_moudle.beard.Count);
                    break;
                case "입":
                    Create_Button(cc.character_moudle.mouth.Count);
                    break;
                case "머리카락":
                    Create_Button(cc.character_moudle.hair.Count);
                    break;
                case "머리":
                    Create_Button(cc.character_moudle.head.Count);
                    break;
                case "상의":
                    Create_Button(cc.character_moudle.top.Count);
                    break;
                case "하의":
                    Create_Button(cc.character_moudle.pants.Count);
                    break;
                case "신발":
                    Create_Button(cc.character_moudle.shoes.Count);
                    break;
                case "손":
                    Create_Button(cc.character_moudle.gloves.Count);
                    break;
                case "장식1":
                    Create_Button(cc.character_moudle.accessory01.Count);
                    break;
                case "장식2":
                    Create_Button(cc.character_moudle.accessory02.Count);
                    break;
                case "모자":
                    Create_Button(cc.character_moudle.helmet.Count);
                    break;
                case "무기":
                    Create_Button(cc.character_moudle.weapon.Count);
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
            Bntt.GetComponent<Button>().onClick.AddListener(() => Change_Module(-1));

            GameObject item_name = Bntt.transform.Find("ItemName").gameObject;
            Text bntt_name = item_name.GetComponent<Text>();

            GameObject item_price = Bntt.transform.Find("Price").gameObject;
            Text bntt_price = item_price.GetComponent<Text>();

            GameObject bntImage = Bntt.transform.Find("ItemImage").gameObject;
            Image bnttImage = bntImage.GetComponent<Image>();

            GameObject buy_button = Bntt.transform.Find("Buy_button").gameObject;
            buy_button.GetComponent<Button>().onClick.AddListener(() => Buy_Character(int.Parse(Bntt.name)));
            buy_button.GetComponent<Button>().onClick.AddListener(() => Change_Module(int.Parse(Bntt.name)));

            if (input_value != null) {
                switch (input_value) {
                    case "눈썹":
                        bnttImage.sprite = cs.item_moudle.Item_eyebrow[i];
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
            }
        }
    }

    public void Buy_Character(int index)
    {
        buy_panel.SetActive(true);
        buy_text.text = index.ToString() + "번 외형을 변경하시겠습니까?";
    }

    public void Result_Character()
    {
        money -= 10;

        ac.user.money = money;

        ac.Update_User_Info();
        ac.Update_Character_Button();

        Change_Value();
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
    public void Change_Module(int index)
    {
        Transform Eyebrow_Part = GameObject.Find("Eyebrow_Part").transform;
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

        string index_string;

        if (index >= 0) {
            index_string = index.ToString();
        }else {
            index_string = EventSystem.current.currentSelectedGameObject.name;
        }

        if (input_value != null)
        {
            switch (input_value)
            {
                case "눈썹":
                    if (Eyebrow_Part.childCount != 0)
                    {
                        cc.character.eyebrow = int.Parse(index_string);
                        ac.cc_user.eyebrow = cc.character.eyebrow;
                        cc.Change_Character("Eyebrow_Part", "eyebrow");
                    }
                    break;
                case "눈":
                    if (Eye_Part.childCount != 0)
                    {
                        cc.character.eye = int.Parse(index_string);
                        ac.cc_user.eye = cc.character.eye;
                        cc.Change_Character("Eye_Part", "eye");
                    }
                    break;
                case "수염":
                    if (Beard_Part.childCount != 0)
                    {
                        cc.character.beard = int.Parse(index_string);
                        ac.cc_user.beard = cc.character.beard;
                        cc.Change_Character("Beard_Part", "beard");
                    }
                    break;
                case "입":
                    if (Mouth_Part.childCount != 0)
                    {
                        cc.character.mouth = int.Parse(index_string);
                        ac.cc_user.mouth = cc.character.mouth;
                        cc.Change_Character("Mouth_Part", "mouth");
                    }
                    break;
                case "머리카락":
                    if (Hair_Part.childCount != 0)
                    {
                        cc.character.hair = int.Parse(index_string);
                        ac.cc_user.hair = cc.character.hair;
                        cc.Change_Character("Hair_Part", "hair");
                    }
                    break;
                case "머리":
                    if (Head_Part.childCount != 0)
                    {
                        cc.character.head = int.Parse(index_string);
                        ac.cc_user.head = cc.character.head;
                        cc.Change_Character("Head_Part", "head");
                    }
                    break;
                case "상의":
                    if (Top_Part.childCount != 0)
                    {
                        cc.character.top = int.Parse(index_string);
                        ac.cc_user.top = cc.character.top;
                        cc.Change_Character("Top_Part", "top");
                    }
                    break;
                case "하의":
                    if (Pants_Part.childCount != 0)
                    {
                        cc.character.pants = int.Parse(index_string);
                        ac.cc_user.pants = cc.character.pants;
                        cc.Change_Character("Pants_Part", "pants");
                    }
                    break;
                case "신발":
                    if (Shoes_Part.childCount != 0)
                    {
                        cc.character.shoes = int.Parse(index_string);
                        ac.cc_user.shoes = cc.character.shoes;
                        cc.Change_Character("Shoes_Part", "shoes");
                    }
                    break;
                case "손":
                    if (Gloves_Part.childCount != 0)
                    {
                        cc.character.gloves = int.Parse(index_string);
                        ac.cc_user.gloves = cc.character.gloves;
                        cc.Change_Character("Gloves_Part", "gloves");
                    }
                    break;
                case "장식1":
                    if (Accessory01_Part.childCount != 0)
                    {
                        cc.character.accessory01 = int.Parse(index_string);
                        ac.cc_user.accessory01 = cc.character.accessory01;
                        cc.Change_Character("Accessory01_Part", "accessory01");
                    }
                    break;
                case "장식2":
                    if (Accessory02_Part.childCount != 0)
                    {
                        cc.character.accessory02 = int.Parse(index_string);
                        ac.cc_user.accessory02 = cc.character.accessory02;
                        cc.Change_Character("Accessory02_Part", "accessory02");
                    }
                    break;
                case "모자":
                    if (Helmet_Part.childCount != 0)
                    {
                        cc.character.helmet = int.Parse(index_string);
                        ac.cc_user.helmet = cc.character.helmet;
                        cc.Change_Character("Helmet_Part", "helmet");
                    }
                    break;
                case "무기":
                    if (Weapon_Part.childCount != 0)
                    {
                        cc.character.weapon = int.Parse(index_string);
                        cc.Change_Character("Weapon_Part", "weapon");
                    }
                    break;
            }
        }
    }

    #endregion

    IEnumerator Load_Characters()
    {
        ac.Get_User_Info();
        ac.Get_Character_Button();

        yield return new WaitForSeconds(1.0f);

        cc.character.eyebrow = ac.cc_user.eyebrow;
        cc.character.eye = ac.cc_user.eye;
        cc.character.beard = ac.cc_user.beard;
        cc.character.mouth = ac.cc_user.mouth;
        cc.character.hair = ac.cc_user.hair;
        cc.character.head = ac.cc_user.head;
        cc.character.top = ac.cc_user.top;
        cc.character.pants = ac.cc_user.pants;
        cc.character.shoes = ac.cc_user.shoes;
        cc.character.gloves = ac.cc_user.gloves;
        cc.character.accessory01 = ac.cc_user.accessory01;
        cc.character.accessory02 = ac.cc_user.accessory02;
        cc.character.helmet = ac.cc_user.helmet;

        cc.Change_All();
        Change_Value();
    }

    void Change_Value()
    {
        uid = ac.user.uid;
        username = ac.user.username;
        money = ac.user.money;

        money_text.text = money.ToString() + "G";
    }
}
