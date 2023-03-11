using SettingsManagers.Abstract;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace SettingsManagers
{
    public class LanguageSettingsManager : MonoBehaviour, ISettingsManager
    {
        public enum Language
        {
            English,
            Turkish,
            Deutch,
            Spanish,
            French,
            Italian,
            Russian
        }

        public static Language currentLanguage { get; private set; }
        public static bool showSubtitles { get; private set; }

        [Header("Language Settings Elements")]
        [SerializeField] private TMP_Dropdown languageDropdown;
        [SerializeField] private ToggleGroup showSubtitlesToggleGroup;

        [Header("Debug")]
        [SerializeField] private bool enableLogging;

        void Awake()
        {
            showSubtitlesToggleGroup.GetComponentInChildren<Toggle>().onValueChanged.AddListener(OnShowSubtitlesToggleGroupValueChange);

            GetAndSetInitialValues();
        }

        #region OnSettingsValueChange

        public void OnLanguageDropdownValueChange(int value)
        {
            StartCoroutine(SetLocale(value));
            currentLanguage = (Language)value;
        }

        public void OnShowSubtitlesToggleGroupValueChange(bool value)
        {
            showSubtitles = value;
            SetShowSubtitlesToggleGroupValue(System.Convert.ToInt32(value));
        }

        #endregion

        IEnumerator SetLocale(int _localeID)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];

            //string currentLanguageCode = LocalizationSettings.SelectedLocale.Formatter.ToString();
            //CultureInfo.CurrentCulture = new CultureInfo(currentLanguageCode + "-" + currentLanguageCode.ToUpper());
        }

        private void SetShowSubtitlesToggleGroupValue(int value)
        {
            Toggle[] toggles = showSubtitlesToggleGroup.GetComponentsInChildren<Toggle>();
            if (value == 1)
                toggles[0].isOn = true;
            else
                toggles[1].isOn = true;
        }

        public void GetAndSetInitialValues()
        {
            ResetSettings();

            // Load the current language settings from PlayerPrefs or set the default language settings
            if (PlayerPrefs.HasKey("language"))
                OnLanguageDropdownValueChange(PlayerPrefs.GetInt("language"));
            if (PlayerPrefs.HasKey("showSubtitles"))
                OnShowSubtitlesToggleGroupValueChange(PlayerPrefs.GetInt("showSubtitles") == 1);

            SetPlayerPrefs();
        }

        public void SetPlayerPrefs()
        {
            PlayerPrefs.SetInt("language", (int)currentLanguage);
            PlayerPrefs.SetInt("showSubtitles", System.Convert.ToInt32(showSubtitles));
        }

        public void ResetSettings()
        {
            OnLanguageDropdownValueChange((int)Language.Turkish);
            OnShowSubtitlesToggleGroupValueChange(true);
        }

    }

}