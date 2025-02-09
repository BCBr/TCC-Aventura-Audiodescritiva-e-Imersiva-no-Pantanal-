﻿using System.Collections;
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

    public int TapCount;
    public float MaxNextTapTime = 0.3f;
    float NewTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goToNextAudioClip();
        goToNextScene();
        TapCounter();
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

    public void TapCounter()
    {

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                NewTime = Time.time + MaxNextTapTime;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (NewTime >= Time.time)
                {
                    TapCount++;
                    NewTime = Time.time + MaxNextTapTime;
                }
                else
                {
                    TapCount = 0;
                    NewTime = 0;
                }
            }
        }

        if (TapCount > 0)
        {
            if (NewTime < Time.time)
            {
                if (TapCount == 4)
                {
                    ChangeSceneManager.GoToSomeScene("SelecaoDeFases");
                }

                TapCount = 0;
                //NewTime = 0;
            }
        }
    }
}
