using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBaloonController : MonoBehaviour
{

    public GameObject currentTarget;
    public GameObject nextTarget;
    public int state; // 0 for on the road, 1 for on collision

    // these will be added on each frame to baloons location
    Vector3 location_offset;
    Vector3 rotation_offset = new Vector3(0, 1,0);
    float diff_multiplier = 0.0001f;
    public void UpdateTarget()
    {
        state = 1;
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Balloon Locations");
        int nextTargetIndex = (Array.IndexOf(allTargets, nextTarget) + 1) % allTargets.Length;
        currentTarget = nextTarget;
        nextTarget = allTargets[nextTargetIndex];

        // get new offsets
        var diff = currentTarget.transform.position - this.gameObject.transform.position;
        this.location_offset = diff * diff_multiplier;
        

      
    }
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        // TODO: Does targets really come in order ?
        GameObject[] target = GameObject.FindGameObjectsWithTag("Balloon Locations");
        Debug.Log("Targets len: " + target.Length);
        currentTarget = GameObject.FindGameObjectsWithTag("Balloon Locations")[0];
        nextTarget = GameObject.FindGameObjectsWithTag("Balloon Locations")[1];

        // get new offsets
        var diff = currentTarget.transform.position - this.gameObject.transform.position;
        this.location_offset = diff * diff_multiplier;



    }

    // Update is called once per frame
    void Update()
    {
        
        this.gameObject.transform.position += this.location_offset;
        this.gameObject.transform.Rotate(rotation_offset, Space.Self);
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Balloon Locations")
            return;
        // if a car collides with this box then update its targets
        if (collider.gameObject != this.currentTarget)
        {
            Debug.Log("Collided with me but baloons's current target is not me+");
            return;
        }
        Debug.Log("Collided and updating taget!!!");
        this.UpdateTarget();
    }
    


}
