﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutCollision : MonoBehaviour
{
    public NarrativeControl DungeonMaster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "PlayerRange")
        {
            DungeonMaster.SetTutorialBreak(false);
        }
    }
}
