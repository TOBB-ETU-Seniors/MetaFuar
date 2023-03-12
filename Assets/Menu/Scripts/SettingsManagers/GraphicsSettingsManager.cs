using SettingsManagers.Abstract;
using System;
using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Bloom = UnityEngine.Rendering.PostProcessing.Bloom;
using ShadowQuality = UnityEngine.ShadowQuality;

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
            Ultra,
            Custom
        }

        public enum TextureQuality
        {
            Low,
            Medium,
            High,
            VeryHigh
        }

        private int[] antiAliasingLevels = { 1, 2, 4, 8 };

        private bool canOverallQualitySetCustom = true;

        private UniversalRenderPipelineAsset currentURPAsset;
        [SerializeField] private PostProcessVolume postProcessVolume;

        [Header("ADVANCED - UI Elements")]
        [Tooltip("The Main Canvas Gameobject")]
        public Canvas mainCanvas;

        [Header("Graphics Settings Elements")]
        [SerializeField] private Slider uIScaleSlider;
        [SerializeField] private Slider renderScaleSlider;
        [SerializeField] private TMP_Dropdown overallQualityDropdown;
        [SerializeField] private TMP_Dropdown textureQualityDropdown;
        [SerializeField] private TMP_Dropdown shadowQualityDropdown;
        [SerializeField] private TMP_Dropdown antiAliasingDropdown;
        [SerializeField] private Toggle vSyncToggle;
        [SerializeField] private Toggle hDRToggle;
        [SerializeField] private Toggle bloomToggle;
        [SerializeField] private Toggle castShadowsToggle;
        //public Toggle displayFPSToggle;

        public static float uIScale { get; private set; }
        public static float renderScale { get; private set; }
        public static int overallQuality { get; private set; }
        public static int textureQuality { get; private set; }
        public static int shadowQuality { get; private set; }
        public static int antiAliasing { get; private set; }
        public static bool vSync { get; private set; }
        public static bool hDR { get; private set; }
        public static bool bloom { get; private set; }
        public static bool castShadows { get; private set; }

        [Header("Debug")]
        [SerializeField] private bool enableLogging;

        private void Awake()
        {
            currentURPAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;

            GetAndSetInitialValues();
        }

        #region OnSettingsValueChange

        public void OnUIScaleSliderValueChange(float value)
        {
            if (value < 0.5f)
                value = 0.5f;
            uIScale = value;
            uIScaleSlider.GetComponent<TMP_Text>().text = value.ToString("F1") + "x";
            uIScaleSlider.value = value;

            mainCanvas.transform.localScale = new Vector3(0.002f * uIScale, 0.002f * uIScale, 0.002f);

            if (enableLogging)
                Debug.Log("UI Scale Slider Value Changed: " + value);
        }

        public void OnRenderScaleSliderValueChange(float value)
        {
            if (value != renderScale)
            {
                SetOverallQualityDropdownCustom();

                if (value < 0.5f)
                    value = 0.5f;
                renderScale = value;
                renderScaleSlider.value = value;
                renderScaleSlider.GetComponent<TMP_Text>().text = value.ToString("F1");

                currentURPAsset.renderScale = value;

                if (enableLogging)
                    Debug.Log("Render Scale Slider Value Changed: " + value);
            }
        }

        public void OnOverallQualityDropdownValueChange(int value)
        {
            if (value != overallQuality)
            {
                overallQuality = value;
                overallQualityDropdown.value = value;
                QualitySettings.SetQualityLevel(value, true);

                currentURPAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;

                if (value != 6)
                {
                    canOverallQualitySetCustom = false;
                    RefreshSubValues();
                    StartCoroutine(ResetCanOverallQualitySetCustom());
                }
                else
                {
                    SetOverallQualityDropdownCustom();
                }

                if (enableLogging)
                    Debug.Log("Overall Quality Value Changed: " + (OverallQuality)value);
            }
        }

        public void OnTextureQualityDropdownValueChange(int value)
        {
            if (value != textureQuality)
            {
                SetOverallQualityDropdownCustom();

                textureQuality = value;
                textureQualityDropdown.value = value;
                QualitySettings.masterTextureLimit = 3 - value;

                if (enableLogging)
                    Debug.Log("Texture Quality Value Changed: " + (TextureQuality)value);
            }

        }

        public void OnShadowQualityDropdownValueChange(int value)
        {
            if (value != shadowQuality)
            {
                SetOverallQualityDropdownCustom();

                shadowQuality = value;
                shadowQualityDropdown.value = value;
                currentURPAsset.shadowCascadeCount = value + 1;

                if (enableLogging)
                    Debug.Log("Shadow Quality Value Changed: " + (ShadowQuality)(value + 1));
            }
        }

        public void OnAntiAliasingDropdownValueChange(int value)
        {
            if (value != antiAliasing)
            {
                SetOverallQualityDropdownCustom();

                antiAliasing = value;
                antiAliasingDropdown.value = value;
                currentURPAsset.msaaSampleCount = antiAliasingLevels[value];

                if (enableLogging)
                    Debug.Log("Anti Aliasing Value Changed: " + antiAliasingLevels[value]);
            }
        }

        public void OnVSyncToggleValueChange(bool value)
        {
            if (value != vSync)
            {
                SetOverallQualityDropdownCustom();

                vSync = value;
                vSyncToggle.isOn = value;
                QualitySettings.vSyncCount = value ? 1 : 0;

                if (enableLogging)
                    Debug.Log("VSync Value Changed: " + value);
            }
        }

        public void OnHDRToggleValueChange(bool value)
        {
            if (value != hDR)
            {
                SetOverallQualityDropdownCustom();

                hDR = value;
                hDRToggle.isOn = value;
                currentURPAsset.supportsHDR = value;

                if (enableLogging)
                    Debug.Log("HDR Value Changed: " + value);
            }
        }

        public void OnBloomToggleValueChange(bool value)
        {
            if (value != bloom)
            {
                bloom = value;
                bloomToggle.isOn = value;

                Bloom _bloom;
                postProcessVolume.profile.TryGetSettings(out _bloom);
                _bloom.active = value;

                if (enableLogging)
                    Debug.Log("Bloom Value Changed: " + value);
            }
        }

        public void OnCastShadowsToggleValueChange(bool value)
        {
            if (value != castShadows)
            {
                SetOverallQualityDropdownCustom();
            }

            castShadows = value;
            castShadowsToggle.isOn = value;
            //QualitySettings.shadows = value ? ShadowQuality.All : ShadowQuality.Disable;
            currentURPAsset.shadowDistance = value ? 50 : 0;

            if (enableLogging)
                Debug.Log("Cast Shadows Value Changed: " + QualitySettings.shadows);
        }

        #endregion

        IEnumerator ResetCanOverallQualitySetCustom()
        {
            yield return new WaitForSeconds(1f);
            canOverallQualitySetCustom = true;
        }


        private void SetOverallQualityDropdownCustom()
        {
            if (canOverallQualitySetCustom)
            {
                canOverallQualitySetCustom = false;

                overallQuality = 6;
                overallQualityDropdown.value = 6;
                QualitySettings.SetQualityLevel(6, true);
                currentURPAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;

                OnUIScaleSliderValueChange(uIScaleSlider.value);
                OnRenderScaleSliderValueChange(renderScaleSlider.value);

                OnTextureQualityDropdownValueChange(textureQualityDropdown.value); // very high
                OnShadowQualityDropdownValueChange(shadowQualityDropdown.value); // very high
                OnAntiAliasingDropdownValueChange(antiAliasingDropdown.value); // 4x MSAA

                OnVSyncToggleValueChange(vSyncToggle.isOn);
                OnHDRToggleValueChange(hDRToggle.isOn);
                OnBloomToggleValueChange(bloomToggle.isOn);
                OnCastShadowsToggleValueChange(castShadowsToggle.isOn);

                StartCoroutine(ResetCanOverallQualitySetCustom());
            }
        }

        private void RefreshSubValues()
        {
            OnRenderScaleSliderValueChange(currentURPAsset.renderScale);
            OnTextureQualityDropdownValueChange(3 - QualitySettings.masterTextureLimit);
            OnShadowQualityDropdownValueChange(currentURPAsset.shadowCascadeCount - 1);
            OnAntiAliasingDropdownValueChange((int)Math.Log(currentURPAsset.msaaSampleCount, 2));
            OnVSyncToggleValueChange(QualitySettings.vSyncCount == 1);
            OnHDRToggleValueChange(currentURPAsset.supportsHDR);

            Bloom _bloom;
            postProcessVolume.profile.TryGetSettings(out _bloom);
            OnBloomToggleValueChange(_bloom.active);

            OnCastShadowsToggleValueChange(currentURPAsset.shadowDistance != 0);

            if (enableLogging)
                Debug.Log("Sub Values Refreshed");
        }

        public void GetAndSetInitialValues()
        {
            ResetSettings();

            // Load the current audio settings from PlayerPrefs or leave from the default audio settings
            if (PlayerPrefs.HasKey("uIScale"))
                OnUIScaleSliderValueChange(PlayerPrefs.GetFloat("uIScale"));

            if (PlayerPrefs.HasKey("overallQuality"))
                OnOverallQualityDropdownValueChange(PlayerPrefs.GetInt("overallQuality"));

            if (QualitySettings.GetQualityLevel() == 6) // Custom settings
            {
                if (PlayerPrefs.HasKey("renderScale"))
                    OnRenderScaleSliderValueChange(PlayerPrefs.GetFloat("renderScale"));
                if (PlayerPrefs.HasKey("textureQuality"))
                    OnTextureQualityDropdownValueChange(PlayerPrefs.GetInt("textureQuality"));
                if (PlayerPrefs.HasKey("shadowQuality"))
                    OnShadowQualityDropdownValueChange(PlayerPrefs.GetInt("shadowQuality"));
                if (PlayerPrefs.HasKey("antiAliasing"))
                    OnShadowQualityDropdownValueChange(PlayerPrefs.GetInt("antiAliasing"));
                if (PlayerPrefs.HasKey("vSync"))
                    OnVSyncToggleValueChange(PlayerPrefs.GetInt("vSync") == 1);
                if (PlayerPrefs.HasKey("hDR"))
                    OnHDRToggleValueChange(PlayerPrefs.GetInt("hDR") == 1);
                if (PlayerPrefs.HasKey("bloom"))
                    OnBloomToggleValueChange(PlayerPrefs.GetInt("bloom") == 1);
                if (PlayerPrefs.HasKey("castShadows"))
                    OnCastShadowsToggleValueChange(PlayerPrefs.GetInt("castShadows") != 0);
            }

            SetPlayerPrefs();
        }

        public void SetPlayerPrefs()
        {
            PlayerPrefs.SetFloat("uIScale", uIScale);
            PlayerPrefs.SetFloat("renderScale", renderScale);
            PlayerPrefs.SetInt("overallQuality", overallQuality);
            PlayerPrefs.SetInt("textureQuality", textureQuality);
            PlayerPrefs.SetInt("shadowQuality", shadowQuality);
            PlayerPrefs.SetInt("antiAliasing", antiAliasing);

            PlayerPrefs.SetInt("vSync", Convert.ToInt32(vSync));
            PlayerPrefs.SetInt("hDR", Convert.ToInt32(hDR));
            PlayerPrefs.SetInt("bloom", Convert.ToInt32(bloom));
            PlayerPrefs.SetInt("castShadows", Convert.ToInt32(castShadows));

            if (enableLogging)
                Debug.Log("Graphics Settings Player Prefs Set");
        }

        public void ResetSettings()
        {
            OnUIScaleSliderValueChange(1f);
            OnRenderScaleSliderValueChange(1f);

            OnTextureQualityDropdownValueChange((int)TextureQuality.VeryHigh); // very high
            OnShadowQualityDropdownValueChange(3); // very high
            OnAntiAliasingDropdownValueChange(2); // 4x MSAA

            OnVSyncToggleValueChange(true);
            OnHDRToggleValueChange(true);
            OnBloomToggleValueChange(true);
            OnCastShadowsToggleValueChange(true);

            OnOverallQualityDropdownValueChange((int)OverallQuality.Ultra); // ultra

            if (enableLogging)
                Debug.Log("Graphics Settings Has Been Reset");
        }
    }
}