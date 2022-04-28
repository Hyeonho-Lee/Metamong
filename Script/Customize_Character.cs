using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public class Player
{
    public string name;
    public Vector3 player_position;
}

[System.Serializable]
public class Deco
{
    public int index;
    public Vector3 position;
}

[System.Serializable]
public class Character
{
    public int head; // 두상
    public int hair; // 머리카락
    public int eyebrows; // 눈썹
    public int nose; // 코
    public int mouths; // 입
    public int body; // 성별
    public int top; // 상의
    public int pants; // 하의
    public int shoes; // 신발

    public int accessory_1; // 머리 장식
    public int accessory_2; // 귀 장식
    public int accessory_3; // 얼굴 장식
}

[System.Serializable]
public class Character_Moudle
{
    public List<GameObject> head; // 두상
    public List<GameObject> hair; // 머리카락
    public List<GameObject> eyebrows; // 눈썹
    public List<GameObject> nose; // 코
    public List<GameObject> mouths; // 입
    public List<GameObject> body; // 성별
    public List<GameObject> top; // 상의
    public List<GameObject> pants; // 하의
    public List<GameObject> shoes; // 신발

    public List<GameObject> accessory_1; // 머리 장식
    public List<GameObject> accessory_2; // 귀 장식
    public List<GameObject> accessory_3; // 얼굴 장식

    public void Reset_Input()
    {
        head = new List<GameObject>();
        hair = new List<GameObject>();
        eyebrows = new List<GameObject>();
        nose = new List<GameObject>();
        mouths = new List<GameObject>();
        body = new List<GameObject>();
        top = new List<GameObject>();
        pants = new List<GameObject>();
        shoes = new List<GameObject>();

        accessory_1 = new List<GameObject>();
        accessory_2 = new List<GameObject>();
        accessory_3 = new List<GameObject>();
    }
}

public class Customize_Character : MonoBehaviour
{
    //public List<Player> player = new List<Player>();
    public List<Deco> deco = new List<Deco>();
    public List<Character> character = new List<Character>();

    public Character_Moudle character_moudle = new Character_Moudle();

    public void Check_Module()
    {
        character_moudle.Reset_Input();

        string[] value = new string[] { "Head", "Body", "Top", "Pants" };

        for (int i = 0; i < value.Length; i++) {
            DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/Prefab/Character/" + value[i]);
            FileInfo[] file = di.GetFiles("*.prefab");

            for (int j = 0; j < file.Length; j++) {
                string path = "Assets\\Prefab\\Character\\" + value[i] + "\\" + file[j].Name;
                GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

                switch(value[i]) {
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
        }
    }

    public void Create_Chatacter()
    {
        for (int i = 0; i < character.Count; i++) {

            Transform test = MonoBehaviour.FindObjectOfType<Customize_Character>().transform;

            if (test.childCount == 0) {

                GameObject head = MonoBehaviour.Instantiate(character_moudle.head[character[i].head]);
                head.name = "headssssss";
                head.transform.position = new Vector3(0, 2, 0);
                head.transform.SetParent(test);

                GameObject body = MonoBehaviour.Instantiate(character_moudle.body[character[i].body]);
                body.name = "body";
                body.transform.position = new Vector3(0, 1, 0);
                body.transform.SetParent(test);

                GameObject top = MonoBehaviour.Instantiate(character_moudle.top[character[i].top]);
                top.name = "top";
                top.transform.position = new Vector3(0, -1, 0);
                top.transform.SetParent(test);

                GameObject pants = MonoBehaviour.Instantiate(character_moudle.pants[character[i].pants]);
                pants.name = "head";
                pants.transform.position = new Vector3(0, -3, 0);
                pants.transform.SetParent(test);

                //print("캐릭터를 생성 하였습니다.");
            } else {
                for (int j = 0; j < 4; j++) {
                    MonoBehaviour.DestroyImmediate(test.GetChild(0).gameObject, true);
                }
                //print("캐릭터를 삭제 하였습니다.");
            }
        }
    }
}