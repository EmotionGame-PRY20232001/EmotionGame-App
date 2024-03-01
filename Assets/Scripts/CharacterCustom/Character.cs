using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum ESkinColor { Lightest, Light, Medium, Dark, Deep }
    public enum EHairColor { Black, Brown, Redhead, Blonde, Albino }
    public enum EHairCut { None, WeavyShort, Medium, Long }
    public enum EEyeColor { Dark, Brown, Green, Blue, Gray }
    public enum EShirt { Calm, Sport, Andean, Forest, Plaid, }

    
    [System.Serializable]
    public struct Custom
    {
        public ESkinColor SkinColor;
        public EHairColor HairColor;
        public EHairCut HairCut;
        public EEyeColor EyeColor;
        public EShirt Shirt;
    }

    [System.Serializable]
    public class GOParts
    {
        public GameObject Body;
        public GameObject HairBack;
        public GameObject EyelashesL;
        public GameObject EyelashesR;
        public GameObject EyebrowL;
        public GameObject EyebrowR;
        public GameObject ScleraL;
        public GameObject ScleraR;
        public GameObject IrisL;
        public GameObject IrisR;
        public GameObject Mouth;
        public GameObject Shirt;
    }
}