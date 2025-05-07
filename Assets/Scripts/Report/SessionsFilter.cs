using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SessionsFilter : MonoBehaviour
{
    [SerializeField]
    protected Button BtnAll;
    [SerializeField]
    protected Button BtnClean;
    [SerializeField]
    protected Button BtnFilter;

    [SerializeField]
    protected RectTransform SessionsContainer;
    [SerializeField]
    protected SessionItem SessionItemPrefab;

    [SerializeField]
    protected List<SessionItem.SessionData> Sessions = new List<SessionItem.SessionData>();
    [SerializeField]
    protected List<SessionItem> SessionItems = new List<SessionItem>();
    [SerializeField]
    protected List<DateTime> AllDates = new List<DateTime>();

    [SerializeField]
    protected ReportManager Manager;

    protected void Awake()
    {
        if (BtnAll != null)
            BtnAll.onClick.AddListener(SelectAll);

        if (BtnClean != null)
            BtnClean.onClick.AddListener(DeselectAll);

        if (BtnFilter != null)
            BtnFilter.onClick.AddListener(FilterSessions);
    }

    public void LoadSessions()
    {
        if (Manager == null ||
            Manager.Responses == null || Manager.Responses.Count == 0)
            return;

        bool enableAll = Manager.SelectedDates.Count == 0;
        uint i = 1;
        var grByDate = Manager.Responses.GroupBy(r => r.response.CompletedAt.Date);
        foreach (var d in grByDate)
        {
            AllDates.Add(d.Key);

            SessionItem.SessionData sd;
            sd.Date = d.Key;
            sd.Emotions = d.Select(r => r.idCont.emotion).Distinct().ToList();
            sd.Games = d.Select(r => r.exercise.ActivityId).Distinct().ToList();
            sd.Num = i;
            i++;
            Sessions.Add(sd);

            SessionItem si = Instantiate(SessionItemPrefab, SessionsContainer);
            if (si != null)
            {
                si.SetData(sd);
                si.toggle.isOn = enableAll ? true : sd.Date == DateTime.Today.Date;
                si.toggle.onValueChanged.AddListener(EnableFilterButton);
                SessionItems.Add(si);
            }
        }

        if (SessionsContainer != null)
        {
            Vector2 cPos = Vector2.down * SessionItems.Count * 0.5f * -300;
            SessionsContainer.anchoredPosition = cPos;
        }
    }

    protected void SelectAll()
    {
        foreach(var si in SessionItems)
        {
            si.toggle.isOn = true;
        }
        BtnFilter.interactable = true;
    }
    protected void DeselectAll()
    {
        foreach(var si in SessionItems)
        {
            si.toggle.isOn = false;
        }
        BtnFilter.interactable = false;
    }

    protected List<DateTime> GetAllSelectedDates()
    {
        List<DateTime> selectedDates = new List<DateTime>();

        foreach (var si in SessionItems)
        {
            if (si.toggle.isOn)
                selectedDates.Add(si.session.Date);
        }

        return selectedDates;
    }

    protected void EnableFilterButton(bool _)
    {
        List<DateTime> allDates = GetAllSelectedDates();
        BtnFilter.interactable = allDates.Count > 0;
    }

    public void FilterSessions()
    {
        List<DateTime> allDates = GetAllSelectedDates();

        if (Manager != null)
        {
            Manager.SetSelectedDates(allDates);
        }
    }
}
