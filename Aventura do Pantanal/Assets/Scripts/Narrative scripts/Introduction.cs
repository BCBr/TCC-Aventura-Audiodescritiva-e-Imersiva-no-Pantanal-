using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] soundsOfIntroduction;

    [SerializeField]
    private int currentAudioClip = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goToNextAudioClip();
        goToNextScene();
    }

    private void goToNextAudioClip()
    {
        if(!audioSource.isPlaying && currentAudioClip < soundsOfIntroduction.Length)
        {
            audioSource.PlayOneShot(soundsOfIntroduction[currentAudioClip]);
            currentAudioClip++;
        }
    }

    private void goToNextScene()
    {
        if(!audioSource.isPlaying && currentAudioClip >= soundsOfIntroduction.Length)
        {
            ChangeSceneManager.GoToSomeScene("2 Tutorial");
        }
    }
}
