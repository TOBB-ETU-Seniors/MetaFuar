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

        public T GetSettingsManager<T>() where T : class, ISettingsManager
        {
            foreach (var settingsManager in settingsManagers)
            {
                if (settingsManager is T result)
                {
                    return result;
                }
            }
            return null;
        }

        public void GetAndSetInitialValues(){}

        public void SetPlayerPrefs()
        {
            foreach (var settingsManager in settingsManagers)
            {
                if (!settingsManager.Equals(this))
                    settingsManager.SetPlayerPrefs();
            }
            if (enableLogging)
                Debug.Log("PlayerPrefs set");
        }

        public void ResetSettings()
        {
            foreach (var settingsManager in settingsManagers)
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