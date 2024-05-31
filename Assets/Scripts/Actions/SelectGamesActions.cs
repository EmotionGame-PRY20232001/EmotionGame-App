using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectGamesActions : MonoBehaviour
{
    protected EmotionExercise.EActivity GameSelected = EmotionExercise.EActivity.None;
    [SerializeField]
    protected PopUp PopUpEmotions;
    [SerializeField]
    protected EmotionToggles EmotionToggles;
    // TODO ADD buttons to unselect?

	public void Start()
	{
        if (PopUpEmotions != null)
        {
            if (EmotionToggles != null)
            {
                PopUpEmotions.onAccept += EmotionToggles.SaveEmotionsChecked;
            }
            PopUpEmotions.onAccept += SaveSelectedGame;
        }
    }

	public void SelectGameChoose()
    {
        GameSelected = EmotionExercise.EActivity.Choose;
    }
    public void SelectGameContext()
    {
        GameSelected = EmotionExercise.EActivity.Context;
    }
    public void SelectGameImitate()
    {
        GameSelected = EmotionExercise.EActivity.Imitate;
    }

    public void SaveSelectedGame()
    {
        GameManager.Instance.LastPlayedGame = GameSelected;
    }
}
