using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class Theme : MonoBehaviour
{
    public enum EBackground { Main, Space, Clouds, Forest, Underwater, }
    public enum ETheme { Warm, Cold, }
    public enum ETypes { Primary, Accent, Paper, Danger }
    public enum ELightness { Main, Light, Dark, Disabled }
    
    
    [System.Serializable]
    public struct ColorBgShape
    {
        [field:SerializeField]
        public Color Background { get; private set; }
        [field:SerializeField]
        public Color Shape { get; private set; }
        //TODO: check if disabled color and text
    }

    [System.Serializable]
    public struct ThemeColor
    {
        [field: SerializeField]
        public ColorBgShape Light { get; private set; }
        [field: SerializeField]
        public ColorBgShape Main { get; private set; }
        [field: SerializeField]
        public ColorBgShape Dark { get; private set; }
        [field: SerializeField]
        public ColorBgShape Disabled { get; private set; }
    }

    [System.Serializable]
    public struct CustomPalette
    {
        [field:SerializeField]
        public string Name { get; private set; }
        [field:SerializeField]
        public ThemeColor Primary { get; private set; }
        [field:SerializeField]
        public ThemeColor Accent { get; private set; }
        [field:SerializeField]
        public ThemeColor Paper { get; private set; }
        [field:SerializeField]
        public ThemeColor Danger { get; private set; }
    }

    [System.Serializable]
    public struct CustomBackground
    {
        [field:SerializeField]
        public string Name { get; private set; }
        [field:SerializeField]
        public Texture Texture { get; private set; }
        [field:SerializeField]
        public Sprite Thumbnail { get; private set; }
    }
}
