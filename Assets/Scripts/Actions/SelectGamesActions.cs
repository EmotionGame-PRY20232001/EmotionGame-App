using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectGamesActions : MonoBehaviour
{
    protected UIActions.EGames GameSelected = UIActions.EGames.None;
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
        GameSelected = UIActions.EGames.Choose;
    }
    public void SelectGameContext()
    {
        GameSelected = UIActions.EGames.Context;
    }
    public void SelectGameImitate()
    {
        GameSelected = UIActions.EGames.Imitate;
    }

    public void SaveSelectedGame()
    {
        GameManager.Instance.LastPlayedGame = GameSelected;
    }
}
