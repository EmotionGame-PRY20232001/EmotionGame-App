using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Stepper : MonoBehaviour
{
    // [SerializeField]
    List<Step> Steps;
    [SerializeField]
    GameObject StepsGroup;
    [SerializeField]
    Button ButtonPrev;
    [SerializeField]
    Button ButtonNext;
    public int CurrentStep { get; protected set; }

    // To spawn
    [SerializeField]
    protected Step StepPrefab;
    public delegate void OnStepChange();
    public OnStepChange onStepChange;

    void Awake()
    {
        LoadSteps();
        CurrentStep = 0;
        ActiveStep(true);

        if (ButtonPrev != null)
            ButtonPrev.onClick.AddListener(delegate { SetPreviousStep(); });

        if (ButtonNext != null)
            ButtonNext.onClick.AddListener(delegate { SetNextStep(); });

        onStepChange += CheckStep;
    }

    void LoadSteps()
    {
        // StepsGroup
        if (StepsGroup != null)
            Steps = StepsGroup.GetComponentsInChildren<Step>().ToList<Step>();
        CheckButtons();
    }

    public void AddStep(GameObject content)
    {
        if (StepPrefab != null && StepsGroup != null)
        {
            var step = Instantiate(StepPrefab, StepsGroup.transform);
            if (step != null)
            {
                step.TabContent = content;
                step.Stepper = this;
            }
        }
    }
    
    public void CheckButtons()
    {
        if (Steps.Count < 2)
            gameObject.SetActive(false);
        else
        {
            ShowButton(ButtonPrev, true);
            ShowButton(ButtonNext, true);
            EnableButton(ButtonPrev, false);
            for (uint i = 0; i < Steps.Count; i++)
            {
                Step step = Steps.ElementAt((int)i);
                step.Index = i;
            }
            // foreach(Toggle step in Steps)
        }
    }
    void ShowButton(Button button, bool value)
    {
        if (button != null)
            button.gameObject.SetActive(value);
    }

    void CheckStep() //uint index
    {
        if (CurrentStep == 0)
        {
            EnableButton(ButtonPrev, false);
            EnableButton(ButtonNext, true);
        }
        else if (CurrentStep == Steps.Count - 1)
        {
            EnableButton(ButtonPrev, true);
            EnableButton(ButtonNext, false);
        }
        // else
        // {
        //     EnableButton(ButtonPrev, true);
        //     EnableButton(ButtonNext, true);
        // }
        onStepChange?.Invoke();
    }
    public void CheckStep(uint index)
    {
        CurrentStep = (int)index;
        CheckStep();
    }

    /// BUTTON LOAD
    void EnableButton(Button button, bool value)
    {
        if (button != null && button.gameObject.activeSelf)
        {
            button.interactable = value;
        }
    }
    
    void SetPreviousStep()
    {
        if (CurrentStep == 0) { return; }
        ActiveStep(false);
        CurrentStep -= 1;
        ActiveStep(true);
        CheckStep();
    }
    void SetNextStep()
    {
        if (CurrentStep == Steps.Count - 1) { return; }
        ActiveStep(false);
        CurrentStep += 1;
        ActiveStep(true);
        CheckStep();
    }
    void ActiveStep(bool value)
    {
        // Debug.Log(CurrentStep + (value ? " true" : " false"));
        if (Steps.ElementAt(CurrentStep) != null)
            Steps[CurrentStep].EnableStep(value);
    }
}
