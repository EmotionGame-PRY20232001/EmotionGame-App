using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterExpressions : MonoBehaviour
{
    protected Animator Anim;

    protected Dictionary<Exercise.EEmotion, string> TriggerNames;

    public void Awake()
    {
        Anim = gameObject.GetComponent<Animator>();

        TriggerNames = new Dictionary<Exercise.EEmotion, string>();
        TriggerNames.Add(Exercise.EEmotion.Neutral, "Neutral");
        TriggerNames.Add(Exercise.EEmotion.Anger, "Anger");
        TriggerNames.Add(Exercise.EEmotion.Disgust, "Disgust");
        TriggerNames.Add(Exercise.EEmotion.Fear, "Fear");
        TriggerNames.Add(Exercise.EEmotion.Happy, "Happy");
        TriggerNames.Add(Exercise.EEmotion.Sad, "Sad");
        TriggerNames.Add(Exercise.EEmotion.Surprise, "Surprise");
    }

    public void PlayEmotion(Exercise.EEmotion emotion)
    {
        if (Anim == null) return;

        Anim.SetTrigger(TriggerNames[emotion]);
    }
}
