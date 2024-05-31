using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class EmotionContext : EmotionPhoto
{
    [field:SerializeField]
    public string Text { get; protected set; }
    [field:SerializeField]
    public Character.Custom RndCharacter { get; protected set; }

    public override void SetContextEmotion(Emotion.EEmotion emotion, string text) {
        PhotoEmotion = emotion;
        Text = text;
        RndCharacter = Character.Custom.GetRandom();
    }
    /// TODO: 
}
