using SettingsManagers;
using SettingsManagers.Abstract;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GraphicsSettingsManager settingsManager = SettingsManager.Instance.GetSettingsManager<GraphicsSettingsManager>();
        Debug.Log(settingsManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
