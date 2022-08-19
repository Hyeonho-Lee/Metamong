using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendCharacterInfo : MonoBehaviour
{
    private Custom_Character_Offline cc;
    private Auth_Controller ac;

    private void Start()
    {
        cc = GameObject.Find("Custom_Character").GetComponent<Custom_Character_Offline>();

        if (GameObject.Find("Send_Info")) {
            ac = GameObject.Find("Send_Info").GetComponent<Auth_Controller>();
        } else {
            print("오프라인 입니다.");
        }
    }

    public void Save_Character()
    {
        ac.Update_Character_Button();
    }

    public void Load_Character()
    {
        StartCoroutine(Load_Characters());
    }

    IEnumerator Load_Characters()
    {
        ac.Get_Character_Button();
        
        yield return new WaitForSeconds(0.5f);

        cc.character.eyebrow = ac.cc_user.eyebrow;
        cc.character.eye = ac.cc_user.eye;
        cc.character.beard = ac.cc_user.beard;
        cc.character.mouth = ac.cc_user.mouth;
        cc.character.hair = ac.cc_user.hair;
        cc.character.head = ac.cc_user.head;
        cc.character.top = ac.cc_user.top;
        cc.character.pants = ac.cc_user.pants;
        cc.character.shoes = ac.cc_user.shoes;
        cc.character.gloves = ac.cc_user.gloves;
        cc.character.accessory01 = ac.cc_user.accessory01;
        cc.character.accessory02 = ac.cc_user.accessory02;
        cc.character.helmet = ac.cc_user.helmet;

        cc.Change_All();
    }
}
