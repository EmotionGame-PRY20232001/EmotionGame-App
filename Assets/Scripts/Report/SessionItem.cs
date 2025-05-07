using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(ThemeColorFilled))]
public class SessionItem : MonoBehaviour
{
    [field: SerializeField]
    public SessionData session { get; protected set; }
    [field: SerializeField]
    public Toggle toggle { get; protected set; }

    [SerializeField]
    protected TMPro.TMP_Text txtTitle;
    [SerializeField]
    protected TMPro.TMP_Text txtGames;
    [SerializeField]
    protected TMPro.TMP_Text txtEmotions;
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

    public void SetData(SessionData s)
    {
        session = s;

        if (txtTitle != null)
            txtTitle.text = session.Num + ". " + session.Date.ToShortDateString();

        var gm = GameManager.Instance;
        if (gm == null) return;

        string separator = ", ";
        if (txtGames != null)
        {
            txtGames.text = "";
            for (int i = 0; i < session.Games.Count; i++)
            {
                txtGames.text += gm.Games[session.Games[i]].Name;
                if (i > 0)
                    txtGames.text += separator;
            }
        }

        if (txtEmotions!=null)
        {
            txtEmotions.text = "";
            for (int i = 0; i < session.Emotions.Count; i++)
            {
                txtEmotions.text += gm.Emotions[session.Emotions[i]].Name;
                if (i > 0)
                    txtEmotions.text += separator;
            }
        }
    }

    protected void SetEnabled(bool enabled)
    {
        if (theme != null)
        {
            theme.OnLightnessChange(enabled ? Theme.ELightness.Light
                                            : Theme.ELightness.Dark);
        }
    }

    [Serializable]
    public struct SessionData
    {
        public uint Num;
        public DateTime Date;
        public List<EmotionExercise.EActivity> Games;
        public List<Emotion.EEmotion> Emotions;
    }
}
