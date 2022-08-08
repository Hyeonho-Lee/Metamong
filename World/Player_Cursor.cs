using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class Player_Cursor : MonoBehaviour
{
    public bool is_ctrl;
    public bool is_voice;
    private bool is_voice_lock;

    public Sprite ctrl_on;
    public Sprite ctrl_off;
    public Image ctrl_image;

    public Sprite voice_on;
    public Sprite voice_off;
    public Sprite voice_load;
    public Image voice_image;

    private PhotonVoiceNetwork PVN;

    private void Awake()
    {
        PVN = GameObject.Find("Voice_Manager").GetComponent<PhotonVoiceNetwork>();
    }

    private void Start()
    {
        is_ctrl = false;
        is_voice = true;

        StartCoroutine(Voice_Off(3.0f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            if (!is_ctrl) {
                is_ctrl = true;
            }else {
                is_ctrl = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.V)) {
            StartCoroutine(Voice_On(2.0f));
            StartCoroutine(Voice_Off(2.0f));
        }

        if (is_ctrl) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            ctrl_image.sprite = ctrl_on;
        } else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ctrl_image.sprite = ctrl_off;
        }
    }

    IEnumerator Voice_On(float delay)
    {
        if (!is_voice && !is_voice_lock) {
            is_voice_lock = true;
            is_voice = false;
            voice_image.sprite = voice_load;
            yield return new WaitForSeconds(delay);
            is_voice = true;
            is_voice_lock = false;
            voice_image.sprite = voice_on;
            VoiceSwitchOnClick();
            //print("온라인");
        }
    }

    IEnumerator Voice_Off(float delay)
    {
        if (is_voice && !is_voice_lock) {
            is_voice_lock = true;
            is_voice = true;
            voice_image.sprite = voice_load;
            yield return new WaitForSeconds(delay);
            is_voice = false;
            is_voice_lock = false;
            voice_image.sprite = voice_off;
            VoiceSwitchOnClick();
            //print("오프라인");
        }
    }

    private void VoiceSwitchOnClick()
    {
        //print(this.PVN.ClientState);
        if (this.PVN.ClientState == Photon.Realtime.ClientState.Joined) {
            this.PVN.Disconnect();
        } else if (this.PVN.ClientState == Photon.Realtime.ClientState.PeerCreated
                   || this.PVN.ClientState == Photon.Realtime.ClientState.Disconnected) {
            this.PVN.ConnectAndJoinRoom();
        }
        //print(this.PVN.ClientState);
    }
}
