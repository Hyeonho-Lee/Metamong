using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Info : MonoBehaviour
{
    public string userName;
    public string localId;
    public string idToken;
    public bool is_load;

    private Customize_Character cc;
    private Auth_Controller ac;

    private void Update()
    {
        if (GameObject.Find("Title_Console")) {
            GameObject empty = GameObject.Find("Title_Console");
            userName = empty.gameObject.GetComponent<Auth_Controller>().userName;
            localId = empty.gameObject.GetComponent<Auth_Controller>().localId;
            idToken = empty.gameObject.GetComponent<Auth_Controller>().idToken;
            Destroy(empty);
        }

        if (userName != null && localId != null && cc == null) {
            cc = GetComponent<Customize_Character>();
            ac = GameObject.Find("World_Console").GetComponent<Auth_Controller>();
        }

        if (userName != null && localId != null && cc != null && !is_load) {
            Load_Character();
            is_load = true;
        }
    }

    public void Load_Character()
    {
        StartCoroutine(Load_Characters());
    }

    IEnumerator Load_Characters()
    {
        ac.userName = userName;
        ac.localId = localId;
        ac.idToken = idToken;

        ac.Get_Character_Button();

        yield return new WaitForSeconds(0.2f);

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

        cc.Change_Character("Eyebrow_Part", "eyebrow");
        cc.Change_Character("Eyebrow_Part", "eyebrow");
        cc.Change_Character("Eye_Part", "eye");
        cc.Change_Character("Beard_Part", "beard");
        cc.Change_Character("Mouth_Part", "mouth");
        cc.Change_Character("Hair_Part", "hair");
        cc.Change_Character("Head_Part", "head");
        cc.Change_Character("Top_Part", "top");
        cc.Change_Character("Pants_Part", "pants");
        cc.Change_Character("Shoes_Part", "shoes");
        cc.Change_Character("Gloves_Part", "gloves");
        cc.Change_Character("Accessory01_Part", "accessory01");
        cc.Change_Character("Accessory02_Part", "accessory02");
        cc.Change_Character("Helmet_Part", "helmet");
    }
}
