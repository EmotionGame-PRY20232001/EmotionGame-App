using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Toggle))]
public class Step : Tab
{
    public uint Index;
    public bool WasVisited { get; protected set; }
    public Stepper Stepper;
    public Image StepImage;
    public float AlphaVisited = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        TabButton.isOn = false;
        WasVisited = false;
        StepImage = gameObject.GetComponent<Image>();
    }

    protected override void UpdateActive(Toggle change)
    {
        base.UpdateActive(change);

        if (change.isOn)
        {
            Stepper?.ChangeStep((int)Index);
            WasVisited = true;
        }
        else
        {
            if (WasVisited)
                StepImage?.CrossFadeAlpha(AlphaVisited, 0.1f, true);
        }
        
    }

    public void EnableStep(bool value)
    {
        if (TabButton != null && TabButton.isOn != value)
            TabButton.isOn = value;
    }

    public void Fill(Stepper stepper, uint index)
    {
        Stepper = stepper;
        Index = index;

        if (TabButton != null)
        {
            if (stepper.Group != null)
            {
                TabButton.group = stepper.Group;
            }
        }
    }
}
