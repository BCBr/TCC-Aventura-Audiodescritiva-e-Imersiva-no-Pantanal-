using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseSelection : MonoBehaviour
{
    public Joystick joystick;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private bool canChangePhase = false;

    [SerializeField]
    private int phaseSelected = 0;

    [SerializeField]
    private AudioClip[] soundsPhaseSelectionIntroduction;

    [SerializeField]
    private AudioClip[] soundsPhaseSelected;

    [SerializeField]
    private string[] phaseNames;

    public bool UnlockInstruction = true;

    public bool PlayWelcome = true;

    public bool PlayFirstPhaseSelected = true;

    void Start()
    {
        
    }

    void Awake()
    {
        audioSource.PlayOneShot(soundsPhaseSelectionIntroduction[0]);
        PlayWelcome = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playInstructionsSound();
        SetChangePhase();
        GoToSelectedPhase();
    }

    private void SetChangePhase()
    {

        if (joystick.Horizontal >= 0.3f)
        {
            if(canChangePhase & !UnlockInstruction)
            {
                canChangePhase = false;
                phaseSelected++;
                resetPhaseSelected();
                playSoundSelectedPhase();
                UnlockInstruction = true;
            }
            
        }
        else if (joystick.Horizontal <= -0.3)
        {
            if(canChangePhase & !UnlockInstruction)
            {
                canChangePhase = false;
                phaseSelected--;
                resetPhaseSelected();
                playSoundSelectedPhase();
                UnlockInstruction = true;
            }
            
        }
        else
        {
            canChangePhase = true;
        }

    }

    private void GoToSelectedPhase()
    {
        if (joystick.Vertical >= 0.3f & canChangePhase)
        {
            goToNextScene();
        }
    }

    private void resetPhaseSelected()
    {
        if (phaseSelected > 2)
            phaseSelected = 0;
        else if(phaseSelected < 0)
            phaseSelected = 2;
    }

    private void playSoundSelectedPhase()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundsPhaseSelected[phaseSelected]);
    }

    private void playInstructionsSound()
    {
        if(!audioSource.isPlaying && UnlockInstruction && !PlayWelcome)
        {
            audioSource.PlayOneShot(soundsPhaseSelectionIntroduction[1]);
            UnlockInstruction = false;
        }else if(!audioSource.isPlaying && PlayFirstPhaseSelected && !PlayWelcome)
        {
            playSoundSelectedPhase();
            PlayFirstPhaseSelected = false;
        }
    }

    private void goToNextScene()
    {
            ChangeSceneManager.GoToSomeScene(phaseNames[phaseSelected]);
    }
}
