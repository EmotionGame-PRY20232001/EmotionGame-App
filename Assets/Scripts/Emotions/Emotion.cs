using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotion : MonoBehaviour
{
    public enum EEmotion { Anger, Disgust, Fear, Happy, Neutral, Sad, Surprise }
    
    // [System.Flags]
    // public enum EFEmotions
    // {
    //     Happy    = 0,
    //     Sad      = 1 << 0,
    //     Anger    = 1 << 1,
    //     Disgust  = 1 << 2,
    //     Fear     = 1 << 3,
    //     Surprise = 1 << 4
    // }

    
    [System.Serializable]
    public struct Data
    {
        public Sprite Sprite;
        public string Name;
        public List<Sprite> Faces;
        public List<string> Contexts;
    }
}