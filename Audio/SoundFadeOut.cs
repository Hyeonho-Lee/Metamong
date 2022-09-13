using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFadeOut : MonoBehaviour
{
    private AudioSource audioSource;
    public double fadeOutSeconds = 1.0;
    bool isFadeOut = true;
    double fadeDeltaTime = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isFadeOut) {
            fadeDeltaTime += Time.deltaTime;
            if (fadeDeltaTime >= fadeOutSeconds) {
                fadeDeltaTime = fadeOutSeconds;
                isFadeOut = false;
            }
            audioSource.volume = (float)(1.0 - (fadeDeltaTime / fadeOutSeconds));
        }
    }

}
