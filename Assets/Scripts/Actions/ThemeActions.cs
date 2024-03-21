using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThemeActions : MonoBehaviour
{
    [SerializeField]
    private TMP_Text activityText;
    [SerializeField]
    private GameObject themeLayout;
    [SerializeField]
    private GameObject guideLayout;

    public void ShowThemeLayout()
    {
        activityText.text = "Elegir Tema";
        guideLayout.SetActive(false);
        themeLayout.SetActive(true);
    }

    public void ShowGuideLayout()
    {
        activityText.text = "Elegir Guía";
        themeLayout.SetActive(false);
        guideLayout.SetActive(true);
    }

    public void ApplyChanges()
    {
        themeLayout?.GetComponent<ThemeLayout>().ApplySelection();
        // guideLayout?.GetComponent<GuideLayout>().ApplySelection();
        // guideLayout?.GetComponent<CharacterCustomization>().ApplySelection();
    }
}
