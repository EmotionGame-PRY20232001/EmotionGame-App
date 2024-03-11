using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class Theme : MonoBehaviour
{
    public enum EBackground { Main, Space, Clouds, Forest, Underwater, }
    public enum ETheme { Warm, Cold, }
    
    
    [System.Serializable]
    public struct ThemeColor
    {
        [field:SerializeField]
        public Color Light { get; private set; }
        [field:SerializeField]
        public Color Main { get; private set; }
        [field:SerializeField]
        public Color Dark { get; private set; }
    }
    
    [System.Serializable]
    public struct ThemeColorText
    {
        [field:SerializeField]
        public ThemeColor Background { get; private set; }
        [field:SerializeField]
        public ThemeColor Text { get; private set; }
        //TODO: check if disabled color and text
    }

    [System.Serializable]
    public struct CustomTheme
    {
        [field:SerializeField]
        public string Name { get; private set; }
        [field:SerializeField]
        public ThemeColorText Primary { get; private set; }
        [field:SerializeField]
        public ThemeColorText Accent { get; private set; }
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
