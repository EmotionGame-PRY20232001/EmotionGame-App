using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(ThemeColorFilled))]
public class SessionItem : MonoBehaviour
{
    public int num;
    public DateTime date;
    public List<EmotionExercise.EActivity> games;
    public List<Emotion.EEmotion> emotions;

    [SerializeField]
    protected TMPro.TMP_Text txtTitle;
    [SerializeField]
    protected TMPro.TMP_Text txtGames;
    [SerializeField]
    protected TMPro.TMP_Text txtEmotions;
    [SerializeField]
    protected Toggle toggle;
    protected ThemeColorFilled theme;

    protected void Awake()
    {
        theme = GetComponent<ThemeColorFilled>();
    }

    protected void Start()
    {
        if (toggle)
            toggle.onValueChanged.AddListener(SetEnabled);
    }

    public void SetData(int n, DateTime d, List<EmotionExercise.EActivity> gs, List<Emotion.EEmotion> ems)
    {
        num = n;
        date = d;
        games = gs;
        emotions = ems;

        if (txtTitle != null)
            txtTitle.text = num + ". " + date.ToShortDateString();

        var gm = GameManager.Instance;
        if (gm == null) return;

        string separator = ", ";
        if (txtGames != null)
        {
            txtGames.text = "";
            for (int i = 0; i < games.Count; i++)
            {
                txtGames.text = "";
            }
        }

        if (txtEmotions!=null)
        {
            txtEmotions.text = "";
            for (int i = 0; i < emotions.Count; i++)
            {
                txtEmotions.text += gm.Emotions[emotions[i]].Name;
                if (i > 0)
                    txtEmotions.text += separator;
            }
        }
    }

    public bool IsSelected()
    {
        return toggle.isOn;
    }

    protected void SetEnabled(bool enabled)
    {
        if (theme != null)
        {
            theme.OnLightnessChange(enabled ? Theme.ELightness.Light
                                            : Theme.ELightness.Dark);
        }
    }
}
