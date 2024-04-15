using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    // [SerializeField]
    // private Image guideImage;

    public static class Scenes
    {
        public static readonly string NONE = "";
        public static readonly string PLAYER_SELECT = "PlayerSelect";
        public static readonly string MAIN_MENU = "MainMenu";
        public static readonly string SELECT_THEME = "SelectTheme";
        public static readonly string SELECT_GAMES = "SelectGames";
        public static readonly string GAME_CHOOSE = "GameChoose";
        public static readonly string GAME_CONTEXT = "GameContext";
        public static readonly string GAME_IMITATE = "GameImitate";
        public static readonly string LEARN_EMOTIONS = "EmotionsTeaching";
        public static readonly string LEARN_COMPLETE = "TeachingComplete";
        public static readonly string GAME_COMPLETE = "GameComplete";
    };
    
    private void Awake()
    {
        // var player = GameManager.Instance.GetCurrentPlayer();
        //GameManager.Instance.GetCurrentPlayer().Info();
        // if (guideImage) guideImage.sprite = GameManager.Instance.GetGuideSprites()[player.GuideId];
    }


    ////========  ========////

    public void GoToPlayerSelecion()
    {
        GameManager.Instance.SetCurrentPlayer(null);
        SceneManager.LoadScene(Scenes.PLAYER_SELECT);
    }

    public static void GoToMainMenu()
    {
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
        DBManager.Instance.UpdatePlayerToDb(player);
        GoToMainMenu();
    }


    ////======== GO TO GAME SCREENS ========////
    public void GoToLastGame()
    {
        Exercise.EGame GameSelected = GameManager.Instance.LastPlayedGame;
        switch (GameSelected)
		{
            case Exercise.EGame.Choose:
				GoToChooseGame();
                break;
            case Exercise.EGame.Context:
				GoToContextGame();
                break;
            case Exercise.EGame.Imitate:
				GoToImitateGame();
                break;
            default:
                break;
        }
    }
    public static void GoToGameSelection()
    {
        SceneManager.LoadScene(Scenes.SELECT_GAMES);
    }

    public static void GoToChooseGame()
    {
        SceneManager.LoadScene(Scenes.GAME_CHOOSE);
    }

    public static void GoToContextGame()
    {
        SceneManager.LoadScene(Scenes.GAME_CONTEXT);
    }

    public static void GoToImitateGame()
    {
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

}
