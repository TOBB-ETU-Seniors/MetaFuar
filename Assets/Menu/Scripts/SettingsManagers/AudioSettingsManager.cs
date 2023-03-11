using OVR;
using SettingsManagers.Abstract;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SettingsManagers
{

    public class AudioSettingsManager : MonoBehaviour, ISettingsManager
    {
        #region Properties

        public static float masterVolume { get; private set; }
        public static float musicVolume { get; private set; }
        public static float uIVolume { get; private set; }
        public static float soundEffectsVolume { get; private set; }
        public static float ambientVolume { get; private set; }
        public static float speechVolume { get; private set; }

        [Header("Audio Settings Sliders")]
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider uISlider;
        [SerializeField] private Slider soundEffectsSlider;
        [SerializeField] private Slider ambientSlider;
        [SerializeField] private Slider speechSlider;

        [Header("Debug")]
        [SerializeField] private bool enableLogging;

        #endregion

        private void Awake()
        {
            GetAndSetInitialValues();
        }

        #region OnSliderValueChange

        public void OnMasterSliderValueChange(float value)
        {
            masterVolume = value;
            masterSlider.GetComponent<TMP_Text>().text = ((int)(value * 100)).ToString() + "%";
            AudioManager.Instance.UpdateMasterMixerVolume(value);
        }

        public void OnMusicSliderValueChange(float value)
        {
            musicVolume = value;
            musicSlider.GetComponent<TMP_Text>().text = ((int)(value * 100)).ToString() + "%";
            AudioManager.Instance.UpdateMusicMixerVolume(value);
        }

        public void OnUISliderValueChange(float value)
        {
            uIVolume = value;
            uISlider.GetComponent<TMP_Text>().text = ((int)(value * 100)).ToString() + "%";
            AudioManager.Instance.UpdateUIMixerVolume(value);
        }

        public void OnSoundEffectsSliderValueChange(float value)
        {
            soundEffectsVolume = value;
            soundEffectsSlider.GetComponent<TMP_Text>().text = ((int)(value * 100)).ToString() + "%";
            AudioManager.Instance.UpdateSoundEffectsMixerVolume(value);
        }

        public void OnAmbientSliderValueChange(float value)
        {
            ambientVolume = value;
            ambientSlider.GetComponent<TMP_Text>().text = ((int)(value * 100)).ToString() + "%";
            AudioManager.Instance.UpdateAmbientMixerVolume(value);
        }

        public void OnSpeechSliderValueChange(float value)
        {
            speechVolume = value;
            speechSlider.GetComponent<TMP_Text>().text = ((int)(value * 100)).ToString() + "%";
            AudioManager.Instance.UpdateSpeechMixerVolume(value);
        }

        #endregion

        public void GetAndSetInitialValues()
        {
            ResetSettings();

            // Load the current audio settings from PlayerPrefs or leave from the default audio settings
            if (PlayerPrefs.HasKey("masterVolume"))
                OnMasterSliderValueChange(PlayerPrefs.GetFloat("masterVolume"));
            if (PlayerPrefs.HasKey("musicVolume"))
                OnMusicSliderValueChange(PlayerPrefs.GetFloat("musicVolume"));
            if (PlayerPrefs.HasKey("uIVolume"))
                OnUISliderValueChange(PlayerPrefs.GetFloat("uIVolume"));
            if (PlayerPrefs.HasKey("soundEffectsVolume"))
                OnSoundEffectsSliderValueChange(PlayerPrefs.GetFloat("soundEffectsVolume"));
            if (PlayerPrefs.HasKey("ambientVolume"))
                OnAmbientSliderValueChange(PlayerPrefs.GetFloat("ambientVolume"));
            if (PlayerPrefs.HasKey("speechVolume"))
                OnSpeechSliderValueChange(PlayerPrefs.GetFloat("speechVolume"));

            SetPlayerPrefs();
        }

        public void SetPlayerPrefs()
        {
            PlayerPrefs.SetFloat("masterVolume", masterVolume);
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
            PlayerPrefs.SetFloat("uIVolume", uIVolume);
            PlayerPrefs.SetFloat("soundEffectsVolume", soundEffectsVolume);
            PlayerPrefs.SetFloat("ambientVolume", ambientVolume);
            PlayerPrefs.SetFloat("speechVolume", speechVolume);
        }

        public void ResetSettings()
        {
            OnMasterSliderValueChange(0.8f);
            OnMusicSliderValueChange(0.8f);
            OnUISliderValueChange(0.8f);
            OnSoundEffectsSliderValueChange(0.8f);
            OnAmbientSliderValueChange(0.8f);
            OnSpeechSliderValueChange(0.8f);
        }

    }
}
