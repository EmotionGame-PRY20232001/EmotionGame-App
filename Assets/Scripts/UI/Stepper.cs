using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stepper : MonoBehaviour
{
    // [SerializeField]
    Step[] Steps;
    [SerializeField]
    GameObject StepsGroup;
    [SerializeField]
    GameObject ButtonPrev;
    [SerializeField]
    GameObject ButtonNext;
    uint CurrentStep;

    void Awake()
    {
        LoadSteps();
        CurrentStep = 0;
        ActiveStep(true);
    }

    void LoadSteps()
    {
        // StepsGroup
        if (StepsGroup != null)
            Steps = StepsGroup.GetComponentsInChildren<Step>();
        
        if (Steps.Length < 2)
            gameObject.SetActive(false);
        else
        {
            ShowButton(ButtonPrev, true);
            ShowButton(ButtonNext, true);
            EnableButton(ButtonPrev, false);
            for (uint i = 0; i < Steps.Length; i++)
            {
                Step step = Steps[i];
                step.Index = i;
            }
            // foreach(Toggle step in Steps)
        }
    }
    
    void ShowButton(GameObject button, bool value)
    {
        if (button != null)
            button.SetActive(value);
    }

    public void CheckStep(uint index)
    {
        CurrentStep = index;
        if (index == 0)
        {
            EnableButton(ButtonPrev, false);
            EnableButton(ButtonNext, true);
        }
        else if (index == Steps.Length-1)
        {
            EnableButton(ButtonPrev, true);
            EnableButton(ButtonNext, false);
        }
        // else
        // {
        //     EnableButton(ButtonPrev, true);
        //     EnableButton(ButtonNext, true);
        // }
    }

    void EnableButton(GameObject button, bool value)
    {
        if (button != null && button.activeSelf)
        {
            Button btn = button.GetComponent<Button>();
            btn.interactable = value;
        }
    }

    
    public void SetPreviousStep()
    {
        if (CurrentStep == 0) { return; }
        ActiveStep(false);
        CurrentStep -= 1;
        ActiveStep(true);
    }
    public void SetNextStep()
    {
        if (CurrentStep == Steps.Length-1) { return; }
        ActiveStep(false);
        CurrentStep += 1;
        ActiveStep(true);
    }
    void ActiveStep(bool value)
    {
        // Debug.Log(CurrentStep + (value ? " true" : " false"));
        if (Steps[CurrentStep] != null)
            Steps[CurrentStep].EnableStep(value);
    }
}
