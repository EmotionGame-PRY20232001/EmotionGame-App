using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField]
    private Player currentPlayer;
    [field:SerializeField]
    public Texture2D MainBackground { get; private set; }
    [SerializeField]
    private List<Texture2D> backgrounds;
    [SerializeField]
    private List<Sprite> guideSprites;
    [SerializeField]
    private List<string> backgroundNames;
    [SerializeField]
    private List<Sprite> emotionSprites;
    
    [field:SerializeField]
    public CharacterCustomParts CharacterCustom { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void ChangeBackground(int backgroundId)
    {
        GameObject.Find("Background").GetComponent<RawImage>().texture = backgrounds[backgroundId];
    }

    public void SetCurrentPlayer(Player player)
    {
        currentPlayer = player;
    }

    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public List<Texture2D> GetBackgrounds()
    {
        return backgrounds;
    }

    public List<Sprite> GetGuideSprites()
    {
        return guideSprites;
    }

    public List<string> GetBackgroundNames() 
    {
        return backgroundNames;
    }

    public List<Sprite> GetEmotionSprites()
    {
        return emotionSprites;
    }

    
    [System.Serializable]
    public struct CharacterCustomParts
    {
        //TODO: Check if HairColor struct needed
        [field:SerializeField][SerializedDictionary("Skin Color", "Sprite")]
        public SerializedDictionary<Character.ESkinColor, Sprite> SkinColors { get; private set; }
        [field:SerializeField][SerializedDictionary("Eye Color", "Sprite")]
        public SerializedDictionary<Character.EEyeColor, Sprite> EyeColors { get; private set; }
        [field:SerializeField][SerializedDictionary("Hair Color", "Sprites")]
        public SerializedDictionary<Character.EHairColor, CustomHair> Hairs { get; private set; }
        // [field:SerializeField][SerializedDictionary("Hair Cuts Icons", "Sprite")]
        // public SerializedDictionary<Character.EHairCut, Sprite> HairCuts { get; private set; }
        [field:SerializeField][SerializedDictionary("Shirt", "Sprite")]
        public SerializedDictionary<Character.EShirt, Sprite> Shirts { get; private set; }
    }

    [System.Serializable]
    public struct CustomHair
    {
        [field:SerializeField]
        public Color Color { get; private set; }
        [field:SerializeField]
        public Sprite Eyebrow { get; private set; }
        [field:SerializeField]
        public Sprite Eyelashes { get; private set; }
        [field:SerializeField][SerializedDictionary("Haircut", "Sprite")]
        public SerializedDictionary<Character.EHairCut, Sprite> HairCuts { get; private set; }
    }
}
