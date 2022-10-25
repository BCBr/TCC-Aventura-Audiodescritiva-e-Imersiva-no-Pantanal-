using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JaguarIA : MonoBehaviour
{

    private NavMeshAgent agent;
    public Transform[] pointsPositions;
    private Vector3 CurrentTarget;

    public int myCurrentTargetN = 0;
    private bool AIstarted = false;
    // Start is called before the first frame update
    void Start()
    {
        CurrentTarget = pointsPositions[0].position;
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(AIstarted)
        {
            agent.destination = CurrentTarget;
        }
    }

    public void StartAI()
    {
        if(!AIstarted)
        {
            AIstarted = true;
            GetComponent<AudioSource>().Play(0);
        }
    }

    public void ChnageTarget()
    {
        myCurrentTargetN++;
        if(myCurrentTargetN>7)
        {
            NarrativeControl.SetNarrativeGo();
            Destroy(gameObject);
        }else
        {
            CurrentTarget = pointsPositions[myCurrentTargetN].position;
        } 
    }
}
