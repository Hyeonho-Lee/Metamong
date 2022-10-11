using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Custom_Character_Offline : MonoBehaviour
{
    public Character character = new Character(); //캐릭터 개체 생성
    public Character_Moudle character_moudle = new Character_Moudle(); //아이템 개체 생성

    private void Start()
    {
        Check_Module();
    }

    public void Check_Module()
    {
        Transform Head_Part = this.transform.GetChild(1);
        Transform Top_Part = this.transform.GetChild(2);
        Transform Pants_Part = this.transform.GetChild(3);
        Transform Shoes_Part = this.transform.GetChild(4);
        Transform Gloves_Part = this.transform.GetChild(5);
        Transform Eye_Part = this.transform.GetChild(6);
        Transform Eyebrow_Part = this.transform.GetChild(7);
        Transform Accessory02_Part = this.transform.GetChild(8);

        Transform Head = this.transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0);
        Transform Hair_Part = Head.transform.GetChild(6);
        Transform Beard_Part = Head.transform.GetChild(7);
        Transform Helmet_Part = Head.transform.GetChild(8);
        Transform Accessort01_Part = Head.transform.GetChild(9);
        Transform Mouth_Part = Head.transform.GetChild(4).transform.GetChild(0);

        for (int j = 0; j < Head_Part.childCount; j++) {
            character_moudle.head.Add(Head_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Top_Part.childCount; j++) {
            character_moudle.top.Add(Top_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Pants_Part.childCount; j++) {
            character_moudle.pants.Add(Pants_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Shoes_Part.childCount; j++) {
            character_moudle.shoes.Add(Shoes_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Gloves_Part.childCount; j++) {
            character_moudle.gloves.Add(Gloves_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Eye_Part.childCount; j++) {
            character_moudle.eye.Add(Eye_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Eyebrow_Part.childCount; j++) {
            character_moudle.eyebrow.Add(Eyebrow_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Accessory02_Part.childCount; j++) {
            character_moudle.accessory02.Add(Accessory02_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Mouth_Part.childCount; j++) {
            character_moudle.mouth.Add(Mouth_Part.GetChild(j).gameObject);
        }

        //--------------------------//

        for (int j = 0; j < Hair_Part.childCount; j++) {
            character_moudle.hair.Add(Hair_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Beard_Part.childCount; j++) {
            character_moudle.beard.Add(Beard_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Helmet_Part.childCount; j++) {
            character_moudle.helmet.Add(Helmet_Part.GetChild(j).gameObject);
        }

        for (int j = 0; j < Accessort01_Part.childCount; j++) {
            character_moudle.accessory01.Add(Accessort01_Part.GetChild(j).gameObject);
        }
    }

    public void Change_Character(string path, string name)
    {
        switch (name) {
            case "eyebrow":
                for (int i = 0; i < this.character_moudle.eyebrow.Count; i++) {
                    if (i != this.character.eyebrow) {
                        this.character_moudle.eyebrow[i].SetActive(false);
                    } else {
                        this.character_moudle.eyebrow[i].SetActive(true);
                    }
                }
                break;
            case "eye":
                for (int i = 0; i < this.character_moudle.eye.Count; i++) {
                    if (i != this.character.eye) {
                        this.character_moudle.eye[i].SetActive(false);
                    } else {
                        this.character_moudle.eye[i].SetActive(true);
                    }
                }
                break;
            case "beard":
                for (int i = 0; i < this.character_moudle.beard.Count; i++) {
                    if (i != this.character.beard) {
                        this.character_moudle.beard[i].SetActive(false);
                    } else {
                        this.character_moudle.beard[i].SetActive(true);
                    }
                }
                break;
            case "mouth":
                for (int i = 0; i < this.character_moudle.mouth.Count; i++) {
                    if (i != this.character.mouth) {
                        this.character_moudle.mouth[i].SetActive(false);
                    } else {
                        this.character_moudle.mouth[i].SetActive(true);
                    }
                }
                break;
            case "hair":
                for (int i = 0; i < this.character_moudle.hair.Count; i++) {
                    if (i != this.character.hair) {
                        this.character_moudle.hair[i].SetActive(false);
                    } else {
                        this.character_moudle.hair[i].SetActive(true);
                    }
                }
                break;
            case "head":
                for (int i = 0; i < this.character_moudle.head.Count; i++) {
                    if (i != this.character.head) {
                        this.character_moudle.head[i].SetActive(false);
                    } else {
                        this.character_moudle.head[i].SetActive(true);
                    }
                }
                break;
            case "top":
                for (int i = 0; i < this.character_moudle.top.Count; i++) {
                    if (i != this.character.top) {
                        this.character_moudle.top[i].SetActive(false);
                    } else {
                        this.character_moudle.top[i].SetActive(true);
                    }
                }
                break;
            case "pants":
                for (int i = 0; i < this.character_moudle.pants.Count; i++) {
                    if (i != this.character.pants) {
                        this.character_moudle.pants[i].SetActive(false);
                    } else {
                        this.character_moudle.pants[i].SetActive(true);
                    }
                }
                break;
            case "shoes":
                for (int i = 0; i < this.character_moudle.shoes.Count; i++) {
                    if (i != this.character.shoes) {
                        this.character_moudle.shoes[i].SetActive(false);
                    } else {
                        this.character_moudle.shoes[i].SetActive(true);
                    }
                }
                break;
            case "gloves":
                for (int i = 0; i < this.character_moudle.gloves.Count; i++) {
                    if (i != this.character.gloves) {
                        this.character_moudle.gloves[i].SetActive(false);
                    } else {
                        this.character_moudle.gloves[i].SetActive(true);
                    }
                }
                break;
            case "accessory01":
                for (int i = 0; i < this.character_moudle.accessory01.Count; i++) {
                    if (i != this.character.accessory01) {
                        this.character_moudle.accessory01[i].SetActive(false);
                    } else {
                        this.character_moudle.accessory01[i].SetActive(true);
                    }
                }
                break;
            case "accessory02":
                for (int i = 0; i < this.character_moudle.accessory02.Count; i++) {
                    if (i != this.character.accessory02) {
                        this.character_moudle.accessory02[i].SetActive(false);
                    } else {
                        this.character_moudle.accessory02[i].SetActive(true);
                    }
                }
                break;
            case "helmet":
                for (int i = 0; i < this.character_moudle.helmet.Count; i++) {
                    if (i != this.character.helmet) {
                        this.character_moudle.helmet[i].SetActive(false);
                    } else {
                        this.character_moudle.helmet[i].SetActive(true);
                    }
                }
                break;
            case "weapon":
                for (int i = 0; i < this.character_moudle.weapon.Count; i++) {
                    if (i != this.character.weapon) {
                        this.character_moudle.weapon[i].SetActive(false);
                    } else {
                        this.character_moudle.weapon[i].SetActive(true);
                    }
                }
                break;

        }
    }

    public void Change_All()
    {
        Change_Character("Eyebrow_Part", "eyebrow");
        Change_Character("Eyebrow_Part", "eyebrow");
        Change_Character("Eye_Part", "eye");
        Change_Character("Beard_Part", "beard");
        Change_Character("Mouth_Part", "mouth");
        Change_Character("Hair_Part", "hair");
        Change_Character("Head_Part", "head");
        Change_Character("Top_Part", "top");
        Change_Character("Pants_Part", "pants");
        Change_Character("Shoes_Part", "shoes");
        Change_Character("Gloves_Part", "gloves");
        Change_Character("Accessory01_Part", "accessory01");
        Change_Character("Accessory02_Part", "accessory02");
        Change_Character("Helmet_Part", "helmet");
    }
}