using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuaracyIA : MonoBehaviour
{
    private NavMeshAgent GuaracyAI;
    public bool GoToPoint = false;
    private Vector3 currentTarget;

    public bool pointArrived = false;

    public Transform[] targets;
    // Start is called before the first frame update
    void Start()
    {
        GuaracyAI = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        goToMyTarget();
    }

    private void goToMyTarget()
    {
        if(GoToPoint)
        {
            GuaracyAI.destination = currentTarget;
        }
    }

    public void SetTarget(int target)
    {
        currentTarget = targets[target].position;
        if(targets[target].childCount > 1)
        targets[target].GetChild(1).gameObject.SetActive(true);
        
        //pointArrived = false;
    }

    public void SetGoToPoint(bool go)
    {
        GoToPoint = go;
    }

    public bool getPointArrived()
    {
        return pointArrived;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag=="pointIA")
        {
            pointArrived = true;
        }
        Debug.Log(other.transform.parent);
    }
}
