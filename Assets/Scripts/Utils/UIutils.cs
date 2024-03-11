using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIutils : MonoBehaviour
{
    static public void SetImage(GameObject _object, Sprite sprite)
    {
        if (_object == null) return;
        Image image = _object.GetComponent<Image>();
        SetImage(image, sprite);
    }
    
    static public void SetImage(Image image, Sprite sprite)
    {
        if (image == null) return;
        
        if (sprite == null)
        {
            image.enabled = false;
        }
        else
        {
            if (!image.enabled)
                image.enabled = true;
            // image.overrideSprite = sprite;
            image.sprite = sprite;
        }
    }
    

    // [System.Serializable]
    // public struct CustomSprites<T>
    // {
    //     public T Key;
    //     public Sprite Value;
    // }

    // [System.Serializable]
    // public struct CustomSpritesMul<T,G>
    // {
    //     public T Key1;
    //     public G Key2;
    //     public Sprite Value;
    // }

    [System.Serializable]
    public struct SpriteName
    {
        [field:SerializeField]
        public string Name { get; private set; }
        [field:SerializeField]
        public Sprite Sprite { get; private set; }
    }
    [System.Serializable]
    public struct TextureName
    {
        [field:SerializeField]
        public string Name { get; private set; }
        [field:SerializeField]
        public Texture Texture { get; private set; }
    }
}