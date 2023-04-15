using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSettingsManager : MonoBehaviour
{
    private static float session_start;
    private void Start()
    {
        GeneralSettingsManager.session_start = Time.time;
    }

    public static float GetSessionStartTime()
    {
        return GeneralSettingsManager.session_start;
    }

    
}
