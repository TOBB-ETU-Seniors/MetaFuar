using SettingsManagers.Abstract;
using System.Collections;
using UnityEngine;

namespace SettingsManagers
{
    //-------------------------------------------------------------------------
    // Service Locator & Composite
    //-------------------------------------------------------------------------
    public class SettingsManager : Singleton<SettingsManager>, ISettingsManager
    {
        private ISettingsManager[] settingsManagers;

        private bool saveLock = false;

        [Header("Debug")]
        [SerializeField] private bool enableLogging;

        protected override void Awake()
        {
            base.Awake();
            settingsManagers = GetComponents<ISettingsManager>();
        }


        public void GetAndSetInitialValues(){}

        public void SetPlayerPrefs()
        {
            foreach (ISettingsManager settingsManager in settingsManagers)
            {
                if (!settingsManager.Equals(this))
                    settingsManager.SetPlayerPrefs();
            }
            if (enableLogging)
                Debug.Log("PlayerPrefs set");
        }

        public void ResetSettings()
        {
            foreach (ISettingsManager settingsManager in settingsManagers)
            {
                if (!settingsManager.Equals(this))
                    settingsManager.ResetSettings();
            }
            SavePlayerPrefs();
            if (enableLogging)
                Debug.Log("Settings reset");
        }

        public void SavePlayerPrefs()
        {
            if (!saveLock)
            {
                saveLock = true;
                SetPlayerPrefs();
                PlayerPrefs.Save();
                if (enableLogging)
                    Debug.Log("PlayerPrefs saved");
                StartCoroutine(ResetSaveLock());
            }
        }

        IEnumerator ResetSaveLock()
        {
            yield return new WaitForSeconds(1f);
            saveLock = false;
        }

        private void OnApplicationQuit()
        {
            SavePlayerPrefs();
        }

    }
}