using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Linq;

public class SwitchLanguages : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;
    private Locale[] availableLocales;

    void Start()
    {
        InitializeLocales();
        PopulateLanguageOptions();
        
        languageDropdown.value = FindCurrentLanguageIndex();
        languageDropdown.RefreshShownValue();
        languageDropdown.onValueChanged.AddListener(HandleLanguageChange);
    }

    void InitializeLocales()
    {
        availableLocales = LocalizationSettings.AvailableLocales.Locales.ToArray();
    }

    void PopulateLanguageOptions()
    {
        var options = availableLocales
            .Select(locale => locale.Identifier.CultureInfo.NativeName)
            .ToList();
        
        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(options);
    }

    void HandleLanguageChange(int selectedIndex)
    {
        LocalizationSettings.SelectedLocale = availableLocales[selectedIndex];
    }

    int FindCurrentLanguageIndex()
    {
        for (int i = 0; i < availableLocales.Length; i++)
        {
            if (availableLocales[i] == LocalizationSettings.SelectedLocale)
            {
                return i;
            }
        }
        return 0;
    }
}
