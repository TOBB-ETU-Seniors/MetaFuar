using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Racecar_Controller : MonoBehaviour
{

    public GameObject currentTarget;
    public GameObject nextTarget;
    public int state; // 0 for on the road, 1 for on collision
    
    public NavMeshAgent agent;
    // sets next target as current target and searches real next target
    public void UpdateTarget()
    {
        state = 1;
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Navmesh Target");
        int nextTargetIndex = (Array.IndexOf(allTargets, nextTarget) + 1) % allTargets.Length;
        currentTarget = nextTarget;
        nextTarget= allTargets[nextTargetIndex];
        bool success = agent.SetDestination(currentTarget.gameObject.transform.position);
        Debug.Log("Destination is set as: " + currentTarget.gameObject.transform.position);
        Debug.Log("Next Target is: " + nextTarget.gameObject.transform.position);


        Debug.Log("New target success: "+success);
        Debug.Log("Target Updated");
    }
    // Start is called before the first frame update
    void Start()
    {
     
        
        state = 0; 
        // TODO: Does targets really come in order ?
        GameObject[] target = GameObject.FindGameObjectsWithTag("Navmesh Target");
        Debug.Log("Targets len: " + target.Length);
        currentTarget = GameObject.FindGameObjectsWithTag("Navmesh Target")[0];
        nextTarget = GameObject.FindGameObjectsWithTag("Navmesh Target")[1];
        agent.destination = currentTarget.gameObject.transform.position;    

        Debug.Log("Destination is set as: " + currentTarget.gameObject.transform.position);
        Debug.Log("Next Target is: " + nextTarget.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    
}
