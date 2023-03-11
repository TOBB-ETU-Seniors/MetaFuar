using SettingsManagers.Abstract;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace SettingsManagers
{
    public class GraphicsSettingsManager : MonoBehaviour, ISettingsManager
    {
        public enum OverallQuality
        {
            VeryLow,
            Low,
            Medium,
            High,
            VeryHigh,
            Ultra
        }

        public enum TextureQuality
        {
            Low,
            Medium,
            High,
            VeryHigh
        }

        private int[] antiAliasingLevels = { 0, 2, 4, 8 };

        [Header("ADVANCED - UI Elements")]
        [Tooltip("The Main Canvas Gameobject")]
        public Canvas mainCanvas;

        [Header("Graphics Settings Elements")]
        [SerializeField] private Slider uIScaleSlider;
        [SerializeField] private Slider brightnessSlider;
        [SerializeField] private TMP_Dropdown overallQualityDropdown;
        [SerializeField] private TMP_Dropdown textureQualityDropdown;
        [SerializeField] private TMP_Dropdown shadowQualityDropdown;
        [SerializeField] private TMP_Dropdown antiAliasingDropdown;
        [SerializeField] private Toggle vSyncToggle;
        [SerializeField] private Toggle hDRToggle;
        [SerializeField] private Toggle bloomToggle;
        [SerializeField] private Toggle fogToggle;
        //public Toggle displayFPSToggle;

        public static float uIScale { get; private set; }
        public static float brightness { get; private set; }
        public static int overallQuality { get; private set; }
        public static int textureQuality { get; private set; }
        public static int shadowQuality { get; private set; }
        public static int antiAliasing { get; private set; }
        public static bool vSync { get; private set; }
        public static bool hDR { get; private set; }
        public static bool bloom { get; private set; }
        public static bool fog { get; private set; }

        [Header("Debug")]
        [SerializeField] private bool enableLogging;

        private void Awake()
        {
            ////////////////////////// Tier
            //uIScaleSlider.onValueChanged.AddListener(OnUIScaleSliderValueChange);

            GetAndSetInitialValues();
        }

        #region OnSettingsValueChange

        public void OnUIScaleSliderValueChange(float value)
        {
            if (value < 0.5f)
                value = 0.5f;
            uIScale = 0.002f * value;
            uIScaleSlider.GetComponent<TMP_Text>().text = value.ToString("F1") + "x";
            mainCanvas.transform.localScale = new Vector3(uIScale, uIScale, 0.002f);
        }

        public void OnBrightnessSliderValueChange(float value)
        {
            if (value < 0.0001f)
                value = 0.0001f;
            brightness = value;
            brightnessSlider.GetComponent<TMP_Text>().text = ((int)(value * 100)).ToString() + "%";
            Screen.brightness = value;

            if (UnityEngine.XR.XRSettings.enabled)
            {
                UnityEngine.XR.XRSettings.eyeTextureResolutionScale = brightness;
                Debug.Log("XR parlaklýðý ayarlandý");
            }
        }

        public void OnOverallQualityDropdownValueChange(int value)
        {
            overallQuality = value;
            QualitySettings.SetQualityLevel(value, true);
        }

        public void OnTextureQualityDropdownValueChange(int value)
        {
            textureQuality = value;
            QualitySettings.masterTextureLimit = value;
        }

        public void OnShadowQualityDropdownValueChange(int value)
        {
            shadowQuality = value;
            QualitySettings.shadowResolution = (ShadowResolution)value;
        }

        public void OnAntiAliasingDropdownValueChange(int value)
        {
            antiAliasing = antiAliasingLevels[value];
            QualitySettings.antiAliasing = antiAliasingLevels[value];
        }

        public void OnVSyncToggleValueChange(bool value)
        {
            vSync = value;
            QualitySettings.vSyncCount = value ? 1 : 0;
        }

        public void OnHDRToggleValueChange(bool value)
        {
            hDR = value;
            ////////////////////////////////////////////////////////// URP
        }

        public void OnBloomToggleValueChange(bool value)
        {
            bloom = value;
            ////////////////////////////////////////////////////////// Post Processing
        }

        public void OnFogToggleValueChange(bool value)
        {
            fog = value;
            RenderSettings.fog = value;
        }

        #endregion

        public void GetAndSetInitialValues()
        {
            /*ResetSettings();

            // Load the current audio settings from PlayerPrefs or leave from the default audio settings
            if (PlayerPrefs.HasKey("uIScale"))
                OnUIScaleSliderValueChange(PlayerPrefs.GetFloat("uIScale") / 0.002f);
            if (PlayerPrefs.HasKey("brightness"))
                OnBrightnessSliderValueChange(0.8f);//PlayerPrefs.GetFloat("brightness"));
            if (PlayerPrefs.HasKey("overallQuality"))
                OnOverallQualityDropdownValueChange(PlayerPrefs.GetInt("overallQuality"));
            if (PlayerPrefs.HasKey("textureQuality"))
                OnTextureQualityDropdownValueChange(PlayerPrefs.GetInt("textureQuality"));
            if (PlayerPrefs.HasKey("shadowQuality"))
                OnShadowQualityDropdownValueChange(PlayerPrefs.GetInt("shadowQuality"));
            if (PlayerPrefs.HasKey("antiAliasing"))
                OnShadowQualityDropdownValueChange(Array.IndexOf(antiAliasingLevels, PlayerPrefs.GetInt("antiAliasing")));
            if (PlayerPrefs.HasKey("vSync"))
                OnVSyncToggleValueChange(PlayerPrefs.GetInt("vSync") == 1);
            if (PlayerPrefs.HasKey("hDR"))
                OnHDRToggleValueChange(PlayerPrefs.GetInt("hDR") == 1);
            if (PlayerPrefs.HasKey("bloom"))
                OnBloomToggleValueChange(PlayerPrefs.GetInt("bloom") == 1);
            if (PlayerPrefs.HasKey("fog"))
                OnFogToggleValueChange(PlayerPrefs.GetInt("fog") == 1);

            SetPlayerPrefs();*/
        }

        public void SetPlayerPrefs()
        {
            PlayerPrefs.SetFloat("uIScale", uIScale);
            PlayerPrefs.SetFloat("brightness", brightness);
            PlayerPrefs.SetInt("overallQuality", overallQuality);
            PlayerPrefs.SetInt("textureQuality", textureQuality);
            PlayerPrefs.SetInt("shadowQuality", shadowQuality);
            PlayerPrefs.SetInt("antiAliasing", antiAliasing);

            PlayerPrefs.SetInt("vSync", Convert.ToInt32(vSync));
            PlayerPrefs.SetInt("hDR", Convert.ToInt32(hDR));
            PlayerPrefs.SetInt("bloom", Convert.ToInt32(bloom));
            PlayerPrefs.SetInt("fog", Convert.ToInt32(fog));
        }

        public void ResetSettings()
        {
            OnUIScaleSliderValueChange(1f);
            OnBrightnessSliderValueChange(0.8f);
            OnOverallQualityDropdownValueChange((int)OverallQuality.Ultra);
            OnTextureQualityDropdownValueChange((int)TextureQuality.VeryHigh);
            OnShadowQualityDropdownValueChange((int)ShadowResolution.High);
            OnAntiAliasingDropdownValueChange(2);

            OnVSyncToggleValueChange(true);
            OnHDRToggleValueChange(true);
            OnBloomToggleValueChange(true);
            OnFogToggleValueChange(true);
        }
    }
}