using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    // [SerializeField]
    // private Image guideImage;

    // to from
    protected static Dictionary<string, string> BackScene = new Dictionary<string, string>();

    public static class Scenes
    {
        public static readonly string NONE = "";
        public static readonly string PLAYER_SELECT = "PlayerSelect";
        public static readonly string MAIN_MENU = "MainMenu";
        public static readonly string SELECT_THEME = "SelectTheme";
        //public static readonly string SELECT_GAMES = "SelectGames";
        public static readonly string GAME_CHOOSE = "GameChoose";
        public static readonly string GAME_CONTEXT = "GameContext";
        public static readonly string GAME_IMITATE = "GameImitate";
        public static readonly string LEARN_EMOTIONS = "EmotionsTeaching";
        public static readonly string LEARN_COMPLETE = "TeachingComplete";
        public static readonly string GAME_COMPLETE = "GameComplete";
        public static readonly string REPORT = "ReportStats";
    };
    
    private void Awake()
    {
        // var player = GameManager.Instance.GetCurrentPlayer();
        //GameManager.Instance.GetCurrentPlayer().Info();
        // if (guideImage) guideImage.sprite = GameManager.Instance.GetGuideSprites()[player.GuideId];
    }

    private void Start()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.Load();
    }

    private static bool CheckPauseStatus()
    {
        return Time.timeScale == 0;
    }


    ////========  ========////

    public static void GoToPlayerSelecion()
    {
        GameManager.Instance?.SetCurrentPlayer(null);
        AudioManager.Instance?.Load();
        SceneManager.LoadScene(Scenes.PLAYER_SELECT);
    }

    public static void GoToMainMenu()
    {
        if (CheckPauseStatus()) GameManager.Instance.ResumeGame();
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }


    ////======== CUSTOMIZATION SCREENS ========////
    
    public static void GoToSelectTheme()
    {
        SceneManager.LoadScene(Scenes.SELECT_THEME);
    }

    public void ApplySelectedOptions()
    {
        var player = GameManager.Instance.GetCurrentPlayer();
        if (player == null)
        {
            Debug.Log("Current player is null");
            return;
        }
        if (DBManager.Instance != null)
            DBManager.Instance.UpdatePlayerToDb(player);
        else
        {
            Debug.LogWarning("DBManager.Instance is null");
        }
    }


    ////======== GO TO GAME SCREENS ========////
    public void GoToLastGame()
    {
        EmotionExercise.EActivity GameSelected = GameManager.Instance.LastPlayedGame;
        switch (GameSelected)
		{
            case EmotionExercise.EActivity.Learn:
				GoToLearnEmotions();
                break;
            case EmotionExercise.EActivity.Choose:
				GoToChooseGame();
                break;
            case EmotionExercise.EActivity.Context:
				GoToContextGame();
                break;
            case EmotionExercise.EActivity.Imitate:
				GoToImitateGame();
                break;
            default:
                break;
        }
    }
    //public static void GoToGameSelection()
    //{
    //    SceneManager.LoadScene(Scenes.SELECT_GAMES);
    //}

    public static void GoToChooseGame()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LastPlayedGame = EmotionExercise.EActivity.Choose;
        SceneManager.LoadScene(Scenes.GAME_CHOOSE);
    }

    public static void GoToContextGame()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LastPlayedGame = EmotionExercise.EActivity.Context;
        SceneManager.LoadScene(Scenes.GAME_CONTEXT);
    }

    public static void GoToImitateGame()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.LastPlayedGame = EmotionExercise.EActivity.Imitate;
        SceneManager.LoadScene(Scenes.GAME_IMITATE);
    }
    public static void GoToGameComplete()
    {
        SceneManager.LoadScene(Scenes.GAME_COMPLETE);
    }
    public static void GoToLearnEmotions()
    {
        SceneManager.LoadScene(Scenes.LEARN_EMOTIONS);
    }
    public static void GoToLearnComplete()
    {
        SceneManager.LoadScene(Scenes.LEARN_COMPLETE);
    }
    
    public static void GoToReport()
    {
        string currScene = SceneManager.GetActiveScene().name;
        BackScene[Scenes.REPORT] = currScene;
        SceneManager.LoadScene(Scenes.REPORT);
    }
    public static void ReturnFromReport()
    {
        if (BackScene.ContainsKey(Scenes.REPORT))
        {
            string oldScene = BackScene[Scenes.REPORT];

            if (oldScene == Scenes.PLAYER_SELECT)
                GoToPlayerSelecion();

            else
                SceneManager.LoadScene(oldScene);
        }

        else
            GoToPlayerSelecion();
    }

    public static void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
