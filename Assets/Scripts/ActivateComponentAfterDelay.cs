using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateComponentAfterDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteAfterTime(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        this.GetComponent<ConfigurePosition>().enabled = true;
    }
}
