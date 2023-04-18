using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RespawnPins : MonoBehaviour
{
    public GameObject go;
    public List<GameObject> children_pins = new List<GameObject>();

    private List<Vector3> initialPositions = new List<Vector3>();
    private List<Quaternion> initialRotations = new List<Quaternion>();
    private List<Rigidbody> rigidBodies = new List<Rigidbody>();
    void Start()
    {
        getChilderen(go);
        saveStartTransforms();
    }

    private void getChilderen(GameObject go)
    {
        for(int i=0; i<10; i++)
        {
            children_pins.Add(go.transform.GetChild(i).gameObject);
        }
    }

    private void saveStartTransforms()
    {
        for (int i = 0; i < 10; i++)
        {
            //initialTransforms.Add(children_pins[0].transform);
            initialPositions.Add(children_pins[i].transform.position);
            initialRotations.Add(children_pins[i].transform.rotation);
            rigidBodies.Add(children_pins[i].GetComponent<Rigidbody>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetPins()
    {
        

        for (int i = 0; i < 10; i++)
        {
            children_pins[i].transform.SetPositionAndRotation(initialPositions[i],initialRotations[i]);
            rigidBodies[i].velocity = Vector3.zero;
            rigidBodies[i].angularVelocity = Vector3.zero;
        }
    }
}
