using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSettingsManager : MonoBehaviour
{
    public static float startTime;
    
    void Start()
    {
        startTime = UnityEngine.Time.time;
    }
    
    public static float ElapsedSeconds()
    {
        return UnityEngine.Time.time - startTime;
    }
}
