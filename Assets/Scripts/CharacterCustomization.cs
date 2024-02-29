using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// Testing OnSelectionChange
// using UnityEditor;
// using UnityEngine.UIElements;

public class CharacterCustomization : MonoBehaviour
{
    // public Dictionary<Character.ESkinColor, Sprite> SkinColors = new Dictionary<Character.ESkinColor, Sprite>();
    [SerializeField]
    private List<CustomSprites<Character.ESkinColor>> SkinColors;
    [SerializeField]
    private List<CustomSpritesMul<Character.EHairCut, Character.EHairColor>> Hairs;
    [SerializeField]
    private List<CustomSprites<Character.EHairColor>> Eyebrows;
    [SerializeField]
    private List<CustomSprites<Character.EHairColor>> Eyelashes;
    [SerializeField]
    private List<CustomSprites<Character.EEyeColor>> EyeColors;
    [SerializeField]
    private List<CustomSprites<Character.EShirt>> Shirts;

    public Character.ESkinColor SkinColor;
    public Character.EHairColor HairColor;
    public Character.EHairCut HairCut;
    public Character.EEyeColor EyeColor;
    public Character.EShirt Shirt;
    
    public GameObject Body;

    // Start is called before the first frame update
    void Start()
    {
        Body.GetComponent<UnityEngine.UI.Image>().overrideSprite = SkinColors.Find( x => x.Key == SkinColor ).Value;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }


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
