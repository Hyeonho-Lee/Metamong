using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreManager : MonoBehaviour
{
    public string input_value;

    public GameObject item_button;

    private Customize_Character cc;

    private void Awake()
    {
        cc = GameObject.Find("Character").GetComponent<Customize_Character>();
    }


    #region ¹öÆ° »ý¼º
    public void Create_Bnt()
    {
        if (input_value != null) {
            switch (input_value) {
                case "´«½ç":
                    Create_Button(cc.character_moudle.eyebrow.Count);
                    break;
                case "´«":
                    Create_Button(cc.character_moudle.eye.Count);
                    break;
                case "¼ö¿°":
                    Create_Button(cc.character_moudle.beard.Count);
                    break;
                case "ÀÔ":
                    Create_Button(cc.character_moudle.mouth.Count);
                    break;
                case "¸Ó¸®Ä«¶ô":
                    Create_Button(cc.character_moudle.hair.Count);
                    break;
                case "¸Ó¸®":
                    Create_Button(cc.character_moudle.head.Count);
                    break;
                case "»óÀÇ":
                    Create_Button(cc.character_moudle.top.Count);
                    break;
                case "ÇÏÀÇ":
                    Create_Button(cc.character_moudle.pants.Count);
                    break;
                case "½Å¹ß":
                    Create_Button(cc.character_moudle.shoes.Count);
                    break;
                case "¼Õ":
                    Create_Button(cc.character_moudle.gloves.Count);
                    break;
                case "Àå½Ä1":
                    Create_Button(cc.character_moudle.accessory01.Count);
                    break;
                case "Àå½Ä2":
                    Create_Button(cc.character_moudle.accessory02.Count);
                    break;
                case "¸ðÀÚ":
                    Create_Button(cc.character_moudle.helmet.Count);
                    break;
                case "¹«±â":
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
            Bntt.GetComponent<Button>().onClick.AddListener(() => Change_Module());
        }
    }

    public void Check_Module()
    {
        //character_moudle.Reset_Input();

        string[] value = new string[] { "Accessory01", "Accessory02", "Beard", "Eye", "Eyebrow", 
            "Gloves", "Hair", "Head", "Helmet", "Mouth", "Pants", "Shoes", "Top" };

        /*for (int i = 0; i < value.Length; i++)
        {
            DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/Prefab/Character/" + value[i]);
            FileInfo[] file = di.GetFiles("*.prefab");

            for (int j = 0; j < file.Length; j++)
            {
                string path = "Assets\\Prefab\\Character\\" + value[i] + "\\" + file[j].Name;
                GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

                switch (value[i])
                {
                    case "Head":
                        character_moudle.head.Add(obj);
                        break;
                    case "Body":
                        character_moudle.body.Add(obj);
                        break;
                    case "Top":
                        character_moudle.top.Add(obj);
                        break;
                    case "Pants":
                        character_moudle.pants.Add(obj);
                        break;
                }
            }
        }*/
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

    #region ¸ðµâ ¹Ù²Ù±â
    public void Change_Module()
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

        string index_string = EventSystem.current.currentSelectedGameObject.name;

        if (input_value != null)
        {
            switch (input_value)
            {
                case "´«½ç":
                    if (Eyebrow_Part.childCount != 0)
                    {
                        cc.character.eyebrow = int.Parse(index_string);
                        cc.Change_Character("Eyebrow_Part", "eyebrow");
                    }
                    break;
                case "´«":
                    if (Eye_Part.childCount != 0)
                    {
                        cc.character.eye = int.Parse(index_string);
                        cc.Change_Character("Eye_Part", "eye");
                    }
                    break;
                case "¼ö¿°":
                    if (Beard_Part.childCount != 0)
                    {
                        cc.character.beard = int.Parse(index_string);
                        cc.Change_Character("Beard_Part", "beard");
                    }
                    break;
                case "ÀÔ":
                    if (Mouth_Part.childCount != 0)
                    {
                        cc.character.mouth = int.Parse(index_string);
                        cc.Change_Character("Mouth_Part", "mouth");
                    }
                    break;
                case "¸Ó¸®Ä«¶ô":
                    if (Hair_Part.childCount != 0)
                    {
                        cc.character.hair = int.Parse(index_string);
                        cc.Change_Character("Hair_Part", "hair");
                    }
                    break;
                case "¸Ó¸®":
                    if (Head_Part.childCount != 0)
                    {
                        cc.character.head = int.Parse(index_string);
                        cc.Change_Character("Head_Part", "head");
                    }
                    break;
                case "»óÀÇ":
                    if (Top_Part.childCount != 0)
                    {
                        cc.character.top = int.Parse(index_string);
                        cc.Change_Character("Top_Part", "top");
                    }
                    break;
                case "ÇÏÀÇ":
                    if (Pants_Part.childCount != 0)
                    {
                        cc.character.pants = int.Parse(index_string);
                        cc.Change_Character("Pants_Part", "pants");
                    }
                    break;
                case "½Å¹ß":
                    if (Shoes_Part.childCount != 0)
                    {
                        cc.character.shoes = int.Parse(index_string);
                        cc.Change_Character("Shoes_Part", "shoes");
                    }
                    break;
                case "¼Õ":
                    if (Gloves_Part.childCount != 0)
                    {
                        cc.character.gloves = int.Parse(index_string);
                        cc.Change_Character("Gloves_Part", "gloves");
                    }
                    break;
                case "Àå½Ä1":
                    if (Accessory01_Part.childCount != 0)
                    {
                        cc.character.accessory01 = int.Parse(index_string);
                        cc.Change_Character("Accessory01_Part", "accessory01");
                    }
                    break;
                case "Àå½Ä2":
                    if (Accessory02_Part.childCount != 0)
                    {
                        cc.character.accessory02 = int.Parse(index_string);
                        cc.Change_Character("Accessory02_Part", "accessory02");
                    }
                    break;
                case "¸ðÀÚ":
                    if (Helmet_Part.childCount != 0)
                    {
                        cc.character.helmet = int.Parse(index_string);
                        cc.Change_Character("Helmet_Part", "helmet");
                    }
                    break;
                case "¹«±â":
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
}
