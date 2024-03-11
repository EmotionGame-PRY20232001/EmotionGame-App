using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum ESkinColor { Lightest, Light, Medium, Dark, Deep }
    public enum EEyeColor { Dark, Brown, Green, Blue, Gray }
    public enum EHairColor { Black, Brown, Redhead, Blonde, Albino }
    public enum EHairCut { None, WeavyShort, Medium, Long }
    public enum EShirt { Calm, Sport, Andean, Forest, Plaid, }

    
    [System.Serializable]
    public struct Custom
    {
        public ESkinColor SkinColor;
        public EEyeColor EyeColor;
        public EHairColor HairColor;
        public EHairCut HairCut;
        public EShirt Shirt;

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
        public static Custom FromJson(string jsonString)
        {
            if (jsonString == "") return GetDefault();
            return JsonUtility.FromJson<Custom>(jsonString);
        }

        public static Custom GetDefault()
        {
            var custom = new Custom();
            custom.SkinColor   = Character.ESkinColor.Medium;
            custom.EyeColor    = Character.EEyeColor.Dark;
            custom.HairColor   = Character.EHairColor.Black;
            custom.HairCut     = Character.EHairCut.Medium;
            custom.Shirt       = Character.EShirt.Calm;
            return custom;
        }
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