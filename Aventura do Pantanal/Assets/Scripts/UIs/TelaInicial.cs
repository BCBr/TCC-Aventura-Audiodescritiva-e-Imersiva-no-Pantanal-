using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaInicial : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameObject esquerda;

    [SerializeField]
    private GameObject direita;

    [SerializeField]
    private AudioClip[] soundsOfIntroduction;

    [SerializeField]
    private int currentAudioClip = 0;

    [SerializeField]
    private bool canActivateNextAudioClip = true;

    public int TapCount;
    public float MaxNextTapTime;
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
        if (!audioSource.isPlaying && currentAudioClip < soundsOfIntroduction.Length && canActivateNextAudioClip)
        {
            audioSource.PlayOneShot(soundsOfIntroduction[currentAudioClip]);
            ActiveLeftSound();
            ActiveRightSound();
            currentAudioClip++;

            verifyCurrentAudioClipToStop(2);

            verifyCurrentAudioClipToStop(4);
            verifyCurrentAudioClipToStop(5);
        }
    }

    private void goToNextScene()
    {
        if (!audioSource.isPlaying && currentAudioClip >= soundsOfIntroduction.Length && canActivateNextAudioClip)
        {
            ChangeSceneManager.GoToSomeScene("SelecaoDeFases");
        }
    }

    private void verifyCurrentAudioClipToStop(int phaseToStop)
    {
        if (currentAudioClip == phaseToStop)
            canActivateNextAudioClip = false;
    }

    private void ActiveLeftSound()
    {
        if(currentAudioClip == 3 && canActivateNextAudioClip)
        {
            esquerda.SetActive(true);
        }
    }

    private void ActiveRightSound()
    {
        if (currentAudioClip == 4 && canActivateNextAudioClip)
        {
            direita.SetActive(true);
            esquerda.SetActive(false);
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
                if (TapCount == 1)
                {
                    canActivateNextAudioClip = true;
                }

                TapCount = 0;
            }
        }
    }
}
