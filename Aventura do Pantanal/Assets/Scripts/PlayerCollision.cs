using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public bool AnimalCatched = false;
    public bool guideFound = false;
    public bool IcanTouchAnimal = false;
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
        if(other.tag=="animal" & IcanTouchAnimal)
        {
            AnimalCatched = true;
            IcanTouchAnimal = false;
            Destroy(other.gameObject);
        }
        

        if(other.tag=="guide")
        {
            guideFound = true;
            Destroy(other.gameObject);
        }
        
    }
}
