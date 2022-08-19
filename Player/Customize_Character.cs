using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

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

public class Customize_Character : MonoBehaviourPunCallbacks, IPunObservable
{
    public Character character = new Character(); //ĳ���� ��ü ����
    public Character_Moudle character_moudle = new Character_Moudle(); //������ ��ü ����

    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        Check_Module();
    }

    private void Update()
    {
        if (PV.IsMine) {
            PV.RPC("Change_All", RpcTarget.AllBuffered);
        }
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

        /*string[] value = new string[]
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
        }*/

    }

    public void Change_Character(string path, string name)
    {
        switch (name)
        {
            case "eyebrow":
                for (int i = 0; i < this.character_moudle.eyebrow.Count; i++)
                {
                    if (i != this.character.eyebrow)
                    {
                        this.character_moudle.eyebrow[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.eyebrow[i].SetActive(true);
                    }
                }
                break;
            case "eye":
                for (int i = 0; i < this.character_moudle.eye.Count; i++)
                {
                    if (i != this.character.eye)
                    {
                        this.character_moudle.eye[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.eye[i].SetActive(true);
                    }
                }
                break;
            case "beard":
                for (int i = 0; i < this.character_moudle.beard.Count; i++)
                {
                    if (i != this.character.beard)
                    {
                        this.character_moudle.beard[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.beard[i].SetActive(true);
                    }
                }
                break;
            case "mouth":
                for (int i = 0; i < this.character_moudle.mouth.Count; i++)
                {
                    if (i != this.character.mouth)
                    {
                        this.character_moudle.mouth[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.mouth[i].SetActive(true);
                    }
                }
                break;
            case "hair":
                for (int i = 0; i < this.character_moudle.hair.Count; i++)
                {
                    if (i != this.character.hair)
                    {
                        this.character_moudle.hair[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.hair[i].SetActive(true);
                    }
                }
                break;
            case "head":
                for (int i = 0; i < this.character_moudle.head.Count; i++)
                {
                    if (i != this.character.head)
                    {
                        this.character_moudle.head[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.head[i].SetActive(true);
                    }
                }
                break;
            case "top":
                for (int i = 0; i < this.character_moudle.top.Count; i++)
                {
                    if (i != this.character.top)
                    {
                        this.character_moudle.top[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.top[i].SetActive(true);
                    }
                }
                break;
            case "pants":
                for (int i = 0; i < this.character_moudle.pants.Count; i++)
                {
                    if (i != this.character.pants)
                    {
                        this.character_moudle.pants[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.pants[i].SetActive(true);
                    }
                }
                break;
            case "shoes":
                for (int i = 0; i < this.character_moudle.shoes.Count; i++)
                {
                    if (i != this.character.shoes)
                    {
                        this.character_moudle.shoes[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.shoes[i].SetActive(true);
                    }
                }
                break;
            case "gloves":
                for (int i = 0; i < this.character_moudle.gloves.Count; i++)
                {
                    if (i != this.character.gloves)
                    {
                        this.character_moudle.gloves[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.gloves[i].SetActive(true);
                    }
                }
                break;
            case "accessory01":
                for (int i = 0; i < this.character_moudle.accessory01.Count; i++)
                {
                    if (i != this.character.accessory01)
                    {
                        this.character_moudle.accessory01[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.accessory01[i].SetActive(true);
                    }
                }
                break;
            case "accessory02":
                for (int i = 0; i < this.character_moudle.accessory02.Count; i++)
                {
                    if (i != this.character.accessory02)
                    {
                        this.character_moudle.accessory02[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.accessory02[i].SetActive(true);
                    }
                }
                break;
            case "helmet":
                for (int i = 0; i < this.character_moudle.helmet.Count; i++)
                {
                    if (i != this.character.helmet)
                    {
                        this.character_moudle.helmet[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.helmet[i].SetActive(true);
                    }
                }
                break;
            case "weapon":
                for (int i = 0; i < this.character_moudle.weapon.Count; i++)
                {
                    if (i != this.character.weapon)
                    {
                        this.character_moudle.weapon[i].SetActive(false);
                    }
                    else
                    {
                        this.character_moudle.weapon[i].SetActive(true);
                    }
                }
                break;

        }
    }

    [PunRPC]
    private void Change_All()
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            stream.SendNext(character.eyebrow);
            stream.SendNext(character.eye);
            stream.SendNext(character.beard);
            stream.SendNext(character.mouth);
            stream.SendNext(character.hair);
            stream.SendNext(character.head);
            stream.SendNext(character.top);
            stream.SendNext(character.pants);
            stream.SendNext(character.shoes);
            stream.SendNext(character.gloves);
            stream.SendNext(character.accessory01);
            stream.SendNext(character.accessory02);
            stream.SendNext(character.helmet);
        } else {
            character.eyebrow = (int)stream.ReceiveNext();
            character.eye = (int)stream.ReceiveNext();
            character.beard = (int)stream.ReceiveNext();
            character.mouth = (int)stream.ReceiveNext();
            character.hair = (int)stream.ReceiveNext();
            character.head = (int)stream.ReceiveNext();
            character.top = (int)stream.ReceiveNext();
            character.pants = (int)stream.ReceiveNext();
            character.shoes = (int)stream.ReceiveNext();
            character.gloves = (int)stream.ReceiveNext();
            character.accessory01 = (int)stream.ReceiveNext();
            character.accessory02 = (int)stream.ReceiveNext();
            character.helmet = (int)stream.ReceiveNext();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //PV.RPC("Change_All", RpcTarget.AllBuffered);
        print(newPlayer + " ���� �����Ͽ����ϴ�.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print(otherPlayer + " ���� �������ϴ�.");
    }
}