using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Character_Info : MonoBehaviourPunCallbacks, IPunObservable
{
    public string userName;
    public string localId;
    public string idToken;
    public bool is_load;

    private Customize_Character cc;
    private Auth_Controller ac;
    private Player_Console pc;
    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PV.IsMine) {
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
                pc = GameObject.Find("World_Console").GetComponent<Player_Console>();
            }

            if (userName != null && localId != null && cc != null && !is_load) {
                Load_Character();
                is_load = true;
            }
        }
    }

    public void Load_Character()
    {
        StartCoroutine(Load_Characters());
    }

    IEnumerator Load_Characters()
    {
        pc.Update_PlayerList();

        ac.userName = userName;
        ac.localId = localId;
        ac.idToken = idToken;

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

        //cc.Change_All();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            stream.SendNext(userName);
            stream.SendNext(localId);
            stream.SendNext(idToken);
            stream.SendNext(is_load);
        } else {
            userName = (string)stream.ReceiveNext();
            localId = (string)stream.ReceiveNext();
            idToken = (string)stream.ReceiveNext();
            is_load = (bool)stream.ReceiveNext();
        }
    }
}
