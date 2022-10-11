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
    public int eyebrow; //눈썹
    public int eye; //눈
    public int beard; //수염
    public int mouth; //입
    public int hair; // 머리카락
    public int head; //코 모양

    public int top; //상의
    public int pants; //하의
    public int shoes; //신발
    public int gloves; //손

    public int accessory01; //얼굴 악세서리
    public int accessory02; //목에 거는거
    public int helmet; //모자
    public int weapon; //무기

} //캐릭터 구성요소

[System.Serializable]
public class Character_Moudle
{
    public List<GameObject> eyebrow; // 눈썹
    public List<GameObject> eye; // 눈
    public List<GameObject> beard; // 수염
    public List<GameObject> mouth; // 입
    public List<GameObject> hair; // 머리카락
    public List<GameObject> head; //코 모양

    public List<GameObject> top; // 상의
    public List<GameObject> pants; // 하의
    public List<GameObject> shoes; // 신발
    public List<GameObject> gloves; //손

    public List<GameObject> accessory01; // 
    public List<GameObject> accessory02; // 
    public List<GameObject> helmet; //모자
    public List<GameObject> weapon; // 무기

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
} //아이템 리스트로 관리

public class Customize_Character : MonoBehaviourPunCallbacks, IPunObservable
{
    public Character character = new Character(); //캐릭터 개체 생성
    public Character_Moudle character_moudle = new Character_Moudle(); //아이템 개체 생성

    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        Check_Module();

        if (PV.IsMine) {
            StartCoroutine(C_All(1.0f));
        }
    }

    private void Update()
    {
        /*if (PV.IsMine) {
            PV.RPC("Change_All", RpcTarget.AllBuffered);
        }*/
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PV.IsMine) {
            StartCoroutine(C_All(1.0f));
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PV.IsMine) {
            StartCoroutine(C_All(1.0f));
        }
    }

    IEnumerator C_All(float delay)
    {
        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(delay);
            PV.RPC("Change_All", RpcTarget.All);
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
}