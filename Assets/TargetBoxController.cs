using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Race Car")
            return;
        // if a car collides with this box then update its targets
        if (collider.gameObject.GetComponent<Racecar_Controller>().state == 0)
        {
            if(collider.gameObject.GetComponent<Racecar_Controller>().currentTarget != this.gameObject)
            {
                Debug.Log("Collided with me but car's current target is not me+");
                return; 
            }

            Debug.Log("Target reached !!!");
            collider.gameObject.GetComponent<Racecar_Controller>().UpdateTarget();
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag != "Race Car")
            return;
        collider.gameObject.GetComponent<Racecar_Controller>().state = 0;
    }
}
