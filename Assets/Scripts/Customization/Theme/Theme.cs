using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class Theme : MonoBehaviour
{
    public enum EBackground { Main, Space, Clouds, Forest, Underwater, }
    public enum ETheme { Warm, Cold, }
    public enum ETypes { Primary, Accent, Paper, Danger }
    
    
    // [System.Serializable]
    // public struct ThemeColor
    // {
    //     [field:SerializeField]
    //     public Color Light { get; private set; }
    //     [field:SerializeField]
    //     public Color Main { get; private set; }
    //     [field:SerializeField]
    //     public Color Dark { get; private set; }
    // }
    
    [System.Serializable]
    public struct ColorText
    {
        [field:SerializeField]
        public Color Background { get; private set; }
        [field:SerializeField]
        public Color Text { get; private set; }
        //TODO: check if disabled color and text
    }

    [System.Serializable]
    public struct CustomPalette
    {
        [field:SerializeField]
        public string Name { get; private set; }
        [field:SerializeField]
        public ColorText Primary { get; private set; }
        [field:SerializeField]
        public ColorText Accent { get; private set; }
        [field:SerializeField]
        public ColorText Paper { get; private set; }
        [field:SerializeField]
        public ColorText Danger { get; private set; }
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
