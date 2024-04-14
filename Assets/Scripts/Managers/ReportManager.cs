using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ReportManager : MonoBehaviour
{
    /// UI shared
    [SerializeField]
    public TMP_Text CurrentPlayerName;
    [SerializeField]
    protected TMP_Text CurrentTabName;
    [SerializeField]
    protected TMP_Text CurrentGameName;
    [SerializeField]
    protected TMP_Text CurrentDateText;

    /// Currents
    protected Player CurrentPlayer = null;
    protected UIActions.EGames CurrentGame = UIActions.EGames.None;
    protected List<DateTime> SelectedDates = new List<DateTime>();

    protected void Start()
    {
        LoadPlayer();
    }

    protected void LoadPlayer()
    {
        var gm = GameManager.Instance;
        if (gm != null)
        {
            CurrentPlayer = gm.currentPlayer;
            CurrentPlayerName.text = CurrentPlayer.Name;
        }
    }

    public void SetReportName(string name)
    {
        if (CurrentTabName != null)
            CurrentTabName.text = name;
    }
    protected void SetGameName(string name)
    {
        if (CurrentGameName != null)
            CurrentGameName.text = name;
    }
    protected void SetSelectedDates(List<DateTime> selectedDates)
    {
        if (selectedDates != null)
            SelectedDates = selectedDates;

        DateTime minDate = DateTime.MaxValue;
        DateTime maxDate = DateTime.MinValue;
        //TODO: Optimize
        foreach (DateTime date in selectedDates)
        {
            if (date < minDate)
                minDate = date;
            if (date > maxDate)
                maxDate = date;
        }

        if (CurrentDateText != null)
            CurrentDateText.text = minDate.ToShortDateString();

        if (minDate != maxDate)
            CurrentDateText.text += (" - " + maxDate.ToShortDateString());

    }
}
