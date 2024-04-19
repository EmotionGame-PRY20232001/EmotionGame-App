using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectGamesActions : MonoBehaviour
{
    protected Exercise.EActivity GameSelected = Exercise.EActivity.None;
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
        GameSelected = Exercise.EActivity.Choose;
    }
    public void SelectGameContext()
    {
        GameSelected = Exercise.EActivity.Context;
    }
    public void SelectGameImitate()
    {
        GameSelected = Exercise.EActivity.Imitate;
    }

    public void SaveSelectedGame()
    {
        GameManager.Instance.LastPlayedGame = GameSelected;
    }
}
