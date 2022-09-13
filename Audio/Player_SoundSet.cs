using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Player_SoundSet : MonoBehaviourPunCallbacks, IPunObservable
{
    public List<AudioClip> all_audio = new List<AudioClip>();

    private PhotonView PV;
    private AudioSource audioSource;

    public void Start()
    {
        PV = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();

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

            audioSource.Play();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
