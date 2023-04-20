using SettingsManagers.Abstract;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SettingsManagers
{


    public class GeneralSettingsManager : MonoBehaviour, ISettingsManager
    {
        public static int sessionWarningTime { get; private set; }

        [Header("General Settings Sliders")]
        [SerializeField] private Slider sessionWarningTimeSlider;

        [Header("Debug")]
        [SerializeField] private bool enableLogging;

        private static float session_start;

        private void Awake()
        {
            GetAndSetInitialValues();
            session_start = Time.time;
        }

        public static float GetSessionStartTime()
        {
            return session_start;
        }

        public void OnSessionWarningTimeSliderValueChange(float value)
        {
            sessionWarningTime = (int)value;
            sessionWarningTimeSlider.value = value;
            sessionWarningTimeSlider.GetComponent<TMP_Text>().text = value.ToString();
        }

        public void GetAndSetInitialValues()
        {
            ResetSettings();

            // Load the current audio settings from PlayerPrefs or leave from the default audio settings
            if (PlayerPrefs.HasKey("sessionWarningTime"))
                OnSessionWarningTimeSliderValueChange(PlayerPrefs.GetInt("sessionWarningTime"));

            SetPlayerPrefs();
        }

        public void SetPlayerPrefs()
        {
            PlayerPrefs.SetInt("sessionWarningTime", sessionWarningTime);
        }

        public void ResetSettings()
        {
            OnSessionWarningTimeSliderValueChange(20);
        }

    }

}
