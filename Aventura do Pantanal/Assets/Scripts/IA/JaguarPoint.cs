using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JaguarPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag=="animal")
        {
            other.GetComponent<JaguarIA>().ChnageTarget();
        }
        //Debug.Log("Colidiu");
    }
}
