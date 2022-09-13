using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;

public class World_Navigation : MonoBehaviourPunCallbacks
{
    public int value;
    public bool is_navi;

    public Button button;
    public GameObject Navi_Effect;
    public List<Transform> All_Position;

    [Header("House_Prefab")]
    public List<GameObject> A_House_Prefab;
    public List<GameObject> B_House_Prefab;
    public List<GameObject> C_House_Prefab;

    public List<int> nullList = Enumerable.Range(0, 42).ToList();

    private float navi_time;
    private float navi_realtime;

    private GameObject House_Position;
    private GameObject Navi_Effect_On;

    private Auth_Controller ac;
    private PhotonView PV;


    private void Start()
    {
        ac = GetComponent<Auth_Controller>();
        PV = GetComponent<PhotonView>();

        All_Position = new List<Transform>();
        A_House_Prefab = new List<GameObject>();
        B_House_Prefab = new List<GameObject>();
        C_House_Prefab = new List<GameObject>();

        House_Position = GameObject.Find("House_Position");

        for (int j = 0; j < House_Position.transform.GetChildCount(); j++) {
            Transform Position = House_Position.transform.GetChild(j);

            for (int i = 0; i < Position.childCount; i++) {
                All_Position.Add(Position.GetChild(i));
            }
        }

        Check_House_Prefab();

        navi_time = 10.0f;
        navi_realtime = navi_time;
    }

    private void Update()
    {
        if (!is_navi) {
            if (navi_realtime <= navi_time) {
                navi_realtime += Time.deltaTime;
            } else {
                is_navi = true;
            }
        } else {
            if (Navi_Effect_On != null) {
                Destroy(Navi_Effect_On);
                button.gameObject.SetActive(true);
            }
        }
    }

    public void Click_Button()
    {
        button.gameObject.SetActive(false);

        Vector3 Spawn_Position = All_Position[value].transform.position;

        if (is_navi) {
            Navi_Effect_On = Instantiate(Navi_Effect, Spawn_Position - new Vector3(0, 10.0f, 0), Quaternion.identity);
        }

        is_navi = false;
        navi_realtime = 0;
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

    public override void OnJoinedRoom()
    {
        StartCoroutine(Create_House(1.0f));
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        StartCoroutine(Create_House(1.0f));
    }

    private GameObject[] house_parents;
    private GameObject[] portal_parents;

    IEnumerator Create_House(float delay)
    {
        ac.Get_House();
        ac.Get_All_House();

        yield return new WaitForSeconds(delay);

        PV.RPC("Destroy_House", RpcTarget.AllBuffered);

        for (int i = 0; i < ac.h_position_index.Count; i++) {
            int j = ac.h_position_index[i] - 1;
            //nullList.RemoveAt(j);
            nullList[j] = -1;
        }

        // 后磊府 扒拱 积己
        for (int i = 0; i < nullList.Count; i++) {
            if (0 <= nullList[i] && nullList[i] <= 14) {
                Vector3 portal = All_Position[nullList[i]].localPosition + (All_Position[nullList[i]].forward * 15.0f) - All_Position[nullList[i]].up * 10.0f;
                GameObject portals = PhotonNetwork.Instantiate("cube", portal, Quaternion.identity);
                portals.name = (i + 1).ToString();

                Vector3 dir = (All_Position[nullList[i]].localPosition - new Vector3(0f, 10f, 0f)) - portal;

                Quaternion forcus = Quaternion.LookRotation(dir);
                forcus = forcus * Quaternion.Euler(new Vector3(-90f, 0f, 0f));

                GameObject house = PhotonNetwork.Instantiate("house_prefab/A/A_0", All_Position[nullList[i]].localPosition - new Vector3(0f, 10f, 0f), forcus);
            } else if (15 <= nullList[i] && nullList[i] <= 26) {
                Vector3 portal = All_Position[nullList[i]].localPosition + (All_Position[nullList[i]].forward * 15.0f) - All_Position[nullList[i]].up * 10.0f;
                GameObject portals = PhotonNetwork.Instantiate("cube", portal, Quaternion.identity);
                portals.name = (i + 1).ToString();

                Vector3 dir = (All_Position[nullList[i]].localPosition - new Vector3(0f, 10f, 0f)) - portal;

                Quaternion forcus = Quaternion.LookRotation(dir);
                forcus = forcus * Quaternion.Euler(new Vector3(-90f, 0f, 0f));

                GameObject house = PhotonNetwork.Instantiate("house_prefab/B/B_0", All_Position[nullList[i]].localPosition - new Vector3(0f, 10f, 0f), forcus);
            } else if (27 <= nullList[i] && nullList[i] <= 42) {
                Vector3 portal = All_Position[nullList[i]].localPosition + (All_Position[nullList[i]].forward * 15.0f) - All_Position[nullList[i]].up * 10.0f;
                GameObject portals = PhotonNetwork.Instantiate("cube", portal, Quaternion.identity);
                portals.name = (i + 1).ToString();

                Vector3 dir = (All_Position[nullList[i]].localPosition - new Vector3(0f, 10f, 0f)) - portal;

                Quaternion forcus = Quaternion.LookRotation(dir);
                forcus = forcus * Quaternion.Euler(new Vector3(-90f, 0f, 0f));

                GameObject house = PhotonNetwork.Instantiate("house_prefab/C/C_0", All_Position[nullList[i]].localPosition - new Vector3(0f, 10f, 0f), forcus);
            }
        }

        // 备概茄 磊府 扒拱 积己
        for (int i = 0; i < ac.h_position_index.Count; i++) {
            if (0 <= ac.h_position_index[i] && ac.h_position_index[i] <= 14 && ac.h_is_house[i]) {
                Vector3 portal = All_Position[ac.h_position_index[i] - 1].localPosition + (All_Position[ac.h_position_index[i] - 1].forward * 15.0f) - All_Position[ac.h_position_index[i] - 1].up * 10.0f;
                GameObject portals = PhotonNetwork.Instantiate("cube", portal, Quaternion.identity);
                portals.name = (i + 2).ToString();

                Vector3 dir = (All_Position[ac.h_position_index[i] - 1].localPosition - new Vector3(0f, 10f, 0f)) - portal;

                Quaternion forcus = Quaternion.LookRotation(dir);
                forcus = forcus * Quaternion.Euler(new Vector3(-90f, 0f, 0f));

                GameObject house = PhotonNetwork.Instantiate("house_prefab/A/" + A_House_Prefab[ac.h_house_index[i]].name, All_Position[ac.h_position_index[i] - 1].localPosition - new Vector3(0f, 10f, 0f), forcus);
            } else if (15 <= ac.h_position_index[i] && ac.h_position_index[i] <= 26 && ac.h_is_house[i]) {
                Vector3 portal = All_Position[ac.h_position_index[i] - 1].localPosition + (All_Position[ac.h_position_index[i] - 1].forward * 15.0f) - All_Position[ac.h_position_index[i] - 1].up * 10.0f;
                GameObject portals = PhotonNetwork.Instantiate("cube", portal, Quaternion.identity);
                portals.name = (i + 2).ToString();

                Vector3 dir = (All_Position[ac.h_position_index[i] - 1].localPosition - new Vector3(0f, 10f, 0f)) - portal;

                Quaternion forcus = Quaternion.LookRotation(dir);
                forcus = forcus * Quaternion.Euler(new Vector3(-90f, 0f, 0f));

                GameObject house = PhotonNetwork.Instantiate("house_prefab/B/" + B_House_Prefab[ac.h_house_index[i]].name, All_Position[ac.h_position_index[i] - 1].localPosition - new Vector3(0f, 10f, 0f), forcus);
            } else if (27 <= ac.h_position_index[i] && ac.h_position_index[i] <= 42 && ac.h_is_house[i]) {
                Vector3 portal = All_Position[ac.h_position_index[i] - 1].localPosition + (All_Position[ac.h_position_index[i] - 1].forward * 15.0f) - All_Position[ac.h_position_index[i] - 1].up * 10.0f;
                GameObject portals = PhotonNetwork.Instantiate("cube", portal, Quaternion.identity);
                portals.name = (i + 2).ToString();

                Vector3 dir = (All_Position[ac.h_position_index[i] - 1].localPosition - new Vector3(0f, 10f, 0f)) - portal;

                Quaternion forcus = Quaternion.LookRotation(dir);
                forcus = forcus * Quaternion.Euler(new Vector3(-90f, 0f, 0f));

                GameObject house = PhotonNetwork.Instantiate("house_prefab/C/" + C_House_Prefab[ac.h_house_index[i]].name, All_Position[ac.h_position_index[i] - 1].localPosition - new Vector3(0f, 10f, 0f), forcus);
            }
        }
    }

    [PunRPC]

    public void Destroy_House()
    {
        house_parents = new GameObject[0];
        portal_parents = new GameObject[0];

        if (GameObject.FindGameObjectWithTag("World_House")) {
            house_parents = GameObject.FindGameObjectsWithTag("World_House");

            for (int i = 0; i < house_parents.Length; i++) {
                Destroy(house_parents[i]);
            }
        }

        if (GameObject.FindGameObjectWithTag("World_Portal")) {
            portal_parents = GameObject.FindGameObjectsWithTag("World_Portal");

            for (int i = 0; i < portal_parents.Length; i++) {
                Destroy(portal_parents[i]);
            }
        }
    }
}
