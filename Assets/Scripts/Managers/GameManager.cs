using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    // [SerializeField]
    public Player currentPlayer;
    public EmotionExercise.EActivity LastPlayedGame;

    [field: SerializeField] [SerializedDictionary("Game", "Data")]
    public SerializedDictionary<EmotionExercise.EActivity, GameData> Games { get; protected set; }

    [field:SerializeField][SerializedDictionary("Emotion", "Data")]
    public SerializedDictionary<Emotion.EEmotion, Emotion> Emotions { get; protected set; }

    [field:SerializeField]
    public List<Emotion.EEmotion> AllEmotions { get; private set; }
    [field: SerializeField]
    public List<Emotion.EEmotion> SelectedEmotions = new List<Emotion.EEmotion>();
    
    [field:SerializeField]
    public CustomThemes ThemeCustom { get; private set; }
    [field:SerializeField]
    public CharacterCustomParts CharacterCustom { get; private set; }

    //Temporal fix
    public sbyte LastNumCorrectAnswers;
    public sbyte LastNumExcercises;
    public int LastScore;

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

    public bool IsPlayerActive()
    {
        //if on Player select menu then true else false
        if (currentPlayer == null) return false;
        return currentPlayer.Id != -1 && currentPlayer.Name != null;
    }
    
    public void SetCurrentPlayerTheme(Theme.EBackground selectedBg)
    {
        currentPlayer.BackgroundId = (int)selectedBg;
    }
    
    public void SetCurrentPlayerGuide(string customization)
    {
        currentPlayer.GuideJSON = customization;
    }

    public void SetCurrentPlayer(Player player)
    {
        currentPlayer = player;
    }

    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    // public List<Sprite> GetGuideSprites()
    // {
    //     return guideSprites;
    // }
    
    public SerializedDictionary<Theme.EBackground, Theme.CustomBackground> GetBackgrounds() 
    {
        return ThemeCustom.Backgrounds;
    }

    
    [System.Serializable]
    public struct CustomThemes
    {
        [field:SerializeField][SerializedDictionary("Background", "TextureName")]
        public SerializedDictionary<Theme.EBackground, Theme.CustomBackground> Backgrounds { get; private set; }
        [field:SerializeField][SerializedDictionary("Theme", "CustomPalette")]
        public SerializedDictionary<Theme.ETheme, Theme.CustomPalette> Themes { get; private set; }
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

    [System.Serializable]
    public struct GameData
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public Sprite Icon { get; private set; }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
