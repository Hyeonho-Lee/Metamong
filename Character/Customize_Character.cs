using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Character
{
    public int eyebrow; //����
    public int eye; //��
    public int beard; //����
    public int mouth; //��
    public int hair; // �Ӹ�ī��
    public int head; //�� ���

    public int top; //����
    public int pants; //����
    public int shoes; //�Ź�
    public int gloves; //��

    public int accessory01; //�� �Ǽ�����
    public int accessory02; //�� �Ŵ°�
    public int helmet; //����
    public int weapon; //����

} //ĳ���� �������

[System.Serializable]
public class Character_Moudle
{
    public List<GameObject> eyebrow; // ����
    public List<GameObject> eye; // ��
    public List<GameObject> beard; // ����
    public List<GameObject> mouth; // ��
    public List<GameObject> hair; // �Ӹ�ī��
    public List<GameObject> head; //�� ���

    public List<GameObject> top; // ����
    public List<GameObject> pants; // ����
    public List<GameObject> shoes; // �Ź�
    public List<GameObject> gloves; //��

    public List<GameObject> accessory01; // 
    public List<GameObject> accessory02; // 
    public List<GameObject> helmet; //����
    public List<GameObject> weapon; // ����

    public void Reset_Input()
    {
        eyebrow = new List<GameObject>();
        eye = new List<GameObject>();
        beard = new List<GameObject>();
        mouth = new List<GameObject>();
        hair = new List<GameObject>();
        head = new List<GameObject>();

        top = new List<GameObject>();
        pants = new List<GameObject>();
        shoes = new List<GameObject>();
        gloves = new List<GameObject>();

        accessory01 = new List<GameObject>();
        accessory02 = new List<GameObject>();
        helmet = new List<GameObject>();
        weapon = new List<GameObject>();
    }
} //������ ����Ʈ�� ����

public class Customize_Character : MonoBehaviour
{
    public Character character = new Character(); //ĳ���� ��ü ����
    public Character_Moudle character_moudle = new Character_Moudle(); //������ ��ü ����

    private Auth_Controller ac;

    private void Start()
    {
        if (GameObject.Find("Title_Console")) {
            ac = GameObject.Find("Title_Console").GetComponent<Auth_Controller>();
        } else {
            print("�������� �Դϴ�.");
        }
        Check_Module();
    }

    public void Check_Module()
    {
        string[] value = new string[]
        { "Eyebrow_Part", "Eye_Part", "Beard_Part", "Mouth_Part", "Hair_Part", "Head_Part", //��
            "Top_Part", "Pants_Part", "Shoes_Part", "Gloves_Part", //��
            "Accessory01_Part", "Accessory02_Part", "Helmet_Part", "Weapon_Part" //�Ǽ�����
        };

        for (int i = 0; i < value.Length; i++)
        {
            Transform location = GameObject.Find(value[i].ToString()).transform; // �θ�����

            switch (value[i].ToString())
            {
                case "Eyebrow_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.eyebrow.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Eye_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.eye.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Beard_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.beard.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Mouth_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.mouth.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Hair_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.hair.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Head_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.head.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Top_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.top.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Pants_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.pants.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Shoes_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.shoes.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Gloves_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.gloves.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Accessory01_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.accessory01.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Accessory02_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.accessory02.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Helmet_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.helmet.Add(location.GetChild(j).gameObject);
                    }
                    break;
                case "Weapon_Part":
                    for (int j = 0; j < location.childCount; j++)
                    {
                        character_moudle.weapon.Add(location.GetChild(j).gameObject);
                    }
                    break;

            }
        }

    }

    public void Change_Character(string path, string name)
    {
        switch (name)
        {
            case "eyebrow":
                for (int i = 0; i < character_moudle.eyebrow.Count; i++)
                {
                    if (i != character.eyebrow)
                    {
                        character_moudle.eyebrow[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.eyebrow[i].SetActive(true);
                    }
                }
                break;
            case "eye":
                for (int i = 0; i < character_moudle.eye.Count; i++)
                {
                    if (i != character.eye)
                    {
                        character_moudle.eye[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.eye[i].SetActive(true);
                    }
                }
                break;
            case "beard":
                for (int i = 0; i < character_moudle.beard.Count; i++)
                {
                    if (i != character.beard)
                    {
                        character_moudle.beard[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.beard[i].SetActive(true);
                    }
                }
                break;
            case "mouth":
                for (int i = 0; i < character_moudle.mouth.Count; i++)
                {
                    if (i != character.mouth)
                    {
                        character_moudle.mouth[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.mouth[i].SetActive(true);
                    }
                }
                break;
            case "hair":
                for (int i = 0; i < character_moudle.hair.Count; i++)
                {
                    if (i != character.hair)
                    {
                        character_moudle.hair[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.hair[i].SetActive(true);
                    }
                }
                break;
            case "head":
                for (int i = 0; i < character_moudle.head.Count; i++)
                {
                    if (i != character.head)
                    {
                        character_moudle.head[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.head[i].SetActive(true);
                    }
                }
                break;
            case "top":
                for (int i = 0; i < character_moudle.top.Count; i++)
                {
                    if (i != character.top)
                    {
                        character_moudle.top[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.top[i].SetActive(true);
                    }
                }
                break;
            case "pants":
                for (int i = 0; i < character_moudle.pants.Count; i++)
                {
                    if (i != character.pants)
                    {
                        character_moudle.pants[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.pants[i].SetActive(true);
                    }
                }
                break;
            case "shoes":
                for (int i = 0; i < character_moudle.shoes.Count; i++)
                {
                    if (i != character.shoes)
                    {
                        character_moudle.shoes[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.shoes[i].SetActive(true);
                    }
                }
                break;
            case "gloves":
                for (int i = 0; i < character_moudle.gloves.Count; i++)
                {
                    if (i != character.gloves)
                    {
                        character_moudle.gloves[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.gloves[i].SetActive(true);
                    }
                }
                break;
            case "accessory01":
                for (int i = 0; i < character_moudle.accessory01.Count; i++)
                {
                    if (i != character.accessory01)
                    {
                        character_moudle.accessory01[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.accessory01[i].SetActive(true);
                    }
                }
                break;
            case "accessory02":
                for (int i = 0; i < character_moudle.accessory02.Count; i++)
                {
                    if (i != character.accessory02)
                    {
                        character_moudle.accessory02[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.accessory02[i].SetActive(true);
                    }
                }
                break;
            case "helmet":
                for (int i = 0; i < character_moudle.helmet.Count; i++)
                {
                    if (i != character.helmet)
                    {
                        character_moudle.helmet[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.helmet[i].SetActive(true);
                    }
                }
                break;
            case "weapon":
                for (int i = 0; i < character_moudle.weapon.Count; i++)
                {
                    if (i != character.weapon)
                    {
                        character_moudle.weapon[i].SetActive(false);
                    }
                    else
                    {
                        character_moudle.weapon[i].SetActive(true);
                    }
                }
                break;

        }
    }
}