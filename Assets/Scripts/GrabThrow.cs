using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabThrow : MonoBehaviour
{

    List<GameObject> objects = new List<GameObject>();

    bool isHolding = false;


    void Start()
    {
        
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) && !isHolding)
        {
            Debug.Log("1");
            isHolding = true;
            foreach(GameObject go in objects)
            {
                go.transform.parent = transform;
                if(go.GetComponent<Rigidbody>())
                {
                    Rigidbody rb = go.GetComponent<Rigidbody>();
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }
            }
        }else if(!OVRInput.Get(OVRInput.RawButton.LHandTrigger) && isHolding)
        {
            Debug.Log("2");
            isHolding = false;
            foreach(GameObject go in objects)
            {
                if(go.GetComponent<Rigidbody>())
                {
                    Rigidbody rb = go.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch)* 10;
                }
            }
            transform.DetachChildren();
        }
    }


    void OnTriggerEnter(Collider col)
    {
        Debug.Log("3");
        objects.Add(col.gameObject);
    }


    void OnTriggerExit(Collider col)
    {
        Debug.Log("4");
        objects.Remove(col.gameObject);
    }


}
