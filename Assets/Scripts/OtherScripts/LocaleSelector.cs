using System.Collections;
using UnityEngine;

using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool active = false;

    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        SaveSelectedLocale(_localeID);
        active = false;
    }

    void SaveSelectedLocale(int localeID)
    {
        PlayerPrefs.SetInt("SelectedLocaleID", localeID);
        PlayerPrefs.Save();
    }

    void Start()
    {
        int savedLocaleID = PlayerPrefs.GetInt("SelectedLocaleID", 1);
        StartCoroutine(SetLocale(savedLocaleID));
    }
}
