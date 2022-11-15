using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CharacterController Controller;
    private float Tspeed = 1.5f;
    private float Rspeed = 50f;

    public bool IcanWalk = true;
    public bool IcanRotate = true;
    public bool LeftBlock = false;
    public bool RightBlock = false;
    public bool FrontBlock = false;
    public bool BackBlock = false;

    public AudioClip[] paths;
    float Rotation;
    float Translation;

    private AudioSource myAS;
    public Joystick joystick;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        myAS = GetComponent<AudioSource>();
        myAS.clip = paths[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float Rotation = Input.GetAxisRaw("Horizontal");
        //float Translation = Input.GetAxisRaw("Vertical");

        //float Rotation = joystick.Horizontal;
        //float Translation = joystick.Vertical;

        SetRotation();
        SetTranslation();

        Vector3 tTranslation = new Vector3(0,0, Translation*Tspeed*Time.deltaTime);
        tTranslation = rigidbody.rotation * tTranslation;
        rigidbody.MovePosition(transform.position + tTranslation);

        Vector3 tRotation = new Vector3(0,Rotation*Rspeed,0);
        Quaternion qtRotation = Quaternion.Euler(tRotation*Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * qtRotation);
    }

    private void SetRotation()
    {
        
        if(joystick.Horizontal >= 0.3f & IcanRotate & !RightBlock)
        {
            Rotation = 1f;
            PlaySoundWalk();
        }else if(joystick.Horizontal<= -0.3 & IcanRotate & !LeftBlock)
        {
            Rotation = -1f;
            PlaySoundWalk();
        }else
        {
            Rotation = 0;
        }
    }

    private void SetTranslation()
    {
        if(joystick.Vertical >= 0.3f & IcanWalk & !FrontBlock)
        {
            Translation = 1f;
            PlaySoundWalk();
        }else if(joystick.Vertical<= -0.3 & IcanWalk & !BackBlock)
        {
            Translation = -1f;
            PlaySoundWalk();
        }else
        {
            Translation = 0;
        }
    }

    public void SetLeftBlock(bool block)
    {
        LeftBlock = block;
    }

    public void SetRightBlock(bool block)
    {
        RightBlock = block;
    }

    public void SetFrontBlock(bool block)
    {
        FrontBlock = block;
    }

    public void SetBackBlock(bool block)
    {
        BackBlock = block;
    }

    public void SetRotationFree(bool canRotate)
    {
        IcanRotate = canRotate;
    }

    public void SetWalkFree(bool canWalk)
    {
        IcanWalk = canWalk;

        LeftBlock = canWalk;
        RightBlock = canWalk;
        FrontBlock = canWalk;
        BackBlock = canWalk;
        IcanRotate = canWalk;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "lowGrass")
        {
            myAS.clip = paths[0];
        }

        if(other.tag == "highGrass")
        {
            myAS.clip = paths[1];
        }

        if(other.tag == "lake")
        {
            myAS.clip = paths[2];
        }
    }

    private void PlaySoundWalk()
    {
        if(!myAS.isPlaying)
        {
            myAS.Play(0);
        }
    }

}
