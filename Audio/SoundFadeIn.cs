using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFadeIn : MonoBehaviour
{
    private AudioSource audioSource;
    public double fadeInSeconds = 3.0;
    bool isFadeIn = true;
    double fadeDeltaTime = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isFadeIn) {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime >= fadeInSeconds) {
                fadeDeltaTime = fadeInSeconds;
                isFadeIn = false;
            }
            audioSource.volume = (float)(fadeDeltaTime / fadeInSeconds);
        }

    }

}
