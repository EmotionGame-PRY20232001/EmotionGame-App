using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Toggle))]
public class Step : Tab
{
    public uint Index;
    [SerializeField]
    protected Stepper Stepper;

    protected override void UpdateActive(Toggle change)
    {
        base.UpdateActive(change);
        
        if (!change.isOn) return;
        Stepper?.CheckStep(Index);
    }

    public void EnableStep(bool value)
    {
        if (TabButton != null)
            TabButton.isOn = value;
    }
}
