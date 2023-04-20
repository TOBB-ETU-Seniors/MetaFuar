using SettingsManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionWarningController : MonoBehaviour
{
    [SerializeField]
    private GameObject sessionWarningPanel;

    private float lastWarning;

    private void Start()
    {
        lastWarning = 0;

        InvokeRepeating("CheckElapsedTime", 1f, 1f);
    }

    private void CheckElapsedTime()
    {
        float elapsedTime = Time.time - GeneralSettingsManager.GetSessionStartTime();

        if ((elapsedTime - lastWarning) >= GeneralSettingsManager.sessionWarningTime * 60)
        {
            Instantiate(sessionWarningPanel);
            lastWarning = elapsedTime;
        }
    }
}
