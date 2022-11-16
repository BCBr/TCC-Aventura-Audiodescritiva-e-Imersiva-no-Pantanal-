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
