using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;

public class House_Console : MonoBehaviour
{
    public string Level_Label;
    public int spot_index;

    [Header("UI_Panel")]
    public GameObject Position_Panel;
    public GameObject House_Panel;
    public GameObject Index_Panel;

    [Header("House_Transform")]
    public List<Transform> All_Position;

    [Header ("House_Prefab")]
    public List<GameObject> A_House_Prefab;
    public List<GameObject> B_House_Prefab;
    public List<GameObject> C_House_Prefab;

    [Header("House_Sprite")]
    public List<Sprite> A_House_Sprite;
    public List<Sprite> B_House_Sprite;
    public List<Sprite> C_House_Sprite;
    public List<Sprite> Rank_Sprite;

    private bool is_index;

    private Vector3 locals;
    private GameObject House_Position;

    private House_Camera house_camera;
    private House_Manager house_manager;
    private Auth_Controller ac;

    private void Start()
    {
        if (GameObject.Find("Send_Info")) {
            ac = GameObject.Find("Send_Info").GetComponent<Auth_Controller>();
        }

        house_camera = GetComponent<House_Camera>();
        house_manager = GetComponent<House_Manager>();

        All_Position = new List<Transform>();
        A_House_Prefab = new List<GameObject>();
        B_House_Prefab = new List<GameObject>();
        C_House_Prefab = new List<GameObject>();

        A_House_Sprite = new List<Sprite>();
        B_House_Sprite = new List<Sprite>();
        C_House_Sprite = new List<Sprite>();

        House_Position = GameObject.Find("House_Position");

        for (int j = 0; j < House_Position.transform.childCount; j++) {
            Transform Position = House_Position.transform.GetChild(j);

            for (int i = 0; i < Position.childCount; i++) {
                All_Position.Add(Position.GetChild(i));
            }
        }

        Check_House_Prefab();
        Check_House_Sprite();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void zoom_on()
    {
        string index_string = EventSystem.current.currentSelectedGameObject.name;
        int value = int.Parse(index_string);
        house_manager.find_house = false;

        //print(value);
        //print(house_manager.position_index);

        for (int i = 0; i < ac.h_position_index.Count; i++) {
            if (ac.h_position_index[i] == value + 1) {
                house_manager.find_house = true;
            }
        }

        if (house_manager.position_index == value + 1 && house_manager.find_house) {

            if (0 <= value && value <= 14) {
                Level_Label = "A";
            } else if (15 <= value && value <= 26) {
                Level_Label = "B";
            } else if (27 <= value && value <= 41) {
                Level_Label = "C";
            }

            spot_index = value;
            house_camera.pos_house = All_Position[value].localPosition + (All_Position[value].forward * 30.0f) + (-All_Position[value].right * 3.0f);
            Vector3 look_dir = All_Position[value].localPosition - house_camera.pos_house;

            house_camera.is_camera = true;
            house_camera.camera_empty = house_camera.pos_house;
            house_camera.camera_look = look_dir;

            Position_Panel.SetActive(false);
            House_Panel.SetActive(true);

            switch (Level_Label) {
                case "A":
                    house_manager.Create_House_Button(A_House_Prefab.Count);
                    break;
                case "B":
                    house_manager.Create_House_Button(B_House_Prefab.Count);
                    break;
                case "C":
                    house_manager.Create_House_Button(C_House_Prefab.Count);
                    break;
            }
        }
    }

    public void zoom_off()
    {
        if (house_camera.is_camera) {
            house_camera.is_camera = false;
            house_camera.camera_empty = house_camera.camera_position;

            Position_Panel.SetActive(true);
            House_Panel.SetActive(false);

            is_index = false;
            StartCoroutine(On_Index(4.5f));

            Level_Label = "";
        }
    }

    IEnumerator On_Index(float delay)
    {
        if (!is_index) {
            Index_Panel.SetActive(false);
            yield return new WaitForSeconds(delay);
            Index_Panel.SetActive(true);

            is_index = true;
        }
    }

    public void Check_House_Prefab()
    {
        GameObject[] a_house = Resources.LoadAll<GameObject>("house_prefab/A");
        GameObject[] b_house = Resources.LoadAll<GameObject>("house_prefab/B");
        GameObject[] c_house = Resources.LoadAll<GameObject>("house_prefab/C");

        A_House_Prefab = a_house.OfType<GameObject>().ToList();
        B_House_Prefab = b_house.OfType<GameObject>().ToList();
        C_House_Prefab = c_house.OfType<GameObject>().ToList();
    }

    public void Check_House_Sprite()
    {
        Sprite[] a_house = Resources.LoadAll<Sprite>("house_sprite/A");
        Sprite[] b_house = Resources.LoadAll<Sprite>("house_sprite/B");
        Sprite[] c_house = Resources.LoadAll<Sprite>("house_sprite/C");

        A_House_Sprite = a_house.OfType<Sprite>().ToList();
        B_House_Sprite = b_house.OfType<Sprite>().ToList();
        C_House_Sprite = c_house.OfType<Sprite>().ToList();
    }

    public void Change_House(int index)
    {
        string index_string;

        if (index >= 0) {
            index_string = index.ToString();
        } else {
            index_string = EventSystem.current.currentSelectedGameObject.name;
        }

        int value = int.Parse(index_string);

        if (All_Position[spot_index].transform.childCount != 0) {
            for (int i = 0; i < All_Position[spot_index].transform.childCount; i++) {
                Destroy(All_Position[spot_index].transform.GetChild(0).gameObject);
            }
        }

            switch (Level_Label) {
                case "A":
                    GameObject a_house = Instantiate(A_House_Prefab[value], All_Position[spot_index]);
                    break;
                case "B":
                    GameObject b_house = Instantiate(B_House_Prefab[value], All_Position[spot_index]);
                    break;
                case "C":
                    GameObject c_house = Instantiate(C_House_Prefab[value], All_Position[spot_index]);
                    break;
            }
    }

    public void Change_Scene()
    {
        GameObject Send_Info = GameObject.Find("Send_Info");
        Send_Info.name = "Title_Console";

        GameObject Send_Spawn = Instantiate(Send_Info); ;
        Send_Spawn.name = "Send_Spawn";

        DontDestroyOnLoad(Send_Info);
        DontDestroyOnLoad(Send_Spawn);

        SceneManager.LoadScene("Main_World");
    }
}
