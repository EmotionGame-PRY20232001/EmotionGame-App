using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterExpressions : MonoBehaviour
{
    protected bool m_loaded = false;
    protected Animator Anim;
    protected Dictionary<Emotion.EEmotion, string> TriggerNames;

    public void Awake()
    {
        Load();
    }

    public void Load()
    {
        if (m_loaded) return;

        Anim = gameObject.GetComponent<Animator>();

        TriggerNames = new Dictionary<Emotion.EEmotion, string>();
        TriggerNames.Add(Emotion.EEmotion.Neutral, "Neutral");
        TriggerNames.Add(Emotion.EEmotion.Anger, "Anger");
        TriggerNames.Add(Emotion.EEmotion.Disgust, "Disgust");
        TriggerNames.Add(Emotion.EEmotion.Fear, "Fear");
        TriggerNames.Add(Emotion.EEmotion.Happy, "Happy");
        TriggerNames.Add(Emotion.EEmotion.Sad, "Sad");
        TriggerNames.Add(Emotion.EEmotion.Surprise, "Surprise");

        m_loaded = true;
    }

    public void PlayEmotion(Emotion.EEmotion emotion)
    {
        if (Anim == null) return;

        Anim.SetTrigger(TriggerNames[emotion]);
    }
}
