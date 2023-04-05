using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

[AddComponentMenu("Localization/Localize Dropdown")]
public class LocalizeDropdown : MonoBehaviour
{
    [Serializable]
    public class LocalizedDropdownOption
    {
        public LocalizedString text;
    }

    public List<LocalizedDropdownOption> options;
    public int selectedOptionIndex = 0;

    private static List<TMP_Dropdown> allDropdowns = new List<TMP_Dropdown>();
    private static Locale currentLocale = null;

    private TMP_Dropdown dropdown;

    private void OnEnable()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        allDropdowns.Add(dropdown);
        LocalizationSettings.SelectedLocaleChanged += UpdateAllDropdowns;
        UpdateDropdown();
    }

    private void OnDisable()
    {
        allDropdowns.Remove(dropdown);
        LocalizationSettings.SelectedLocaleChanged -= UpdateAllDropdowns;
    }

    private void OnDestroy()
    {
        allDropdowns.Remove(dropdown);
        LocalizationSettings.SelectedLocaleChanged -= UpdateAllDropdowns;
    }

    private void Start()
    {
        if (currentLocale == null)
        {
            currentLocale = LocalizationSettings.SelectedLocale;
        }
        UpdateDropdown();
    }

    private void UpdateDropdown()
    {
        selectedOptionIndex = dropdown.value;
        dropdown.ClearOptions();

        for (int i = 0; i < options.Count; i++)
        {
            string localizedText = options[i].text.GetLocalizedString();
            dropdown.options.Add(new TMP_Dropdown.OptionData(localizedText));
        }

        dropdown.value = selectedOptionIndex;
        dropdown.RefreshShownValue();
    }

    private static void UpdateAllDropdowns(Locale locale)
    {
        currentLocale = locale;
        foreach (TMP_Dropdown dropdown in allDropdowns)
        {
            LocalizeDropdown localizeDropdown = dropdown.GetComponent<LocalizeDropdown>();
            localizeDropdown.UpdateDropdown();
        }
    }
}
