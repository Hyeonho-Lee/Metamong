using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendRoomInfo : MonoBehaviour
{
    private Customize_Room cr;
    private Auth_Controller ac;

    private void Start()
    {
        cr = GameObject.Find("Room_Console").GetComponent<Customize_Room>();

        if (GameObject.Find("Send_Info")) {
            ac = GameObject.Find("Send_Info").GetComponent<Auth_Controller>();
        } else {
            print("오프라인 입니다.");
        }
    }

    public void Save_Room()
    {
        ac.Update_Room_Custom();
    }

    public void Load_Room()
    {
        StartCoroutine(Load_Rooms());
    }

    IEnumerator Load_Rooms()
    {
        ac.Get_Room_Custom();

        yield return new WaitForSeconds(0.5f);

        cr.room.wall01 = ac.rc_user.wall01;
        cr.room.wall_accessory01 = ac.rc_user.wall_accessory01;
        cr.room.ground_accessory01 = ac.rc_user.ground_accessory01;
        cr.room.wall02 = ac.rc_user.wall02;
        cr.room.wall_accessory02 = ac.rc_user.wall_accessory02;
        cr.room.ground_accessory02 = ac.rc_user.ground_accessory02;
        cr.room.ground = ac.rc_user.ground;
        cr.room.chair01 = ac.rc_user.chair01;
        cr.room.chair02 = ac.rc_user.chair02;
        cr.room.table = ac.rc_user.table;
        cr.room.table_accessory01 = ac.rc_user.table_accessory01;

        cr.Check_Module();
    }
}
