using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionWarningController : MonoBehaviour
{
    [SerializeField]
    private GameObject sessionWarningPanel;
    [SerializeField]
    private float warnInEveryNseconds = 1200;

    private float lastWarning;

    private void Start()
    {
        lastWarning = 0;

        InvokeRepeating("CheckElapsedTime", 1f, 1f);
    }

    private void CheckElapsedTime()
    {
        float elapsedTime = GeneralSettingsManager.ElapsedSeconds();

        if((elapsedTime-lastWarning) >= warnInEveryNseconds)
        {
            Instantiate(sessionWarningPanel);
            lastWarning = elapsedTime;
        }
    }
}
