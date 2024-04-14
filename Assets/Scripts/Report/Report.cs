using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Report : MonoBehaviour
{
    [SerializeField]
    protected ReportManager Manager;
    [SerializeField]
    protected string Name;

    protected void Start()
    {
        if (Manager != null)
            Manager.SetReportName(Name);
    }
}
