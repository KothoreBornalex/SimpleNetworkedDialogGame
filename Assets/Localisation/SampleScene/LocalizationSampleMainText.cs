using LocalizationPackage;
using TMPro;
using UnityEngine;

public class LocalizationSampleMainText : MonoBehaviour
{
    [SerializeField] LocalizationComponent localizationComponent;
    [Space(20)]
    [SerializeField] TextMeshProUGUI MainText;
    [SerializeField] TextMeshProUGUI DescriptionText;

    private void OnEnable()
    {
        LocalizationManager.OnRefresh += RefreshTexts;
    }
    private void OnDisable()
    {
        LocalizationManager.OnRefresh -= RefreshTexts;
    }

    private void RefreshTexts(SystemLanguage currentLanguage)
    {

    }
}