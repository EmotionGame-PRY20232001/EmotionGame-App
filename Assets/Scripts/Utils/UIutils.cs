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
        if (image != null)
            image.overrideSprite = sprite;
    }
    

    [System.Serializable]
    public struct CustomSprites<T>
    {
        public T Key;
        public Sprite Value;
    }

    [System.Serializable]
    public struct CustomSpritesMul<T,G>
    {
        public T Key1;
        public G Key2;
        public Sprite Value;
    }
}