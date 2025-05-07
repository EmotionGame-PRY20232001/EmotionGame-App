using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Report : MonoBehaviour
{
    [field:SerializeField]
    public string ReportName { get; protected set; }
    [SerializeField]
    protected ReportManager Manager;
    [SerializeField]
    protected Button ButtonAux;
    //Filter
    //action button (download|help)

    protected bool loaded = false;
    public bool awaitingReload = false;

    protected virtual void Start()
    {
        if (Manager != null)
            Manager.SetReportName(ReportName);
    }

    protected virtual void OnEnable()
    {
        if (ButtonAux != null)
            ButtonAux.gameObject.SetActive(true);

        if (Manager != null)
            Manager.SetReportName(ReportName);

        Load();
    }

    protected virtual void OnDisable()
    {
        if (ButtonAux != null)
            ButtonAux.gameObject.SetActive(false);
    }

    public virtual void SnapToTop()
    {
        //if (target == null || StepperContent == null || StepperScrollRect == null) return;
        //if (StepperContent.sizeDelta.x <= StepperScrollRect.preferredWidth) return;

        ////https://stackoverflow.com/questions/30766020/how-to-scroll-to-a-specific-element-in-scrollrect-with-unity-ui
        //Canvas.ForceUpdateCanvases();

        //StepperContent.anchoredPosition =
        //        (Vector2)StepperScrollRect.transform.InverseTransformPoint(StepperContent.position)
        //        - (Vector2)StepperScrollRect.transform.InverseTransformPoint(target.position);
    }

    protected virtual bool CanLoad()
    {
        if (loaded) return false;

        //Debug.Log("StatsReport.LoadResponsesChart [--start--]");
        if (Manager == null ||
            Manager.FilteredResponses == null ||
            Manager.Responses == null ||
            Manager.Responses.Count == 0)
            return false;

        return true;
    }

    public void Load()
    {
        if (awaitingReload)
        {
            loaded = false;
            awaitingReload = false;
        }

        if (CanLoad())
            OnLoad();
    }

    protected abstract void OnLoad();

    //protected abstract void OnGameChoose();
    //protected abstract void OnGameContext();
    //protected abstract void OnGameImitate();
    //protected abstract void OnGameAll();
}
