using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Report : MonoBehaviour
{
    [SerializeField]
    protected ReportManager Manager;
    [SerializeField]
    protected string Name;
    [SerializeField]
    protected Button ButtonAux;
    //Filter
    //action button (download|help)


    protected virtual void Start()
    {
        if (Manager != null)
            Manager.SetReportName(Name);
    }

    protected virtual void OnEnable()
    {
        if (ButtonAux != null)
            ButtonAux.gameObject.SetActive(true);
    }

    protected virtual void OnDisable()
    {
        if (ButtonAux != null)
            ButtonAux.gameObject.SetActive(false);
    }

    protected abstract void OnFilerDates(List<System.DateTime> dates);

    protected abstract void OnGameToggleChanged(Exercise.EActivity game);

    protected void OnLoad(List<System.DateTime> dates, Exercise.EActivity game)
    {
    }


    //protected abstract void OnGameChoose();
    //protected abstract void OnGameContext();
    //protected abstract void OnGameImitate();
    //protected abstract void OnGameAll();
}
