using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World_Navigation : MonoBehaviour
{
    public int value;
    public bool is_navi;

    public Button button;
    public GameObject Navi_Effect;
    public List<Transform> All_Position;

    private float navi_time;
    private float navi_realtime;

    private GameObject House_Position;
    private GameObject Navi_Effect_On;


    private void Start()
    {
        All_Position = new List<Transform>();

        House_Position = GameObject.Find("House_Position");

        for (int j = 0; j < House_Position.transform.GetChildCount(); j++) {
            Transform Position = House_Position.transform.GetChild(j);

            for (int i = 0; i < Position.childCount; i++) {
                All_Position.Add(Position.GetChild(i));
            }
        }

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
}
