using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class Player_SoundSet : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool is_voice;
    public bool is_voice_lock;

    public List<AudioClip> all_audio = new List<AudioClip>();

    private PhotonView PV;
    private AudioSource audioSource;
    private PhotonVoiceNetwork PVN;
    private Player_Cursor PC;
    private Player_Chat P_Chat;

    private void Start()
    {
        is_voice = true;

        PV = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
        PVN = GameObject.Find("Voice_Manager").GetComponent<PhotonVoiceNetwork>();

        if (SceneManager.GetActiveScene().name == "Room_World") {
            PC = GameObject.Find("Room_Console").GetComponent<Player_Cursor>();
        } else {
            PC = GameObject.Find("World_Console").GetComponent<Player_Cursor>();
        }

        P_Chat = GetComponent<Player_Chat>();

        if (PV.IsMine) {
            switch (SceneManager.GetActiveScene().name) {
                case "Title":
                    audioSource.clip = all_audio[1];
                    break;
                case "Main_World":
                    audioSource.clip = all_audio[0];
                    break;
                case "House_Custom":
                    audioSource.clip = all_audio[3];
                    break;
                case "Room_Custom":
                    audioSource.clip = all_audio[4];
                    break;
                case "Character_Custom":
                    audioSource.clip = all_audio[5];
                    break;
            }

            //audioSource.Play();

            StartCoroutine(Voice_Off(2.0f));
            StartCoroutine(V_All(1.0f));
        }
    }

    private void Update()
    {
        if (PV.IsMine) {
            if (Input.GetKeyDown(KeyCode.V)) {
                StartCoroutine(Voice_On(2.0f));
                StartCoroutine(Voice_Off(2.0f));
            }
        }
    }

    IEnumerator Voice_On(float delay)
    {
        if (!is_voice && !is_voice_lock) {
            is_voice_lock = true;
            is_voice = false;
            PC.voice_image.sprite = PC.voice_load;
            yield return new WaitForSeconds(delay);
            is_voice = true;
            is_voice_lock = false;
            PC.voice_image.sprite = PC.voice_on;
            VoiceSwitchOnClick();
            StartCoroutine(V_All(1.0f));
            //print("온라인");
        }
    }

    IEnumerator Voice_Off(float delay)
    {
        if (is_voice && !is_voice_lock) {
            is_voice_lock = true;
            is_voice = true;
            PC.voice_image.sprite = PC.voice_load;
            yield return new WaitForSeconds(delay);
            is_voice = false;
            is_voice_lock = false;
            PC.voice_image.sprite = PC.voice_off;
            VoiceSwitchOnClick();
            StartCoroutine(V_All(1.0f));
            //print("오프라인");
        }
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(V_All(1.0f));
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        StartCoroutine(V_All(1.0f));
    }

    IEnumerator V_All(float delay)
    {
        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(delay);
            PV.RPC("VoiceIcon", RpcTarget.All);
        }
    }

    [PunRPC]
    private void VoiceIcon()
    {
        if (is_voice) {
            P_Chat.voice_onn.SetActive(true);
        }

        if (!is_voice) {
            P_Chat.voice_onn.SetActive(false);
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) {
            stream.SendNext(is_voice);
        } else {
            is_voice = (bool)stream.ReceiveNext();
        }
    }
}
