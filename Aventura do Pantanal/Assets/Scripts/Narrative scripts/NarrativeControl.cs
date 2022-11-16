using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeControl : MonoBehaviour
{
    //touch vars
    public int TapCount;
    public float MaxNextTapTime;
    float NewTime;

    //Intro vars
    public bool IntroOn;
    public int IntroSoundsQtd;
    public int IntroSoundCount;
    public AudioSource IntroAudio;
    public AudioClip[] IntroAudios;

    //Tutorial vars
    public bool TutorialOn;
    public int TutSoundsQtd;
    public int TutSoundCount;
    public bool TutorialBreak;
    public AudioSource TutorialAudio;
    public AudioClip[] TutorialAudios;
    public AudioSource TutCallAS;
    public Transform GuaracyTutT;
    public Transform[] PosTutTransforms;
    public GameObject PlayerTtut;
    public Vector3 initialPosPlayerTut;
    private PlayerControl playerControlT;

    //narrative control vars
    public int NarrativeCurrentPhase;
    public static bool NarrativeGo;

    public AudioSource CallAS;
    public AudioClip[] Calls;

    //gameplay vars
    public AudioSource GameplayAS;
    public GameObject GAMEPLAY;
    public GameObject TUTORIAL;
    public PlayerControl playerGp;
    public GuaracyIA guaracyIA;
    public AudioClip[] NarrativeGpAudios;

    public PlayerCollision playerCollision;

    public JaguarIA Jaguar;

    public Compass compass;

    // Start is called before the first frame update
    void Start()
    {
        if(IntroOn)
        {
            initialPosPlayerTut = PlayerTtut.transform.position;

            playerControlT = PlayerTtut.GetComponent<PlayerControl>();
            playerControlT.SetBackBlock(true);
            playerControlT.SetLeftBlock(true);
            playerControlT.SetRightBlock(true);
            playerControlT.SetFrontBlock(true);
            GuaracyTutT.transform.position = PosTutTransforms[5].position;
        }else
        {
            NarrativeGo = true;
        }
        TapCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        TapCounter();
        IntroControl();
        TutorialControl();
        GameplayControls();
    }


    public void TapCounter()
    {
        if(Input.touchCount==1)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase==TouchPhase.Began)
            {
                NewTime = Time.time + MaxNextTapTime;
            }

            if(touch.phase == TouchPhase.Ended)
            {
                if(NewTime>=Time.time)
                {
                    TapCount++;
                    NewTime = Time.time + MaxNextTapTime;
                }else
                {
                    TapCount =0;
                    NewTime = 0;
                }
            }
        }

        if(TapCount>0)
        {
            if (NewTime < Time.time)
            {
                if (TapCount == 1)
                {
                    Debug.Log("Chama o guia");

                    if (TutSoundCount == 4)
                        TutorialBreak = false;

                    if (TutSoundCount >= 5 & TutSoundCount <= 9 & TutorialOn & !TutorialAudio.isPlaying)
                    {
                        //TutCallAS.PlayOneShot(Calls[Random.Range(0,3)]);
                        compass.MessageGuaracySolicited();
                    }

                    if (!TutorialOn & !IntroOn & !GameplayAS.isPlaying)
                    {
                        //CallAS.PlayOneShot(Calls[Random.Range(0,3)]);
                        compass.MessageGuaracySolicited();
                    }

                }
                else if (TapCount == 4)
                {
                    Debug.Log("Aciona o tutorial");
                    if (TutSoundCount == 2 & TutorialOn)
                    {
                        TutorialBreak = false;
                    }

                    if(TutSoundCount > 2 || !TutorialOn)
                    {
                        ChangeSceneManager.GoToSomeScene("SelecaoDeFases");
                    }
                }
                
                TapCount = 0;
                //NewTime = 0;
            }
        }
    }

    public void IntroControl()
    {
        if(IntroOn)
        {
            TutorialOn = false;

            if(!IntroAudio.isPlaying)
            {
                if(IntroSoundCount<IntroSoundsQtd)
                {
                    IntroAudio.clip = IntroAudios[IntroSoundCount];
                    IntroAudio.Play(0);
                    IntroSoundCount++;
                }else
                {   
                    IntroOn = false;
                    TutorialOn = true;
                    IntroSoundCount = 0;
                }
            }
        }
    }

    public void TutorialControl()
    {
        if(TutorialOn)
        {
            SetGuidePosition();
            SetControlBreaks();

            if(!TutorialBreak)
            {
                if(!TutorialAudio.isPlaying)
                {
                    if(TutSoundCount<TutSoundsQtd)
                    {
                        TutorialAudio.clip = TutorialAudios[TutSoundCount];
                        TutorialAudio.Play(0);
                        TutSoundCount++;
                    }else
                    {   
                        IntroOn = false;
                        ChangeToTutorial(false);
                        TutSoundCount = 0;
                        NarrativeGo = true;
                    }
                    if(TutSoundCount >= 4 & TutSoundCount <= 9)
                    {
                        TutorialBreak = true;
                    }
                    if(TutSoundCount == 2)
                        TutorialBreak = true;
                }
            }
        }
        
    }

    public void SetGuidePosition()
    {
        if(TutSoundCount >= 5 & TutSoundCount <= 10 & TutorialAudio.isPlaying)
                {
                    PlayerTtut.transform.position = initialPosPlayerTut;
                    PlayerTtut.transform.rotation = GuaracyTutT.transform.rotation;
                    GuaracyTutT.transform.position = PosTutTransforms[TutSoundCount-5].position;
                }
    }

    private void setPlayerLookAtGuaracy()
    {
        PlayerTtut.transform.LookAt(new Vector3(GuaracyTutT.transform.position.x, PlayerTtut.transform.position.y, GuaracyTutT.transform.position.z));
    }

    public void SetControlBreaks()
    {
        if(TutSoundCount==5)
        {
            playerControlT.SetBackBlock(true);
            playerControlT.SetLeftBlock(true);
            playerControlT.SetRightBlock(true);
            playerControlT.SetFrontBlock(false);
        }else if(TutSoundCount==6)
        {
            playerControlT.SetBackBlock(false);
            playerControlT.SetLeftBlock(true);
            playerControlT.SetRightBlock(true);
            playerControlT.SetFrontBlock(true);
        }else if(TutSoundCount==7)
        {
            playerControlT.SetBackBlock(true);
            playerControlT.SetLeftBlock(false);
            playerControlT.SetRightBlock(true);
            playerControlT.SetFrontBlock(true);
        }else if(TutSoundCount==8)
        {
            playerControlT.SetBackBlock(true);
            playerControlT.SetLeftBlock(true);
            playerControlT.SetRightBlock(false);
            playerControlT.SetFrontBlock(true);
        }else if (TutSoundCount == 9)
        {
            playerControlT.SetBackBlock(false);
            playerControlT.SetLeftBlock(false);
            playerControlT.SetRightBlock(false);
            playerControlT.SetFrontBlock(false);
        } else if (TutSoundCount == 10)
        {
            playerControlT.SetBackBlock(true);
            playerControlT.SetLeftBlock(true);
            playerControlT.SetRightBlock(true);
            playerControlT.SetFrontBlock(true);
        }
    }

    public void SetTutorialBreak(bool Break)
    {
        TutorialBreak = Break;
    }

    private void ChangeToTutorial(bool change)
    {
            TUTORIAL.SetActive(change);

        ChangeSceneManager.GoToSomeScene("3 Phase 1");
            //GAMEPLAY.SetActive(!change);

            //TutorialOn = change;
            
    }

    private void GameplayControls()
    {
        if(!TutorialOn & !IntroOn)
        {
            if(!GameplayAS.isPlaying & !CallAS.isPlaying)
            {
                
                if(NarrativeGo)
                {
                    GameplayAS.clip = NarrativeGpAudios[NarrativeCurrentPhase];
                    GameplayAS.Play(0);
                    NarrativeCurrentPhase++;
                    guaracyIA.pointArrived = false;
                }
                VerifyNarrative();
            }
        }
    }


    private void VerifyNarrative()
    {
        if(NarrativeCurrentPhase == 10)
        {
            playerGp.SetWalkFree(true);
            playerGp.SetRotationFree(true);
            guaracyIA.SetTarget(0);
            guaracyIA.SetGoToPoint(true);
            //NarrativeGo = guaracyIA.getPointArrived();
            NarrativeGo = playerCollision.guideFound;
            playerCollision.guideFound = false;
        }
        if(NarrativeCurrentPhase == 11)
        {
            if(GameplayAS.isPlaying)
            {
                playerGp.SetWalkFree(false);
                setPlayerLookAtGuaracy();
                //playerGp.SetRotationFree(false);
            }
            else
            {
                playerGp.SetWalkFree(true);
                playerGp.SetRotationFree(true);
                guaracyIA.SetTarget(1);
                guaracyIA.SetGoToPoint(true);
                playerCollision.IcanTouchAnimal = true;
            }
            NarrativeGo = playerCollision.AnimalCatched;
        }

        if(NarrativeCurrentPhase == 12)
        {
            if(GameplayAS.isPlaying)
            {
                playerGp.SetWalkFree(false);
                setPlayerLookAtGuaracy();
                //playerGp.SetRotationFree(false);
            }
        }

        if(NarrativeCurrentPhase == 19)
        {
            playerGp.SetWalkFree(true);
            playerGp.SetRotationFree(true);
            guaracyIA.SetTarget(2);
            guaracyIA.SetGoToPoint(true);
            NarrativeGo = playerCollision.guideFound;
            playerCollision.guideFound = false;
        }

        if(NarrativeCurrentPhase == 20)
        {
            playerGp.SetWalkFree(true);
            playerGp.SetRotationFree(true);
            guaracyIA.SetTarget(3);
            guaracyIA.SetGoToPoint(true);
            NarrativeGo = playerCollision.guideFound;
            playerCollision.guideFound = false;
            playerCollision.AnimalCatched = false;
        }

        if(NarrativeCurrentPhase == 21)
        {
            playerGp.SetWalkFree(true);
            playerGp.SetRotationFree(true);
            guaracyIA.SetTarget(4);
            guaracyIA.SetGoToPoint(true);
            NarrativeGo = playerCollision.guideFound;
            playerCollision.guideFound = false;
            playerCollision.IcanTouchAnimal = true;
        }

        if(NarrativeCurrentPhase == 22)
        {
            playerGp.SetWalkFree(true);
            playerGp.SetRotationFree(true);
            guaracyIA.SetTarget(5);
            guaracyIA.SetGoToPoint(true);
            NarrativeGo = playerCollision.AnimalCatched;
        }

        if(NarrativeCurrentPhase == 23)
        {
            if(GameplayAS.isPlaying)
            {
                playerGp.SetWalkFree(false);
                setPlayerLookAtGuaracy();
                //playerGp.SetRotationFree(false);
            }
        }

        if(NarrativeCurrentPhase == 31)
        {
            playerGp.SetWalkFree(true);
            playerGp.SetRotationFree(true);
            guaracyIA.SetTarget(6);
            guaracyIA.SetGoToPoint(true);
            NarrativeGo = playerCollision.guideFound;
            playerCollision.guideFound = false;
            playerCollision.IcanTouchAnimal = true;
        }

        if(NarrativeCurrentPhase == 32)
        {
            if(GameplayAS.isPlaying)
            {
                playerGp.SetWalkFree(false);
                setPlayerLookAtGuaracy();
                //playerGp.SetRotationFree(false);
                NarrativeGo = false;
            }else
            {
                playerGp.SetWalkFree(true);
                playerGp.SetRotationFree(true);
                guaracyIA.SetTarget(7);
                guaracyIA.SetGoToPoint(true);
                NarrativeGo = playerCollision.guideFound;
                playerCollision.guideFound = false;
            } 
        }

        if(NarrativeCurrentPhase == 33)
        {
            if(GameplayAS.isPlaying)
            {
                playerGp.SetWalkFree(false);
                setPlayerLookAtGuaracy();
                playerGp.SetRotationFree(false);
                NarrativeGo = false;
            }else
            {
                Jaguar.StartAI();
            }
        }

        if(NarrativeCurrentPhase == 42)
        {
            playerGp.SetWalkFree(true);
            playerGp.SetRotationFree(true);
            guaracyIA.SetTarget(8);
            guaracyIA.SetGoToPoint(true);
            NarrativeGo = playerCollision.guideFound;
            playerCollision.guideFound = false;
            playerCollision.IcanTouchAnimal = true;
        }

        if(NarrativeCurrentPhase == 44)
        {
            if(GameplayAS.isPlaying)
            {
                playerGp.SetWalkFree(false);
                setPlayerLookAtGuaracy();
                //playerGp.SetRotationFree(false);
                NarrativeGo = false;
            }
            if (!GameplayAS.isPlaying)
            {
                ChangeSceneManager.GoToSomeScene("SelecaoDeFases");
            }
        }
    }

    public static void SetNarrativeGo()
    {
        NarrativeGo = true;
    }
}
