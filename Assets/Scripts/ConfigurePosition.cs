using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurePosition : MonoBehaviour
{
    // Start is called before the first frame update
    public float x;
    public float y;
    public float z;
    void Start()
    {
        //StartCoroutine(ExecuteAfterTime(2));
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(x, y, z);
        //StartCoroutine(ExecuteAfterTime(2));
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        this.transform.localPosition = new Vector3(x, y, z);
        // Code to execute after the delay
    }
}
