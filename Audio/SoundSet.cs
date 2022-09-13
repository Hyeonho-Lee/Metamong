using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSet : MonoBehaviour
{
    //public AudioMixer masterMixer;
    //public Slider audioSlider;
    //public Animator Btnani;

    public List<AudioClip> all_audio = new List<AudioClip>();

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

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

    /*public void SetVolume()
    {
        float sound = audioSlider.value;

        if (sound == -40f) masterMixer.SetFloat("Music", -80);
        else masterMixer.SetFloat("Music", sound);
    }

    public void SetButtonVolume(float volume)
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;

        if (AudioListener.volume == 0) {
            Btnani.SetBool("Click", true);
        } else
            Btnani.SetBool("Click", false);
    }*/

}
