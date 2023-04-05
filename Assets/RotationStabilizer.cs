using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationStabilizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = this.transform.parent.transform.rotation.eulerAngles;
        rotation *= -1;
        this.transform.rotation = Quaternion.Euler(0,0,0);
    }
}
